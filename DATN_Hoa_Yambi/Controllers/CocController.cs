using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class CocController : Controller
    {
        // GET: Coc
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
        [HttpPost]
        public string getListCoc()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var pr_coc = db.tbl_cocs;
            if (pr_coc.Any())
            {
                return JsonConvert.SerializeObject(pr_coc.ToList());

            }
            return "";
        }
    }
}