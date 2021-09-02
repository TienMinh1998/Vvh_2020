using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class SualopdatController : Controller
    {
        // GET: Sualopdat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Editlopdat()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();

            string malopdat = Request["ma_lop_dat"];
            var qr_lopdat = db.tbl_lop_dats.Where(o => o.MaLopDat == malopdat).SingleOrDefault();
           // get layder đất :
            tbl_lop_dat layerNeedEdit = new tbl_lop_dat();
            layerNeedEdit.MaLopDat = malopdat;
            layerNeedEdit.loaidat = qr_lopdat.loaidat;
            layerNeedEdit.trangthai = qr_lopdat.trangthai;
            layerNeedEdit.chieuday = qr_lopdat.chieuday;
            layerNeedEdit.dungtrongrieng = qr_lopdat.dungtrongrieng;
            layerNeedEdit.modundanhoi = qr_lopdat.modundanhoi;
            layerNeedEdit.gocmasat = qr_lopdat.gocmasat;
            layerNeedEdit.doroi = qr_lopdat.doroi;
            layerNeedEdit.suckhangxuyen = qr_lopdat.suckhangxuyen;
            layerNeedEdit.n = qr_lopdat.n;
            layerNeedEdit.k = qr_lopdat.k;
            layerNeedEdit.alpha = qr_lopdat.alpha;
            layerNeedEdit.vitritrentrudiachat = qr_lopdat.vitritrentrudiachat;



            return JsonConvert.SerializeObject(layerNeedEdit);


              

        }


        [HttpPost]
        public string LuuLaiChinhsua()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            tbl_lop_dat lopdat = new tbl_lop_dat();

            string malopdat = Request["txt_malopdat"];
            string loaidat = Request["txt_loaidat"];
            string trangthai = Request["txt_trangthai"];
            double chieuday =double.Parse(Request["txt_chieuday"]);
           double dungtrongrieng =double.Parse(Request["txt_dungtrongrieng"]);
            int modundanhoi =int.Parse(Request["txt_modundanhoi"]);
            int gocmasattrong =int.Parse(Request["txt_modundanhoi"]);
            int doroi =int.Parse(Request["txt_modundanhoi"]);

            int qc =int.Parse(Request["txt_qc"]);
            byte n =byte.Parse(Request["txt_n"]);
            double k =double.Parse(Request["txt_k"]);
            double alpha =double.Parse(Request["txt_alpha"]);

            var qr_lopdat = db.tbl_lop_dats.Where(o => o.MaLopDat == malopdat).SingleOrDefault();
            qr_lopdat.loaidat = loaidat;
            qr_lopdat.trangthai = trangthai;
            qr_lopdat.chieuday =chieuday;
            qr_lopdat.dungtrongrieng = dungtrongrieng;
            qr_lopdat.modundanhoi = modundanhoi;
            qr_lopdat.gocmasat = gocmasattrong;
            qr_lopdat.doroi = doroi;
            qr_lopdat.suckhangxuyen = qc;
            qr_lopdat.n = n;
            qr_lopdat.k = k;
            qr_lopdat.alpha = alpha;

            db.SubmitChanges();        
            return "Sửa thành công!!!";
        }
    }
}