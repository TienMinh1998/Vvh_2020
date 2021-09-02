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

            string macoc = Request["txt_ma_coc"];
            string tencoc = Request["txt_ten_coc"];
            double dodaicoc = double.Parse(Request["txt_do_dai_coc"]);
            double dk_coc = double.Parse(Request["txt_duong_kinh_coc"]);
            double sothanh = double.Parse(Request["txt_so_thanh_thep"]);
            double dk_thep = double.Parse(Request["txt_duong_kinh_thep_coc"]);
            int ID_betong = int.Parse(Request["cbb_betong"]);
            int ID_cotthep = int.Parse(Request["cbb_cotthep"]);

            dk_thep = dk_thep / 10; // đổi đơn vị ra cm
            // Tính As coc : 
            double As_coc = (Math.PI * Math.Pow(dk_thep, 2)) / 4 * sothanh;
            // Tính Fa <Diện tích tiết diện cọc:>
            double Fa_coc = Math.Pow(dk_coc, 2) * 10000; // đổitừ m2 ra cm2

            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            tbl_betong bt = db.tbl_betongs.Where(o => o.id_betong == ID_betong).FirstOrDefault();
            tbl_cot_thep ct = db.tbl_cot_theps.Where(x => x.IDCotThep == ID_cotthep).FirstOrDefault();


            double? Pvl = 0.9 * (Fa_coc * bt.rb / 10 + As_coc * ct.Rsc / 10); // chia 10 để đổi đơn vị sang cm
            Pvl = Math.Round((double)Pvl);
          
            return Pvl.ToString();
        }


        [HttpPost]
        public String AddNewCoc()
        {
            string macoc = Request["txt_ma_coc"];
            string tencoc = Request["txt_ten_coc"];
            double dodaicoc = double.Parse(Request["txt_do_dai_coc"]);
            double dk_coc = double.Parse(Request["txt_duong_kinh_coc"]);
            double sothanh = double.Parse(Request["txt_so_thanh_thep"]);
            double dk_thep = double.Parse(Request["txt_duong_kinh_thep_coc"]);
            int ID_betong = int.Parse(Request["cbb_betong"]);
            int ID_cotthep = int.Parse(Request["cbb_cotthep"]);

            dk_thep = dk_thep / 10; // đổi đơn vị ra cm
            // Tính As coc : 
            double As_coc = (Math.PI * Math.Pow(dk_thep, 2)) / 4 * sothanh;
            // Tính Fa <Diện tích tiết diện cọc:>
            double Fa_coc = Math.Pow(dk_coc, 2) * 10000; // đổitừ m2 ra cm2
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            tbl_betong bt = db.tbl_betongs.Where(o => o.id_betong == ID_betong).FirstOrDefault();
            tbl_cot_thep ct = db.tbl_cot_theps.Where(x => x.IDCotThep == ID_cotthep).FirstOrDefault();
            double? Pvl = 0.9 * (Fa_coc * bt.rb / 10 + As_coc * ct.Rsc / 10); // chia 10 để đổi đơn vị sang cm
            Pvl = Math.Round((double)Pvl);
            // Thêm cọc vào cơ sở DỮ Liệu
            tbl_coc new_coc = new tbl_coc();
            new_coc.MaCoc = macoc;
            new_coc.TenCoc = tencoc;
            new_coc.DoDai = dodaicoc.ToString();
            new_coc.DuongKinhCoc = dk_coc;
            new_coc.SoThanhThep = (byte)sothanh;
            new_coc.DuongKinhThep =(byte) dk_thep;
            new_coc.id_betong = ID_betong;
            new_coc.IDCotThep = ID_cotthep;
            new_coc.SucChiuTai = Pvl;
            new_coc.is_deleted = 0;
            new_coc.DienTich_TD = Fa_coc/10000; // chia 10000 để đổi ra đơn vị mét
            new_coc.ChuVi_TD = dk_coc * 4;




            db.tbl_cocs.InsertOnSubmit(new_coc);
            db.SubmitChanges();



            return "Thêm Cọc Thành Công";
        }

  
    }
}