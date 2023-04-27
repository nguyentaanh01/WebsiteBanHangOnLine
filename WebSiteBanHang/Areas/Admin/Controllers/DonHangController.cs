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
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        public ActionResult DanhSach(string tenDonHang)
        {
            mapDonHang mapDH = new mapDonHang();
            var data = mapDH.DanhSachTheoTenDonHang(tenDonHang);
            ViewBag.tenDonHang = tenDonHang;
            TempData["Dashboard"] = "DonHang";
            return View(data);
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new DonHang() { });
        }

        [HttpPost]
        public ActionResult ThemMoi(DonHang model)
        {
            mapDonHang mapDH = new mapDonHang();
            if (mapDH.ThemMoi(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapDH.error);
                return View(model);
            }
        }

        public ActionResult CapNhat(int idDonHang)
        {
            var updateDH = new mapDonHang().ChiTiet(idDonHang);
            ViewBag.TrangThai = updateDH.TrangThai;
            return View(updateDH);
        }
        [HttpPost]
        public ActionResult CapNhat(DonHang model)
        {
            mapDonHang mapDH = new mapDonHang();
            if (mapDH.CapNhat(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapDH.error);
                return View(model);
            }
        }

        public ActionResult Xoa(int idDonHang)
        {
            mapDonHang mapDH = new mapDonHang();
            mapDH.Xoa(idDonHang);
            return RedirectToAction("DanhSach");
        }
    }
}