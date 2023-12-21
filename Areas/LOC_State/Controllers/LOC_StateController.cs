using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Collections.Generic;
using Themeimplement.Models;
using Themeimplement.Areas.LOC_State.Models;
using Microsoft.Data.SqlClient;
using SmartBreadcrumbs.Attributes;
using Themeimplement.Areas.LOC_Country.Models;

namespace Themeimplement.Areas.LOC_State.Controllers
{
    
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;

        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
      
        #region SelectAll
        public IActionResult LOC_StateList()
        {
            string connectionString = this.Configuration.GetConnectionString("connString");

            #region Country DropDown
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand objCmd1 = connection1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader1 = objCmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            connection1.Close();

            List<LOC_CountryModel> list = new List<LOC_CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryModel countryModel = new LOC_CountryModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryListForDropdown = list;

            #endregion



            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion
        #region Delete
        public IActionResult LOC_StateDelete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_DeleteByPK";
            command.Parameters.AddWithValue("@StateID", StateID);
            command.ExecuteNonQuery();
            TempData["State_Delete"] = "Record Deleted Successfully!";
            return RedirectToAction("LOC_StateList");
        }
        #endregion
        #region SelectByID
        public IActionResult LOC_StateListByID(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectByPK";
            command.Parameters.AddWithValue("StateID", StateID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            LOC_StateModel modelLOC_State = new LOC_StateModel();
            table.Load(reader);
            foreach (DataRow row in table.Rows)
            {
                modelLOC_State.StateName = row["StateName"].ToString();
                modelLOC_State.StateCode = row["StateCode"].ToString();
                modelLOC_State.CountryID = Convert.ToInt32(row["CountryID"]);
            }
            connection.Close();
            LOC_StateAdd(StateID);
            return View("LOC_StateAddEdit", modelLOC_State);

        }
        #endregion

        #region Save
        public IActionResult LOC_StateSave(LOC_StateModel lOC_StateModel, int StateID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (StateID == 0)
            {
                command.CommandText = "PR_State_Insert";
                TempData["State_AddEdit"] = "Record Inserted Successfully!";
            }
            else
            {
                command.CommandText = "PR_State_UpdateByPK";
                command.Parameters.AddWithValue("@StateID", StateID);
                command.Parameters.AddWithValue("@Modified", DBNull.Value);
                TempData["State_AddEdit"] = "Record Updated Successfully!";
            }
            command.Parameters.AddWithValue("@StateName", lOC_StateModel.StateName);
            command.Parameters.AddWithValue("@StateCode", lOC_StateModel.StateCode);
            command.Parameters.AddWithValue("@CountryID", lOC_StateModel.CountryID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region Add

        public IActionResult LOC_StateAdd(int StateID = 0)
        {
            #region ComboBox
            string connectionString = this.Configuration.GetConnectionString("connString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow row in table1.Rows)
            {
                LOC_CountryDropDownModel lOC_CountryDropDownModel = new LOC_CountryDropDownModel();
                lOC_CountryDropDownModel.CountryID = Convert.ToInt32(row["CountryID"]);
                lOC_CountryDropDownModel.CountryName = row["CountryName"].ToString();
                list.Add(lOC_CountryDropDownModel);
            }
            ViewBag.CountryList = list;
            #endregion

            #region Add
            if (StateID != 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_State_SelectByPK";
                command.Parameters.AddWithValue("@StateID", StateID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                LOC_StateModel lOC_StateModel = new LOC_StateModel();
                foreach (DataRow dataRow in table.Rows)
                {
                    lOC_StateModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_StateModel.StateName = dataRow["StateName"].ToString();
                    lOC_StateModel.StateCode = dataRow["StateCode"].ToString();
                    lOC_StateModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                }
                return View("LOC_StateAddEdit", lOC_StateModel);
            }
            return View("LOC_StateAddEdit");
            #endregion
        }
        #endregion


        #region Filter

        public IActionResult LOC_StateFilter(LOC_StateFilterModel filterModel)
        {
            string connectionStr = this.Configuration.GetConnectionString("connString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_filter";
            objCmd.Parameters.AddWithValue("@CountryID", filterModel.CountryID);
            objCmd.Parameters.AddWithValue("@StateName", filterModel.StateName);
            objCmd.Parameters.AddWithValue("@StateCode", filterModel.StateCode);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            ModelState.Clear();

            #region Country DropDown
            SqlConnection connection1 = new SqlConnection(connectionStr);
            connection1.Open();
            SqlCommand objCmd1 = connection1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader1 = objCmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            connection1.Close();

            List<LOC_CountryModel> list = new List<LOC_CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryModel countryModel = new LOC_CountryModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryListForDropdown = list;

            #endregion

            return View("LOC_StateList", dt);
        }

        #endregion




    }
}