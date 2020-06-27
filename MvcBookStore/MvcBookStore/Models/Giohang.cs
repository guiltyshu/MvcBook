using System;
using System.Linq;

namespace MvcBookStore.Models
{
    public class Giohang
    {
        private dbQLBansachDataContext data = new dbQLBansachDataContext();
        public int iMasach { get; set; }
        public string sTensach { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoluong { get; set; }

        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDongia = double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }
    }
}