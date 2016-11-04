using QuanLyLinhKienDienThoai.Common;
using QuanLyLinhKienDienThoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyLinhKienDienThoai.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private LinhKienDienThoaiEntities db = new LinhKienDienThoaiEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// hàm trả về trang Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Hàm đăng nhập submit dữ liệu về form có 2 tham số 
        /// tham số 1: tài khoản
        /// tham số 2: mật khẩu
        /// </summary>
        /// <returns>kết quả trả về là một dòng dữ dữ liệu( 1 người dùng)</returns>
        [HttpPost]
        public ActionResult Login(string taiKhoan, string matKhau)
        {
            string passMD5 = MaHoa.EncryptMD5(taiKhoan + matKhau);
            var user = db.NGUOIDUNGs.SingleOrDefault(n => n.TAIKHOAN == taiKhoan && n.MATKHAU == passMD5);
            if (user != null)
            {
                Session["MAND"] = user.MAND;
                Session["Taikhoan"] = user.TAIKHOAN;
                Session["HoTen"] = user.HOTEN;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Đăng nhập sai hoặc mật khẩu không tồn tại";

            return View();
        }
        /// <summary>
        /// Hàm đăng xuất
        /// </summary>
        /// <returns>Cho giá trị session==null</returns>
        public ActionResult Logout()
        {
            Session["MAND"] = null;
            Session["Taikhoan"] = null;
            Session["HoTen"] = null;
            return RedirectToAction("Index");
        }
    }
}