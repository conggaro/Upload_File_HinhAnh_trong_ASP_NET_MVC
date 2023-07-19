using Hoc_Upload_File.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hoc_Upload_File.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        public ActionResult View_TrangChu()
        {
            // tạo ViewBag tiêu đề
            ViewBag.tieu_de = $"Trang chủ ASP.NET MVC";

            // tạo ViewBag thông báo
            ViewBag.thong_bao = $"Đây là trang chủ";

            // tạo đối tượng db
            QuanLy_HinhAnhEntities db = new QuanLy_HinhAnhEntities();

            // tạo đối tượng query
            var dt_query = from item in db.Upload_File_HinhAnh
                           select item;

            List<Upload_File_HinhAnh> ds = dt_query.ToList();

            // tạo ViewBag để đưa dữ liệu đến View
            ViewBag.ds = ds;

            // tạo ViewBag đếm
            ViewBag.dem = ds.Count();

            return View();
        }
    }
}