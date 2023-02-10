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
    public class clsMTipoTransaccion : clsTipoTransaccion
    {

        #region "Atributos"  
        public clsConexion oAD { get; set; }
        public List<clsStoreProcedure> oProcedimientos { get; set; }

        #endregion 

        public clsMTipoTransaccion()
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
                oProcedimientos.Add(new clsStoreProcedure("TipoTransaccionGet"));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

        public List<clsTipoTransaccion> Obtener()
        {
            try
            {

                oAD = new clsConexion();
                DataView dv;

                dv = oAD.RunProcSQL_DataView(oProcedimientos[0].strNombreSP, this, oProcedimientos[0].oParams);
                List<clsTipoTransaccion> oObjeto = new List<clsTipoTransaccion>();
                foreach (DataRowView oRow in dv)
                {
                    oObjeto.Add(new clsTipoTransaccion(Convert.ToInt32(oRow["IdTipoTransaccion"]),
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