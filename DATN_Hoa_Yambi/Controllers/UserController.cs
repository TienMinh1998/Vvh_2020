using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string AddNewUser()
        {
            string UserCode = Request["txt_ma_nguoi_dung"];
            string UserName = Request["txt_ten_nguoi_dung"];
            string UserDIenThoai = Request["txt_so_dien_thoai"];

            try
            {
                VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
                tbl_User user = new tbl_User();
                user.MaNguoiDung = UserCode;
                user.Ten = UserName;
                user.SoDienThoai = UserDIenThoai;
                db.tbl_Users.InsertOnSubmit(user);
                db.SubmitChanges();
                return "Thêm Thành Công Mã : " + UserCode + " Tên :" + UserName + "Điện Thoại: " + UserDIenThoai;
            }
            catch (Exception)
            {

                return "Người Dùng đã tồn tại...";
            }
            // thêm vào bảng User : 
         

        }

        public string BatDauTinhToan()
        {
            // Bạn có thể bắt đầu phiên làm việc
            string UserCode = Request["txt_ma_nguoi_dung"];
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var user = db.tbl_Users.Where(xxx => xxx.MaNguoiDung == UserCode).SingleOrDefault();
            Session["User"] = user.MaNguoiDung;

            return user.Ten;
        }

        [HttpPost]
        public string AddnewLoad()
        {
            // Kiểm tra session : 
            if (Session["User"]!=null)
            {
                double No = double.Parse(Request["txt_No"]);
                double Mo = double.Parse(Request["txt_Mo"]);
                double Qo = double.Parse(Request["txt_Qo"]);
                double _bcot = double.Parse(Request["txt_Bcot"]);
                double _hcot = double.Parse(Request["txt_Hcot"]);
                double _hdai = double.Parse(Request["txt_Hdai"]);

                // Cập nhật vào cơ sở dữ liệu;
                VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
                var qr_user = (from item in db.tbl_Users
                               where item.MaNguoiDung == Session["User"].ToString()
                               select item).SingleOrDefault();
                qr_user.Mo = Mo;
                qr_user.No = No;
                qr_user.Qo = Qo;
                qr_user.b_cot = _bcot;
                qr_user.h_cot = _hcot;
                qr_user.h_dai = _hdai;

                db.SubmitChanges();
                return "Thêm Thành Công";
            }
            else
            {
                return "500";
            }
         
        }
    }
}