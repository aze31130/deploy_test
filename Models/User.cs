using System.ComponentModel.DataAnnotations;

namespace deploy_test.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public int age { get; set; }
        public string passwordhash { get; set; }
    }
}
