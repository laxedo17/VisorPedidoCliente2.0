using Dapper;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using VisorPedidoCliente2._0.Modelos;

namespace VisorPedidoCliente2._0.Repository
{
    class ComandoArticulo
    {
        private string _stringConexion;

        public ComandoArticulo(string stringConexion)
        {
            _stringConexion = stringConexion;
        }

        public IList<ModeloArticulo> ObtenerLista()
        {
            List<ModeloArticulo> articulos = new List<ModeloArticulo>();

            var sql = "Articulo_ObtenerLista";

            using (SqlConnection conexion = new SqlConnection(_stringConexion))
            {
                articulos = conexion.Query<ModeloArticulo>(sql).ToList(); //mapea os atributos do obxeto ModeloArticulo cos da Base de Datos, gracias a Dapper que simplifica moitisimo o traballo. Dapper e parte de Linq e mapea a unha lista
            }

            return articulos;
        }
    }
}
