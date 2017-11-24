using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base
{
    public class CheckInBaseViewModel
    {
        public int CheckInId { get; set; }
        public DateTime CheckInTime { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
    }
}