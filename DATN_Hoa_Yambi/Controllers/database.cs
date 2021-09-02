using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATN_Hoa_Yambi.Models;

namespace DATN_Hoa_Yambi.Controllers
{
    public class database
    {
        public static tbl_betong betong_coc = new tbl_betong();
        public static tbl_betong betong_dai = new tbl_betong();

        public static tbl_cot_thep cotthep_dai = new tbl_cot_thep();
        public static tbl_cot_thep cotthep_coc = new tbl_cot_thep();
        // Dữ liệu của cọc:
        public static Cls_coc cls_Coc = new Cls_coc();
        public static double As_coc;
        public static double Pvl;
        public static double SPT;
        
    }

}