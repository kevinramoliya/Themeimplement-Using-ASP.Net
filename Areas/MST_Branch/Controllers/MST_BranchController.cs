using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Themeimplement.Areas.MST_Branch.Models;
using Themeimplement.Models;




namespace Themeimplement.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    [Route("MST_Branch/[controller]/[action]")]
    public class MST_BranchController : Controller
    {
        private IConfiguration Configuration;

        public MST_BranchController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult MST_BranchList()
        {
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion
        public IActionResult LOC_BranchListByID(int BranchID)
        {
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_SelectByPK";
            command.Parameters.AddWithValue("BranchID", BranchID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            MST_BranchModel modelMST_Branch = new MST_BranchModel();
            table.Load(reader);
            foreach (DataRow row in table.Rows)
            {
                modelMST_Branch.BranchName = row["BranchName"].ToString();
                modelMST_Branch.BranchCode = row["BranchCode"].ToString();
            }
            connection.Close();
            MST_BranchAdd(BranchID);
            return View("MST_BranchAddEdit", modelMST_Branch);
        }
        public IActionResult MST_BranchDelete(int BranchID)
        {
            string connectionstr = this.Configuration.GetConnectionString("connString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionstr);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_DeleteByPK";
            command.Parameters.AddWithValue("@BranchID", BranchID);
            command.ExecuteNonQuery();
			TempData["Branch_Delete"] = "Record Deleted Successfully!"; ;
			return RedirectToAction("MST_BranchList");

        }

        [HttpPost]
        public IActionResult MST_BranchSave(MST_BranchModel mST_BranchModel, int BranchID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (BranchID == 0)
            {
                command.CommandText = "PR_Branch_Insert";
				TempData["Branch_AddEdit"] = "Record Inserted Successfully!";
			}
            else
            {
                command.CommandText = "PR_Branch_UpdateByPK";
                command.Parameters.AddWithValue("@BranchID", mST_BranchModel.BranchID);
				TempData["Branch_AddEdit"] = "Record Updated Successfully!";
			}
            command.Parameters.AddWithValue("@BranchName", mST_BranchModel.BranchName);
            command.Parameters.AddWithValue("@BranchCode", mST_BranchModel.BranchCode);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_BranchList");

        }
        public IActionResult MST_BranchAdd(int BranchID)
        {
            if (BranchID != 0) {
                string connectionstr = this.Configuration.GetConnectionString("connString");
                SqlConnection connection = new SqlConnection(connectionstr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Branch_SelectByPK";
                command.Parameters.AddWithValue("@BranchID", BranchID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                MST_BranchModel mST_BranchModel = new MST_BranchModel();
                foreach (DataRow dataRow in table.Rows)
                {
                    mST_BranchModel.BranchID = Convert.ToInt32(dataRow["BranchID"]);
                    mST_BranchModel.BranchName = dataRow["BranchName"].ToString();
                    mST_BranchModel.BranchCode = dataRow["BranchCode"].ToString();
                }
                return View("MST_BranchAddEdit", mST_BranchModel);
            }
            return View("Mst_BranchAddEdit");
            
        }


        public IActionResult MST_BranchFilter(MST_BranchFilterModel filterModel)
        {
            string connectionStr = this.Configuration.GetConnectionString("connString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Branch_Filter";
            objCmd.Parameters.AddWithValue("@BranchName", filterModel.BranchName);
            objCmd.Parameters.AddWithValue("@BranchCode", filterModel.BranchCode);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            ModelState.Clear();
            return View("MST_BranchList", dt);
        }



    }
}