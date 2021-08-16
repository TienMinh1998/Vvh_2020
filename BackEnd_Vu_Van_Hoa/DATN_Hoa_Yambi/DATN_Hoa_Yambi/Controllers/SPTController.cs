using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class SPTController : Controller
    {
        // GET: SPT
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string getSPT()
        {
            // lấy ra các lớp đất 
            database.cls_Coc.betongcoc = database.betong_coc;
            database.cls_Coc.cothepcoc = database.cotthep_coc;

            List<tbl_lop_dat> dsld = new List<tbl_lop_dat>();
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr = db.tbl_lop_dats;
            if (qr.Any())
            {
                foreach (tbl_lop_dat item in qr)
                {
                    dsld.Add(item); // thêm vào danh sách lớp đất;
                }
            }
            // Tính chu vi tiết diện cọc : 
            double u = database.cls_Coc.dk_coc * 4;
            // Tính diện tích của cọc :
            double F = database.cls_Coc.dk_coc * database.cls_Coc.dk_coc;
            // Tính Qc
            double n = (double)dsld.LastOrDefault().n;
            double Qc = (400 * n * F) / 3;
            // Tính Qa
            double Qa,qa_chuacoheso = 0;
            foreach (var item in dsld)
            {
                qa_chuacoheso += (double)item.chieuday * (double)item.n;
            }
            Qa = (qa_chuacoheso * u * 2)/3; // 3 là hệ số.


            return Math.Round( (Qa+ Qc),3).ToString() + "KN" ;
        }
    }
}