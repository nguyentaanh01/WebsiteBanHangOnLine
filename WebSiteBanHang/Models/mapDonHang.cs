using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class mapDonHang
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        //tìm đơn hàng theo ID
        public DonHang ChiTiet(int? idDonhang)
        {
            return db.DonHangs.Find(idDonhang);
        }
        //Lấy danh sách tất cả đơn hàng 
        public List<DonHang> DanhSach()
        {
            List<DonHang> data = db.DonHangs.ToList();
            return data;
        }

        //Tìm đơn hàng theo tên khách hàng
        public List<DonHang> DanhSachTheoTenDonHang(string tenDonHang)
        {
            var data = (from donhang in db.DonHangs
                        where donhang.TenDonHang.ToLower().Contains(tenDonHang.ToLower()) || string.IsNullOrEmpty(tenDonHang.ToLower())
                        select donhang
                        ).ToList();
            return data;
        }

        public bool ThemMoi(DonHang model)
        {
            try
            {
                if (model.TenDonHang == null)
                {
                    error = "Thiếu thông tin tên đơn hàng";
                    return false;
                }
                if (model.IDKhachHang == null)
                {
                    error = "Thiếu thông tin khách hàng";
                    return false;
                }
                db.DonHangs.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
        }

        public bool CapNhat(DonHang model)
        {
            try
            {
                DonHang updateDH = ChiTiet(model.IDDonHang);
                if (updateDH == null)
                {
                    error = "Không tìm thấy Khach Hang";
                    return false;
                }
                else
                {
                    updateDH.IDKhachHang = model.IDKhachHang;
                    updateDH.TenDonHang = model.TenDonHang;
                    updateDH.GhiChu = model.GhiChu;
                    updateDH.TrangThai = model.TrangThai;
                    updateDH.NgayOder = model.NgayOder;
                    updateDH.GhiChu = model.GhiChu;

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

        public bool Xoa(int idDonhang)
        {
            DonHang deleteDH = db.DonHangs.Find(idDonhang);
            if (deleteDH != null)
            {
                db.DonHangs.Remove(deleteDH);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}