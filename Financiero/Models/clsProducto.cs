using Financiero.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Models
{
    public class clsProducto
    {

        #region "Atributos"

        public int IdProducto { get; set; }
        public int IdTipoProducto { get; set; }
        public string NumeroCuenta { get; set; }
        public int intEstado { get; set; }
        public double numSaldo { get; set; }
        public string ExentaGMF { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        public int IdCliente { get; set; }

        #endregion

        #region "Constructores"

        public clsProducto()
        {
            IdProducto = 0;
            IdTipoProducto = 0;
            NumeroCuenta = String.Empty;
            intEstado = 0;
            numSaldo = 0.00;
            ExentaGMF = String.Empty;   
            dtFechaCreacion = DateTime.Now;
            dtFechaModificacion = DateTime.Now;
            IdCliente = 0;

    }

        public clsProducto(int _IdProducto, int _IdTipoProducto, string _NumeroCuenta, int _intEstado, double _numSaldo, string _ExentaGMF, DateTime _dtFechaCreacion, DateTime _dtFechaModificacion, int _IdCliente)
        {

            IdProducto = _IdProducto;
            IdTipoProducto = _IdTipoProducto;
            NumeroCuenta = _NumeroCuenta;
            intEstado = _intEstado;
            numSaldo = _numSaldo;
            ExentaGMF = _ExentaGMF;
            dtFechaCreacion = _dtFechaCreacion;
            dtFechaModificacion = _dtFechaModificacion;
            IdCliente = _IdCliente;
        }

        #endregion

    }
}