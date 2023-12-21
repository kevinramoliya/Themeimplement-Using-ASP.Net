namespace Themeimplement.Bal
{

public static class CommonVariables
{


	private static IHttpContextAccessor _httpContextAccessor;
	static CommonVariables()
	{
		_httpContextAccessor = new HttpContextAccessor();
	}
	public static int? UserID()
	{


		return Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID").ToString());

	}
	public static string? UserName()
	{
		return _httpContextAccessor.HttpContext.Session.GetString("UserName");
	}



}
}