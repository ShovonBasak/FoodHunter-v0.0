using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHunter.Web.DataLayer
{
    public class CheckIn
    {
        public CheckIn()
        {
        }

        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckInId { get; set; }
        public DateTime CheckInTime { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
        [ForeignKey("UserId")]
        public Foodie Foodie { get; set; }
    }
}
