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
    public class clsMTransaccion : clsTransaccion
    {

        #region "Atributos"  
        public int IdProducto { get; set; }

        public clsConexion oAD { get; set; }
        public List<clsStoreProcedure> oProcedimientos { get; set; }

        #endregion 

        public clsMTransaccion()
        {

            IdProducto = 0;

            DefinicionSP();
        }

        #region "Metodos"

        private void DefinicionSP()
        {
            try
            {
                oProcedimientos = new List<clsStoreProcedure>();
                //Insertar
                oProcedimientos.Add(new clsStoreProcedure("TransaccionAdd"));
                oProcedimientos[0].AddParametro("@IdTipoTransaccion", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@IdProductoOrigen", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@IdProductoDestino", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);
                oProcedimientos[0].AddParametro("@numSaldo", OleDbType.Numeric, ParameterDirection.Input, DBNull.Value);

                //Obtener
                oProcedimientos.Add(new clsStoreProcedure("TransaccionGetByProducto"));
                oProcedimientos[1].AddParametro("@IdProducto", OleDbType.Integer, ParameterDirection.Input, DBNull.Value);             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Insertar(int _IdTipoTransaccion, int _IdProductoOrigen, int _IdProductoDestino, double _numSaldo)
        {
            oAD = new clsConexion();

            IdTipoTransaccion = _IdTipoTransaccion;
            IdProductoOrigen = _IdProductoOrigen;
            IdProductoDestino = _IdProductoDestino;
            numSaldo = _numSaldo;

            IdTransaccion = oAD.RunProcSQL_Int(oProcedimientos[0].strNombreSP, this, oProcedimientos[0].oParams);
            return (IdTransaccion > 0) ? true : false;
        }       

        public List<clsTransaccion> ObtenerByProducto(int _IdProducto)
        {
            try
            {
                oAD = new clsConexion();
                DataView dv;

                IdProducto = _IdProducto;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[1].strNombreSP, this, oProcedimientos[1].oParams);
                List<clsTransaccion> oObjeto = new List<clsTransaccion>();
                foreach (DataRowView oRow in dv)
                {                  
                    oObjeto.Add(new clsTransaccion(Convert.ToInt32(oRow["IdTransaccion"]),
                                                Convert.ToInt32(oRow["IdTipoTransaccion"]),                                                
                                                Convert.ToDouble(oRow["numSaldo"]),                                                                                                
                                                Convert.ToInt32(oRow["IdProductoOrigen"]),
                                                Convert.ToInt32(oRow["IdProductoDestino"])));

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