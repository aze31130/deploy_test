﻿using System.ComponentModel.DataAnnotations;

namespace deploy_test.DTO
{
    public class RegisterModel
    {
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public int age { get; set; }
    }
}
