using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base
{
    public class NewsBaseViewModel
    {
        public string NewsPicture { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string NewsBody { get; set; }
    }
}