using Financiero.Metodos;
using Financiero.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Financiero.Controllers
{
    public class ClientesController : ApiController
    {

        public clsMCliente oMCliente { get; set; } = new clsMCliente();
        public List<clsCliente> oListCliente { get; set; } = new List<clsCliente>();

        [HttpPost]
        public string Insertar(clsCliente _Cliente)
        {

            try
            {
                bool bnResult = false;

                if (_Cliente.strTipoIdentificacion != "" || _Cliente.strNumeroIdentificacion != "" ||
                    _Cliente.strNombre != "" || _Cliente.strApellido != "" || _Cliente.strEmail != "" || _Cliente.dtFechaNacimiento != null)
                {
                    bnResult = oMCliente.Insertar(_Cliente.strTipoIdentificacion, _Cliente.strNumeroIdentificacion,
                                        _Cliente.strNombre, _Cliente.strApellido, _Cliente.strEmail, _Cliente.dtFechaNacimiento);

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
        public string Actualizar(clsCliente _Cliente)
        {
            try
            {
                bool bnResult = false;

                if (_Cliente.IdCliente != 0 || _Cliente.strTipoIdentificacion != "" || _Cliente.strNumeroIdentificacion != "" ||
                    _Cliente.strNombre != "" || _Cliente.strApellido != "" || _Cliente.strEmail != "" || _Cliente.dtFechaNacimiento != null)
                {
                    bnResult = oMCliente.Actualizar(_Cliente.IdCliente, _Cliente.strTipoIdentificacion, _Cliente.strNumeroIdentificacion,
                                        _Cliente.strNombre, _Cliente.strApellido, _Cliente.strEmail, _Cliente.dtFechaNacimiento);

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

        [HttpDelete]
        public string Eliminar(int _IdCliente)
        {
            try
            {
                bool bnResult = false;

                bnResult = oMCliente.Eliminar(_IdCliente);

                if (bnResult == true)
                {
                    return "Eliminado correctamente";
                }
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se pudo eliminar, verifique que los datos estén correctos o comuníquese con el área de TI"));
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public List<clsCliente> Get()
        {
            try
            {
                oListCliente = oMCliente.Obtener();

                if (oListCliente.Count != 0)
                {
                    return oListCliente;
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