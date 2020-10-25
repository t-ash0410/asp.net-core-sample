using System;
using MySql.Data.MySqlClient;

namespace Lib.Infrastructure
{
  public class DBContext
  {
    private string _connectionString;
    public MySqlConnection Connection { get; private set; }
    public MySqlCommand Command { get; private set; }

    public DBContext(string connectionString)
    {
      this._connectionString = connectionString;
    }

    public void Open()
    {
      try
      {
        this.Connection = new MySqlConnection()
        {
          ConnectionString = this._connectionString
        };
        this.Connection.Open();
        this.Command = this.Connection.CreateCommand();
      }
      catch
      {
        this.Close();
        throw;
      }
    }

    public void Close()
    {
      if (this.Command != null)
      {
        this.Command.Dispose();
      }
      if (this.Connection != null)
      {
        this.Connection.Dispose();
      }
    }
  }
}
