using DATN_Hoa_Yambi.Controllers;
using DATN_Hoa_Yambi;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitest.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void ConnetDatabase()
        {
            CPTController cpt = new CPTController();
            cpt.TinhTi();

        }    
    }
}
