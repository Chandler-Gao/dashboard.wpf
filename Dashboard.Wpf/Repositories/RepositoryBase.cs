using Microsoft.Data.SqlClient;

namespace Dashboard.Wpf.Repositories;

public class RepositoryBase
{
    private readonly string _connectionString;

    public RepositoryBase()
    {        
        _connectionString = "Server=(local); Database=MVVMLoginDb; Integrated Security=true; TrustServerCertificate=True;";
    }

    protected SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
