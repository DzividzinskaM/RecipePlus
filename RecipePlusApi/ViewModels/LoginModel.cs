﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.ViewModels
{
    public class LoginModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}