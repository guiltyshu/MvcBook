using MvcBookStore.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcBookStore.Controllers
{
    [AllowAnonymous]
    public class BookStoreController : Controller
    {
        private dbQLBansachDataContext data = new dbQLBansachDataContext();

        private List<SACH> getNewBooks(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // GET: BookStore
        public ActionResult Index(int? page)
        {
            int pageSizes = 5;
            int pageNum = (page ?? 1);

            var newBooks = getNewBooks(5);
            return View(newBooks.ToPagedList(pageNum, pageSizes));
        }

        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }

        public ActionResult SPTheochude(int id, int? page)
        {
            int pageSizes = 5;
            int pageNum = (page ?? 1);

            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach.ToPagedList(pageNum, pageSizes));
        }

        public ActionResult SPTheoNXB(int id, int? page)
        {
            int pageSizes = 5;
            int pageNum = (page ?? 1);

            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach.ToPagedList(pageNum, pageSizes));
        }

        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes where s.Masach == id select s;
            return View(sach.Single());
        }
    }
}