using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class CPTController : Controller
    {
        // GET: CPT
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string getCPT()
        {
            // Lấy được lớp đất ra đã
            List<tbl_lop_dat> dsld = new List<tbl_lop_dat>();
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr = db.tbl_lop_dats;

            double[] Ti = new double[10];
            double[] Li = new double[10]; //m
            double u; //m
            double Qms = 0, Qs = 0;
            double Qa = 0;
            if (qr.Any())
            {
                int i = 0;
                foreach (tbl_lop_dat item in qr)
                {
                    // Sức kháng xuyên bằng Qc/ alpha!
                    Ti[i] = (double)(item.suckhangxuyen / item.alpha);
                    // Tính Chiều dày lớp đất Li : 
                    Li[i] = (double)item.chieuday;
                    // Chuvi : 
                    u = database.cls_Coc.dk_coc * 4; // đơn vị là m
                     Qms += Ti[i] * Li[i] * u;


                    if (item == qr.LastOrDefault())
                    {
                        Qs = (double)(item.k * item.suckhangxuyen);
                    }
                    Qa = (Qms + Qs) / 2.5;
                    i++;
                   
                 }
             
            }


            return Qa.ToString();

        }

       
    }
}