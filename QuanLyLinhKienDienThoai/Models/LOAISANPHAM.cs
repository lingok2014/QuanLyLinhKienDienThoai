﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyLinhKienDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class LOAISANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAISANPHAM()
        {
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        [DisplayName("Mã Loại SP")]
        public int MALOAISP { get; set; }
        [DisplayName(" Tên Loại")]
        [Required(ErrorMessage = "Tên loại sản phâm bắt buộc nhập")]
        public string TENLOAISP { get; set; }
        [DisplayName("Mã Loại cha")]
        public Nullable<int> MALOAICHA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
