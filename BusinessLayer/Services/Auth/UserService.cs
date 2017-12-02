using System;
using System.Diagnostics;
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
			/*Debug.WriteLine("X: google => " + this.CreatePassword("google"));
			Debug.WriteLine("X: sony => " + this.CreatePassword("sony"));
			Debug.WriteLine("X: lubos => " + this.CreatePassword("lubos"));
			Debug.WriteLine("X: david => " + this.CreatePassword("david"));*/

			string inputHashedPassNoSalt = CreatePassword(inputPassword, false);

			int len = userHashedPassword.Length - base64SaltSize;
			string userHashedPasswordsNoSalt = userHashedPassword.Substring(base64SaltSize / 2, len);

			return userHashedPasswordsNoSalt == inputHashedPassNoSalt;
		}

		public string CreatePassword(string password, bool withSalt = true)
		{
			using (var sha256 = new SHA256Managed())
			{
				byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

				if (withSalt)
				{
					byte[] salt = GenerateSaltBytes(rawSaltSize);
					string str = Convert.ToBase64String(salt);

					string result = Convert.ToBase64String(hash);

					result = str.Substring(0, str.Length / 2) + result + str.Substring(str.Length / 2);

					return result;
				}
				else
				{
					string result = Convert.ToBase64String(hash);
					return result;
				}
			}
		}

		private byte[] GenerateSaltBytes(int length)
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
