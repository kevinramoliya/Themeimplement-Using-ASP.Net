using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Themeimplement.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int StudentID { get; set; }
        [Required]
        public string? StudentName { get; set; }
        [Required]

        public string? MobileNoStudent { get; set; }
        [Required]

        public string? Email { get; set; }
        [Required]

        public string? MobileNoFather { get; set; }
        [Required]

        public string? Address { get; set; }
        [Required]

        public DateTime BirthDate { get; set; }
        [Required]

        public string? Age { get; set; }
        [Required]

        public string? IsActive { get; set; }
        [Required]

        public string? Gender { get; set; }
        [Required]

        public string? Password { get; set; }
        [Required]

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public int CountryID { get; set; }

        public int StateID { get; set; }

        public int BranchID { get; set; }

        public int CityID { get; set; }
        public int UserId { get; set; }

    }


	public class MST_StudentFilterModel
	{
		public int? StudentID { get; set; }
		public string? StudentName { get; set; }
		public int? CityID { get; set; }
		public int? BranchID { get; set; }
	}
}
