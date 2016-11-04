using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyLinhKienDienThoai.Models;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace QuanLyLinhKienDienThoai.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private LinhKienDienThoaiEntities db = new LinhKienDienThoaiEntities();


        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.LOAISANPHAM).Include(s => s.THUONGHIEU);
            return View(sANPHAMs.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP");
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTH");
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Exclude = "MASP")] SANPHAM sANPHAM, HttpPostedFileBase fileUpload)
        {
            SANPHAM sp = new SANPHAM();
            try
            {
                
                if (fileUpload != null)
                {
                    //Upload file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn file ảnh 
                    var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                    //Kiểm tra file đã tồn tại
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sp.GIA = sANPHAM.GIA;
                    sp.MALOAISP = sANPHAM.MALOAISP;
                    sp.MATHUONGHIEU = sANPHAM.MATHUONGHIEU;
                    sp.SOLUONG = sANPHAM.SOLUONG;
                    sp.THONGTIN = sANPHAM.THONGTIN;
                    sp.TENSP = sANPHAM.TENSP;
                    sANPHAM.HINHANH = fileUpload.FileName;
                    if (ModelState.IsValid)
                    {
                        db.SANPHAMs.Add(sANPHAM);
                        db.SaveChanges();
                        TempData["Message"] = "thêm thành công!";

                    }
                    else
                    {
                        TempData["Message"] = "thêm không thành công!";
                    }

                }

            }
            catch (Exception)
            {

                TempData["Message"] = "thêm thất bại!";
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP", sANPHAM.MALOAISP);
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTH", sANPHAM.MATHUONGHIEU);
            return View(sANPHAM);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var bookUpdate = db.SANPHAMs.Find(id);
            if (TryUpdateModel(bookUpdate, "", new string[] { "TENSP", "GIA", "HINHANH", "THONGTIN", "MALOAISP", "MATHUONGHIEU" }))
            {
                try
                {
                    db.Entry(bookUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Cập Nhật thành công!";
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }


        [HttpPost]

        public ActionResult Delete(int id)
        {
            try
            {
                SANPHAM sANPHAM = db.SANPHAMs.Find(id);
                db.SANPHAMs.Remove(sANPHAM);
                db.SaveChanges();
                TempData["Message"] = "xóa thành công!";
            }
            catch (Exception)
            {
                TempData["Message"] = "Sản phẩm đã có ở đơn hàng không thể xóa được!";

            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
