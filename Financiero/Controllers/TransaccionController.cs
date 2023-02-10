using Financiero.Metodos;
using Financiero.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financiero.Controllers
{
    public class TransaccionController : ApiController
    {

        public clsMTransaccion oMTransaccion { get; set; } = new clsMTransaccion();
        public List<clsTransaccion> oListTransaccion { get; set; } = new List<clsTransaccion>();

        [HttpPost]
        public string Insertar(clsTransaccion _Transaccion)
        {

            try
            {
                bool bnResult = false;
                string strError = "";

                if (_Transaccion.IdTipoTransaccion != 1 && _Transaccion.IdProductoOrigen == 0)
                {
                    strError = strError + "El producto origen es obligatorio para hacer retiros y transacciones \n ";
                }

                if (_Transaccion.IdTipoTransaccion != 2 && _Transaccion.IdProductoDestino == 0)
                {
                    strError = strError + "El producto destino es obligatorio para hacer consignaciones y transacciones \n ";
                }

                if (strError == "")
                {              
                    bnResult = oMTransaccion.Insertar(_Transaccion.IdTipoTransaccion, _Transaccion.IdProductoOrigen, _Transaccion.IdProductoDestino, _Transaccion.numSaldo);

                    if (bnResult == true)
                    {
                        return "Insertado correctamente";
                    }
                    else
                    {
                        return "No se pudo insertar, verifique que los datos estén correctos o comuníquese con el área de TI";
                    }
                }
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se pudo insertar." + strError));
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        [HttpGet]
        public List<clsTransaccion> Get(int _IdProducto)
        {
            try
            {
                oListTransaccion = oMTransaccion.ObtenerByProducto(_IdProducto);

                if (oListTransaccion.Count != 0)
                {
                    return oListTransaccion;
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