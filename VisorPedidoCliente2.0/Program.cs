using System;
using System.Collections.Generic;
using System.Linq;

using VisorPedidoCliente2._0.Modelos;
using VisorPedidoCliente2._0.Repository;

namespace VisorPedidoCliente2._0
{
    class Program
    {
        private static string _stringConexion = @"Data Source=localhost;Initial Catalog=VisorPedidoCliente;Integrated Security=True";
        private static readonly ComandoPedidoCliente comandoPedidoCliente = new ComandoPedidoCliente(_stringConexion);
        private static readonly ComandoCliente comandoCliente = new ComandoCliente(_stringConexion);
        private static readonly ComandoArticulo comandoArticulo = new ComandoArticulo(_stringConexion);

        static void Main(string[] args)
        {
            try
            {
                var idUsuario = string.Empty;
                var continuarResolviendo = true;

                Console.WriteLine("Cal e o teu nome de usuario?");
                idUsuario = Console.ReadLine();

                do
                {
                    Console.WriteLine("1 - Mostrar todos | 2 - ActualizarInsertar Pedido Cliente | 3 - Borrar Pedido Usuario | 4 - Salir");

                    int opcion = Convert.ToInt32(Console.ReadLine());

                    if (opcion == 1)
                    {
                        MostrarTodos();
                    }
                    else if (opcion == 2)
                    {
                        ActualizarInsertar(idUsuario);
                    }
                    else if (opcion == 3)
                    {
                        BorrarPedidoCliente(idUsuario);
                    }
                    else if (opcion == 4)
                    {
                        continuarResolviendo = false; //continuarManexando false para o loop e basicamente o programa
                    }
                    else
                    {
                        Console.WriteLine("Opcion no encontrada");
                    }

                } while (continuarResolviendo == true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo foi mal: {0}", ex.Message);
            }
        }

        private static void MostrarTodos()
        {
            Console.WriteLine("{0} Todos los pedidos de clientes: {1}", Environment.NewLine, Environment.NewLine);
            MostrarPedidosClientes();

            Console.WriteLine("{0} Todos los Clientes: {1}", Environment.NewLine, Environment.NewLine);
            MostrarClientes();

            Console.WriteLine("{0} Todos los Articulos {1}", Environment.NewLine, Environment.NewLine);
            MostrarArticulos();

            Console.WriteLine();
        }

        private static void MostrarArticulos()
        {
            IList<ModeloArticulo> articulos = comandoArticulo.ObtenerLista();

            if(articulos.Any())
            {
                foreach (ModeloArticulo articulo in articulos)
                {
                    Console.WriteLine("{0}: Descripcion {1}, Precio{2}", articulo.IdArticulo, articulo.Descripcion, articulo.Precio);
                }
            }
        }

        private static void MostrarClientes()
        {
            IList<ModeloCliente> clientes = comandoCliente.ObtenerLista();

            if (clientes.Any())
            {
                foreach (ModeloCliente cliente in clientes)
                {
                    Console.WriteLine("{0}: Nombre: {1}, SdoNombre: {2}, Apellido: {3}, Edad {4}", cliente.IdCliente, cliente.Nombre, cliente.SegundoNombre ?? "N/D", cliente.Apellido, cliente.Edad); //se SegundoNombre e null, enton verase N/D (Non Disponible)
                }
            }
        }

        private static void MostrarPedidosClientes()
        {
            IList<ModeloDeDetallePedidoCliente> detallesPedidoCliente = comandoPedidoCliente.ObtenerLista();

            if (detallesPedidoCliente.Any())
            {
                foreach (ModeloDeDetallePedidoCliente detallePedidoCliente in detallesPedidoCliente)
                {
                    Console.WriteLine(String.Format("{0}: Nombre Completo: {1} {2} {3} (Id: {4}) - comprou {5} por {6}€ (Id: {7})",
                        detallePedidoCliente.IdPedidoCliente,
                        detallePedidoCliente.Nombre,
                        detallePedidoCliente.SegundoNombre,
                        detallePedidoCliente.Apellido,
                        detallePedidoCliente.IdCliente,
                        detallePedidoCliente.Descripcion,
                        detallePedidoCliente.Precio,
                        detallePedidoCliente.IdArticulo));
                }
            }
        }

        private static void BorrarPedidoCliente(string idUsuario)
        {
            Console.WriteLine("Entra a Id de Pedido de Cliente");
            int idPedidoCliente = Convert.ToInt32(Console.ReadLine());

            comandoPedidoCliente.Borrar(idPedidoCliente, idUsuario);
        }

        private static void ActualizarInsertar(string idUsuario)
        {
            Console.WriteLine("Nota: Para actualizar/insertar en Ids de Pedido Cliente existentes, para novas insercions de PedidoCliente entra -1"); //como e unha app de consola poñemos -1 como valor posible xa que non existirá. Se fose unha UI -interfaz de usuario- xa coloca -1 para novas entradas automaticamente

            Console.WriteLine("Entra IdPedidoUsuario");
            int novaIdPedidoUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Entra IdCliente:");
            int novaIdCliente = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Entra IdArticulo");
            int novaIdArticulo = Convert.ToInt32(Console.ReadLine());

            comandoPedidoCliente.ActualizarInsertar(novaIdPedidoUsuario, novaIdCliente, novaIdArticulo, idUsuario);

        }
    }
}
