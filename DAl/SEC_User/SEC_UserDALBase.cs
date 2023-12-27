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

		#region Method:User Insert 
		public bool PR_User_Create_Account(string UserName, string Password) { 
			SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
			DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Create_Account");
			sqlDatabase.AddInParameter(dbCommand, "UserName", SqlDbType.VarChar, UserName);
			DataTable dt = new DataTable();
			using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand)) { 
				dt.Load(dr);
	}
			if (dt.Rows.Count > 0) { return false; }
			else {
				DbCommand dbCommand1 = sqlDatabase.GetStoredProcCommand("PR_User_Create_Account");
				sqlDatabase.AddInParameter(dbCommand1, "UserName", SqlDbType.VarChar, UserName);
                sqlDatabase.AddInParameter(dbCommand1, "Password", SqlDbType.VarChar, Password);
                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand1)))
                {
					return true;
                }
                else
                {
                    return false;
                }
            }
		}


	}
    #endregion
}