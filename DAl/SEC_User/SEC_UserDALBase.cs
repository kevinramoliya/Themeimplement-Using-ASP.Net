using System.Data.Common;
using System.Data;
using Themeimplement.DAl.SEC_User;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace AdminPanel.DAL.SEC_User
{
	public class SEC_UserDALBase : DALHelper
	{
		#region Method: dbo_PR_SEC_User_SelectByPK
		public DataTable EMP_login(string UserName, string Password)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("Emp_Login");
				sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
				sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

				DataTable dt = new DataTable();
				using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
				{
					dt.Load(dr);
				}

				return dt;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		#endregion

	}
}