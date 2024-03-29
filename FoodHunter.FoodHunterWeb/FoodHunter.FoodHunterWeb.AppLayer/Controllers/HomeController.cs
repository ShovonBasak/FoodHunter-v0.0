﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FoodHunter.FoodHunterWeb.AppLayer.Helpers;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Details;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.List;
using FoodHunter.Web.AppLayer.ViewModels.List;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.FoodHunterWeb.AppLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly INewsRepository _newsContext;
        private readonly Facade _facade;

        public HomeController()
        {
            _foodRepository = Factory.GetFoodRepository();
            _restaurantRepository = Factory.GetRestaurantRepository();
            _newsContext = Factory.GetNewsRepository();
            _facade = new Facade();
        }
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("NewsList");
        }

        [HttpGet]
        public ActionResult NewsList()
        {
             //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<News,NewsListViewModel>());
            var mapper = config.CreateMapper();
            //Copy values
            List<NewsListViewModel> viewModelsList = new List<NewsListViewModel>();

                foreach (News news in _newsContext.GetAll().OrderBy(news => news.PostedOn))
                {
                    NewsListViewModel newsListViewModel = mapper.Map<NewsListViewModel>(news);
                    newsListViewModel.RestaurantName = _restaurantRepository.Get(news.RestaurantId).RestaurantName;

                    if(newsListViewModel.NewsBody.Length > 250)
                        newsListViewModel.NewsBody = newsListViewModel.NewsBody.Substring(0, 250);

                    viewModelsList.Add(newsListViewModel);
                }
            

            return View(viewModelsList);
        }

        [HttpGet]
        public ActionResult TopRestaurantList()
        {
            //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Restaurant, TopRestaurantListViewModel>());
            var mapper = config.CreateMapper();
            //Copy values
            List<TopRestaurantListViewModel> viewModelsList = new List<TopRestaurantListViewModel>();

            foreach (Restaurant restaurant in _restaurantRepository.GetAll())
            {
                TopRestaurantListViewModel topRestaurant = mapper.Map<TopRestaurantListViewModel>(restaurant);
                topRestaurant.Rating = _facade.GetRestaurantRating(restaurant.RestaurantId);

                viewModelsList.Add(topRestaurant);
            }

            return View(viewModelsList.OrderByDescending(r => r.Rating));
        }

        [HttpGet]
        public ActionResult TopFoodList()
        {
            //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Food, FoodListViewModel>());
            var mapper = config.CreateMapper();
            //Copy values
            List<FoodListViewModel> viewModelsList = new List<FoodListViewModel>();

            foreach (Food food in _foodRepository.GetAll())
            {
                FoodListViewModel viewModelFood = mapper.Map<FoodListViewModel>(food);
                viewModelFood.Rating = _facade.GetFoodRating(food.FoodId);
                viewModelsList.Add(viewModelFood);
            }
            return View(viewModelsList);
        }

        [HttpGet]
        public ActionResult CheckInList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewArrivalList()
        {
            //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsListViewModel>());
            var mapper = config.CreateMapper();
            //Copy values
            List<NewsListViewModel> viewModelsList = new List<NewsListViewModel>();

            List<Restaurant> restaurants = _restaurantRepository.GetAll()
                .Where(u => u.UserId == Convert.ToInt32(Session["UserId"])).ToList();

            foreach (Restaurant restaurant in restaurants)
            {
                foreach (News news in _newsContext.GetAll().Where(r => r.RestaurantId == restaurant.RestaurantId))
                {
                    NewsListViewModel newsListViewModel = mapper.Map<NewsListViewModel>(news);
                    newsListViewModel.RestaurantName = _restaurantRepository.Get(news.RestaurantId).RestaurantName;
                    viewModelsList.Add(newsListViewModel);
                }
            }


            return View(viewModelsList);
        }
    }
}