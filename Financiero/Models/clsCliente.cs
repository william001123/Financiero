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

        //De apoyo
        private clsConexion oAD { get; set; }
        private List<clsStoreProcedure> oProcedimientos { get; set; }

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

        #region "Metodos"

        private void DefinicionSP()
        {
            try
            {
                oProcedimientos = new List<clsStoreProcedure>();
                //Insertar
                oProcedimientos.Add(new clsStoreProcedure("ClienteAdd"));
                oProcedimientos[0].AddParametro("@strTipoIdentificacion", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@strNumeroIdentificacion", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@strNombre", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@strApellido", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@strEmail", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@dtFechaNacimiento", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);

                //Actualizar
                oProcedimientos.Add(new clsStoreProcedure("ClienteUpdate")); 
                oProcedimientos[1].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strTipoIdentificacion", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strNumeroIdentificacion", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strNombre", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strApellido", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strEmail", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@dtFechaNacimiento", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);

                //Eliminar
                oProcedimientos.Add(new clsStoreProcedure("ClienteDelete"));
                oProcedimientos[2].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);

                //Obtener
                oProcedimientos.Add(new clsStoreProcedure("ClienteGet"));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Boolean Insertar(string _strTipoIdentificacion, string _strNumeroIdentificacion, string _strNombre, string _strApellido, string _strEmail, DateTime _dtFechaNacimiento)
        {
            oAD = new clsConexion();

            strTipoIdentificacion = _strTipoIdentificacion;
            strNumeroIdentificacion = _strNumeroIdentificacion;
            strNombre = _strNombre;
            strApellido = _strApellido;
            strEmail = _strEmail;
            dtFechaNacimiento = _dtFechaNacimiento;

            IdCliente = oAD.RunProcSQL_Int(oProcedimientos[0].strNombreSP, this, oProcedimientos[0].oParams);
            return (IdCliente > 0) ? true : false;
        }

        private Boolean Actualizar(int _IdCliente, string _strTipoIdentificacion, string _strNumeroIdentificacion, string _strNombre, string _strApellido, string _strEmail, DateTime _dtFechaNacimiento)
        {
            oAD = new clsConexion();
            Boolean blEstado = false;

            IdCliente = _IdCliente;
            strTipoIdentificacion = _strTipoIdentificacion;
            strNumeroIdentificacion = _strNumeroIdentificacion;
            strNombre = _strNombre;
            strApellido = _strApellido;
            strEmail = _strEmail;
            dtFechaNacimiento = _dtFechaNacimiento;

            blEstado = oAD.RunProcSQL(oProcedimientos[1].strNombreSP, this, oProcedimientos[1].oParams);
            return blEstado;
        }

        private Boolean Eliminar(int _IdCliente)
        {
            oAD = new clsConexion();
            Boolean blEstado = false;

            IdCliente = _IdCliente;

            blEstado = oAD.RunProcSQL(oProcedimientos[2].strNombreSP, this, oProcedimientos[2].oParams);
            return blEstado;
        }

        public List<clsCliente> Obtener()
        {
            try
            {

                oAD = new clsConexion();
                DataView dv;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[3].strNombreSP, this, oProcedimientos[3].oParams);
                List<clsCliente> oObjeto = new List<clsCliente>();
                foreach (DataRowView oRow in dv)
                {
                    oObjeto.Add(new clsCliente(Convert.ToInt32(oRow["IdCliente"]),
                                                Convert.ToString(oRow["strTipoIdentificacion"]),
                                                Convert.ToString(oRow["strNumeroIdentificacion"]),
                                                Convert.ToString(oRow["strNombre"]),
                                                Convert.ToString(oRow["strApellido"]),
                                                Convert.ToString(oRow["strEmail"]),
                                                Convert.ToDateTime(oRow["dtFechaNacimiento"]),
                                                Convert.ToDateTime(oRow["dtFechaCreacion"]),
                                                Convert.ToDateTime(oRow["dtFechaModificacion"])));

                }
                return oObjeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}