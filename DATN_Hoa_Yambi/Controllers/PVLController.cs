using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace DATN_Hoa_Yambi.Controllers
{
    public class PVLController : Controller
    {
        // GET: PVL
        public ActionResult Index()
        {
            if (Session["User"] != null )
            {           
                    return View();                    
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public string getPVL()
        {
            if (Session["User"] != null) // nếu có User thì mới bắt đầu tính toán
            {
                string macoc = Request["id_dm"];
                VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext(); // kết nối 
                if (macoc != null)
                {
                    Session["MaCoc"] = macoc;
                    var qr = (from p in db.tbl_Users where p.MaNguoiDung == Session["User"].ToString() select p).FirstOrDefault(); // lấy ra người Dùng                                                                                                                     // lấy ra cọc
                    qr.MaCoc = macoc; // gán mã cọc của người dùng bằng mã cọc mới.
                    db.SubmitChanges(); // lưu lại
                }

                var qr_macoc = db.tbl_Users.Where(o => o.MaNguoiDung == Session["User"].ToString()).SingleOrDefault();
                var query = from coc in db.tbl_cocs
                            join bt in db.tbl_betongs
                            on coc.id_betong equals bt.id_betong
                            where coc.MaCoc == qr_macoc.MaCoc
                            select new { Coc = coc, Bt = bt };
                CalulatorSPT();
                CalulatorCPT();

                return JsonConvert.SerializeObject(query.SingleOrDefault());
            }
            else
            {
                return "500";
            }
           
            
        }

        public void CalulatorSPT()
        {

            List<tbl_lop_dat> dsld = new List<tbl_lop_dat>();
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            // đầu cọc cách mắt đất lấy tạm 1m;        
            double Qa = 0;
            double Qs = 0;
            var qr = db.tbl_lop_dats;
            if (qr.Any())
            {
                foreach (tbl_lop_dat item in qr)
                {
                    dsld.Add(item); // thêm vào danh sách lớp đất;
                }
            }

            // Tính cao độ cho các lớp đất
            // cao độ 0 là đầu lớp đất 1
            // Cao độ 1 là cuối lớp đất 1 và đầu lớp đất 2
            // Cao độ 2 tính từ 0 cho đến cuối lớp đất 2 nền bằng lớp đất 1 cộng lớp đất 2, tương tự
            List<double> Caodo = new List<double>();
            Caodo.Add(0);
            for (int i = 0; i <= dsld.Count - 1; i++)
            {
                double tong = 0;
                for (int j = 0; j <= i; j++)
                {
                    tong = (tong + (double)dsld[j].chieuday);
                }
                Caodo.Add(tong);
            }


            // Lấy ra người dùng.

            var qr_us = db.tbl_Users.Where(NA => NA.MaNguoiDung == Session["User"].ToString()).SingleOrDefault();
            // lấy ra được cọc của người đó đã chọn
            var qr_coc = db.tbl_cocs.Where(NA => NA.MaCoc == qr_us.MaCoc).SingleOrDefault();
            // xem cọc nằm ở lớp đất nào ? 
            int lopdat_index = 0;
            for (int i = 0; i < Caodo.Count - 1; i++)
            {
                if (double.Parse(qr_coc.DoDai) > Caodo[i] && double.Parse(qr_coc.DoDai) < Caodo[i + 1])
                {
                    lopdat_index = i + 1;
                }
            }
            // Tính chu vi của cọc:
            double u = (double)qr_coc.ChuVi_TD;
            // Tính diện tích của cọc :
            double F = (double)qr_coc.DienTich_TD;
            #region "Tính Qs"
            double[] qa = new double[10];
            for (int i = 0; i < lopdat_index; i++)
            {
                if (i == 0)
                {
                    // đoạn ngàm vào đài la 1m ,nên không thể lấy trọn vẹn lớp 1
                    qa[i] = ((double)dsld[i].chieuday - 1) * (double)dsld[i].n * 2 * u; // 2 là hệ số; u là chu vi
                    qa[i] = qa[i] / 3;  // 3 là hệ số mình lấy
                }
                else if (i == lopdat_index - 1)
                {
                    // Tính đoạn ngàm vào lớp đất cuối : chiều dài cọc + đoạn ngàm rồi trừ đi cao độ lớp trước 
                    double lngam_end = 1 + double.Parse(qr_coc.DoDai) - Caodo[lopdat_index - 1];
                    qa[i] = lngam_end * (double)dsld[i].n * 2 * u;
                    qa[i] = qa[i] / 3;
                }
                else
                {
                    qa[i] = ((double)dsld[i].chieuday) * (double)dsld[i].n * 2 * u;
                    qa[i] = qa[i] / 3;
                }

                Qs = Qs + qa[i];
            }
            #endregion
            #region "Tính Qc"
      

            // Tính Qc
            double n = (double)dsld.LastOrDefault().n; // Lấy ra lớp đất cuối cùng trong danh sách
            double Qc = (400 * n * F) / 3; // chuyển đôi sức kháng mũi sang sức kháng ma sát.
                                           // Tính Qa
            #endregion
            #region "Tính Qa"
            Qa = Math.Round((Qs + Qc), 3);
            #endregion   // Qa là Giá trị SPT mình Tính ra
            // Cập nhật Qa vào dữ liệu của người dùng 
            qr_us.Spt = Qa;
            db.SubmitChanges();
        }

        public void CalulatorCPT()
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
                        Li[k] = (double)dsld[k].chieuday - 1;
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

            Qs = (double)(pr_last.k * pr_last.suckhangxuyen) * (double)qr_coc.DuongKinhCoc * (double)qr_coc.DuongKinhCoc;
            Qa = (Qms + Qs) / 2;

            // lưu dữ liệu
            qr_user.Cpt = Qa;
            db.SubmitChanges();
        }
    }
}