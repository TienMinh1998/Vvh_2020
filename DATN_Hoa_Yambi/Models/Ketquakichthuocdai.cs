using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_Hoa_Yambi.Models
{
    /// <summary>
    /// Lớp này viết ra để trả ra kết quả kích thước của đài, kiểm tra đâm thủng và chọc thủng của các trường hợp
    /// Trường hợp 3 cọc hơi khó nên đang làm
    /// Trường hợp 7 cọc và 8 cọc chưa làm.
    /// </summary>
    public class Ketquakichthuocdai
    {
        public int Sococ { get; set; }
        public double Ldai { get; set; }
        public double Bdai { get; set; }

        public List<ketquabotritungcoc> dsCoc = new List<ketquabotritungcoc>();
        public string Damthung { get; set; }
        public string Chocthung { get; set; }
        public double ThepCanhDai { get; set; }
        public double ThepCanhNgan { get; set; }
        public double Gc { get; set; }
        public double Pomax { get; set; }

        public string str_ketquakiemtra { get; set; }
        public void TinhPomax()
        {
           
            if (dsCoc.Count!=0)
            {
                double pmax = dsCoc[0].Pi;
                for (int i = 0; i < dsCoc.Count; i++) if (pmax < dsCoc[i].Pi) pmax = dsCoc[i].Pi;
                if (Gc!=0)
                {
                    Pomax = this.Gc + pmax;
                }
            }
        }

        public void kiemtra(double P_chophep)
        {
            if (this.Pomax<P_chophep)
            {
                str_ketquakiemtra = "1";
            }
            else
            {
                str_ketquakiemtra = "0";
            }
        }
    }
}