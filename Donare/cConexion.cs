using System;
using System.Data;
using System.Data.SqlClient;

namespace Donare
{
    public class cConexion
    {
        public SqlConnection ConexionServer()
        {
            SqlConnection conn;
            try
            {
                string cadenaConexion = "Server=tcp:sqlserver-tarea.database.windows.net,1433;" +
                    "Initial Catalog=bancoDeSangre;" +
                    "Persist Security Info=False;" +
                    "User ID=tareabanco;" +
                    "Password=Sql123456;" +  
                    "MultipleActiveResultSets=False;" +
                    "Encrypt=True;" +
                    "TrustServerCertificate=False;" +
                    "Connection Timeout=30;";
                conn = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al Conectar", ex);
            }
            return conn;
        }
    }
}
