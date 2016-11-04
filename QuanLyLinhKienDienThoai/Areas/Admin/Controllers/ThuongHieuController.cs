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
    public class ThuongHieuController : Controller
    {
        private LinhKienDienThoaiEntities db = new LinhKienDienThoaiEntities();

        // GET: Admin/ThuongHieu
        public ActionResult Index()
        {
            return View(db.THUONGHIEUx.ToList());
        }

       
        // GET: Admin/ThuongHieu/Create
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATHUONGHIEU,TENTH,HINHTH")] THUONGHIEU tHUONGHIEU)
        {
            if (ModelState.IsValid)
            {
                db.THUONGHIEUx.Add(tHUONGHIEU);
                db.SaveChanges();
                TempData["Message"] = "thêm thành công!";
                return RedirectToAction("Index");
            }

            return View(tHUONGHIEU);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = db.THUONGHIEUx.Find(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATHUONGHIEU,TENTH,HINHTH")] THUONGHIEU tHUONGHIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUONGHIEU).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "cập nhật thành công!";
                return RedirectToAction("Index");
               
            }
            return View(tHUONGHIEU);
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUONGHIEU tHUONGHIEU = db.THUONGHIEUx.Find(id);
            if (tHUONGHIEU == null)
            {
                return HttpNotFound();
            }
            return View(tHUONGHIEU);
        }

      
        [HttpPost]
      
        public ActionResult Delete(int id)
        {
            try
            {
                THUONGHIEU tHUONGHIEU = db.THUONGHIEUx.Find(id);
                db.THUONGHIEUx.Remove(tHUONGHIEU);
                db.SaveChanges();
                TempData["Message"] = "Xóa thành công!";
            }
            catch (Exception)
            {

                TempData["Message"] = "thương hiệu đã có sản phẩm không thể xóa!";
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
