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
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        public ActionResult DanhSach(string tenLoaiSanPham)
        {
            mapLoaiSanPham mapLSP = new mapLoaiSanPham();
            var data = mapLSP.DanhSachTheotenLoai(tenLoaiSanPham);
            ViewBag.tenLoaiSanPham = tenLoaiSanPham;
            TempData["Dashboard"] = "LoaiSanPham";
            return View(data);
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new LoaiSanPham() { });
        }
       
        [HttpPost]
        public ActionResult ThemMoi(LoaiSanPham model)
        {
            mapLoaiSanPham mapLSP = new mapLoaiSanPham();
            if (mapLSP.ThemMoi(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapLSP.error);
                return View(model);
            }
        }

        public ActionResult CapNhat(int idLoaiSanPham)
        {
            return View(new mapLoaiSanPham().ChiTiet(idLoaiSanPham));
        }
        [HttpPost]
        public ActionResult CapNhat(LoaiSanPham model)
        {
            mapLoaiSanPham mapLSP = new mapLoaiSanPham();
            if(mapLSP.CapNhat(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapLSP.error);
                return View(model);
            }
        }

        public ActionResult Xoa(int idLoaiSanPham)
        {
            mapLoaiSanPham mapLSP = new mapLoaiSanPham();
            mapLSP.Xoa(idLoaiSanPham);
            return RedirectToAction("DanhSach");
        }
       
    }
}