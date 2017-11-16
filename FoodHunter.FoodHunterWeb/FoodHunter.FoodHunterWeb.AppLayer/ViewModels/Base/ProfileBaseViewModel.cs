﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base
{
    public class ProfileBaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression("^([+]?88)?01[15-9]d{8}$", ErrorMessage = "Number format is not valid")]
        public string PhoneNo { get; set; }
        public string ProfilePicture { get; set; }
        public string Address { get; set; }
    }
}