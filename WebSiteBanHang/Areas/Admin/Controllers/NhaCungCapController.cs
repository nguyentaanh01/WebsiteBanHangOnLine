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
    public class NhaCungCapController : Controller
    {
        // GET: Admin/NhaCungCap
        public ActionResult DanhSach(string tenNhaCungCap)
        {
            ViewBag.tenNhaCungCap = tenNhaCungCap;
            TempData["Dashboard"] = "NhaCungCap";
            return View(new mapNhaCungCap().DanhSachTheoTenNhaCungCap(tenNhaCungCap));
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new NhaCungCap() { });
        }

        [HttpPost]
        public ActionResult ThemMoi(NhaCungCap model)
        {
            mapNhaCungCap mapNCC = new mapNhaCungCap();
            if(mapNCC.ThemMoi(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapNCC.error);
                return View(model);
            }
        }

        public ActionResult CapNhat(int idNhaCungCap)
        {
            return View(new mapNhaCungCap().ChiTiet(idNhaCungCap));
        }

        [HttpPost]
        public ActionResult CapNhat(NhaCungCap model)
        {
            mapNhaCungCap mapNCC = new mapNhaCungCap();
            if (mapNCC.CapNhat(model) == true)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapNCC.error);
                return View(model);
            }
        }

        public ActionResult Xoa(int idNhaCungCap)
        {
            mapNhaCungCap mapNCC = new mapNhaCungCap();
            mapNCC.Xoa(idNhaCungCap);
            return RedirectToAction("DanhSach");
        }
    }
}