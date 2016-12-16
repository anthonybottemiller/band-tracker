using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("SELECT * FROM venues;", connection);
      SqlDataReader reader = command.ExecuteReader();

      while(reader.Read())
      {
        int venueId = reader.GetInt32(0);
        string venueName = reader.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }

      if (reader != null)
      {
        reader.Close();
      }

      if (connection != null)
      {
        connection.Close();
      }

      return allVenues;
    }

    public void Save()
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("INSERT INTO venues OUTPUT INSERTED.id VALUES (@VenueName);", connection);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@VenueName";
      nameParameter.Value = this.GetName();
      command.Parameters.Add(nameParameter);
      SqlDataReader reader = command.ExecuteReader();

      while(reader.Read())
      {
        this._id = reader.GetInt32(0);
      }

      if (reader != null)
      {
        reader.Close();
      }

      if (connection != null)
      {
        connection.Close();
      }
    }

    public static void DeleteAll()
    {
    SqlConnection connection = DB.Connection();
    connection.Open();
    SqlCommand cmd = new SqlCommand("DELETE FROM venues;", connection);
    cmd.ExecuteNonQuery();
    connection.Close();
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = this.GetId() == newVenue.GetId();
        bool nameEquality = this.GetId() == newVenue.GetId();
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}