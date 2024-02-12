using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class ProductoVendido
    {
        //Propiedades: Id, IdProducto, Stock, IdVenta 

        private int _id;
        private int _idProducto;
        private int _stock;
        private int _idVenta;

        public ProductoVendido() { }    
        public ProductoVendido(int id, int idProducto,int stock, int idVenta)
        {
            _id = id;
            _idProducto = idProducto;
            _stock = stock;
            _idVenta = idVenta;
        }

        public int Id { get => _id; set => _id = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }

        public int Stock { get => _stock; set => _stock = value; }
        public int IdVenta { get => _idVenta; set => _idVenta = value; }

        public override string ToString()
        {
            return ($"Id: {this.Id}, Stock: {this.Stock}, Id Producto: {this.IdProducto}, Id Venta: {this.IdVenta}");
        }
    }


}
