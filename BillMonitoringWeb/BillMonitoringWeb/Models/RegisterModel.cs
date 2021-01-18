using System.ComponentModel.DataAnnotations;

namespace BillMonitoringWeb.Models
{
	public class RegisterModel
	{
		[Display(Name = "User Name*")]
		[Required(ErrorMessage = "User Name is required")]
		public string UserName { get; set; }

		[Display(Name = "Email*")]
		[EmailAddress(ErrorMessage = "The E-mail field is not a valid e-mail address.")]
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }

		[Display(Name = "Password*")]
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		public string ResponseMessage { get; set; }
	}
}
