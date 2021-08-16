using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Service.Controllers
{
    public class APIDiaChatController : ApiController
    {
        /// <summary>
        /// Trả ra danh sách các lớp đất
        /// </summary>
        /// <returns></returns>
        // GET: api/APIDiaChat
        [Route("GetListDiaChat")]
        [HttpPost]
        public string Get()
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var qr = db.tbl_lop_dats.Where(o => o.is_delete == 0);
            if (qr.Any())
            {
                return JsonConvert.SerializeObject(qr.ToList());
            }
            return " ";
        }

        // GET: api/APIDiaChat/5
        [Route("NguyenMInhTien")]
        [HttpGet]
        public string getstring()
        {
            return "value";
        }

        // POST: api/APIDiaChat
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/APIDiaChat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/APIDiaChat/5
        public void Delete(int id)
        {
        }
    }
}
