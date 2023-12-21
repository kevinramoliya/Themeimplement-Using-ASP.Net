using System.ComponentModel.DataAnnotations;

namespace Themeimplement.Areas.MST_Branch.Models
{
    public class MST_BranchModel
    {
        public int? BranchID { get; set; }
        [Required(ErrorMessage = " Field is Reqired")]
        public string? BranchName { get; set; }
        [Required(ErrorMessage = " Field is Reqired")]
        public string? BranchCode { get; set; }
        public string? Created { get; set; }
        public string? Modified { get; set; }
        public int UserId { get; set; }


    }
    public class MST_BranchDropDownModel
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
    }

    public class MST_BranchFilterModel
    {
        public int BranchID { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
    }
}