using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class DB
  {
    public static SqlConnection Connection()
    {
      SqlConnection connection = new SqlConnection(DBConfiguration.ConnectionString);
      return connection;
    }
  }
}