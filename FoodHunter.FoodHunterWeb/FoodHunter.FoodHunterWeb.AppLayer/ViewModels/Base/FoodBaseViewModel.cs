using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base
{
    public class FoodBaseViewModel
    {
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodPicture { get; set; }
        public int DiscoutPercentage { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}