using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FoodHunter.FoodHunterWeb.AppLayer.Helpers.Annotations;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Details;
using FoodHunter.FoodHunterWeb.AppLayer.ViewModels.Edit;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.FoodHunterWeb.AppLayer.Controllers
{
    public class FoodieController : Controller
    {
        private readonly IFoodieRepository _repository;
        private Foodie _profile;


        public FoodieController()
        {
            _repository = Factory.GetFoodieRepository();
        }



        // GET: UserProfile
        [ValidateLogin]
        public ActionResult Index()
        {
            _profile = _repository.Get(Convert.ToInt32(Session["UserId"]));


            //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Foodie, FoodieDetailsViewModel>());
            var mapper = config.CreateMapper();
                
            //Copy values
            FoodieDetailsViewModel profileDetails = mapper.Map<FoodieDetailsViewModel>(_profile);

            if (_profile != null)
            {
                profileDetails.Email = Session["Email"].ToString();
                profileDetails.CheckIns = _profile.CheckIns;
            }

            return View(profileDetails);  
            
        }

        public ActionResult ProfileView()
        {
            _profile = _repository.Get(Convert.ToInt32(Session["UserId"]));


            //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Foodie, FoodieDetailsViewModel>());
            var mapper = config.CreateMapper();

            //Copy values
            FoodieDetailsViewModel profileDetails = mapper.Map<FoodieDetailsViewModel>(_profile);

            if (_profile != null)
            {
                profileDetails.Email = Session["Email"].ToString();
                profileDetails.CheckIns = _profile.CheckIns;
            }

            return View(profileDetails);

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            _profile = _repository.Get(id);


            //Create Map
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Foodie, FoodieDetailsViewModel>());
            var mapper = config.CreateMapper();

            //Copy values
            FoodieDetailsViewModel profileDetails = mapper.Map<FoodieDetailsViewModel>(_profile);
            if (_profile != null)
            {
                profileDetails.CheckIns = _profile.CheckIns;
            }

            return View(profileDetails);
        }


        [HttpGet]
        public ActionResult Edit()
        {
            if (Session["UserId"] != null)
            {
                _profile = _repository.Get(Convert.ToInt32(Session["UserId"]));

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Foodie, FoodieEditViewModel>());
                var mapper = config.CreateMapper();
                //Copy values

                FoodieEditViewModel profileEditViewModel = mapper.Map<FoodieEditViewModel>(_profile);

                return View(profileEditViewModel);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Edit(FoodieEditViewModel input)
        {
            if (Session["UserId"] != null)
            {

                _profile = _repository.Get(Convert.ToInt32(Session["UserId"]));

                var config = new MapperConfiguration(cfg => cfg.CreateMap<FoodieEditViewModel, Foodie>());
                var mapper = config.CreateMapper();
                //Copy values

                Foodie userProfile = mapper.Map<Foodie>(input);
                userProfile.UserId = Convert.ToInt32(Session["UserId"]);

                if(input.PostedPicture != null )
                    FilePreProcessor(input, userProfile);
                
                _repository.Update(userProfile);
                

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Login");
        }

        private void FilePreProcessor(FoodieEditViewModel input, UserProfile userProfile)
        {
            string path = Server.MapPath("~/Uploads/Profile/Foodie/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (input.PostedPicture != null)
            {
                string fileName = Path.GetFileName(input.PostedPicture.FileName);
                input.PostedPicture.SaveAs(path + fileName);
                userProfile.ProfilePicture = "../Uploads/Profile/Foodie/"+fileName;
                ViewBag.Message += $"<b>{fileName}</b> uploaded.<br />";
            }
        }
    }
}