using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Create;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.List;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.FoodHunterWeb.AppLayer.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ICheckInRepository _checkInContext;
        private readonly IFoodieRepository _foodieRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        public CheckInController()
        {
            _checkInContext = Factory.GetCheckInRepository();
            _foodieRepository = Factory.GetFoodieRepository();
            _restaurantRepository = Factory.GetRestaurantRepository();
            _userRepository = Factory.GetUserReposiroty();
        }
        // GET: CheckIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            if (!_checkInContext.GetAll().Exists(c => c.UserId == Convert.ToInt32(Session["UserId"]) &&
                                                      c.RestaurantId == restaurantId &&
                                                      c.CheckInTime.Date == DateTime.Now.Date))
            {
                _checkInContext.Insert(new CheckIn
                {
                    CheckInTime = DateTime.Now,
                    RestaurantId = restaurantId,
                    UserId = Convert.ToInt32(Session["UserId"])
                });
            }

            return RedirectToAction("Details", "Restaurant", new{@id=restaurantId});
        }

        [HttpGet]
        public ActionResult ListByUser(int userId)
        {
            List<CheckIn> checkIns = _checkInContext.GetAll().Where(c => c.UserId == userId).ToList();
            List<CheckInListViewModel> viewModelCheckIns = new List<CheckInListViewModel>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CheckIn, CheckInListViewModel>());
            var mapper = config.CreateMapper();

            //Copy values
            foreach (CheckIn checkIn in checkIns)
            {
                CheckInListViewModel checkInListViewModel = mapper.Map<CheckInListViewModel>(checkIn);
                checkInListViewModel.UserName =
                    _userRepository.Get(checkIn.UserId).UserName;
                checkInListViewModel.RestaurantName = _restaurantRepository.Get(checkIn.RestaurantId).RestaurantName;

                viewModelCheckIns.Add(checkInListViewModel);
            }
            
            return PartialView("List", viewModelCheckIns);
        }

        [HttpGet]
        public ActionResult ListByRestaurant(int restaurantId)
        {
            List<CheckIn> checkIns = _checkInContext.GetAll().Where(c => c.RestaurantId == restaurantId).ToList();
            List<CheckInListViewModel> viewModelCheckIns = new List<CheckInListViewModel>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CheckIn, CheckInListViewModel>());
            var mapper = config.CreateMapper();

            //Copy values
            foreach (CheckIn checkIn in checkIns)
            {
                CheckInListViewModel checkInListViewModel = mapper.Map<CheckInListViewModel>(checkIn);
                checkInListViewModel.UserName = _userRepository.Get(checkIn.UserId).UserName;
                checkInListViewModel.RestaurantName = _restaurantRepository.Get(restaurantId).RestaurantName;

                viewModelCheckIns.Add(checkInListViewModel);
            }

            return PartialView("List", viewModelCheckIns);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}