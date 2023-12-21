using System.ComponentModel.DataAnnotations;

public class SEC_UserModel
{
	public int? UserId { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	
}
public class UserLoginModel
{
	[Required]
	public string UserName { get; set; }
	[Required]
	public string Password { get; set; }
}