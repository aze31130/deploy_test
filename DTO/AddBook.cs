﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace deploy_test.DTO
{
    public class AddBook
    {
        internal IEnumerable<object> books;

        [Required]
        public decimal Book_price { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Book_name { get; set; }
        [Required]
        public string Book_description { get; set; }
    }
}