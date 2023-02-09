using Financiero.Metodos;
using Financiero.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financiero.Controllers
{
    public class ProductoEstadoController : ApiController
    {

        public clsMProductoEstado oMProductoEstado { get; set; } = new clsMProductoEstado();
        public List<clsProductoEstado> oListProductoEstado { get; set; } = new List<clsProductoEstado>();

        [HttpGet]
        public List<clsProductoEstado> Get()
        {
            try
            {
                oListProductoEstado = oMProductoEstado.Obtener();

                if (oListProductoEstado.Count != 0)
                {
                    return oListProductoEstado;
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