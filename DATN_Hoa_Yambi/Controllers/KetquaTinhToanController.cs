using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class KetquaTinhToanController : Controller
    {
        // GET: KetquaTinhToan
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetKetQuaTinhToan()
        {
            if (Session["ketqua"] != null)
            {
                return Session["ketqua"].ToString();
            }
            return "";
        }
        [HttpGet]
        public string GetUserTinhToan()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_user = db.tbl_Users.Where(x => x.MaNguoiDung == Session["user"].ToString()).SingleOrDefault();
            return JsonConvert.SerializeObject(qr_user);
        }

    }
}