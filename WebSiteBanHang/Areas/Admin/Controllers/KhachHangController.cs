using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.App_Start;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [KiemTraQuyen]
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        public ActionResult DanhSach(string tenKhachHang)
        {
            mapKhachHang mapKH = new mapKhachHang();
            var data = mapKH.DanhSachTheoTenKhachhang(tenKhachHang);
            ViewBag.tenKhachHang = tenKhachHang;
            TempData["Dashboard"] = "KhachHang";
            return View(data);
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new KhachHang() { });
        }

        [HttpPost]
        public ActionResult ThemMoi(KhachHang model)
        {
            mapKhachHang mapKH = new mapKhachHang();
            if (mapKH.ThemMoi(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapKH.error);
                return View(model);
            }
        }

        public ActionResult CapNhat(int idKhachHang)
        {
            return View(new mapKhachHang().ChiTiet(idKhachHang));
        }
        [HttpPost]
        public ActionResult CapNhat(KhachHang model)
        {
            mapKhachHang mapKH = new mapKhachHang();
            if (mapKH.CapNhat(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapKH.error);
                return View(model);
            }
        }

        public ActionResult Xoa(int idKhachHang)
        {
            mapKhachHang mapKH = new mapKhachHang();
            mapKH.Xoa(idKhachHang);
            return RedirectToAction("DanhSach");
        }
    }
}