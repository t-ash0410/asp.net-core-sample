using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Lib.Infrastructure
{
  public class DataAccessContext{
    private bool _initialized = false;
    private string _connectionString;
    public MySqlConnection Connection { get; private set; }
    public MySqlCommand Command { get; private set; }

    public DataAccessContext(string connectionString){
      this._connectionString = connectionString;
    }

    public void Open(){
      try{
        this.Connection = new MySqlConnection(){
          ConnectionString = this._connectionString
        };
        this.Connection.Open();
        this.Command = this.Connection.CreateCommand();
      }
      catch{
        if(this.Connection != null){
          this.Connection.Dispose();
        }
        throw;
      }
      this._initialized = true;
    }
  }
}
