using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Themeimplement.Areas.LOC_Country.Models
{
	public class LOC_CountryModel
	{
		//public int? CountryId { get; set; }
		
        public int? CountryID { get;  set; }
		[Required(ErrorMessage = " Field is Reqired")]

		public string CountryName { get; set; }
		[Required(ErrorMessage = " Field is Reqired")]
		public string CountryCode { get; set; }
		
		public DateTime Created { get; set; }
		
		public DateTime Modified { get; set; }
        public int UserId { get; set; }

    }
	public class LOC_CountryDropDownModel
	{
		public int? CountryID { get; set; }

		[Required]
		public string? CountryName { get; set; }
	}
	public class LOC_CountryFilterModel
	{
		public int? CountryID { get; set; }
		public string? CountryName { get; set; }
		public string? CountryCode { get; set; }
	}
}
