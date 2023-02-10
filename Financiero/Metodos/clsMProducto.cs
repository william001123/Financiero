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
    public class clsMProducto : clsProducto
    {

        #region "Atributos"  
        public clsConexion oAD { get; set; }
        public List<clsStoreProcedure> oProcedimientos { get; set; }

        #endregion 

        public clsMProducto()
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
                oProcedimientos.Add(new clsStoreProcedure("ProductoAdd"));
                oProcedimientos[0].AddParametro("@IdTipoProducto", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@numSaldo", OleDbType.Numeric, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@ExentaGMF", OleDbType.VarChar, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);

                //Actualizar
                oProcedimientos.Add(new clsStoreProcedure("ProductoUpdateEstado")); 
                oProcedimientos[1].AddParametro("@IdProducto", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[1].AddParametro("@intEstado", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);

                //Obtener por cliente
                oProcedimientos.Add(new clsStoreProcedure("ProductoGetByCliente"));
                oProcedimientos[2].AddParametro("@IdCliente", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);

                //Obtener todo
                oProcedimientos.Add(new clsStoreProcedure("ProductoGet"));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Insertar(int _IdTipoProducto, double _numSaldo, string _ExentaGMF, int _IdCliente)
        {
            oAD = new clsConexion();

            IdTipoProducto = _IdTipoProducto;
            numSaldo = _numSaldo;
            ExentaGMF = _ExentaGMF;
            IdCliente = _IdCliente;

            IdCliente = oAD.RunProcSQL_Int(oProcedimientos[0].strNombreSP, this, oProcedimientos[0].oParams);
            return (IdCliente > 0) ? true : false;
        }

        public Boolean ActualizarEstado(int _IdProducto, int _intEstado)
        {
            oAD = new clsConexion();
            Boolean blEstado = false;

            IdProducto = _IdProducto;
            intEstado = _intEstado;

             blEstado = oAD.RunProcSQL(oProcedimientos[1].strNombreSP, this, oProcedimientos[1].oParams);
            return blEstado;
        }

        public List<clsProducto> ObtenerByCliente(int _IdCliente)
        {
            try
            {
                oAD = new clsConexion();
                DataView dv;

                IdCliente = _IdCliente;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[2].strNombreSP, this, oProcedimientos[2].oParams);
                List<clsProducto> oObjeto = new List<clsProducto>();
                foreach (DataRowView oRow in dv)
                {                  
                    oObjeto.Add(new clsProducto(Convert.ToInt32(oRow["IdProducto"]),
                                                Convert.ToInt32(oRow["IdTipoProducto"]),
                                                Convert.ToString(oRow["NumeroCuenta"]),
                                                Convert.ToInt32(oRow["intEstado"]),
                                                Convert.ToDouble(oRow["numSaldo"]),
                                                Convert.ToString(oRow["ExentaGMF"]),
                                                Convert.ToDateTime(oRow["dtFechaCreacion"]),
                                                Convert.ToDateTime(oRow["dtFechaModificacion"]),
                                                Convert.ToInt32(oRow["IdCliente"])));

                }
                return oObjeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<clsProducto> Obtener()
        {
            try
            {
                oAD = new clsConexion();
                DataView dv;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[3].strNombreSP, this, oProcedimientos[3].oParams);
                List<clsProducto> oObjeto = new List<clsProducto>();
                foreach (DataRowView oRow in dv)
                {
                    oObjeto.Add(new clsProducto(Convert.ToInt32(oRow["IdProducto"]),
                                                Convert.ToInt32(oRow["IdTipoProducto"]),
                                                Convert.ToString(oRow["NumeroCuenta"]),
                                                Convert.ToInt32(oRow["intEstado"]),
                                                Convert.ToDouble(oRow["numSaldo"]),
                                                Convert.ToString(oRow["ExentaGMF"]),
                                                Convert.ToDateTime(oRow["dtFechaCreacion"]),
                                                Convert.ToDateTime(oRow["dtFechaModificacion"]),
                                                Convert.ToInt32(oRow["IdCliente"])));

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