using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.App_Start;

namespace WebSiteBanHang.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //CookieHelper.Create("user-banhang", "anhnt", DateTime.Now.AddDays(10));
            return View();
        }
    }
}