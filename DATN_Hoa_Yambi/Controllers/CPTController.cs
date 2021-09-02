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
        public string getCPT()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            List<tbl_lop_dat> dsld = new List<tbl_lop_dat>();
            List<double> Caodo = new List<double>();

            var pr_last = db.tbl_lop_dats.ToList().LastOrDefault();
            var qr = db.tbl_lop_dats;
            var qr_user = db.tbl_Users.Where(o => o.MaNguoiDung == Session["User"].ToString()).FirstOrDefault();
            var qr_coc = db.tbl_cocs.Where(o => o.MaCoc == qr_user.MaCoc).SingleOrDefault();

            double[] Ti = new double[10];
            double[] Li = new double[10]; //m
            double u; //m
            double Qms = 0, Qs = 0;
            double Qa = 0;
         
            dsld = qr.ToList();


         


       
           
          
            if (qr.Any())
            {
                //int i = 0;
                //foreach (tbl_lop_dat item in qr)
                //{
                //    // Sức kháng xuyên bằng Qc/ alpha!
                //    Ti[i] = (double)(item.suckhangxuyen / item.alpha);
                //    // Tính Chiều dày lớp đất Li : 
                //    Li[i] = (double)item.chieuday;
                //    // Chuvi : 
                //    u =(double)qr_coc.DuongKinhCoc * 4; // đơn vị là m
                //    // TÍnh Qms : 
                //     Qms += Ti[i] * Li[i] * u;                      
                //    i++;
                   
                // }




              
                Caodo.Add(0);
                for (int k = 0; k <= dsld.Count - 1; k++)
                {
                    double tong = 0;
                    for (int j = 0; j <= k; j++)
                    {
                        tong = (tong + (double)dsld[j].chieuday);
                    }
                    Caodo.Add(tong);
                }

                int lopdat_index = 0;
                for (int i = 0; i < Caodo.Count - 1; i++)
                {
                    if (double.Parse(qr_coc.DoDai) > Caodo[i] && double.Parse(qr_coc.DoDai) < Caodo[i + 1])
                    {
                        lopdat_index = i + 1;
                    }
                }

                for (int k = 0; k < lopdat_index; k++)
                {
                    if (k == 0)
                    {
                        // đoạn ngàm vào đài la 1m ,nên không thể lấy trọn vẹn lớp 1
                        Ti[k] = (double)(dsld[k].suckhangxuyen / dsld[k].alpha);
                        // Tính Chiều dày lớp đất Li : 
                        Li[k] = (double)dsld[k].chieuday-1;
                        // Chuvi : 
                        u = (double)qr_coc.DuongKinhCoc * 4; // đơn vị là m
                                                         
                        Qms += Ti[k] * Li[k] * u;
                    }
                    else if (k == lopdat_index - 1)
                    {
                        // Tính đoạn ngàm vào lớp đất cuối : chiều dài cọc + đoạn ngàm rồi trừ đi cao độ lớp trước 
                        double lngam_end = 1 + double.Parse(qr_coc.DoDai) - Caodo[lopdat_index - 1];
                        // đoạn ngàm vào đài la 1m ,nên không thể lấy trọn vẹn lớp 1
                        Ti[k] = (double)(dsld[k].suckhangxuyen / dsld[k].alpha);                       
                        u = (double)qr_coc.DuongKinhCoc * 4; // đơn vị là m
                        Qms += Ti[k] * lngam_end * u;
                    }
                    else
                    {
                        Ti[k] = (double)(dsld[k].suckhangxuyen / dsld[k].alpha);
                        // Tính Chiều dày lớp đất Li : 
                        Li[k] = (double)dsld[k].chieuday;
                        // Chuvi : 
                        u = (double)qr_coc.DuongKinhCoc * 4; // đơn vị là m

                        Qms += Ti[k] * Li[k] * u;
                    }        
                }

            }
                
            Qs =(double) (pr_last.k * pr_last.suckhangxuyen)* (double)qr_coc.DuongKinhCoc * (double)qr_coc.DuongKinhCoc;
            Qa = (Qms + Qs) / 2;

            // lưu dữ liệu
            qr_user.Cpt = Qa;
            db.SubmitChanges();
            return Qa.ToString();
        }

       
    }
}