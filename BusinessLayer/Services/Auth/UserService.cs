using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;

namespace BusinessLayer.Services.Auth
{
	public class UserService : ServiceBase, IUserService
	{
		private const int rawSaltSize = 24;
		private const int base64SaltSize = rawSaltSize * 8 / 6;

		private readonly QueryObjectBase<UserBaseDto, UserBase, UserBaseFilterDto, IQuery<UserBase>> userQueryObject;

		public UserService(IMapper mapper, QueryObjectBase<UserBaseDto, UserBase, UserBaseFilterDto, IQuery<UserBase>> userQueryObject) : base(mapper)
		{
			this.userQueryObject = userQueryObject;
		}

		public async Task<UserBaseDto> GetUserByUsernameAsync(string username)
		{
			var queryResult = await this.userQueryObject.ExecuteQuery(new UserBaseFilterDto { UserName = username });
			return queryResult.Items.SingleOrDefault();
		}

		public bool VerifyHashedPassword(string userHashedPassword, string inputPassword)
		{
			string inputHashedPassNoSugar = CreatePassword(inputPassword, false);

			int len = userHashedPassword.Length - base64SaltSize;
			string userHashedPasswordsNoSugar = userHashedPassword.Substring(base64SaltSize / 2, len);

			return userHashedPasswordsNoSugar == inputHashedPassNoSugar;
		}

		public string CreatePassword(string password, bool withSalt = true)
		{
			using (var sha256 = new SHA256Managed())
			{
				byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

				if (withSalt)
				{
					byte[] sugar = this.GenerateSugar(rawSaltSize);
					string sugarStr = Convert.ToBase64String(sugar);

					string result = Convert.ToBase64String(hash);
					result = sugarStr.Substring(0, sugarStr.Length / 2) + result + sugarStr.Substring(sugarStr.Length / 2);
					return result;
				}
				else
				{
					string result = Convert.ToBase64String(hash);
					return result;
				}
			}
		}

		private byte[] GenerateSugar(int length)
		{
			using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] bytes = new byte[length];
				cryptoServiceProvider.GetBytes(bytes);
				return bytes;
			}
		}
	}
}
