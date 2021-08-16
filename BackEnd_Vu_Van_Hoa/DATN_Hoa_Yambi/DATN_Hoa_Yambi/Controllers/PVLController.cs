using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATN_Hoa_Yambi.Controllers
{
    public class PVLController : Controller
    {
        // GET: PVL
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string getPVL()
        {
            try
            {
                return database.Pvl.ToString();
            }
            catch (Exception)
            {

                return "0";
            }
        }

    }
}