using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string Name, int Id = 0)
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

    public void Save()
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("INSERT INTO bands OUTPUT INSERTED.id VALUES (@BandName);", connection);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BandName";
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

    public static Band Find(int id)
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("SELECT * From bands WHERE id = @BandId;", connection);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = id.ToString();
      command.Parameters.Add(bandIdParameter);
      SqlDataReader reader = command.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;

      while(reader.Read())
      {
        foundBandId = reader.GetInt32(0);
        foundBandName = reader.GetString(1);
      }

      Band foundBand = new Band(foundBandName, foundBandId);

      if (reader != null)
      {
        reader.Close();
      }

      if (connection != null)
      {
        connection.Close();
      }
      return foundBand;
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};

      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("SELECT * FROM bands;", connection);
      SqlDataReader reader = command.ExecuteReader();

      while(reader.Read())
      {
        int bandId = reader.GetInt32(0);
        string bandName = reader.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }

      if (reader != null)
      {
        reader.Close();
      }

      if (connection != null)
      {
        connection.Close();
      }

      return allBands;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("INSERT INTO venues_bands (venue_id, band_id) VALUES (@VenueId, @BandId);", connection);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = newVenue.GetId();
      command.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();
      command.Parameters.Add(bandIdParameter);

      command.ExecuteNonQuery();

      if (connection != null)
      {
        connection.Close();
      }
    }

    public List<Venue> GetVenues()
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand command = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands ON (bands.id = venues_bands.band_id) JOIN venues ON (venues_bands.venue_id = venues.id) WHERE bands.id = @BandId;", connection);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();

      command.Parameters.Add(bandIdParameter);

      SqlDataReader reader = command.ExecuteReader();

      var venues = new List<Venue>{};

      while(reader.Read())
      {
        int venueId = reader.GetInt32(0);
        string venueName = reader.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        venues.Add(newVenue);
      }

      if (reader != null)
      {
        reader.Close();
      }

      if (connection != null)
      {
        connection.Close();
      }
      return venues;
    }

    public static void DeleteAll()
    {
    SqlConnection connection = DB.Connection();
    connection.Open();
    SqlCommand command = new SqlCommand("DELETE FROM bands;", connection);
    command.ExecuteNonQuery();
    connection.Close();
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = this.GetId() == newBand.GetId();
        bool nameEquality = this.GetName() == newBand.GetName();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}