using System.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SmartBreadcrumbs.Attributes;
using Themeimplement.Areas.LOC_Country.Models;


namespace Themeimplement.Areas.LOC_Country.Controllers
{
	[Area("LOC_Country")]
	[Route("LOC_Country/[controller]/[action]")]
	[DefaultBreadcrumb(title: "Country")]
	public class LOC_CountryController : Controller
	{
		private readonly IConfiguration Configuration;

		public LOC_CountryController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IActionResult Index()
        {
			string connectionstr = this.Configuration.GetConnectionString("connString");
			SqlConnection connection = new SqlConnection(connectionstr);
			connection.Open();
			SqlCommand objcmd = connection.CreateCommand();
			objcmd.CommandType = CommandType.StoredProcedure;
			objcmd.CommandText = "PR_Country_SelectAll";
            DataTable table = new DataTable();
            SqlDataReader objSDR = objcmd.ExecuteReader();
			table.Load(objSDR);

			return View(table);
		}
		public IActionResult LOC_CountryListByID(int CountryID)
		{
			string connectionString = this.Configuration.GetConnectionString("ConnectionString");
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "PR_Country_SelectByPK";
			command.Parameters.AddWithValue("CountryID", CountryID);
			SqlDataReader reader = command.ExecuteReader();
			DataTable table = new DataTable();
            LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
			table.Load(reader);
            foreach (DataRow row in table.Rows)
            {
                modelLOC_Country.CountryName = row["CountryName"].ToString();
                modelLOC_Country.CountryCode = row["CounryCode"].ToString();       
            }
			connection.Close();
			LOC_CountryAdd(CountryID);
			return View("LOC_CountryAddEdit",modelLOC_Country);
		}
		public IActionResult LOC_CountryDelete(int CountryId)
		{
			string connectionstr = this.Configuration.GetConnectionString("connString");
			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(connectionstr);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "PR_Country_DeleteByPK";
			command.Parameters.AddWithValue("@CountryID", CountryId);
			command.ExecuteNonQuery();
			TempData["Country_Delete"] = "Record Deleted Successfully!";
			return RedirectToAction("Index");
			

		}

		[HttpPost]
		public IActionResult LOC_CountrySave(LOC_CountryModel modelLOC_Country, int CountryID = 0)
		{
			string connectionString = this.Configuration.GetConnectionString("connString");
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			if (CountryID == 0)
			{
				command.CommandText = "PR_Country_Insert";
				TempData["Country_AddEdit"] = "Record Inserted Successfully!";
			}
			else
			{
				command.CommandText = "PR_Country_UpdateByPK";
				command.Parameters.AddWithValue("@CountryID", CountryID);
				TempData["Country_AddEdit"] = "Record Updated Successfully!";
			}
			command.Parameters.AddWithValue("@CountryName", modelLOC_Country.CountryName);
			command.Parameters.AddWithValue("@CountryCode", modelLOC_Country.CountryCode);

			//command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = modelLOC_Country.Modified;
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Index");

		}
        [Breadcrumb(FromAction = "Index", Title = "Country Add/Edit")]
        public IActionResult LOC_CountryAdd(int CountryID = 0)
		{

			string connectionstr = this.Configuration.GetConnectionString("connString");
			SqlConnection connection = new SqlConnection(connectionstr);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "PR_Country_SelectBypk";
			command.Parameters.AddWithValue("@CountryID", CountryID);
			SqlDataReader reader = command.ExecuteReader();
			DataTable table = new DataTable();
			table.Load(reader);

			LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
			foreach (DataRow dr in table.Rows)
			{
				/*modelLOC_Country.CountryId = Convert.ToInt32(dr["CountryID"]);*/
				modelLOC_Country.CountryName = dr["CountryName"].ToString();
				modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
			}
			return View("LOC_CountryAddEdit", modelLOC_Country);

		}


		public IActionResult Add()
		{
			return View("LOC_CountryAddEdit");
		}
		#region Filter

		public IActionResult LOC_CountryFilter(LOC_CountryFilterModel filterModel)
		{
			string connectionStr = this.Configuration.GetConnectionString("connString");
			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(connectionStr);
			connection.Open();
			SqlCommand objCmd = connection.CreateCommand();
			objCmd.CommandType = CommandType.StoredProcedure;
			objCmd.CommandText = "PR_Country_filter";
			objCmd.Parameters.AddWithValue("@CountryName", filterModel.CountryName);
			objCmd.Parameters.AddWithValue("@CountryCode", filterModel.CountryCode);
			SqlDataReader objSDR = objCmd.ExecuteReader();
			dt.Load(objSDR);
			ModelState.Clear();
			return View("Index", dt);
		}

		#endregion


	}
}