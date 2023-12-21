using Microsoft.AspNetCore.Mvc;
using System.Data;
using Themeimplement.Areas.LOC_State.Models;
using Themeimplement.Models;
using Themeimplement.Areas.MST_Student.Models;
using Microsoft.Data.SqlClient;
using Themeimplement.Areas.MST_Branch.Models;
using Themeimplement.Areas.LOC_City.Models;

namespace Themeimplement.Areas.MST_Student.Controllers
{
	[Area("MST_Student")]
	[Route("MST_Student/[controller]/[action]")]
	public class MST_StudentController : Controller
	{
		private IConfiguration Configuration;

		public MST_StudentController(IConfiguration _configuration)
		{
			Configuration = _configuration;
		}
		#region SelectAll
		public IActionResult MST_StudentList()
		{
			string connectionString = this.Configuration.GetConnectionString("connString");

			#region Branch DropDown

			SqlConnection connection1 = new SqlConnection(connectionString);
			connection1.Open();
			SqlCommand objCmd1 = connection1.CreateCommand();
			objCmd1.CommandType = CommandType.StoredProcedure;
			objCmd1.CommandText = "PR_Branch_ComboBox";
			SqlDataReader reader1 = objCmd1.ExecuteReader();
			DataTable dt1 = new DataTable();
			dt1.Load(reader1);
			connection1.Close();

			List<MST_BranchDropDownModel> list = new List<MST_BranchDropDownModel>();

			foreach (DataRow dr in dt1.Rows)
			{
				MST_BranchDropDownModel branchModel = new MST_BranchDropDownModel();
				branchModel.BranchID = Convert.ToInt32(dr["BranchID"]);
				branchModel.BranchName = dr["BranchName"].ToString();
				list.Add(branchModel);
			}
			ViewBag.BranchList = list;

			#endregion

			#region City DropDown

			SqlConnection connection2 = new SqlConnection(connectionString);
			connection2.Open();
			SqlCommand objCmd2 = connection2.CreateCommand();
			objCmd2.CommandType = CommandType.StoredProcedure;
			objCmd2.CommandText = "PR_City_ComboBox";
			SqlDataReader reader2 = objCmd2.ExecuteReader();
			DataTable dt2 = new DataTable();
			dt2.Load(reader2);
			connection2.Close();

			List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();

			foreach (DataRow dr in dt2.Rows)
			{
				LOC_CityDropDownModel cityModel = new LOC_CityDropDownModel();
				cityModel.CityID = Convert.ToInt32(dr["CityID"]);
				cityModel.CityName = dr["CityName"].ToString();
				list2.Add(cityModel);
			}
			ViewBag.CityList = list2;

			#endregion



			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "PR_Student_SelectAll";
			SqlDataReader reader = command.ExecuteReader();
			DataTable table = new DataTable();
			table.Load(reader);
			connection.Close();
			return View(table);
		}
		#endregion

		#region Save
		public IActionResult MST_StudentSave(MST_StudentModel mST_StudentModel)
		{
			string connectionString = this.Configuration.GetConnectionString("connString");
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			if (mST_StudentModel.StudentID == 0)
			{
				command.CommandText = "PR_Student_Insert";
				TempData["Student_AddEdit"] = "Record Inserted Successfully!";
			}
			else
			{
				command.CommandText = "PR_Student_UpdateByPK";
				command.Parameters.AddWithValue("@StudentID", mST_StudentModel.StudentID);
				TempData["Student_AddEdit"] = "Record Updated Successfully!";
			}
			command.Parameters.AddWithValue("@BranchID", mST_StudentModel.BranchID);
			command.Parameters.AddWithValue("@CityID", mST_StudentModel.CityID);
			command.Parameters.AddWithValue("@StudentName", mST_StudentModel.StudentName);
			command.Parameters.AddWithValue("@MobileNoStudent", mST_StudentModel.MobileNoStudent);
			command.Parameters.AddWithValue("@Email", mST_StudentModel.Email);
			command.Parameters.AddWithValue("@MobileNoFather", mST_StudentModel.MobileNoFather);
			command.Parameters.AddWithValue("@Address", mST_StudentModel.Address);
			command.Parameters.AddWithValue("@BirthDate", mST_StudentModel.BirthDate);
			command.Parameters.AddWithValue("@Age", mST_StudentModel.Age);
			command.Parameters.AddWithValue("@IsActive", mST_StudentModel.IsActive);
			command.Parameters.AddWithValue("@Gender", mST_StudentModel.Gender);
			command.Parameters.AddWithValue("@Password", mST_StudentModel.Password);
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("MST_StudentList");
		}
		#endregion

		#region Delete
		public IActionResult MST_StudentDelete(int StudentID)
		{
			string connectionString = this.Configuration.GetConnectionString("connString");
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "PR_Student_DeleteByPK";
			command.Parameters.AddWithValue("@StudentID", StudentID);
			command.ExecuteNonQuery();
			TempData["Student_Delete"] = "Record Deleted Successfully!";
			return RedirectToAction("MST_StudentList");
		}
		#endregion

		#region Add
		public IActionResult MST_StudentAdd(int StudentID = 0)
		{

			#region City ComboBox
			string connectionString = this.Configuration.GetConnectionString("connString");
			SqlConnection connection1 = new SqlConnection(connectionString);
			connection1.Open();
			SqlCommand command1 = connection1.CreateCommand();
			command1.CommandType = CommandType.StoredProcedure;
			command1.CommandText = "PR_City_ComboBox";
			SqlDataReader reader1 = command1.ExecuteReader();
			DataTable table1 = new DataTable();
			table1.Load(reader1);
			connection1.Close();

			List<LOC_CityDropDownModel> list = new List<LOC_CityDropDownModel>();
			foreach (DataRow row in table1.Rows)
			{
				LOC_CityDropDownModel lOC_CityDropDownModel = new LOC_CityDropDownModel();
				lOC_CityDropDownModel.CityID = Convert.ToInt32(row["CityID"]);
				lOC_CityDropDownModel.CityName = row["CityName"].ToString();
				list.Add(lOC_CityDropDownModel);
			}
			ViewBag.CityList = list;
			#endregion

			#region Branch ComboBox
			SqlConnection connection2 = new SqlConnection(connectionString);
			connection2.Open();
			SqlCommand command2 = connection2.CreateCommand();
			command2.CommandType = CommandType.StoredProcedure;
			command2.CommandText = "PR_Branch_ComboBox";
			SqlDataReader reader2 = command2.ExecuteReader();
			DataTable table2 = new DataTable();
			table2.Load(reader2);
			connection2.Close();

		List<MST_BranchDropDownModel> list2 = new List<MST_BranchDropDownModel>();
			foreach (DataRow row in table2.Rows)
			{
				MST_BranchDropDownModel mST_BranchDropDownModel = new MST_BranchDropDownModel();
				mST_BranchDropDownModel.BranchID = Convert.ToInt32(row["BranchID"]);
				mST_BranchDropDownModel.BranchName = row["BranchName"].ToString();
				list2.Add(mST_BranchDropDownModel);
			}
			ViewBag.BranchList = list2;
			#endregion

			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "PR_Student_SelectByPK";
			command.Parameters.AddWithValue("@StudentID", StudentID);
			SqlDataReader reader = command.ExecuteReader();
			DataTable table = new DataTable();
			table.Load(reader);
			MST_StudentModel mST_StudentModel = new MST_StudentModel();
			foreach (DataRow dataRow in table.Rows)
			{
				mST_StudentModel.StudentName = dataRow["StudentName"].ToString();
				mST_StudentModel.BranchID = Convert.ToInt32(dataRow["BranchID"]);
				mST_StudentModel.CityID = Convert.ToInt32(dataRow["CityID"]);
				mST_StudentModel.MobileNoStudent = dataRow["MobileNoStudent"].ToString();
				mST_StudentModel.Email = dataRow["Email"].ToString();
				mST_StudentModel.MobileNoFather = dataRow["MobileNoFather"].ToString();
				mST_StudentModel.Address = dataRow["Address"].ToString();
				mST_StudentModel.BirthDate = Convert.ToDateTime(dataRow["BirthDate"]);
				mST_StudentModel.Age = dataRow["Age"].ToString();
				mST_StudentModel.IsActive = dataRow["IsActive"].ToString();
				mST_StudentModel.Gender = dataRow["Gender"].ToString();
				mST_StudentModel.Password = dataRow["Password"].ToString();

			}
			return View("MST_StudentAddEdit", mST_StudentModel);
		}
		#endregion

		#region FILTER
		public IActionResult MST_StudentFilter(MST_StudentFilterModel FilterModel)
		{
			string connectionStr = this.Configuration.GetConnectionString("connString");

			#region Branch DropDown

			SqlConnection connection1 = new SqlConnection(connectionStr);
			connection1.Open();
			SqlCommand objCmd1 = connection1.CreateCommand();
			objCmd1.CommandType = CommandType.StoredProcedure;
			objCmd1.CommandText = "PR_Branch_ComboBox";
			SqlDataReader reader1 = objCmd1.ExecuteReader();
			DataTable dt1 = new DataTable();
			dt1.Load(reader1);
			connection1.Close();

			List<MST_BranchModel> list = new List<MST_BranchModel>();

			foreach (DataRow dr in dt1.Rows)
			{
				MST_BranchModel branchModel = new MST_BranchModel();
				branchModel.BranchID = Convert.ToInt32(dr["BranchID"]);
				branchModel.BranchName = dr["BranchName"].ToString();
				list.Add(branchModel);
			}
			ViewBag.BranchList = list;

			#endregion

			#region City DropDown

			SqlConnection connection2 = new SqlConnection(connectionStr);
			connection2.Open();
			SqlCommand objCmd2 = connection2.CreateCommand();
			objCmd2.CommandType = CommandType.StoredProcedure;
			objCmd2.CommandText = "PR_City_ComboBox";
			SqlDataReader reader2 = objCmd2.ExecuteReader();
			DataTable dt2 = new DataTable();
			dt2.Load(reader2);
			connection2.Close();

			List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();

			foreach (DataRow dr in dt2.Rows)
			{
				LOC_CityDropDownModel cityModel = new LOC_CityDropDownModel();
				cityModel.CityID = Convert.ToInt32(dr["CityID"]);
				cityModel.CityName = dr["CityName"].ToString();
				list2.Add(cityModel);
			}
			ViewBag.CityList = list2;

			#endregion

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(connectionStr);
			connection.Open();
			SqlCommand objCmd = connection.CreateCommand();
			objCmd.CommandType = CommandType.StoredProcedure;
			objCmd.CommandText = "PR_Student_Filter";
			objCmd.Parameters.AddWithValue("@StudentName", FilterModel.StudentName);
			objCmd.Parameters.AddWithValue("@CityID", FilterModel.CityID);
			objCmd.Parameters.AddWithValue("@BranchID", FilterModel.BranchID);
			SqlDataReader objSDR = objCmd.ExecuteReader();
			dt.Load(objSDR);

			ModelState.Clear();
			return View("MST_StudentList", dt);
		}
		#endregion
	}



}