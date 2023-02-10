using Financiero.App_Start;
using Financiero.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.Metodos
{
    public class clsMCliente : clsCliente
    {

        #region "Atributos"  
        public clsConexion oAD { get; set; }
        public List<clsStoreProcedure> oProcedimientos { get; set; }

        #endregion 

        public clsMCliente()
        {
            DefinicionSP();
        }

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
                oProcedimientos[0].AddParametro("@dtFechaNacimiento", OleDbType.Date, ParameterDirection.Input, DBNull.Value);

                //Actualizar
                oProcedimientos.Add(new clsStoreProcedure("ClienteUpdate")); 
                oProcedimientos[1].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strTipoIdentificacion", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strNumeroIdentificacion", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strNombre", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strApellido", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@strEmail", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@dtFechaNacimiento", OleDbType.Date, ParameterDirection.Input, DBNull.Value);

                //Eliminar
                oProcedimientos.Add(new clsStoreProcedure("ClienteDelete"));
                oProcedimientos[2].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);

                //Obtener
                oProcedimientos.Add(new clsStoreProcedure("ClienteGet"));

                //Obtener Uno
                oProcedimientos.Add(new clsStoreProcedure("ClienteGetSingle"));
                oProcedimientos[4].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Insertar(string _strTipoIdentificacion, string _strNumeroIdentificacion, string _strNombre, string _strApellido, string _strEmail, DateTime _dtFechaNacimiento)
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

        public Boolean Actualizar(int _IdCliente, string _strTipoIdentificacion, string _strNumeroIdentificacion, string _strNombre, string _strApellido, string _strEmail, DateTime _dtFechaNacimiento)
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

        public Boolean Eliminar(int _IdCliente)
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

        public List<clsCliente> ObtenerUno(int _IdCliente)
        {
            try
            {
                oAD = new clsConexion();
                DataView dv;
                IdCliente = _IdCliente;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[4].strNombreSP, this, oProcedimientos[4].oParams);
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

                //if (dv.Count > 0)
                //{
                //    IdCliente = Convert.ToInt32(dv[0]["IdCliente"]);
                //    strTipoIdentificacion = Convert.ToString(dv[0]["strTipoIdentificacion"]);
                //    strNumeroIdentificacion = Convert.ToString(dv[0]["strNumeroIdentificacion"]);
                //    strNombre = Convert.ToString(dv[0]["strNombre"]);
                //    strApellido = Convert.ToString(dv[0]["strApellido"]);
                //    strEmail = Convert.ToString(dv[0]["strEmail"]);
                //    dtFechaNacimiento = Convert.ToDateTime(dv[0]["dtFechaNacimiento"]);
                //    dtFechaCreacion = Convert.ToDateTime(dv[0]["dtFechaCreacion"]);
                //    dtFechaModificacion = Convert.ToDateTime(dv[0]["dtFechaModificacion"]);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}