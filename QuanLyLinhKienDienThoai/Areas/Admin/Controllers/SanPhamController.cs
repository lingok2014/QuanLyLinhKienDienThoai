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
    public class SanPhamController : Controller
    {
        private LinhKienDienThoaiEntities db = new LinhKienDienThoaiEntities();

        // GET: Admin/SanPham
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.LOAISANPHAM).Include(s => s.THUONGHIEU);
            return View(sANPHAMs.ToList());
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP");
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTH");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASP,TENSP,GIA,HINHANH,THONGTIN,SOLUONG,MALOAISP,MATHUONGHIEU")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP", sANPHAM.MALOAISP);
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTH", sANPHAM.MATHUONGHIEU);
            return View(sANPHAM);
        }

        // GET: Admin/SanPham/Edit/5
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

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,GIA,HINHANH,THONGTIN,SOLUONG,MALOAISP,MATHUONGHIEU")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP", sANPHAM.MALOAISP);
            ViewBag.MATHUONGHIEU = new SelectList(db.THUONGHIEUx, "MATHUONGHIEU", "TENTH", sANPHAM.MATHUONGHIEU);
            return View(sANPHAM);
        }

        // GET: Admin/SanPham/Delete/5
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

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
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
