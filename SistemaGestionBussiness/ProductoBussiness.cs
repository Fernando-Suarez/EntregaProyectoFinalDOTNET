using SistemaGestionEntities;
using SistemaGestionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public class ProductoBussiness
    {
        public static List<Producto> ListarProductos()
        {
            return ProductoData.ListarProductos();
        }

        public static Producto ObtenerProducto(int id)
        {
            return ProductoData.ObtenerProducto(id);
        }

        public static List<Producto> ProductosPorIdUsuario(int idUsuario)
        {
            return ProductoData.ProductosPorIdUsuario(idUsuario);
        }

        public static void CrearProducto(Producto producto)
        {
            ProductoData.CrearProducto(producto);
        }

        public static void ModificarProducto( Producto producto)
        {
            ProductoData.ModificarProducto( producto);
        }

        public static bool EliminarProducto(int id)
        {
            return ProductoData.EliminarProducto(id);
        }
    }
}
