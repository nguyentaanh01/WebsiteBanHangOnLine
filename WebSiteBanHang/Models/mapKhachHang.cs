using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class mapKhachHang
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        public KhachHang ChiTiet(int? idKhachHang)
        {
            return db.KhachHangs.Find(idKhachHang);
        }
        public List<KhachHang> DanhSach()
        {
            List<KhachHang> data = db.KhachHangs.ToList();
            return data;
        }

        public List<KhachHang> DanhSachTheoTenKhachhang(string tenKhachhang)
        {
            var data = db.KhachHangs.Where(m => m.TenKhachhang.ToLower().Contains(tenKhachhang.ToLower())
                        || string.IsNullOrEmpty(tenKhachhang)).ToList();
            return data;
        }

        public bool ThemMoi(KhachHang model)
        {
            if (string.IsNullOrEmpty(model.TenKhachhang))
            {
                error = "Chưa nhập tên khách hàng";
                return false;
            }
            try
            {
                db.KhachHangs.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
        }

        public bool CapNhat(KhachHang model)
        {
            if (string.IsNullOrEmpty(model.TenKhachhang))
            {
                error = "Chưa nhập tên nhà cung cấp";
                return false;
            }
            try
            {
                KhachHang updateKH = ChiTiet(model.IDKhachHang);
                if (updateKH == null)
                {
                    error = "Không tìm thấy Khach Hang";
                    return false;
                }
                else
                {
                    updateKH.TenKhachhang = model.TenKhachhang;
                    updateKH.DiaChi = model.DiaChi;
                    updateKH.SoDienThoai = model.SoDienThoai;
                    updateKH.MaGiamGia = model.MaGiamGia;
                    updateKH.Gmail = model.Gmail;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
        }

        public bool Xoa(int idKhachHang)
        {
            KhachHang deleteKH = db.KhachHangs.Find(idKhachHang);
            if (deleteKH != null)
            {
                db.KhachHangs.Remove(deleteKH);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}