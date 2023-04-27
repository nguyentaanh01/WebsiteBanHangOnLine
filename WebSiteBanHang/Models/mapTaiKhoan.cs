using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class mapTaiKhoan
    {
        BanHangDBEntities db = new BanHangDBEntities();
        public string error = "";

        //ham chi tiet (tim kiem tai khoan theo ID)
        public TaiKhoan ChiTiet(int? idTaiKhoan)
        {
            return db.TaiKhoans.Find(idTaiKhoan);
        }

        //Ham tất cả Danh Sach
        public List<TaiKhoan> DanhSach()
        {
            return db.TaiKhoans.ToList();
        }

        //Hàm lọc Danh sách theo tên tai khoan
        public List<TaiKhoan> DanhSachTheoTenHienThi(string tenHienThi)
        {
            var data = db.TaiKhoans.Where(m => m.TenHienThi.ToLower().Contains(tenHienThi.ToLower()) == true
                        || string.IsNullOrEmpty(tenHienThi) == true).ToList();
            return data;
        }

        //Ham Them Moi
        public bool ThemMoi(TaiKhoan model)
        {
            //1. check data (kiểm tra dữ liệu đầu vào, check trường tên loại sản phẩm)
            if (string.IsNullOrEmpty(model.UserName) == true | string.IsNullOrEmpty(model.PassWord) == true)
            {
                error = "Chưa nhập UserName va PassWord";
                return false;
            }
            //2. lưu dữ liệu 
            try
            {
                db.TaiKhoans.Add(model);
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
        public bool CapNhat(TaiKhoan model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserName) == true | string.IsNullOrEmpty(model.PassWord) == true)
                {
                    error = "Chưa nhập UserName va PassWord";
                    return false;
                }
                //1. Tim đối tượng cần sửa theo ID
                TaiKhoan updateLTK = db.TaiKhoans.Find(model.ID);
                //2. check đối tượng đó, nếu tìm dk thì xử lý tiếp
                if (updateLTK == null)
                {
                    error = "Không tìm thấy đối tượng";
                    return false;
                }
                //3. Cập nhật dữ liệu
                updateLTK.UserName = model.UserName;
                updateLTK.PassWord = model.PassWord;
                updateLTK.TenHienThi = model.TenHienThi;
                updateLTK.SoDienThoai = model.SoDienThoai;
                updateLTK.Active = model.Active;
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
        public bool Xoa(int idTaiKhoan)
        {
            //1. Tìm đối tượng cần xóa
            var deleteLTK = ChiTiet(idTaiKhoan);
            if (deleteLTK == null)
            {
                error = "Không tìm thấy đối tượng";
                return false;
            }
            //2. Xóa cả đối tượng đó đi
            db.TaiKhoans.Remove(deleteLTK);
            //3. Lưu lại và return 
            db.SaveChanges();
            return true;
        }
    }
}