using Newtonsoft.Json;
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
        public string getSPT()
        {
          
            List<tbl_lop_dat> dsld = new List<tbl_lop_dat>();
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            // đầu cọc cách mắt đất lấy tạm 1m;
            double daucoccachmatdat = 1;
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
            for (int i = 0; i <= dsld.Count-1; i++)
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
            for (int i = 0; i < Caodo.Count-1; i++)
            {
                if (double.Parse(qr_coc.DoDai) > Caodo[i] && double.Parse(qr_coc.DoDai) < Caodo[i+1])
                {
                    lopdat_index = i+1;
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
            //switch (lopdat_index)
            //{
            //    case 1:
            //        Qa = Qa + ((double)dsld[0].chieuday - 1) * (double)dsld[0].n * u * 2/3;
            //        break;
            //    default:
            //        break;
            //}



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
            return Qa.ToString();              
        }

        [HttpPost]
        public string get_SPT_info()
        {
            // lấy ra người dùng 
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_us = db.tbl_Users.Where(NA => NA.MaNguoiDung == Session["User"].ToString()).SingleOrDefault();
            var qr_coc = db.tbl_cocs.Where(NA => NA.MaCoc == qr_us.MaCoc).SingleOrDefault();
            var qr_betong = db.tbl_betongs.Where(NA => NA.id_betong == qr_coc.id_betong).SingleOrDefault();

           return JsonConvert.SerializeObject(qr_betong);
        }
    }
}