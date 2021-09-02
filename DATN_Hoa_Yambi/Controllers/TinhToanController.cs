
// Tính Thép thep phương cạnh dài vẫn chưa chuẩn, cần sửa lại
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATN_Hoa_Yambi.Models;
using Newtonsoft.Json;

namespace DATN_Hoa_Yambi.Controllers
{

  
    public class TinhToanController : Controller
    {
        public string JsonConvet { get; private set; }

        // GET: TinhToan
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string getALlSucCHiuTai()
        {
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            ketquasucchiutai result = new ketquasucchiutai();
            // Get Pvl :  
            if (Session["User"] != null)
            {
                var qr_user = db.tbl_Users.Where(o => o.MaNguoiDung == Session["User"].ToString()).FirstOrDefault();
                var qr_coc = db.tbl_cocs.Where(o => o.MaCoc == qr_user.MaCoc).SingleOrDefault();
                result.Pvl = (double)qr_coc.SucChiuTai;
                //Set Pcpt :
                result.Pcpt = (double)qr_user.Cpt;
                //set Spt :
                result.Pspt = (double)qr_user.Spt;
                //set Ntc
                result.Ntc = (double)qr_user.No;

                return JsonConvert.SerializeObject(result);
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string KiemTraDai()
        {
            int sc = int.Parse(Request["sococ"]);
            double kc = double.Parse(Request["kkc_2_coc"]);
            double sct = double.Parse(Request["succhiutai"]);
            double c1 = 0;  // là hệ số để tính đâm thủng và chọc thỉnh
            double c2 = 0; // là hệ số để tính đâm thủng và chọc thủng 
            double Pdt = 0;
            double Pcdt = 0;
            double Pct = 0; // chọc thủng
            double Pcct = 0; // chống chọc thủng

            double p1, p2, p3, p4,p5,p6, Rk;
            double alpha1, alpha2;
            double M_canhdai, r_canhdai;
            double M_canhngan, r_canhngan;
            double As_canhdai = 0;
            double As_canhngan = 0;

            Ketquakichthuocdai ketquakichthuocdai = new Ketquakichthuocdai();
            ketquakichthuocdai.Sococ = sc; // get so coc
            // Lấy ra được đường kính của cọc: 
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr_user = db.tbl_Users.Where(o => o.MaNguoiDung == Session["User"].ToString()).SingleOrDefault();
            var qr_coc = db.tbl_cocs.Where(o => o.MaCoc == qr_user.MaCoc).SingleOrDefault();
            var qr_BT = db.tbl_betongs.Where(o => o.id_betong == qr_coc.id_betong).SingleOrDefault();
            var qr_CotThep = db.tbl_cot_theps.Where(o => o.IDCotThep == qr_coc.IDCotThep).SingleOrDefault();

            double L = 0; ;
            double haft_l = 0;
            double B = 0;
            double dk_coc = (double)qr_coc.DuongKinhCoc;
            double Ntc = (double)qr_user.No;
            double Mtc = (double)qr_user.Mo;

            double b_cot =(double)qr_user.b_cot;
            double l_cot = (double)qr_user.h_cot;
            double h_dai = (double)qr_user.h_dai;
            double h_o = (double)h_dai - 0.1;


                switch (sc)
            {
                case 8:
                    haft_l = (kc * dk_coc) * Math.Sin(Math.PI / 4);
                    L = 4 * haft_l + 2 * dk_coc;
                    L = Math.Ceiling(L * 10) / 10; // làm tròn
                    B = 2 * haft_l;

                    B = Math.Ceiling(B * 10) / 10;
                    L = Math.Ceiling(L * 10) / 10;

                    break;
                case 7:
                    // Trường hợp 7 cọc
                    haft_l = kc * (double)qr_coc.DuongKinhCoc + (double)qr_coc.DuongKinhCoc;
                    L = 2 * haft_l;
                    L = Math.Ceiling(L * 10) / 10;
                    B = L;
                    break;
                case 6: // Trường hợp là 6 cọc:
                    haft_l = kc * (double)qr_coc.DuongKinhCoc + (double)qr_coc.DuongKinhCoc;
                    L = 2 * haft_l;
                    B = (double)qr_coc.DuongKinhCoc * kc + 2 * (double)qr_coc.DuongKinhCoc;
                    B = Math.Ceiling(B * 10) / 10;

                    // 
                    double _6_x1 =Math.Round(( -kc * dk_coc),2); // x1 = x3
                    double _6_x2 = 0;
                    double _6_x3 =Math.Round((kc * dk_coc),2) ;
                    double _6_pow_xi = (Math.Pow(_6_x1, 2) + Math.Pow(_6_x2, 2) + Math.Pow(_6_x3, 2))*2;
                    double y1, y6;
                    y1 =Math.Round(((kc * dk_coc) / 2),2);
                    y6 = -Math.Round(((kc * dk_coc) / 2), 2);
                    ketquakichthuocdai.dsCoc.Clear();
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 1",
                        xi = _6_x1,
                        yi = y1,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _6_x1 / _6_pow_xi), 3)
                    }); ;

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 2",
                        xi = _6_x2,
                        yi=y1,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _6_x2 / _6_pow_xi),3)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    
                    {
                        Name = "Cọc thứ 3",
                        xi = _6_x3,
                        yi =y1,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _6_x3 / _6_pow_xi),3)
                    });

                    // 456 khác chút là y thôi
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 4",
                        xi = _6_x1,
                        yi=y6,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _6_x1 / _6_pow_xi),3)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 5",
                        xi = _6_x2,
                        yi = y6,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _6_x2 / _6_pow_xi),3)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 6",
                        xi = _6_x3,
                        yi = y6,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _6_x3 / _6_pow_xi),3)
                    });


                    // ----Tính C1 và C2  :
                    c1 = ketquakichthuocdai.dsCoc[5].xi - l_cot / 2 - dk_coc / 2;
                    c2 = ketquakichthuocdai.dsCoc[5].yi - b_cot / 2 - dk_coc / 2;

                    // ----kiểm tra c1,c2
                    if (c1 < (0.5 * h_o))
                    {
                        c1 = 0.5 * h_o;
                    }
                    if (c2 < (0.5 * h_o))
                    {
                        c2 = 0.5 * h_o;
                    }
                    // ----Tính Pdt :-----
                    p1 = ketquakichthuocdai.dsCoc[0].Pi;
                    p2 = ketquakichthuocdai.dsCoc[1].Pi;
                    p3 = ketquakichthuocdai.dsCoc[2].Pi;
                    p4 = ketquakichthuocdai.dsCoc[3].Pi;
                    p5 = ketquakichthuocdai.dsCoc[4].Pi;
                    p6 = ketquakichthuocdai.dsCoc[5].Pi;
                    Pdt = p1 + p2 + p3 + p4 + p5 + p6;

                    alpha1 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c1), 2));
                    alpha2 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c2), 2));
                    Rk = (double)qr_BT.rbt * 1000;
                    Pcdt = Rk * h_o * (alpha1 * (b_cot + c2) + alpha2 * (l_cot + c1));
                    if (Pcdt > Pdt)
                    {
                        ketquakichthuocdai.Damthung = "1"; // đảm bảo
                    }
                    else
                    {
                        ketquakichthuocdai.Damthung = "0"; // không đảm bảo
                    }

                    #region "Kiểm Tra cọc đâm thủng đài"
                    Pct = p3 + p6;
                    Pcct = 1.5 * Rk * h_o * B;
                    if (Pct > Pcct)
                    {
                        ketquakichthuocdai.Chocthung = "0"; // tức là bị chọc thủng
                    }
                    else
                    {
                        ketquakichthuocdai.Chocthung = "1"; // tức là không bị chọc thủng
                    }

                    #endregion
                    #region "Tính thép cho đài theo phương cạnh dài cho trường hợp 6 cọc"
                    double x6 = ketquakichthuocdai.dsCoc[5].xi;
                    r_canhdai = x6 - (double)qr_user.h_cot / 2;
                    M_canhdai = r_canhdai * (p6 + p3);
                    As_canhdai = M_canhdai / (0.9 * (double)qr_CotThep.Rs * 1000 * h_o) * 10000; // nhân với 10000 để đổi sang cm2
                    ketquakichthuocdai.ThepCanhDai =Math.Round (As_canhdai,3);
                    #endregion
                    #region  "Tính thép đài theo phương cạnh ngắn"
                    double y_3 = ketquakichthuocdai.dsCoc[2].yi;
                    r_canhngan = y_3 - (double)qr_user.b_cot / 2;
                    M_canhngan = r_canhngan * (p1 + p2 + p3);
                    As_canhngan = M_canhngan / (0.9 * (double)qr_CotThep.Rs * 1000 * h_o) * 10000;
                    ketquakichthuocdai.ThepCanhNgan =Math.Round(As_canhngan,3);

                    #endregion
                    break;
                case 5:
                    // Trường hợp là 5 cọc:
                     haft_l = (kc * dk_coc) * Math.Sin(Math.PI / 4);
                    L = 2 * haft_l + 2 * dk_coc;
                    L = Math.Ceiling(L * 10) / 10; // làm tròn
                    B = L;
                    double _5_x1 = -haft_l;
                    double _5_x2 = haft_l;
                    double _5_x5 = 0;
                    double _5_pow_x5 = (Math.Pow((_5_x1), 2) + Math.Pow((_5_x2), 2) + Math.Pow((_5_x5), 2))*2;
                    double _4_y1;
                    double _4_y2;
                    double _4_y3;
                    double _4_y4;
                    _4_y1 = Math.Round(((kc * dk_coc) / 2), 2);
                    _4_y2 = Math.Round(((kc * dk_coc) / 2), 2);
                    _4_y3 = -_4_y1;
                    _4_y4 = -_4_y1;

                    ketquakichthuocdai.dsCoc.Clear();
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 1",
                        xi = _5_x1,
                        yi = _4_y1,
                        Pi =Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _5_x1 / _5_pow_x5),3)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 2",
                        xi = _5_x2,
                        yi = _4_y2,
                        Pi =Math.Round( ((Ntc * 10) / sc) + ((Mtc * 10) * _5_x2 / _5_pow_x5),2)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 3",
                        xi = _5_x2,
                        yi = _4_y3,
                        Pi =Math.Round (((Ntc * 10) / sc) + ((Mtc * 10) * _5_x2 / _5_pow_x5),2)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 4",
                        xi = _5_x1,
                        yi = _4_y4,
                        Pi =Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _5_x1 / _5_pow_x5),2)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 5 (cọc ở giữa)",
                        xi = _5_x5,
                        yi =0,
                        Pi =Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * _5_x5 / _5_pow_x5),2)
                    });
                    #region "Kiểm tra đâm thủng"

                    #endregion
                    // ----Tính C1 và C2  :
                    c1 = ketquakichthuocdai.dsCoc[4].xi - l_cot / 2 - dk_coc / 2;
                    c2 = ketquakichthuocdai.dsCoc[4].yi - b_cot / 2 - dk_coc / 2;

                    // ----kiểm tra c1,c2
                    if (c1 < (0.5 * h_o))
                    {
                        c1 = 0.5 * h_o;
                    }
                    if (c2 < (0.5 * h_o))
                    {
                        c2 = 0.5 * h_o;
                    }
                    // ----Tính Pdt :-----
                    p1 = ketquakichthuocdai.dsCoc[0].Pi;
                    p2 = ketquakichthuocdai.dsCoc[1].Pi;
                    p3 = ketquakichthuocdai.dsCoc[2].Pi;
                    p4 = ketquakichthuocdai.dsCoc[3].Pi;
                    Pdt = p1 + p2 + p3 + p4;

                    alpha1 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c1), 2));
                    alpha2 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c2), 2));
                    Rk = (double)qr_BT.rbt * 1000;
                    Pcdt = Rk * h_o * (alpha1 * (b_cot + c2) + alpha2 * (l_cot + c1));
                    if (Pcdt > Pdt)
                    {
                        ketquakichthuocdai.Damthung = "1"; // đảm bảo
                    }
                    else
                    {
                        ketquakichthuocdai.Damthung = "0"; // không đảm bảo
                    }

                    #region "Kiểm tra cọc đâm thủng đài cho trường hợp 5 cọc :"
                    Pct = p3 + p4;
                    Pcct = 1.5 * Rk * h_o * B;
                    if (Pct > Pcct)
                    {
                        ketquakichthuocdai.Chocthung = "0"; // tức là bị chọc thủng
                    }
                    else
                    {
                        ketquakichthuocdai.Chocthung = "1"; // tức là không bị chọc thủng
                    }

                    #endregion
                    break;
                case 4:
                    // Trường hợp là 4 cọc
                    #region "TÍnh kích thước của đài :"
                    haft_l = (kc * dk_coc) + 2 * dk_coc;
                    L = haft_l;
                    L = Math.Ceiling(L * 10) / 10;
                    B = L;
                    #endregion

                    #region "TÍnh pi cho trường hợp 4 cọc"
                    ketquakichthuocdai.dsCoc.Clear(); // xóa trắng danh sách trước
                    // Tỉnh tổng Xi2 : 
                    double pow2_xi4 = Math.Pow(((kc * dk_coc) / 2), 2) * 4;
                    double x1 = -Math.Round(((kc * dk_coc) / 2), 2);
                    double x2 = Math.Round(((kc * dk_coc) / 2), 2);

                    _4_y1 = Math.Round(((kc * dk_coc) / 2), 2);
                    _4_y2 = Math.Round(((kc * dk_coc) / 2), 2);
                    _4_y3 = -_4_y1;
                    _4_y4 = -_4_y1;
                    // Thêm cọc 1 
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 1",
                        xi = x1,
                        yi = _4_y1,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10)*x1 / pow2_xi4),3)
                    });
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 2",
                        xi = x1,
                        yi = _4_y2,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * x1 / pow2_xi4),3)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 3",
                        xi = x2,
                        yi = _4_y3,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * x2 / pow2_xi4),3)
                    });

                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc thứ 4",
                        xi = x2,
                        yi = _4_y4,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) * x2 / pow2_xi4),3)
                    });
                    #endregion
                    // ----Tính C1 và C2  :
                    c1 = ketquakichthuocdai.dsCoc[2].xi - l_cot / 2 - dk_coc / 2;
                    c2 = ketquakichthuocdai.dsCoc[2].yi-b_cot/2 -dk_coc/2;

                    // ----kiểm tra c1,c2
                    if (c1 < (0.5 * h_o))
                    {
                        c1 = 0.5 * h_o;
                    }
                    if (c2 < (0.5 * h_o))
                    {
                        c2 = 0.5 * h_o;
                    }
                    // ----Tính Pdt :-----
                   p1 = ketquakichthuocdai.dsCoc[0].Pi;
                   p2 = ketquakichthuocdai.dsCoc[1].Pi;
                   p3 = ketquakichthuocdai.dsCoc[2].Pi;
                   p4 = ketquakichthuocdai.dsCoc[3].Pi;
                   Pdt = p1 + p2 + p3 + p4;

                   alpha1 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c1), 2));
                   alpha2 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c2), 2));
                   Rk = (double)qr_BT.rbt * 1000;
                   Pcdt = Rk * h_o * (alpha1 * (b_cot + c2) + alpha2 * (l_cot + c1));
                    if (Pcdt > Pdt)
                    {
                        ketquakichthuocdai.Damthung = "1"; // đảm bảo
                    }
                    else
                    {
                        ketquakichthuocdai.Damthung = "0"; // không đảm bảo
                    }
                    #region "Kiểm tra cọc đâm thủng đài cho trường hợp 4 cọc :"
                    Pct = p3 + p4;
                    Pcct = 1.5 * Rk * h_o * B;
                    if (Pct > Pcct)
                    {
                        ketquakichthuocdai.Chocthung = "0"; // tức là bị chọc thủng
                    }
                    else
                    {
                        ketquakichthuocdai.Chocthung = "1"; // tức là không bị chọc thủng
                    }

                    #endregion

                    #region "Tính thép cho đài theo phương cạnh dài cho trường hợp 4 cọc"
                    double x4 = ketquakichthuocdai.dsCoc[3].xi;
                    r_canhdai = x4 -(double)qr_user.h_cot / 2;
                    M_canhdai = r_canhdai * (p4 + p3);
                    As_canhdai = M_canhdai / (0.9 * (double)qr_CotThep.Rs*1000 * h_o)*10000; // nhân với 10000 để đổi sang cm2
                    ketquakichthuocdai.ThepCanhDai =Math.Round(As_canhdai,3);

                    #endregion
                    #region "Tính Thép cho phương cạnh ngắn trường hợp 4 cọc"
                    double y2 = ketquakichthuocdai.dsCoc[1].yi; // cọc thứ 3
                    r_canhngan =  y2- (double)qr_user.b_cot / 2;
                    M_canhngan = r_canhngan * (p3 + p2);
                    As_canhngan = M_canhngan / (0.9 * (double)qr_CotThep.Rs * 1000 * h_o) * 10000; // nhân với 10000 để đổi sang cm2

                    ketquakichthuocdai.ThepCanhNgan = Math.Round(As_canhngan);
                    #endregion
                    break;
                  
                case 3: // Trường hợp 3 cọc :
                    haft_l = kc * dk_coc + dk_coc;
                    L = haft_l;
                    B = haft_l;
                    // trường hợp này là trường hợp đặc biệt
                    break;
                case 2:
                    #region "Bố trí cọc"
                    haft_l = (kc * dk_coc) + dk_coc * dk_coc;
                    L = haft_l;
                    B = 2 * dk_coc;
                    L = Math.Ceiling(L * 10) / 10;
                    B = Math.Ceiling(B * 10) / 10;
                    // Thêm vào danh sách : 
                    ketquakichthuocdai.dsCoc.Clear(); // xóa trắng danh sách trước
                    // Tỉnh tổng Xi2 : 
                    double pow2_xi = Math.Pow(((kc * dk_coc) / 2), 2) * 2;
                    // Thêm cọc 1 
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc 1",
                        xi = -Math.Round(((kc * dk_coc) / 2), 2),
                        yi = 0,
                        Pi = Math.Round(((Ntc * 10) / sc) - ((Mtc * 10) / pow2_xi), 3)
                    });
                    // Thêm cọc 2 : 
                    ketquakichthuocdai.dsCoc.Add(new ketquabotritungcoc
                    {
                        Name = "Cọc 2",
                        xi = Math.Round(((kc * dk_coc) / 2), 2),
                        yi = 0,
                        Pi = Math.Round(((Ntc * 10) / sc) + ((Mtc * 10) / pow2_xi), 3)
                    });
                    #endregion
                    #region "kiểm tra đâm thủng đài "
                    // ----Tính C1 và C2  :
                    c1 = ketquakichthuocdai.dsCoc[1].xi - l_cot / 2 - dk_coc / 2;
                    c2 = 0;
                    // ----kiểm tra c1,c2
                    if (c1 < (0.5 * h_o))
                    {
                        c1 = 0.5 * h_o;
                    }
                    if (c2 < (0.5 * h_o))
                    {
                        c2 = 0.5 * h_o;
                    }
                    // ----Tính Pdt :-----
                    p1 = ketquakichthuocdai.dsCoc[0].Pi;
                    p2 = ketquakichthuocdai.dsCoc[1].Pi;
                    Pdt = p1 + p2;
                    alpha1 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c1), 2));
                    alpha2 = 1.5 * Math.Sqrt(1 + Math.Pow((h_o / c2), 2));
                    Rk = (double)qr_BT.rbt * 1000;
                    Pcdt = Rk * h_o * (alpha1 * (b_cot + c2) + alpha2 * (l_cot + c1));
                    if (Pcdt > Pdt)
                    {
                        ketquakichthuocdai.Damthung = "1"; // đảm bảo
                    }
                    else
                    {
                        ketquakichthuocdai.Damthung = "0"; // không đảm bảo
                    }
                    #endregion
                    #region "Kiểm tra cọc đâm thủng đài"
                    Pct = ketquakichthuocdai.dsCoc[1].Pi;
                    Pcct = 1.5 * Rk * h_o * B;
                    if (Pct>Pcct)
                    {
                        ketquakichthuocdai.Chocthung = "0"; // tức là bị chọc thủng
                    }
                    else
                    {
                        ketquakichthuocdai.Chocthung = "1"; // tức là không bị chọc thủng
                    }
                    

                    
                    #endregion
                    break;
                default:
                    break;
            }
       
            ketquakichthuocdai.Ldai = L;
            ketquakichthuocdai.Bdai = B;

            // Tính trọng lượng của cọc :
            double Gc = 25 * dk_coc * dk_coc *double.Parse(qr_coc.DoDai);
            ketquakichthuocdai.Gc = Gc;
            ketquakichthuocdai.TinhPomax();
            ketquakichthuocdai.kiemtra(sct);
            qr_user.Ldai = L;
            qr_user.Bdai = B;
            db.SubmitChanges();

            Session["Ketqua"] = JsonConvert.SerializeObject(ketquakichthuocdai);
            return Session["ketqua"].ToString();
        }
       
    }
    
}