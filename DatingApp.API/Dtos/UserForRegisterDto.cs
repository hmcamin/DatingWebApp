using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
		[Required]
        public string Username { get; set; }
        
		[Required]
		[StringLength(8,MininumLength = 4, ErrorMessage = "You must specify password bewteen 4 and 8 charecters")]
		public string Password { get; set; }
    }
}