using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.App_Start
{
    public class KiemTraQuyen : AuthorizeAttribute
    {
        public string ChucNang { set; get; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            var User = (TaiKhoan)HttpContext.Current.Session["user"];

            //Nếu User chưa có trong Session, thì kiểm tra Cookie
            string user = CookieHelper.Get("user-banhang");
            string pass = CookieHelper.Get("pass-banhang");
            //tìm đối tượng đó ra
            var taikhoan = db.TaiKhoans.SingleOrDefault(m=>m.UserName == user);
            if(taikhoan != null)
            {
                if(taikhoan.PassWord == pass)
                {
                    User = taikhoan;
                }
            }

            //Nếu chưa có Session thì bắt đăng nhập
            if (User == null)
            {
                // Chuyển hướng đường link
                var returUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        controller = "TaiKhoan",
                        action = "DangNhap",
                        area = "Admin",
                        returnUrl = returUrl.ToString()
                    }));
                return;
            }

            //Nếu không phân quyền chức năng thì cho nó chạy bình thường
            if(string.IsNullOrEmpty(ChucNang))
            {
                return;
            }

            //Kiểm tra quyền
            var phanQuyen = db.PhanQuyens.Count(m => m.IDTaiKhoan == User.ID & m.MaChucNang == ChucNang);
            if (phanQuyen <= 0)
            {
                //Chuyển hướng đường link sang 1 trang báo Không có Quyền
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        controller = "TaiKhoan",
                        action = "BaoChuaPhanQuyen",
                        area = "Admin",
                    }));
                return;
            }
        }
    }
}