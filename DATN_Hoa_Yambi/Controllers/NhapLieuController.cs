using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class NhapLieuController : Controller
    {
        // GET: NhapLieu
        public ActionResult Index()
        {
            if (Session["User"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        
        }

        [HttpPost]
        public string getdataTaiTrong()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_user = db.tbl_Users.Where(o => o.MaNguoiDung == Session["User"].ToString()).SingleOrDefault();


            return JsonConvert.SerializeObject(qr_user);

        }
    }
}