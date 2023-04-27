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
    public class SanPhamDatHangController : Controller
    {
        // GET: Admin/SanPhamDatHang
        public ActionResult DanhSach()
        {
            mapSanPhamDatHang mapSPDH = new mapSanPhamDatHang();
            var data = mapSPDH.DanhSach();
            TempData["Dashboard"] = "SanPhamDatHang";
            return View(data);
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new SanPhamDatHang() { });
        }

        [HttpPost]
        public ActionResult ThemMoi(SanPhamDatHang model)
        {
            mapSanPhamDatHang mapSPDH = new mapSanPhamDatHang();
            if (mapSPDH.ThemMoi(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapSPDH.error);
                return View(model);
            }
        }

        public ActionResult CapNhat(int idSanPhamDatHang)
        {
            var updateSPDH = new mapSanPhamDatHang().ChiTiet(idSanPhamDatHang);
            return View(updateSPDH);
        }
        [HttpPost]
        public ActionResult CapNhat(SanPhamDatHang model)
        {
            mapSanPhamDatHang mapSPDH = new mapSanPhamDatHang();
            if (mapSPDH.CapNhat(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapSPDH.error);
                return View(model);
            }
        }

        public ActionResult Xoa(int idSanPhamDatHang)
        {
            mapSanPhamDatHang mapSPDH = new mapSanPhamDatHang();
            mapSPDH.Xoa(idSanPhamDatHang);
            return RedirectToAction("DanhSach");
        }
    }
}