using EcommerceASPNETCoreWebAPI.Entities;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EcommerceASPNETCoreWebAPI.Data
{
    public class ProductoData
    {
        private readonly string _conexion;

        public ProductoData(IConfiguration configuration)
        {
            this._conexion = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Producto>> ListarProductosAsync()
        {
            List<Producto> listarProductos = new List<Producto>();

            using (var conn = new SqlConnection(this._conexion))
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("sp_listar_productos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listarProductos.Add(new Producto
                            {
                                Id = Convert.ToInt64(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                Estado = Convert.ToChar(reader["Estado"]),
                                Habilitado = Convert.ToBoolean(reader["Habilitado"]),
                                Eliminado = Convert.ToBoolean(reader["Eliminado"])
                            });
                        }
                    }
                }
            }
            return listarProductos;
        }

        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            bool respuesta = true;

            using (var conn = new SqlConnection(this._conexion))
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("sp_crear_producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("Cantidad", producto.Cantidad);
                    cmd.Parameters.AddWithValue("Estado", producto.Estado);
                    cmd.Parameters.AddWithValue("Habilitado", producto.Habilitado);
                    cmd.Parameters.AddWithValue("Eliminado", producto.Eliminado);

                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
            }
            return respuesta;
        }


    }
}
