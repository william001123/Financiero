using Financiero.Metodos;
using Financiero.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financiero.Controllers
{
    public class TipoProductoController : ApiController
    {

        public clsMTipoProducto oMTipoProducto { get; set; } = new clsMTipoProducto();
        public List<clsTipoProducto> oListTipoProducto { get; set; } = new List<clsTipoProducto>();

        [HttpGet]
        public List<clsTipoProducto> Get()
        {
            try
            {
                oListTipoProducto = oMTipoProducto.Obtener();

                if (oListTipoProducto.Count != 0)
                {
                    return oListTipoProducto;
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