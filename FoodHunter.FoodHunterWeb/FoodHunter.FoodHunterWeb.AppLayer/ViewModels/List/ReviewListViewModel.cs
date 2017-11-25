using System.ComponentModel.DataAnnotations;

namespace FoodHunter.FoodHunterWeb.AppLayer.ViewModels.List
{
    public class ReviewListViewModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte Rating { get; set; }
        public string ReviewPicture { get; set; }
        public string Comment { get; set; }

    }
}