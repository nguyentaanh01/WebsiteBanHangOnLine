using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.App_Start;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        #region Danh Sach Them, Sua, Xoa
        [KiemTraQuyen]
        public ActionResult DanhSach(string tenHienThi)
        {
            mapTaiKhoan mapTK = new mapTaiKhoan();
            var data = mapTK.DanhSachTheoTenHienThi(tenHienThi);
            ViewBag.tenHienThi = tenHienThi;
            TempData["Dashboard"] = "TaiKhoan";
            return View(data);
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new TaiKhoan() { });
        }

        [HttpPost]
        public ActionResult ThemMoi(TaiKhoan model)
        {
            mapTaiKhoan mapTK = new mapTaiKhoan();
            if (mapTK.ThemMoi(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapTK.error);
                return View(model);
            }
        }
        [KiemTraQuyen]
        public ActionResult CapNhat(int id)
        {
            return View(new mapTaiKhoan().ChiTiet(id));
        }
        [HttpPost]
        public ActionResult CapNhat(TaiKhoan model)
        {
            mapTaiKhoan mapTK = new mapTaiKhoan();
            if (mapTK.CapNhat(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapTK.error);
                return View(model);
            }
        }
        [KiemTraQuyen]
        public ActionResult Xoa(int id)
        {
            mapTaiKhoan mapTK = new mapTaiKhoan();
            mapTK.Xoa(id);
            return RedirectToAction("DanhSach");
        }
        #endregion

        #region DangNhap DangXuat
        public ActionResult DangNhap(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]

        public ActionResult DangNhap(string userName, string passWord, string returnUrl)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            //1. check null userName và passWord
            if(string.IsNullOrEmpty(userName) == true | string.IsNullOrEmpty(passWord) == true)
            {
                ViewBag.error = "Chưa nhập Username va passWord";
                return View();
            }
            //2. Lấy tài khoản
            var User = db.TaiKhoans.SingleOrDefault(m => m.UserName.ToLower() == userName.ToLower());
            //3. Kiểm tra tài khoản có tồn tại không
            if (User == null)
            {
                ViewBag.user = userName;
                ViewBag.password = passWord;
                ViewBag.error = "Tài khoản không tồn tại";
                return View();
            }
            //4. Kiểm tra mật khẩu
            if(User.PassWord != passWord)
            {
                ViewBag.user = userName;
                ViewBag.password = passWord;
                ViewBag.error = "Mật khẩu không đúng";
                return View();
            }
            //5. check Active
            if (User.Active == false)
            {
                ViewBag.user = userName;
                ViewBag.password = passWord;
                ViewBag.error = "tài khoản bị khóa";
                return View();
            }
            ViewBag.user = userName;
            ViewBag.password = passWord;
            //6. Lưu Session, cookie
            Session["user"] = User;
            CookieHelper.Create("user-banhang", User.UserName, DateTime.Now.AddDays(10));
            CookieHelper.Create("pass-banhang", User.PassWord, DateTime.Now.AddDays(10));


            //Chuyển hướng nếu có link cũ
            if (string.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("/Admin/Home/DanhSach");
            }
        }

        public ActionResult DangXuat()
        {
            Session.Remove("user");
            CookieHelper.Remove("user-banhang");
            CookieHelper.Remove("pass-banhang");
            return RedirectToAction("DangNhap");
        }
        #endregion 

        [KiemTraQuyen(ChucNang = "TaiKhoan_ChiTiet")]
        public ActionResult ChiTiet(int idTaiKhoan)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            var taiKhoan = db.TaiKhoans.Find(idTaiKhoan);
            TempData["Dashboard"] = "ChiTiet";
            return View(taiKhoan);
        }

        [KiemTraQuyen]
        public ActionResult PhanQuyen(int idTaiKhoan, string maChucNang)
        {
            var db = new BanHangDBEntities();
            var phanQuyen = db.PhanQuyens.SingleOrDefault(m => m.IDTaiKhoan == idTaiKhoan & m.MaChucNang == maChucNang);
            if(phanQuyen != null)
            {
                //xoa
                db.PhanQuyens.Remove(phanQuyen);
                db.SaveChanges();
            }
            else
            {
                phanQuyen = new PhanQuyen();
                phanQuyen.IDTaiKhoan = idTaiKhoan;
                phanQuyen.MaChucNang = maChucNang;
                db.PhanQuyens.Add(phanQuyen);
                db.SaveChanges();
            }
            return RedirectToAction("ChiTiet", new { idTaiKhoan = idTaiKhoan });
        }

        [HttpPost]
        public JsonResult PhanQuyenJson(int idTaiKhoan, string maChucNang)
        {
            var db = new BanHangDBEntities();
            var phanQuyen = db.PhanQuyens.SingleOrDefault(m => m.IDTaiKhoan == idTaiKhoan & m.MaChucNang == maChucNang);
            if (phanQuyen != null)
            {
                //xoa
                db.PhanQuyens.Remove(phanQuyen);
                db.SaveChanges();
            }
            else
            {
                phanQuyen = new PhanQuyen();
                phanQuyen.IDTaiKhoan = idTaiKhoan;
                phanQuyen.MaChucNang = maChucNang;
                db.PhanQuyens.Add(phanQuyen);
                db.SaveChanges();
            }
            return Json(new 
            { 
                status = "Đã phân quyền"
            });
        }

        public ActionResult BaoChuaPhanQuyen()
        {
            return View();
        }
    }
}