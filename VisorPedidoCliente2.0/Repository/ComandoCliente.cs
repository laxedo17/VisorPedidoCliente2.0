using Dapper;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using VisorPedidoCliente2._0.Modelos;

namespace VisorPedidoCliente2._0.Repository
{
    class ComandoCliente
    {
        private string _stringConexion;

        public ComandoCliente(string stringConexion)
        {
            _stringConexion = stringConexion;
        }

        public IList<ModeloCliente> ObtenerLista()
        {
            List<ModeloCliente> clientes = new List<ModeloCliente>();

            var sql = "Cliente_ObtenerLista";

            using (SqlConnection conexion = new SqlConnection(_stringConexion))
            {
                clientes = conexion.Query<ModeloCliente>(sql).ToList(); //mapea os atributos do obxeto ModeloCliente cos da Base de Datos, gracias a Dapper que simplifica moitisimo o traballo. Dapper e parte de Linq e mapea a unha lista
            }

            return clientes;
        }
    }
}
