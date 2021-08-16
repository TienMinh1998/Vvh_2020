using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class DiaChatController : Controller
    {
        // GET: DiaChat
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string GetListDiaChat()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr = db.tbl_lop_dats.Where(o => o.is_delete == 0);
            if (qr.Any())
            {
                return JsonConvert.SerializeObject(qr.ToList());
            }
            return " ";
        }
    }
}