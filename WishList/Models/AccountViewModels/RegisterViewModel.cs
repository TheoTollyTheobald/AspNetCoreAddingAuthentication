using System.ComponentModel.DataAnnotations;

namespace WishList.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required][EmailAddress] string Email
        {
            get; set;
        }
        [Required][StringLength(100)][MinLength(8)][DataType(DataType.Password)] string Password
        {
            get; set;
        }
        [Required][DataType(DataType.Password)][Compare("Password")] string ConfirmPassword
        {
            get; set;
        }
    }
}
