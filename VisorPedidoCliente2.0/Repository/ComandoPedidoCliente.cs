using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using VisorPedidoCliente2._0.Modelos;

namespace VisorPedidoCliente2._0.Repository
{
    class ComandoPedidoCliente
    {
        private string _stringConexion;

        public ComandoPedidoCliente(string stringConexion)
        {
            _stringConexion = stringConexion;
        }

        public IList<ModeloDeDetallePedidoCliente> ObtenerLista()
        {
            List<ModeloDeDetallePedidoCliente> detallesPedidoCliente = new List<ModeloDeDetallePedidoCliente>();

            var sql = "DetallePedidoCliente_ObtenerLista";

            using (SqlConnection conexion = new SqlConnection(_stringConexion))
            {
                detallesPedidoCliente = conexion.Query<ModeloDeDetallePedidoCliente>(sql).ToList(); //mapea os atributos do obxeto ModeloArticulo co da Base de Datos, gracias a Dapper que simplifica moitisimo o traballo. Dapper e parte de Linq e mapea a unha lista
            }

            return detallesPedidoCliente;
        }

        public void Borrar(int idPedidoCliente, string idUsuario)
        {
            var instruccionBorrar = "DetallePedidoCliente_Borrar";

            using (SqlConnection conexion = new SqlConnection(_stringConexion))
            {
                conexion.Execute(instruccionBorrar, new { @IdPedidoCliente = idPedidoCliente, @IdUsuario = idUsuario }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void ActualizarInsertar(int idPedidoCliente, int idCliente, int idArticulo, string idUsuario)
        {
            var actualizarInsertar = "DetallePedidoCliente_ActInsert";

            var tablaDeDatos = new DataTable(); //a maneira de insertar unha DataType definida por nos -a que creamos na base de datos- en C# e usando unha taboa de datos
            tablaDeDatos.Columns.Add("IdPedidoCliente", typeof(int));
            tablaDeDatos.Columns.Add("IdCliente", typeof(int));
            tablaDeDatos.Columns.Add("IdArticulo", typeof(int));
            tablaDeDatos.Rows.Add(idPedidoCliente, idCliente, idArticulo);

            using(SqlConnection conexion = new SqlConnection(_stringConexion))
            {
                conexion.Execute(actualizarInsertar, new { @TipoDePedidoCliente = tablaDeDatos.AsTableValuedParameter("TipoDePedidoCliente"), @IdUsuario = idUsuario }, commandType: CommandType.StoredProcedure); //ten que coincidir o nome do parametro co nome que lle puxemos na Base de Datos
            }
        }
    }
}
