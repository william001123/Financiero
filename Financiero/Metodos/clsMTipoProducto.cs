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
    public class clsMTipoProducto : clsTipoProducto
    {

        #region "Atributos"  
        public clsConexion oAD { get; set; }
        public List<clsStoreProcedure> oProcedimientos { get; set; }

        #endregion 
        public clsMTipoProducto()
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
                oProcedimientos.Add(new clsStoreProcedure("TipoProductoGet"));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        public List<clsTipoProducto> Obtener()
        {
            try
            {

                oAD = new clsConexion();
                DataView dv;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[0].strNombreSP, this, oProcedimientos[0].oParams);
                List<clsTipoProducto> oObjeto = new List<clsTipoProducto>();
                foreach (DataRowView oRow in dv)
                {
                    oObjeto.Add(new clsTipoProducto(Convert.ToInt32(oRow["IdTipoProducto"]),
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