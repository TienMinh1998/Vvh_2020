using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Service.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        [Route("getlistdanhsachdiachat")]
        public string Get()
        {
            return "NGuyễn Viết MInh Tiến Đẹp Trai";
        }

        // GET: api/Default/5
        public string Get(string name)
        {
            return "xin chào " + name;
        }

        // POST: api/Default
        [HttpPost]
        public string Post()
        {
            return "Method : Post ; result : Nguyễn Minh Tiến"; 
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
