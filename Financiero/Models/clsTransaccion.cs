using Financiero.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Models
{
    public class clsTransaccion
    {

        #region "Atributos"

        public int IdTransaccion { get; set; }
        public int IdTipoTransaccion { get; set; }            
        public double numSaldo { get; set; }
        public int IdProductoOrigen { get; set; }
        public int IdProductoDestino { get; set; }

        #endregion

        #region "Constructores"

        public clsTransaccion()
        {
            IdTransaccion = 0;
            IdTipoTransaccion = 0;
            numSaldo = 0.00;
            IdProductoOrigen = 0;
            IdProductoDestino = 0;
        }

        public clsTransaccion(int _IdTransaccion, int _IdTipoTransaccion, double _numSaldo, int _IdProductoOrigen, int _IdProductoDestino)
        {

            IdTransaccion = _IdTransaccion;
            IdTipoTransaccion = _IdTipoTransaccion;
            numSaldo = _numSaldo;
            IdProductoOrigen = _IdProductoOrigen;
            IdProductoDestino = _IdProductoDestino;
        }

        #endregion

    }
}