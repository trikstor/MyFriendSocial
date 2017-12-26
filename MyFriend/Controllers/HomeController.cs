using System;
using System.Data.SQLite;
using System.Web.Mvc;

namespace MyFriend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string actualPassword)
        {
            if (new Users().TryAuth(username, actualPassword))
            {
                Session["username"] = username;
                ViewBag.Message = "Вы успешно вошли в систему.";
            }
            else
                ViewBag.Message = "Неверный логин или пароль.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = Session["username"].ToString();
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}