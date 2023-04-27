using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Models
{
    public class mapLoaiSanPham
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        //ham chi tiet (tim kiem san pham theo ID)
        public LoaiSanPham ChiTiet(int? idLoaiSanPham)
        {
            return db.LoaiSanPhams.Find(idLoaiSanPham);
        }

        //Ham tất cả Danh Sach
        public List<LoaiSanPham> DanhSach()
        {
            return db.LoaiSanPhams.ToList();
        }

        //Hàm lọc Danh sách theo tên loại sản phẩm
        public List<LoaiSanPham> DanhSachTheotenLoai(string tenLoaiSanPham)
        {
            var data = db.LoaiSanPhams.Where(m => m.TenLoai.ToLower().Contains(tenLoaiSanPham.ToLower()) == true 
                        || string.IsNullOrEmpty(tenLoaiSanPham) == true).ToList();
            return data;
        }

        //Ham Them Moi
        public bool ThemMoi(LoaiSanPham model)
        {
            //1. check data (kiểm tra dữ liệu đầu vào, check trường tên loại sản phẩm)
            if(string.IsNullOrEmpty(model.TenLoai) == true)
            {
                error = "Chưa nhập tên loại sản phẩm";
                return false;
            }
            //2. lưu dữ liệu 
            try
            {
                db.LoaiSanPhams.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            }
        }

        //Ham Cap Nhat 
        public bool CapNhat(LoaiSanPham model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.TenLoai))
                {
                    error = "Chưa nhập tên loại";
                    return false;
                }
                //1. Tim đối tượng cần sửa theo ID
                LoaiSanPham updateLSP = db.LoaiSanPhams.Find(model.IDLoaiSanPham);
                //2. check đối tượng đó, nếu tìm dk thì xử lý tiếp
                if (updateLSP == null)
                {
                    error = "Không tìm thấy đối tượng";
                    return false;
                }
                //3. Cập nhật dữ liệu
                updateLSP.TenLoai = model.TenLoai;
                updateLSP.XuatXu = model.XuatXu;
                db.SaveChanges();
                return true;
            }
            catch
            {
                error = "Lỗi hệ thống";
                return false;
            } 
        }

        //Ham Xoa 
        public bool Xoa(int idLoaiSanPham)
        {
            //1. Tìm đối tượng cần xóa
            var deleteLSP = ChiTiet(idLoaiSanPham);
            if(deleteLSP == null)
            {
                error = "Không tìm thấy đối tượng";
                return false;
            }
            //2. Xóa cả đối tượng đó đi
            db.LoaiSanPhams.Remove(deleteLSP);
            //3. Lưu lại và return 
            db.SaveChanges();
            return true;
        }
    }
}