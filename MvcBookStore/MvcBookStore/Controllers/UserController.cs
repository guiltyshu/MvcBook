using MvcBookStore.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcBookStore.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private static dbQLBansachDataContext db = new dbQLBansachDataContext();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logup(FormCollection collection, KHACHHANG kh)
        {
            var name = collection["HotenKH"];
            var user_name = collection["TenDN"];
            var password = collection["Matkhau"];
            var repeat_password = collection["Nhaplaimatkhau"];
            var address = collection["Diachi"];
            var email = collection["Email"];
            var phone_number = collection["Dienthoai"];
            var birth_date = String.Format("{0:MM/DD/YYYY}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(name))
            {
                ViewData["Error_name"] = "Họ tên khách hàng không được để trống.";
            }
            if (String.IsNullOrEmpty(user_name))
            {
                ViewData["Error_user_name"] = "Phải nhập tên đăng nhập.";
            }
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(repeat_password))
            {
                ViewData["Error_password"] = "Phải nhập mật khẩu.";
                ViewData["Error_repeat_password"] = "Phải nhập lại mật khẩu.";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Error_email"] = "Email không được để trống.";
            }
            if (String.IsNullOrEmpty(phone_number))
            {
                ViewData["Error_phone_number"] = "Phải nhập số điện thoại.";
            }
            else
            {
                kh.HoTen = name;
                kh.Taikhoan = user_name;
                kh.Matkhau = password;
                kh.Email = email;
                kh.DiachiKH = address;
                kh.DienthoaiKH = phone_number;
                kh.Ngaysinh = DateTime.Parse(birth_date);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Login");
            }
            return this.Logup();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var user_name = collection["TenDN"];
            var password = collection["Matkhau"];
            if (String.IsNullOrEmpty(user_name))
            {
                ViewData["Error_user_name"] = "Tên đăng nhập đang để trống.";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Error_password"] = "Mật khẩu đang để trống.";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == user_name && n.Matkhau == password);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công.";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "BookStore");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            return View();
        }
    }
}