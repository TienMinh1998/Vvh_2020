// Document : Xử lý nhập liệu đất
// author : Vũ Văn Hòa
// Edit : not yet
// Date : 11/8/2021



using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public string NhapLieuDat()
        {
            string chieudaylopdat = Request["txt_chieu_day"];
            string n = Request["txt_N"];
            string loaidat = Request["txt_loai_dat"];
            string trangthai = Request["txt_trang_thai"];
            string dungtrongrieng = Request["txt_dung_trong_rieng"];
            string modundanhoi = Request["txt_mo_dun_dan_hoi"];
            string gocmasat = Request["txt_goc_ma_sat"];
            string doroi = Request["txt_do_roi"];
            string suckhangxuyen = Request["txt_suc_khang_xuyen"];
            string malopdat = Request["txt_ma_lop_Dat"];
            string k = Request["txt_k"];
            string alpha = Request["txt_alpha"];

            try
            {
                VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
                tbl_lop_dat lopdat_new = new tbl_lop_dat();
                lopdat_new.MaLopDat = malopdat;
                lopdat_new.chieuday = double.Parse(chieudaylopdat);
                lopdat_new.n = byte.Parse(n);
                lopdat_new.loaidat = loaidat;
                lopdat_new.trangthai = trangthai;
                lopdat_new.dungtrongrieng = double.Parse(dungtrongrieng);
                lopdat_new.modundanhoi = int.Parse(modundanhoi);
                lopdat_new.gocmasat = int.Parse(gocmasat);
                lopdat_new.doroi = int.Parse(doroi);
                lopdat_new.suckhangxuyen = int.Parse(suckhangxuyen);
                lopdat_new.is_delete = 0;
                lopdat_new.k = double.Parse(k);
                lopdat_new.alpha = double.Parse(alpha);
                
                db.tbl_lop_dats.InsertOnSubmit(lopdat_new);
                db.SubmitChanges();
                return "Thêm Thành công";
            }
            catch ( Exception ex)
            {

                return ex.Message;
            }



        }
     
        
        [HttpPost]
        public string getBetong()
        {
            try
            {
                return JsonConvert.SerializeObject(database.betong_coc);
            }
            catch (Exception)
            {

                return "0";
            }
          
        }

    }
}