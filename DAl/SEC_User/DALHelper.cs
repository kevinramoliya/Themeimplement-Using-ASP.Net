using Microsoft.AspNetCore.Mvc;

namespace Themeimplement.DAl.SEC_User
{
	public class DALHelper
	{
		#region Connection String
		public static string ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("connString");
		#endregion
	}
}
