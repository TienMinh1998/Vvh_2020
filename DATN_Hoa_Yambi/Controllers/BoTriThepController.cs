using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATN_Hoa_Yambi.Models;
namespace DATN_Hoa_Yambi.Controllers
{
    public class BoTriThepController : Controller
    {
        // GET: BoTriThep
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string LayRathongtinbotri()
        {
            if (Session["ketqua"]!=null)
            {
                return Session["ketqua"].ToString();
            }
            return "0";      
        }

        [HttpPost]
        public string Tinhthep()
        {
            string phuongchonthep = Request["phuongchonthep"];        
            string duongkinh = Request["duongkinh"];
            string sothanh = Request["sothanh"];
            // Tính diện tích thép và khoảng cách thép : 
            double dk = double.Parse(duongkinh)/10;
            double sl = double.Parse(sothanh);
            double _As_1_thanh = (3.14 * dk * dk) / 4;
            double _As_all = _As_1_thanh * sl;
            _As_all = Math.Round(_As_all, 2);
            // Tính khoảng cách : 

            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_user = db.tbl_Users.Where(o => o.MaNguoiDung == Session["User"].ToString()).SingleOrDefault();
            double Ldai =(double)qr_user.Ldai;
            double KC = (Ldai*1000) / (double.Parse(sothanh));
            KC = Math.Ceiling(KC/10)*10;
            string result = "{\"As\":\""+_As_all.ToString()+"\",\"KC\" :\""+KC.ToString()+"\",\"name\": \""+Session["User"].ToString()+"\"}";
            return result;
        }
    }
}