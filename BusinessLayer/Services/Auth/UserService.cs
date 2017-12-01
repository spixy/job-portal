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
		private const int saltSize = 24;

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

		public bool VerifyHashedPassword(string hashedPassword, string password)
		{
			string pass = CreatePassword(password, false);

			int len = hashedPassword.Length - saltSize;

			return hashedPassword.Substring(saltSize / 2, len) == pass;
		}

		public string CreatePassword(string password, bool withSalt = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string salt = withSalt ? GenerateSalt(saltSize) : string.Empty;

			using (var sha256 = new SHA256Managed())
			{
				byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

				stringBuilder.Append(salt.Substring(0, saltSize / 2));
				foreach (byte b in bytes)
				{
					stringBuilder.Append(b.ToString("x2"));
				}
				stringBuilder.Append(salt.Substring(saltSize / 2));

				return stringBuilder.ToString();
			}
		}

		private string GenerateSalt(int length)
		{
			using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] bytes = new byte[length];
				cryptoServiceProvider.GetBytes(bytes);
				return BitConverter.ToString(bytes).Replace("-", string.Empty).Substring(0, length);
			}
		}
	}
}
