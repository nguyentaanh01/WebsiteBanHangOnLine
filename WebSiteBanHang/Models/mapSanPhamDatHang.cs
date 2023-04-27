using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class mapSanPhamDatHang
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        public SanPhamDatHang ChiTiet(int? idSanPhamDatHang)
        {
            return db.SanPhamDatHangs.Find(idSanPhamDatHang);
        }
        public List<SanPhamDatHang> DanhSach()
        {
            List<SanPhamDatHang> data = db.SanPhamDatHangs.ToList();
            return data;
        }
        
        //Thêm mới sản phẩm đặt hàng
        public bool ThemMoi(SanPhamDatHang model)
        {
            try
            {
                var soLuong = db.SanPhamDatHangs.Count(m => m.IDDonHang == model.IDDonHang && m.IDSanPham == model.IDSanPham);
                if (soLuong > 0)
                {
                    error = "đơn hàng đã tồn tại";
                    return false;
                }
                if(model.SoLuong == null)
                {
                    error = "Chưa nhập số lượng sản phẩm đặt, ít nhất là 1 ";
                }
                db.SanPhamDatHangs.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
            
        }

        public bool CapNhat(SanPhamDatHang model)
        {
            try
            {
                var soLuong = db.SanPhamDatHangs.Count(m => m.IDDonHang == model.IDDonHang && m.IDSanPham == model.IDSanPham
                                && m.IDSanPhamDatHang != model.IDSanPhamDatHang);
                if(soLuong > 0)
                {
                    error = "đơn hàng đã tồn tại không cập nhật được";
                    return false;
                }
                //tìm đối tượng cần sửa
                var updateSPDT = db.SanPhamDatHangs.Find(model.IDSanPhamDatHang);
                updateSPDT.IDDonHang = model.IDDonHang;
                updateSPDT.IDSanPham = model.IDSanPham;
                updateSPDT.SoLuong = model.SoLuong;
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
        }

        public bool Xoa(int idSanPhamDatHang)
        {
            //1. tim doi tuong can xoa
            var deleteSPDT = db.SanPhamDatHangs.Find(idSanPhamDatHang);
            if(deleteSPDT != null)
            {
                db.SanPhamDatHangs.Remove(deleteSPDT);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        
    }
}