using Financiero.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Models
{
    public class clsProductoEstado
    {

        #region "Atributos"

        public int IdProductoEstado { get; set; }
        public string strNombre { get; set; }

        #endregion

        #region "Constructores"

        public clsProductoEstado()
        {
            IdProductoEstado = 0;            
            strNombre = String.Empty;            
        }

        public clsProductoEstado(int _IdProductoEstado, string _strNombre)
        {
            IdProductoEstado = _IdProductoEstado;
            strNombre = _strNombre;            
        }

        #endregion

    }
}