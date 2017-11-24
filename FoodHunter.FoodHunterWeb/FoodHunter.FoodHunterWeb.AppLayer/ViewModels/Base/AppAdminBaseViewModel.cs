using FoodHunter.Web.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base
{
    public class AppAdminBaseViewModel
    {

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        public string ProfilePicture { get; set; }
        public string Address { get; set; }

        //UserList

        //RestaurantList
    }
}