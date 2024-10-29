using System.Collections.Generic;
using System.Data.OleDb;

namespace ASPNETMVCCheckboxDemo.Repositories  // Asegúrate de que este sea el espacio de nombres correcto
{
    public class ClienteRepository
    {
        private readonly AccessDbConnection _dbConnection;

        public ClienteRepository(AccessDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Método para obtener todos los clientes
        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            string query = "SELECT * FROM clientes";

            using (OleDbCommand command = new OleDbCommand(query, _dbConnection.GetConnection()))
            {
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),  // Asegúrate de que el tipo sea correcto
                            Compania = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Nombre = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Apellidos = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Correo = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Tel = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Cargo = reader.IsDBNull(6) ? null : reader.GetString(6)
                        };

                        clientes.Add(cliente);
                    }
                }
            }

            return clientes;
        }

    }
}
