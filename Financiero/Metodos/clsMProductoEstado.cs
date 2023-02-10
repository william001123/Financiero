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
    public class clsMProductoEstado : clsProductoEstado
    {

        #region "Atributos"  
        public clsConexion oAD { get; set; }
        public List<clsStoreProcedure> oProcedimientos { get; set; }

        #endregion 

        public clsMProductoEstado()
        {
            DefinicionSP();
        }

        #region "Metodos"

        private void DefinicionSP()
        {
            try
            {
                oProcedimientos = new List<clsStoreProcedure>();

                //Obtener
                oProcedimientos.Add(new clsStoreProcedure("ProductoEstadoGet"));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        public List<clsProductoEstado> Obtener()
        {
            try
            {

                oAD = new clsConexion();
                DataView dv;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[0].strNombreSP, this, oProcedimientos[0].oParams);
                List<clsProductoEstado> oObjeto = new List<clsProductoEstado>();
                foreach (DataRowView oRow in dv)
                {
                    oObjeto.Add(new clsProductoEstado(Convert.ToInt32(oRow["IdProductoEstado"]),
                                                Convert.ToString(oRow["strNombre"])));

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