using Financiero.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Models
{
    public class clsTipoProducto
    {

        #region "Atributos"

        public int IdTipoProducto { get; set; }
        public string strNombre { get; set; }

        #endregion

        #region "Constructores"

        public clsTipoProducto()
        {
            IdTipoProducto = 0;            
            strNombre = String.Empty;            
        }

        public clsTipoProducto(int _IdTipoProducto, string _strNombre)
        {
            IdTipoProducto = _IdTipoProducto;
            strNombre = _strNombre;            
        }

        #endregion

    }
}