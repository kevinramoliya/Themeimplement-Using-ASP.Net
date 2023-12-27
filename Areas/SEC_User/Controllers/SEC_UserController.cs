using AdminPanel.DAL.SEC_User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Themeimplement.Controllers;


namespace Themeimplement.Areas.SEC_User.Controllers
{

	[Area("SEC_User")]
	[Route("SEC_User/[controller]/[action]")]
	public class SEC_UserController : Controller
	{

		public IActionResult SEC_UserLogin()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(SEC_UserModel modelSEC_User)
		{
			string error = null;
			Console.WriteLine("Hello ", modelSEC_User.UserName);
			if (modelSEC_User.UserName == null)
			{
				error += "User Name is required";
			}
			if (modelSEC_User.Password == null)
			{
				error += "<br/>Password is required";
			}

			if (error != null)
			{
				TempData["Error"] = error;
				return RedirectToAction("SEC_UserLogin");
			}
			else
			{
				SEC_UserDALBase sEC_UserDALBase = new SEC_UserDALBase();
				DataTable dt = sEC_UserDALBase.EMP_login(modelSEC_User.UserName, modelSEC_User.Password);
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						Console.WriteLine(dr);
						HttpContext.Session.SetString("UserId", dr["UserId"].ToString());
						HttpContext.Session.SetString("UserName", dr["UserName"].ToString());					
						HttpContext.Session.SetString("Password", dr["Password"].ToString());
						break;
					}
				}
				else
				{
					TempData["Error"] = "User Name or Password is invalid!";
					return RedirectToAction("SEC_UserLogin");
				}
				if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
				{
					return RedirectToAction("Index", "Home");
				}
			}
			return RedirectToAction("Index");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("SEC_UserLogin");
		}

        public IActionResult SignUpUser(SEC_UserModel sEC_UserModel)
        {
            SEC_UserDALBase dal = new SEC_UserDALBase();
			bool IsSuccess = dal.PR_User_Create_Account(sEC_UserModel.UserName, sEC_UserModel.Password);

			if (IsSuccess)
			{
				return RedirectToAction("SEC_UserSignUp");
			}

			else
			{
				TempData["Error"] = "user allready exists";
				return RedirectToAction("SEC_UserLogin");
			}


        }
	}
}