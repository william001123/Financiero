using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Financiero.Controllers
{
    public class ClientesController : ApiController
    {
        // GET: Financiero
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}