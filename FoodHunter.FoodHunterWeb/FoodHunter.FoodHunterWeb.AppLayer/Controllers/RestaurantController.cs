using System;
using System.Web.Mvc;
using AutoMapper;
using FoodHunter.FoodHunterWeb.AppLayer.Helpers;
using FoodHunter.FoodHunterWeb.AppLayer.Helpers.Annotations;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Base;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Details;
using FoodHunter.Web.DataLayer;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Create;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Edit;

namespace FoodHunter.FoodHunterWeb.AppLayer.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantContext;
        private readonly IRestaurantAdminRepository _restaurantAdminContext;
        private readonly Facade _facade;

        public RestaurantController()
        {
            _restaurantContext = Factory.GetRestaurantRepository();
            _restaurantAdminContext = Factory.GetRestaurantAdminRepository();
            _facade = new Facade();
        }

        
        // GET: Restaurant
        /*public ActionResult Index(int id)
        {

            return View();
        }*/

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (_facade.IsValidRestaurantAdmin(Convert.ToInt32(Session["UserId"]), id))
            {
                Restaurant restaurantToUpdate = _restaurantContext.Get(id);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Restaurant, RestaurantEditViewModel>());
                var mapper = config.CreateMapper();
                //Copy values

                RestaurantEditViewModel restaurantEdit = mapper.Map<RestaurantEditViewModel>(restaurantToUpdate);
                TempData["RestaurantId"] = id;

                return View(restaurantEdit);
            }

            return RedirectToAction("Index", "RestaurantAdmin");
        }

        [HttpPost]
        public ActionResult Edit(RestaurantEditViewModel input)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RestaurantEditViewModel, Restaurant>());
            var mapper = config.CreateMapper();
            //Copy values

            Restaurant restaurantToUpdate = mapper.Map<Restaurant>(input);
            restaurantToUpdate.RestaurantId = Convert.ToInt32(TempData["RestaurantId"]);
            restaurantToUpdate.UserId = Convert.ToInt32(Session["UserId"]);
            _restaurantContext.Update(restaurantToUpdate);

            return RedirectToAction("Details", new {id = restaurantToUpdate.RestaurantId});
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RestaurantCreateViewModel restaurantCreate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RestaurantCreateViewModel, Restaurant>());
            var mapper = config.CreateMapper();

            Restaurant restaurant = mapper.Map<Restaurant>(restaurantCreate);
            restaurant.UserId = Convert.ToInt32(Session["UserId"]);
            _restaurantContext.Insert(restaurant);

            return RedirectToAction("Index","RestaurantAdmin");
           
        }

        [HttpGet, ValidateLogin]
        public ActionResult Details(int id)
        {
            Restaurant restaurant = _restaurantContext.Get(id);
            RestaurantAdmin restaurantAdmin = _restaurantAdminContext.Get(restaurant.UserId);

            #region Setting Values to View Model
            //Copy restaurant values to restaurantbase
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Restaurant, RestaurantBaseViewModel>());
            var mapper = config.CreateMapper();

            //Copy values
            RestaurantBaseViewModel restaurantBase = mapper.Map<RestaurantBaseViewModel>(restaurant);

            //Copy restaurant admin values to restaurantAdminBase
            config = new MapperConfiguration(cfg => cfg.CreateMap<RestaurantAdmin, RestaurantAdminBaseViewModel>());
            mapper = config.CreateMapper();

            //Copy values
            RestaurantAdminBaseViewModel restaurantAdminBase = mapper.Map<RestaurantAdminBaseViewModel>(restaurantAdmin);

            RestaurantDetailsViewModel restaurantDetails = new RestaurantDetailsViewModel
            {
                RestaurantBase = restaurantBase,
                ProfileDetails = restaurantAdminBase
            };
            #endregion

            return View(restaurantDetails);
        }
    }
}