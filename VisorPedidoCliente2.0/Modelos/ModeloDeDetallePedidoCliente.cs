using System;
using System.Collections.Generic;
using System.Text;

namespace VisorPedidoCliente2._0.Modelos
{
    class ModeloDeDetallePedidoCliente
    {
        public int IdPedidoCliente { get; set; }
        public int IdCliente { get; set; }
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
