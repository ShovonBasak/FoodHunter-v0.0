using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHunter.Web.DataLayer
{
    public class Review
    {
        internal Review()
        {
        }

        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public string ReviewPicture { get; set; }
        [Range(1,10)]
        public int Rating { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Foodie Reviewer { get; set; }

        public int? RestaurantId { get; set; }
        public int? FoodId { get; set; }
    }
}
