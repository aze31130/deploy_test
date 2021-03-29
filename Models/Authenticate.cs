using System.ComponentModel.DataAnnotations;

namespace deploy_test.Models
{
    public class Authenticate
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
