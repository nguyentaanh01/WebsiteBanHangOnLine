using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Models
{
    public class mapNhaCungCap
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        public NhaCungCap ChiTiet(int? idNhaCungCap)
        {
            return db.NhaCungCaps.Find(idNhaCungCap);
        }
        public List<NhaCungCap> DanhSach()
        {
            List<NhaCungCap> data = db.NhaCungCaps.ToList();
            return data;
        }

        public List<NhaCungCap> DanhSachTheoTenNhaCungCap(string tenNhaCungCap)
        {
            var data = db.NhaCungCaps.Where(m => m.TenNhaCungCap.ToLower().Contains(tenNhaCungCap.ToLower())
                        || string.IsNullOrEmpty(tenNhaCungCap)).ToList();
            return data;
        }

        public bool ThemMoi(NhaCungCap model)
        {
            if (string.IsNullOrEmpty(model.TenNhaCungCap))
            {
                error = "Chưa nhập tên nhà cung cấp";
                return false;
            }
            try
            {
                db.NhaCungCaps.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
        }

        public bool CapNhat(NhaCungCap model)
        {
            if (string.IsNullOrEmpty(model.TenNhaCungCap))
            {
                error = "Chưa nhập tên nhà cung cấp";
                return false;
            }
            try
            {
                NhaCungCap updateNCC = ChiTiet(model.IDNhaCungCap);
                if(updateNCC == null)
                {
                    error = "Không tìm thấy Nhà Cung Cấp";
                    return false;
                }
                else
                {
                    updateNCC.TenNhaCungCap = model.TenNhaCungCap;
                    updateNCC.DiaChi = model.DiaChi;
                    updateNCC.SoDienThoai = model.SoDienThoai;
                    updateNCC.Gmail = model.Gmail;
                    updateNCC.WebSite = model.WebSite;
                    updateNCC.MaBuuDien = model.MaBuuDien;
                    updateNCC.ThanhPho = model.ThanhPho;
                    updateNCC.QuocGia = model.QuocGia;

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

        public bool Xoa(int idNhaCungCap)
        {
            NhaCungCap deleteNCC = db.NhaCungCaps.Find(idNhaCungCap);
            if (deleteNCC != null)
            {
                db.NhaCungCaps.Remove(deleteNCC);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}