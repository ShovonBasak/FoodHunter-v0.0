using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHunter.Web.DataLayer
{
    public class News
    {
        internal News()
        {
        }

        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }
        public string NewsPicture { get; set; }
        public string Title { get; set; }
        public string NewsBody { get; set; }
        public DateTime PostedOn { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public RestaurantAdmin RestaurantAdmin { get; set; }
        public int RestaurantId { get; set; }
        public Status CurrentStatus { get; set; }
    }
}