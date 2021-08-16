using DATN_Hoa_Yambi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VuVanHoa.Unites
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Lấy được lớp đất ra đã
            List<tbl_lop_dat> dsld = new List<tbl_lop_dat>();
            VuVanHoa_DatabaseDataContext db = new VuVanHoa_DatabaseDataContext();
            var qr = db.tbl_lop_dats;
            if (qr.Any())
            {

            }
            // Tính được Tô_i đã

         
        }
    }
}
