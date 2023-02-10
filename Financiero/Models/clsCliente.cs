using Financiero.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Models
{
    public class clsCliente
    {

        #region "Atributos"

        public int IdCliente { get; set; }
        public string strTipoIdentificacion { get; set; }
        public string strNumeroIdentificacion { get; set; }
        public string strNombre { get; set; }
        public string strApellido { get; set; }
        public string strEmail { get; set; }
        public DateTime dtFechaNacimiento { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaModificacion { get; set; }

        #endregion

        #region "Constructores"

        public clsCliente()
        {
            IdCliente = 0;
            strTipoIdentificacion = String.Empty;
            strNumeroIdentificacion = String.Empty;
            strNombre = String.Empty;
            strApellido = String.Empty;
            strEmail = String.Empty;
            dtFechaNacimiento = DateTime.Now;
            dtFechaCreacion = DateTime.Now;
            dtFechaModificacion = DateTime.Now;
        }

        public clsCliente(int _IdCliente, string _strTipoIdentificacion, string _strNumeroIdentificacion, string _strNombre, string _strApellido, string _strEmail,
                            DateTime _dtFechaNacimiento, DateTime _dtFechaCreacion, DateTime _dtFechaModificacion)
        {

            IdCliente = _IdCliente;
            strTipoIdentificacion = _strTipoIdentificacion;
            strNumeroIdentificacion = _strNumeroIdentificacion;
            strNombre = _strNombre;
            strApellido = _strApellido;
            strEmail = _strEmail;
            dtFechaNacimiento = _dtFechaNacimiento;
            dtFechaCreacion = _dtFechaCreacion;
            dtFechaModificacion = _dtFechaModificacion;
        }

        #endregion

    }
}