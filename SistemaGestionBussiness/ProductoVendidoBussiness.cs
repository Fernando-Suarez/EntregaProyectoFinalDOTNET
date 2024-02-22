using SistemaGestionEntities;
using SistemaGestionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public static class ProductoVendidoBussiness
    {
        public static List<ProductoVendido> ListarProductosVendidos()
        {
            return ProductoVendidoData.ListarProductosVendidos();
        }

        public static ProductoVendido ObtenerProductoVendido(int id)
        {
            return ProductoVendidoData.ObtenerProductoVendido(id);
        }

        public static List<ProductoVendido> TraerProductosVendidos(int idUsuario)
        {
            return ProductoVendidoData.TraerProductosVendidos(idUsuario);
        }

        public static void CrearProductoVendido(ProductoVendido productoVendido)
        {
            ProductoVendidoData.CrearProductoVendido(productoVendido);
        }

        public static void ModificarProductoVendido(int id, ProductoVendido productoVendido)
        {
            ProductoVendidoData.ModificarProductoVendido(id, productoVendido);
        }

        public static bool EliminarProductoVendido(int id)
        {
            return ProductoVendidoData.EliminarProductoVendido(id);
        }
    }
}
