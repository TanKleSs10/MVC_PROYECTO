using System.Data.OleDb;
using Microsoft.Extensions.Configuration;

public class AccessDbConnection
{
    private static AccessDbConnection _instance;
    private static readonly object _lock = new object();
    private OleDbConnection _connection;

    // Constructor privado para evitar la creación directa
    private AccessDbConnection(IConfiguration configuration)
    {
        // Lee la cadena de conexión desde appsettings.json
        string connectionString = configuration.GetConnectionString("AccessDbConnection");
        _connection = new OleDbConnection(connectionString);
        _connection.Open();
    }

    // Obtiene la instancia Singleton
    public static AccessDbConnection GetInstance(IConfiguration configuration)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new AccessDbConnection(configuration);
                }
            }
        }
        return _instance;
    }

    // Devuelve la conexión OleDb
    public OleDbConnection GetConnection()
    {
        return _connection;
    }
}
