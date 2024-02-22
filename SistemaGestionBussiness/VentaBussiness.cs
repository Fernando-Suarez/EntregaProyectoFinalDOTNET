using SistemaGestionEntities;
using SistemaGestionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public static class VentaBussiness
    {
        public static List<Venta> ListarVentas()
        {
            return VentaData.ListarVentas();
        }

        public static Venta ObtenerVenta(int id)
        {
            return VentaData.ObtenerVenta(id);
        }

        public static List<Venta> ObtenerVentaPorIdUsuario(int idUsuario)
        {
            return VentaData.ObtenerVentaPorIdUsuario(idUsuario);
        }

        public static void CrearVenta(Venta venta)
        {
            VentaData.CrearVenta(venta);
        }

        public static void CargarVenta(int idUsuario,List<Producto> productos)
        {
            VentaData.CargarVenta(idUsuario,productos);
        }

        public static void ModificarVenta(int id, Venta venta)
        {
            VentaData.ModificarVenta(id, venta);
        }

        public static bool EliminarVenta(int id)
        {
            return VentaData.EliminarVenta(id);
        }
    }
}
