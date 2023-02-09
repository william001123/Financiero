using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Financiero.App_Start
{
    public class clsConexion
    {
        private OleDbConnection oConexion { get; set; }
        private OleDbCommand oComando { get; set; }
        private OleDbDataAdapter oAdapter { get; set; }
        private OleDbDataReader oReader { get; set; }
        private DataSet oDatos { get; set; }

        public clsConexion()
        {
            oConexion = new OleDbConnection(ConfigurationManager.ConnectionStrings["Financiero"].ConnectionString);
        }

        public bool RunProcSQL(string strNomProc, object oObjeto, List<OleDbParameter> oParametros)
        {
            try
            {
                RefinarParametros(oObjeto, ref oParametros);

                oComando = new OleDbCommand(strNomProc, oConexion);
                oComando.CommandTimeout = 100;
                oComando.CommandType = CommandType.StoredProcedure;
                foreach (var item in oParametros)
                {
                    oComando.Parameters.Add(item);
                }

                oConexion.Open();
                oComando.ExecuteNonQuery();
                oConexion.Close();
                oComando.Parameters.Clear();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string[]> RunProcSQLOutPut(string strNomProc, object oObjeto, List<OleDbParameter> oParametros)
        {
            try
            {
                List<string[]> oResultados = new List<string[]>();
                RefinarParametros(oObjeto, ref oParametros);

                oComando = new OleDbCommand(strNomProc, oConexion);
                oComando.CommandTimeout = 100;
                oComando.CommandType = CommandType.StoredProcedure;
                foreach (var item in oParametros)
                {
                    oComando.Parameters.Add(item);
                    if (item.Direction == ParameterDirection.InputOutput || item.Direction == ParameterDirection.Output)
                    {
                        oResultados.Add(new string[] { item.ParameterName, item.OleDbType.ToString(), "" });
                    }
                }

                oConexion.Open();
                oComando.ExecuteNonQuery();
                for (Int16 i = 0; i < oResultados.Count(); i++)
                {
                    oResultados[i][2] = oComando.Parameters[oResultados[i][0]].Value.ToString();
                }
                oConexion.Close();
                oComando.Parameters.Clear();

                return oResultados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RunProcSQL_Int(string strNomProc, object oObjeto, List<OleDbParameter> oParametros)
        {
            try
            {
                int intResul;

                RefinarParametros(oObjeto, ref oParametros);

                oComando = new OleDbCommand(strNomProc, oConexion);
                oComando.CommandTimeout = 100;
                oComando.CommandType = CommandType.StoredProcedure;
                foreach (var item in oParametros)
                {
                    oComando.Parameters.Add(item);
                }

                oConexion.Open();
                intResul = Convert.ToInt32(oComando.ExecuteScalar());
                oConexion.Close();
                oComando.Parameters.Clear();

                return intResul;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
        public Int64 RunProcSQL_Int64(string strNomProc, object oObjeto, List<OleDbParameter> oParametros)
        {
            try
            {
                Int64 intResul;

                RefinarParametros(oObjeto, ref oParametros);

                oComando = new OleDbCommand(strNomProc, oConexion);
                oComando.CommandTimeout = 100;
                oComando.CommandType = CommandType.StoredProcedure;
                foreach (var item in oParametros)
                {
                    oComando.Parameters.Add(item);
                }

                oConexion.Open();
                intResul = Convert.ToInt64(oComando.ExecuteScalar());
                oConexion.Close();
                oComando.Parameters.Clear();

                return intResul;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataView RunProcSQL_DataView(string strNomProc, object oObjeto, List<OleDbParameter> oParametros)
        {
            try
            {
                RefinarParametros(oObjeto, ref oParametros);

                oComando = new OleDbCommand(strNomProc, oConexion);
                oComando.Parameters.Clear();
                oComando.CommandTimeout = 0;
                oComando.CommandType = CommandType.StoredProcedure;
                foreach (var item in oParametros)
                {
                    oComando.Parameters.Add(item);
                }

                oConexion.Open();

                oAdapter = new OleDbDataAdapter(oComando);
                oDatos = new DataSet();
                oAdapter.Fill(oDatos);
                oConexion.Close();
                oComando.Parameters.Clear();

                return new DataView(oDatos.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OleDbDataReader RunProcSQL_DataReader(string strNomProc, object oObjeto, List<OleDbParameter> oParametros)
        {
            try
            {
                RefinarParametros(oObjeto, ref oParametros);

                oComando = new OleDbCommand(strNomProc, oConexion);
                oComando.CommandTimeout = 100;
                oComando.CommandType = CommandType.StoredProcedure;
                foreach (var item in oParametros)
                {
                    oComando.Parameters.Add(item);
                }

                oConexion.Open();
                oReader = oComando.ExecuteReader();
                oReader.Close();
                oConexion.Close();
                oComando.Parameters.Clear();

                return oReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RefinarParametros(object oObjeto, ref List<OleDbParameter> oParametros)
        {

            foreach (var oProp in oObjeto.GetType().GetProperties())
            {
                if (oParametros.Find(x => x.ParameterName == "@" + oProp.Name) != null)
                {
                    oParametros.Find(x => x.ParameterName == "@" + oProp.Name).Value = oProp.GetValue(oObjeto, null);
                }
            }
        }

    }
}