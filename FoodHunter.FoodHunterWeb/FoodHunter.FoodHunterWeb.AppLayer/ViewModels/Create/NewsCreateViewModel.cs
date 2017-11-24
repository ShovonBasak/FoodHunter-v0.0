using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base;
using FoodHunter.Web.DataLayer;
using System.Collections.Generic;

namespace FoodHunter.Web.AppLayer.ViewModels.Create
{
    public class NewsCreateViewModel : NewsBaseViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public string RestaurantId { get; set; }
        
    }
}