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
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.PhanLoaiSanPhams = new HashSet<PhanLoaiSanPham>();
            this.SanPhamDatHangs = new HashSet<SanPhamDatHang>();
        }
    
        public int IDSanPham { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<double> GiaBan { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<bool> ConHang { get; set; }
        public Nullable<double> SoLuong { get; set; }
        public string BaiViet { get; set; }
        public Nullable<int> IDNhaCungCap { get; set; }
        public string HinhAnh { get; set; }
    
        public virtual NhaCungCap NhaCungCap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanLoaiSanPham> PhanLoaiSanPhams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDatHang> SanPhamDatHangs { get; set; }
    }
}
