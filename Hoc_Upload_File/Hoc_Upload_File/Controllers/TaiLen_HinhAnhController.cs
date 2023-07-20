using Hoc_Upload_File.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// đây là controller tải lên hình ảnh

namespace Hoc_Upload_File.Controllers
{
    public class TaiLen_HinhAnhController : Controller
    {
        // GET: TaiLen_HinhAnh
        public ActionResult View_TaiLen_HinhAnh()
        {
            // tạo ViewBag tiêu đề
            ViewBag.tieu_de = $"Tải lên hình ảnh";

            // tạo ViewBag thông báo
            ViewBag.thong_bao = $"Đây là trang tải lên hình ảnh";

            return View();
        }


        // khi làm công việc post
        // thì nó chạy vào hàm XuLy_TaiLen_HinhAnh()


        // xử lý tải lên hình ảnh
        [HttpPost]
        public ActionResult XuLy_TaiLen_HinhAnh(HttpPostedFileBase file_HinhAnh)
        {
            // tham số là đối tượng file_HinhAnh
            // đối tượng được tạo từ class HttpPostedFileBase

            // bây giờ
            // kiểm tra biến file_HinhAnh có dữ liệu không
            // nếu không có dữ liệu
            // thì thông báo "Bạn chưa chọn hình ảnh"
            if (file_HinhAnh == null)
            {
                // tạo ViewBag tiêu đề
                ViewBag.tieu_de = $"Tải lên hình ảnh";

                // tạo ViewBag thông báo
                ViewBag.thong_bao = $"Đây là trang tải lên hình ảnh";

                // tạo ViewBag
                // để thông báo người dùng chưa chọn hình ảnh
                ViewBag.thong_bao_TrangThai_HinhAnh = $"Bạn chưa chọn file";

                return View("View_TaiLen_HinhAnh");
            }
            else if (file_HinhAnh.ContentLength == 0)
            {
                // nếu file tải lên không chứa dữ liệu
                // tức là file tải lên 0 byte ấy
                // tức là file không có nội dung ấy
                // thì thông báo
                // file của bạn không có nội dung


                // tạo ViewBag tiêu đề
                ViewBag.tieu_de = $"Tải lên hình ảnh";

                // tạo ViewBag thông báo
                ViewBag.thong_bao = $"Đây là trang tải lên hình ảnh";

                // tạo ViewBag
                // để thông báo người dùng chưa chọn hình ảnh
                ViewBag.thong_bao_TrangThai_HinhAnh = $"File của bạn không có nội dung";

                return View("View_TaiLen_HinhAnh");
            }


            // xác định đường dẫn lưu file
            // URL tương đối trên server => URL tuyệt đối trên server
            string URL_TuongDoi = $"/DuLieuHTML/img/";

            // ví dụ đường dẫn tuyệt đối:
            // C:\Test Code C#\Hoc_Upload_File\Hoc_Upload_File\DuLieuHTML\img
            // chúng ta không thể chỉ định cứng được
            // vì đường dẫn trên server nó khác
            // có thể trên server thì nó lưu trong ổ D, ổ E
            // chỉ định đường dẫn tuyệt đối có thể gây ra lỗi trên server

            // để server tự tìm đường dẫn
            // thì sử dụng Server.MapPath()
            string URL_TuyetDoi = Server.MapPath(URL_TuongDoi);


            // kiểm tra trùng tên file
            // => đổi tên file = tên file cũ (không kèm đuôi) + "-" + số + đuôi
            // ví dụ:
            // anh.jpg => anh + "-" + 1 + ".jpg" => anh-1.jpg
            // 1. tách tên và đuôi
            // 2. sử dụng biến i để chạy và cộng vào tên file mới
            // 3. kiểm tra lại
            string full_DuongDan = URL_TuyetDoi + file_HinhAnh.FileName;
            int i = 1;
            while (System.IO.File.Exists(full_DuongDan) == true)
            {
                string ten = Path.GetFileNameWithoutExtension(file_HinhAnh.FileName);
                string duoi = Path.GetExtension(file_HinhAnh.FileName);

                full_DuongDan = URL_TuyetDoi + ten + "-"+ i + duoi;
                i++;
            }

            // kiểm tra xong
            // thì lưu file
            file_HinhAnh.SaveAs(full_DuongDan);

            // chúng ta cần tạo đối tượng db
            QuanLy_HinhAnhEntities db = new QuanLy_HinhAnhEntities();

            // tạo biến URL_HinhAnh_db
            // để lưu chuỗi string đường dẫn vào trong CƠ SỞ DỮ LIỆU
            string URL_HinhAnh_db = URL_TuongDoi + Path.GetFileName(full_DuongDan);

            // tạo đối tượng
            // tử cái bảng Upload_File_HinhAnh
            Upload_File_HinhAnh dt = new Upload_File_HinhAnh();

            // gán đường dẫn cho đối tượng
            dt.path_HinhAnh = URL_HinhAnh_db;

            // thêm bản ghi
            // vào trong bảng Upload_File_HinhAnh
            db.Upload_File_HinhAnh.Add(dt);

            // lưu dữ liệu vào trong db
            db.SaveChanges();

            return RedirectToAction("View_TrangChu", "TrangChu");
        }
    }
}