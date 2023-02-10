using Financiero.Metodos;
using Financiero.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financiero.Controllers
{
    public class ProductoController : ApiController
    {

        public clsMProducto oMProducto { get; set; } = new clsMProducto();
        public List<clsProducto> oListProducto { get; set; } = new List<clsProducto>();

        [HttpPost]
        public string Insertar(clsProducto _Producto)
        {

            try
            {
                bool bnResult = false;

                if (_Producto.IdTipoProducto != 0 || _Producto.numSaldo != 0 ||
                    _Producto.ExentaGMF != "" || _Producto.IdCliente != 0)
                {
                    bnResult = oMProducto.Insertar(_Producto.IdTipoProducto, _Producto.numSaldo,
                                                    _Producto.ExentaGMF, _Producto.IdCliente);

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
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se pudo insertar, verifique que los datos estén correctos o comuníquese con el área de TI"));
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        [HttpPut]
        public string ActualizarEstado(clsProducto _Producto)
        {
            try
            {
                bool bnResult = false;

                if (_Producto.IdProducto != 0 || _Producto.intEstado != 0)
                {
                    bnResult = oMProducto.ActualizarEstado(_Producto.IdProducto, _Producto.intEstado);

                    if (bnResult == true)
                    {
                        return "Actualizado correctamente";
                    }
                    else
                    {
                        return "No se pudo actualizar, verifique que los datos estén correctos o comuníquese con el área de TI";
                    }
                }
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se pudo actualizar, verifique que los datos estén correctos o comuníquese con el área de TI"));
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }       

        [HttpGet]
        public List<clsProducto> Get(int _IdCliente)
        {
            try
            {
                oListProducto = oMProducto.ObtenerByCliente(_IdCliente);

                if (oListProducto.Count != 0)
                {
                    return oListProducto;
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