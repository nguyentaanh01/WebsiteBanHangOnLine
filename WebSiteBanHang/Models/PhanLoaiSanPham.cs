//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhanLoaiSanPham
    {
        public int IDPhanLoai { get; set; }
        public Nullable<int> IDSanPham { get; set; }
        public Nullable<int> IDLoaiSanPham { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> NgayHieuLuc { get; set; }
    
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
