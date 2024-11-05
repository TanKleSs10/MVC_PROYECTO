using System.Collections.Generic;
using System.Data.OleDb;
using ASPNETMVCCheckboxDemo.Models;

namespace ASPNETMVCCheckboxDemo.Repositories
{
    public class ClienteRepository
    {
        private readonly AccessDbConnection _dbConnection;

        public ClienteRepository(AccessDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            string query = "SELECT * FROM clientes ORDER BY Apellidos";
            using (OleDbCommand command = new OleDbCommand(query, _dbConnection.GetConnection()))
            {
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Compania = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Nombre = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Apellidos = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Correo = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Tel = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Cargo = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                        clientes.Add(cliente);
                    }
                }
            }
            return clientes;
        }

        public List<Cliente> Search(string field, string query, string order)
        {
            List<Cliente> clientes = new List<Cliente>();
            string sqlQuery;

            if (string.IsNullOrEmpty(field) || field.Length == 0 || query.Length == 0 || string.IsNullOrEmpty(query))
            {
                sqlQuery = $"SELECT * FROM clientes ORDER BY Apellidos {order}";
            }
            else
            {
                sqlQuery = $"SELECT * FROM clientes WHERE {field} LIKE @query ORDER BY {field} {order}";
            }

            using (OleDbCommand command = new OleDbCommand(sqlQuery, _dbConnection.GetConnection()))
            {
                if (!string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(query))
                {
                    command.Parameters.AddWithValue("@query", "%" + query + "%");
                }

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Compania = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Nombre = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Apellidos = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Correo = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Tel = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Cargo = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                        clientes.Add(cliente);
                    }
                }
            }
            return clientes;
        }
    }
}
