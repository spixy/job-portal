using System;

namespace BusinessLayer.Account
{
	public enum Role
	{
		None,
		User,
		Employer
	}

	public static class RoleExtension
	{
		public static string GetString(this Role role)
		{
			switch (role)
			{
				case Role.None:
					return "";
				case Role.User:
					return "User";
				case Role.Employer:
					return "Employer";
				default:
					throw new ArgumentOutOfRangeException(nameof(role), role, null);
			}
		}

		public static Role GetRole(this string str)
		{
			switch (str)
			{
				case "User":
					return Role.User;
				case "Employer":
					return Role.Employer;
				case "":
					return Role.None;
				default:
					throw new ArgumentOutOfRangeException(nameof(str), str, null);
			}
		}
	}
}