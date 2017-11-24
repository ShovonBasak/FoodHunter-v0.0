using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.List
{
    public class CheckInListViewModel : CheckInBaseViewModel
    {
        public string UserName { get; set; }
        public string RestaurantName { get; set; }
    }
}