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
    public class HomeController : Controller
    {
        #region SẢN PHẨM
        // GET: Admin/Home

        public ActionResult DanhSach(string tenSanpham)
        {
            mapSanPham mapSp = new mapSanPham();
            ViewBag.tenSanPham = tenSanpham;
            TempData["Dashboard"] = "SanPham";
            return View(mapSp.DanhSachSanPham(tenSanpham));
        }

        public PartialViewResult ThemMoi()
        {
            return PartialView(new SanPham() { });
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemMoi(SanPham model)
        {
            mapSanPham mapSp = new mapSanPham();
            if (mapSp.ThemMoi(model) != null)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ViewBag.error = mapSp.error;
                ModelState.AddModelError("", mapSp.error);
                return View(model);
            }
        }

        public ActionResult Capnhat(int idSanPham)
        {
            TempData["Dashboard"] = "CapNhatSanPham";
            mapSanPham mapSp = new mapSanPham();
            return View(mapSp.ChiTiet(idSanPham));
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CapNhat(SanPham model)
        {
            mapSanPham mapSp = new mapSanPham();
            if (mapSp.CapNhat(model) != null)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", mapSp.error);
                return View(model);
            }
        }

        public ActionResult Xoa(int idSanPham)
        {
            mapSanPham mapSp = new mapSanPham();
            mapSp.Xoa(idSanPham);
            return RedirectToAction("DanhSach");
        }

        public ActionResult ChiTiet(int idSanPham)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            //1. tim san pham do 
            TempData["Dashboard"] = "Chi tiết";
            SanPham detailSP = db.SanPhams.Find(idSanPham);
            return View(detailSP);
        }

        #endregion

        #region PHÂN LOẠI
        public ActionResult ThemMoiPhanLoai(int idSanPham)
        {
            return View(new PhanLoaiSanPham() { IDSanPham = idSanPham });
        }
        [HttpPost]
        public ActionResult ThemMoiPhanLoai(PhanLoaiSanPham model)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            //check điều kiện, xem sản phẩm ấy tồn tại chưa
            var soLuong = db.PhanLoaiSanPhams.Count(m => m.IDSanPham == model.IDSanPham && m.IDLoaiSanPham == model.IDLoaiSanPham);
            if (soLuong > 0)
            {
                ModelState.AddModelError("", "Sản phẩm đã tồn tại");
                return View(model);
            }
            db.PhanLoaiSanPhams.Add(model);
            db.SaveChanges();
            return RedirectToAction("ChiTiet", new { idSanPham = model.IDSanPham });
        }

        public ActionResult CapNhatPhanLoai(int idPhanLoai)
        {
            TempData["Dashboard"] = "CapNhatPhanLoai";
            BanHangDBEntities db = new BanHangDBEntities();
            var updatePL = db.PhanLoaiSanPhams.Find(idPhanLoai);
            return View(updatePL);
        }
        [HttpPost]
        public ActionResult CapNhatPhanLoai(PhanLoaiSanPham model)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            //tìm đối tượng đó đã
            var updatePL = db.PhanLoaiSanPhams.Find(model.IDPhanLoai);
            if(updatePL == null)
            {
                ModelState.AddModelError("", "Không tìm thấy sản phẩm");
            }
            //check điều kiện, xem sản phẩm ấy tồn tại chưa và phải trừ đi chính nó
            var soLuong = db.PhanLoaiSanPhams.Count(m => m.IDSanPham == model.IDSanPham && m.IDLoaiSanPham == model.IDLoaiSanPham && m.IDPhanLoai != updatePL.IDPhanLoai);
            if (soLuong > 0)
            {
                ModelState.AddModelError("", "Sản phẩm đã tồn tại");
                return View(model);
            }
            updatePL.GhiChu = model.GhiChu;
            updatePL.NgayHieuLuc = model.NgayHieuLuc;
            db.SaveChanges();
            return RedirectToAction("ChiTiet", new { idSanPham = model.IDSanPham });
        }

        public ActionResult XoaPhanLoai(int idPhanLoai, int idSanPham)
        {
            BanHangDBEntities db = new BanHangDBEntities();
            //tim n ra da
            var deletePL = db.PhanLoaiSanPhams.SingleOrDefault(m=>m.IDPhanLoai == idPhanLoai);
            db.PhanLoaiSanPhams.Remove(deletePL);
            db.SaveChanges();
            return RedirectToAction("ChiTiet", new { idSanPham = idSanPham });
        }
        #endregion

        #region HÌNH ẢNH
        public ActionResult CapNhatHinhAnh(int idSanPham)
        {
            TempData["Dashboard"] = "CapNhatHinhAnh";
            ViewBag.idSanPham = idSanPham;
            return View();
        }
        [HttpPost]
        public ActionResult CapNhatHinhAnh(int idSanPham, HttpPostedFileBase fileAnh)
        {
            //1. Tham số đầu vào kiểu HttpPostedFileBase
            //2. Kiểm tra file có dữ liệu hay không 
            if(fileAnh == null)
            {
                ViewBag.error = "Chưa chọn file";
                ViewBag.idSanPham = idSanPham;
                return View();
            }
            if (fileAnh.ContentLength == 0)
            {
                ViewBag.error = "File không có nội dung";
                ViewBag.idSanPham = idSanPham;
                return View();
            }
            //3. Xác định đường dẫn lưu file : URL tương đối => tuyệt đối
            var urlTuongDoi = "/Data/Image/"; //Lấy đường dẫn lưu database
            var urlTuyetDoi = Server.MapPath(urlTuongDoi); //Lấy đường dẫn lưu file trên server
            //4. Lưu file(Chưa kiểm tra trùng file)
            fileAnh.SaveAs(urlTuyetDoi + fileAnh.FileName);
            //5. Lưu database
            mapSanPham mapSP = new mapSanPham();
            mapSP.CapNhatHinhAnh(idSanPham, urlTuongDoi + fileAnh.FileName);

            return RedirectToAction("ChiTiet", new { idSanPham = idSanPham });
        }
        #endregion

        #region Cập Nhật Hình Ảnh bằng CKFinder
        public ActionResult CapNhatHinhAnhCkFinder(int idSanPham)
        {
            TempData["Dashboard"] = "CapNhatHinhAnhCKFinder";
            BanHangDBEntities db = new BanHangDBEntities();
            var sanPham = db.SanPhams.Find(idSanPham);
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult CapNhatHinhAnhCkFinder(SanPham model)
        {
            mapSanPham mapSP = new mapSanPham();
            mapSP.CapNhatHinhAnhBangCKFinder(model);
            return RedirectToAction("ChiTiet", new { idSanPham = model.IDSanPham });
        }
        #endregion
    }
}