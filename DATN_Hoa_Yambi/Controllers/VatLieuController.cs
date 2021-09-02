using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class VatLieuController : Controller
    {
        // GET: VatLieu
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string getvatlieu_coc()
        {
            // truyền vào betong code , trả ra bê tông đó:

            int id_betong =int.Parse(Request["btcode"]);
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_betong = db.tbl_betongs.Where(o => o.id_betong == id_betong);
            database.betong_coc = qr_betong.FirstOrDefault();
            return JsonConvert.SerializeObject(database.betong_coc);

           
        }


        public string getvatlieu_dai()
        {
            // truyền vào betong code , trả ra bê tông đó:

            int id_betong = int.Parse(Request["btcode"]);
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_betong = db.tbl_betongs.Where(o => o.id_betong == id_betong);
            database.betong_dai = qr_betong.FirstOrDefault();
            return JsonConvert.SerializeObject(database.betong_dai);


        }
        // trả ra cốt thép của đài.
        public string getVatLieuCotThepDai()
        {
            // truyền vào betong code , trả ra bê tông đó:
            int id_cotthep = int.Parse(Request["ctcode"]);
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_cotthep = db.tbl_cot_theps.Where(o => o.IDCotThep == id_cotthep).FirstOrDefault();
            database.cotthep_dai = qr_cotthep;
            return JsonConvert.SerializeObject(database.cotthep_dai);

        }
        // trả ra cốt thép cọc.
        public string getVatLieuCotThepCoc()
        {
            // truyền vào betong code , trả ra bê tông đó:
            int id_cotthep = int.Parse(Request["ctcode"]);
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_cotthep = db.tbl_cot_theps.Where(o => o.IDCotThep == id_cotthep).FirstOrDefault();
            database.cotthep_coc = qr_cotthep;
            return JsonConvert.SerializeObject(database.cotthep_coc);

        }
    }
}