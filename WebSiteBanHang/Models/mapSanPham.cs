using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Models
{
    public class mapSanPham
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        public SanPham ChiTiet(int? idSanPham)
        {
            return db.SanPhams.Find(idSanPham);
        }

        public List<SanPham> Danhsach()
        {
            return db.SanPhams.ToList();
        }
        public List<SanPham> DanhSachSanPham(string tenSanPham)
        {
            //1. Lấy hết tất cả danh sách trong bảng Sản Phẩm
            //return db.SanPhams.ToList();
            //2. Sử dụng query linq để lấy dữ liệu ở bảng SanPham
            /*List<SanPham> data = (from sp in db.SanPhams
                                  select sp
                                  ).ToList();
            */
            //3. Lọc lấy danh sách theo tên sản phẩm
            var data = db.SanPhams.Where(m => m.TenSanPham.ToLower().Contains(tenSanPham.ToLower()) == true 
                        || string.IsNullOrEmpty(tenSanPham) == true).ToList();
            return data.OrderBy(m=> m.TenSanPham).ToList();
        }

        public List<PhanLoaiSanPham> DanhSachPhanLoaiSanPham(int idSanpham)
        {
            var data = (from item in db.PhanLoaiSanPhams
                        where item.IDSanPham == idSanpham
                        select item).ToList();
            return data;
        }

        public SanPham ThemMoi(SanPham model)
        {
            //1. check data (kiểm tra dữ liệu đầu vào)
            if (string.IsNullOrEmpty(model.TenSanPham) == true)
            {
                error = "Chưa Nhập Tên Sản Phẩm";
                return null;
            }
            //2. Lưu data
            try
            {
                db.SanPhams.Add(model);
                db.SaveChanges();
                return model;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return null;
            }
        }

        public SanPham CapNhat(SanPham model)
        {
                //1. check data (Kiểm tra dữ liệu đầu vào)
            if(string.IsNullOrEmpty(model.TenSanPham) == true)
            {
                error = "Chưa nhập tên sản phẩm";
                return null;
            }
            try
            {
                //2. Tìm kiếm sản phẩm đó trong danh sách
                var updateSP = ChiTiet(model.IDSanPham);
                //3. check null
                if (updateSP == null)
                {
                    return null;
                }
                updateSP.TenSanPham = model.TenSanPham;
                updateSP.GiaBan = model.GiaBan;
                updateSP.NgayNhap = model.NgayNhap;
                updateSP.ConHang = model.ConHang;
                updateSP.SoLuong = model.SoLuong;
                updateSP.BaiViet = model.BaiViet;
                updateSP.IDNhaCungCap = model.IDNhaCungCap;

                db.SaveChanges();
                return model;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return null;
            }
        }

        public bool Xoa(int idSanPham)
        {
            //Tìm đối tượng
            var model = db.SanPhams.Find(idSanPham);
            if(model != null)
            {
                db.SanPhams.Remove(model);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CapNhatHinhAnh(int idSanPham, string urlHinhAnh)
        {
            try
            {
                //1. tìm ra cái sản phẩm đó
                var sanpham = db.SanPhams.Find(idSanPham);
                sanpham.HinhAnh = urlHinhAnh;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatHinhAnhBangCKFinder(SanPham model)
        {
            try
            {
                //1. tìm ra cái sản phẩm đó
                var sanpham = db.SanPhams.Find(model.IDSanPham);
                sanpham.HinhAnh = model.HinhAnh;
                sanpham.TenSanPham = model.TenSanPham;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}