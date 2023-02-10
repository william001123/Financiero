using Financiero.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Models
{
    public class clsTipoTransaccion
    {

        #region "Atributos"

        public int IdTipoTransaccion { get; set; }
        public string strNombre { get; set; }

        #endregion

        #region "Constructores"

        public clsTipoTransaccion()
        {
            IdTipoTransaccion = 0;            
            strNombre = String.Empty;            
        }

        public clsTipoTransaccion(int _IdTipoTransaccion, string _strNombre)
        {
            IdTipoTransaccion = _IdTipoTransaccion;
            strNombre = _strNombre;            
        }

        #endregion

    }
}