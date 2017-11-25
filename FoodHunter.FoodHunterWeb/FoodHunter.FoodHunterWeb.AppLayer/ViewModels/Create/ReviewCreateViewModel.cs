using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Create
{
    public class ReviewCreateViewModel
    {
        public string Comment { get; set; }
        public string ReviewPicture { get; set; }
        [Range(1, 10)]
        public byte Rating { get; set; }
    }
}