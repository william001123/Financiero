using Financiero.Metodos;
using Financiero.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financiero.Controllers
{
    public class TipoTransaccionController : ApiController
    {
        public clsMTipoTransaccion oMTipoTransaccion { get; set; } = new clsMTipoTransaccion();
        public List<clsTipoTransaccion> oListTipoTransaccion { get; set; } = new List<clsTipoTransaccion>();

        [HttpGet]
        public List<clsTipoTransaccion> Get()
        {
            try
            {
                oListTipoTransaccion = oMTipoTransaccion.Obtener();

                if (oListTipoTransaccion.Count != 0)
                {
                    return oListTipoTransaccion;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}