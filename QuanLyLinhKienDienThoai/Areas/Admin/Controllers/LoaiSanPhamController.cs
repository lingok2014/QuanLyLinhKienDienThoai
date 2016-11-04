using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyLinhKienDienThoai.Models;

namespace QuanLyLinhKienDienThoai.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private LinhKienDienThoaiEntities db = new LinhKienDienThoaiEntities();

        public ActionResult Index()
        {
            return View(db.LOAISANPHAMs.ToList());
        }

    

      
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MALOAISP,TENLOAISP,MALOAICHA")] LOAISANPHAM lOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.LOAISANPHAMs.Add(lOAISANPHAM);
                db.SaveChanges();
                TempData["Message"] = "Thêm thành công!";
                return RedirectToAction("Index");
            }

            return View(lOAISANPHAM);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(lOAISANPHAM);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALOAISP,TENLOAISP,MALOAICHA")] LOAISANPHAM lOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAISANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "cập nhật thành công!";
                return RedirectToAction("Index");
            }
            return View(lOAISANPHAM);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(lOAISANPHAM);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
                db.LOAISANPHAMs.Remove(lOAISANPHAM);
                db.SaveChanges();
                TempData["Message"] = "Xóa thành công!";


            }
            catch (Exception)
            {
                TempData["Message"] = "loại sản phẩm đã có sản phẩm không thể xóa!";
             
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
