using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;



namespace ProveedorService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProveedorSOAP : WebService
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SalesDBConnection"].ConnectionString;

        // Crear un proveedor
        [WebMethod]
        public string CreateProveedor(string nombre, string telefono, string email, string direccion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Proveedores (Nombre, Telefono, Email, Direccion) VALUES (@Nombre, @Telefono, @Email, @Direccion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", direccion);

                conn.Open();
                cmd.ExecuteNonQuery();
                return "Proveedor creado correctamente.";
            }
        }

        // Leer un proveedor por ID
        [WebMethod]
        public DataSet GetProveedor(int proveedorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Proveedores WHERE ProveedorID = @ProveedorID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@ProveedorID", proveedorId);

                DataSet proveedorData = new DataSet();
                adapter.Fill(proveedorData);
                return proveedorData;
            }
        }

        // Actualizar un proveedor
        [WebMethod]
        public string UpdateProveedor(int proveedorId, string nombre, string telefono, string email, string direccion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Proveedores SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email, Direccion = @Direccion WHERE ProveedorID = @ProveedorID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProveedorID", proveedorId);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", direccion);

                conn.Open();
                cmd.ExecuteNonQuery();
                return "Proveedor actualizado correctamente.";
            }
        }

        // Eliminar un proveedor
        [WebMethod]
        public string DeleteProveedor(int proveedorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Proveedores WHERE ProveedorID = @ProveedorID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProveedorID", proveedorId);

                conn.Open();
                cmd.ExecuteNonQuery();
                return "Proveedor eliminado correctamente.";
            }
        }
    }
}
