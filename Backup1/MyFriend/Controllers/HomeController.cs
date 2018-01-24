using System;
using System.Web.Mvc;
using MyFriend.Auth;

namespace MyFriend.Controllers
{
    public class HomeController : Controller
    {
        private void SetUsername()
        {
            ViewBag.headerUsername = Session["username"]?.ToString() ?? "Sign in please.";
        }
        
        public RedirectResult BreakSession()
        {
            Session.Abandon();
            return Redirect("/");
        }
        
        private Sign Sign = new Sign(new FormValidator(), new UserProvider("users.json"));
        
        public ActionResult Index()
        {
            SetUsername();
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string actualPassword)
        {
            try
            {
                Sign.In(new User(username, actualPassword));   
                
                Session["username"] = username;
                SetUsername();
                ViewBag.Message = "Вы успешно вошли в систему.";
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            
            return View();
        }
        
        public ActionResult Register()
        {
            SetUsername();
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string actualPassword, string email)
        {
            try
            {
                Sign.Up(new User(username, actualPassword, email));   
                
                ViewBag.Message = "Вы успешно зарегистрировались.";
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            
            return View();
        }

        public ActionResult About()
        {
            SetUsername();
            
            return View();
        }

        public ActionResult Analyze()
        {
            SetUsername();

            return View();
        }
    }
}