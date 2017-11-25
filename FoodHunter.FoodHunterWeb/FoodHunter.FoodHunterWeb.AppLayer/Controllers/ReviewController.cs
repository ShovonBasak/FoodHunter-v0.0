using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Create;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.List;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.FoodHunterWeb.AppLayer.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IFoodieRepository _foodieRepository;
        private IUserRepository _userRepository;

        public ReviewController()
        {
            _reviewRepository = Factory.GetReviewRepository();
            _foodieRepository = Factory.GetFoodieRepository();
            _userRepository = Factory.GetUserReposiroty();
        }

        // GET: Review
        /*public ActionResult Index()
        {
            return View();
        }*/



        //For food
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Create(string controller, int id)
        {
            TempData["ParentController"] = controller;
            TempData["Id"] = id;

            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(ReviewCreateViewModel input)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ReviewCreateViewModel, Review>());
            var mapper = config.CreateMapper();

            //Copy values
            Review reviewToCreate = mapper.Map<Review>(input);
            reviewToCreate.UserId = Convert.ToInt32(Session["UserId"]);

            if (TempData["ParentController"].ToString() == "Food")
                reviewToCreate.FoodId = Convert.ToInt32(TempData["Id"]);
            else if (TempData["ParentController"].ToString() == "Restaurant")
                reviewToCreate.RestaurantId = Convert.ToInt32(TempData["Id"]);

            _reviewRepository.Insert(reviewToCreate);


            return RedirectToAction("Details", TempData["parentController"].ToString(), new {id = Convert.ToInt32(TempData["Id"]) });
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult List(string controller, int id)
        {
            List<Review> reviews = null;
            if (controller == "Food")
                reviews = _reviewRepository.GetAll().Where(f => f.FoodId == id).ToList();
            else if (controller == "Restaurant")
                reviews = _reviewRepository.GetAll().Where(r => r.RestaurantId == id).ToList();

            var reviewListViewModels = new List<ReviewListViewModel>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Review, ReviewListViewModel>());
            var mapper = config.CreateMapper();

            if (reviews != null)
                foreach (Review review in reviews)
                {
                    ReviewListViewModel reviewListViewModel = mapper.Map<ReviewListViewModel>(review);
                    reviewListViewModels.Add(reviewListViewModel);
                    Foodie foodie = _foodieRepository.Get(reviewListViewModel.UserId);
                    reviewListViewModel.UserName = _userRepository.Get(foodie.UserId).UserName;
                }

            return PartialView(reviewListViewModels);
        }
    }
}