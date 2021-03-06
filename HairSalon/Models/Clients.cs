using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private int _id;
    private int _stylistId;

    public Client(string name,int stylistId, int id = 0)
    {
      _name = name;
      _id = id;
      _stylistId = stylistId;

    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }

    public static List<Client> GetAll()
      {
      List<Client> allClients = new List<Client>{ };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
        {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistID = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistID, clientId );
        allClients.Add(newClient);
        }
        if(conn != null)
        {
          conn.Dispose();
        }
      return allClients;
      }

      public static void ClearAll()
        {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"DELETE FROM clients;";
       cmd.ExecuteNonQuery();
       conn.Close();
         if (conn != null)
         {
          conn.Dispose();
         }
        }
    public static Client Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        Console.WriteLine("Find {0}", id);
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int ClientId = 0;
        string ClientName = "";
        int ClientStylistID = 0;
        while(rdr.Read())
        {
          ClientId = rdr.GetInt32(0);
          ClientName = rdr.GetString(1);
          ClientStylistID = rdr.GetInt32(2);
        }
        Client newClient = new Client(ClientName, ClientStylistID, ClientId);
        Console.WriteLine("Found "+ClientName);
        Console.WriteLine("Found2 " + newClient.GetName());
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newClient;
      }
      public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @GetStylistId);";
          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@name";
          name.Value = this._name;
          cmd.Parameters.Add(name);
          MySqlParameter stylistId = new MySqlParameter();
          stylistId.ParameterName = "@GetStylistId";
          stylistId.Value = this._stylistId;
          cmd.Parameters.Add(stylistId);
          cmd.ExecuteNonQuery();
          _id = (int)cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
        }
      public override bool Equals(System.Object otherClient)
      {
        if (!(otherClient is Client))
        {
        return false;
      }
      else
        {
        Client newClient = (Client)otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool StylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && nameEquality && StylistEquality);
        }
      }
      public void Edit(string newName)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@newName";
        name.Value = newName;
        cmd.Parameters.Add(name);
        cmd.ExecuteNonQuery();
        _name = newName;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
      public void Delete()
      {
        Console.WriteLine("Delete method started");
        MySqlConnection conn = DB.Connection();
        conn.Open();
        Console.WriteLine("Delete {0}", _id);
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";
        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = _id;
        cmd.Parameters.Add(thisId);
        cmd.ExecuteNonQuery();
        conn.Close();

        if(conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
