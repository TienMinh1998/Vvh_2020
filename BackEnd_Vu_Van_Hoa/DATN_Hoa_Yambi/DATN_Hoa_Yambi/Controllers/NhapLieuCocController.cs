using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class NhapLieuCocController : Controller
    {
        // GET: NhapLieuCoc
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string NhapLieuCoc()
        {
            double length_coc = double.Parse(Request["txt_chieu_dai_coc"]);
            double dk_coc = double.Parse(Request["txt_duong_kinh_coc"]);
            double doanngam = double.Parse(Request["txt_doan_ngam_vao_dai"]);
            double sothanh = double.Parse(Request["txt_so_thanh_thep"]);
            double dk_thep = double.Parse(Request["txt_duong_kinh"]);

            dk_thep = dk_thep / 10; // đổi đơn vị ra cm
            // Tính As coc : 
            double As_coc = (Math.PI * Math.Pow(dk_thep, 2)) / 4 * sothanh;
            // Tính Fa <Diện tích tiết diện cọc:>
            double Fa_coc = Math.Pow(dk_coc, 2) * 10000; // đổitừ m2 ra cm2
            double? Pvl = 0.9 * (Fa_coc * database.betong_coc.rb / 10 + As_coc * database.cotthep_coc.Rsc / 10);
            Pvl = Math.Round((double)Pvl);
            database.Pvl = (double)Pvl;
            // Gán giá trị choc cọc
            database.cls_Coc.dk_coc = dk_coc;
            database.cls_Coc.dk_thep = dk_thep;
            database.cls_Coc.chieudai = length_coc;
            database.cls_Coc.sothanhthep = sothanh;
            database.cls_Coc.ngam_vao_dai = doanngam;
            
            return As_coc.ToString() + " Cm2" + " diện tích cọc : " + Fa_coc.ToString() + "Cm2" + "Sức Chịu tải theo vật liệu: " + Pvl.ToString();
        }
    }
}