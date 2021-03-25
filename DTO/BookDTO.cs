using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deploy_test.DTO
{
    public class BookDTO
    {
        public int Book_id { get; set; }
        public decimal Book_price { get; set; }
        public string ISBN { get; set; }
        public string Book_name { get; set; }
        public string Book_description { get; set; }
    }
}
