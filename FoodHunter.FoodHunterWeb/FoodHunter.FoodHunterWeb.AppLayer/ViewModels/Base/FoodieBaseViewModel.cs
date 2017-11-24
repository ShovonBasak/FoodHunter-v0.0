using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base
{
    public class FoodieBaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression("^([+]?88)?01[15-9]d{8}$", ErrorMessage = "Number format is not valid")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }
}