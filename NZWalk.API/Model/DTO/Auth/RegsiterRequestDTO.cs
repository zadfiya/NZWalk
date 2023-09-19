using System.ComponentModel.DataAnnotations;

namespace NZWalk.API.Model.DTO.Auth
{
    public class RegsiterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
        public RegsiterRequestDTO() { }
    }
}
