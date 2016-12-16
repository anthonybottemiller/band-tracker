using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Venue_Equal_ReturnsTrueIfNamesAreTheSame_True()
    {
      Venue firstVenue = new Venue("Roseland");
      Venue secondVenue = new Venue("Roseland");

      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Venue_GetAll_EmptyAtFirst()
    {
      int result = Venue.GetAll().Count;

      Assert.Equal(0, result);
    }
    
    [Fact]
    public void Venue_Save_SavesVenueToDatabase()
    {
      Venue testVenue = new Venue("Roseland");
      testVenue.Save();

      Venue savedVenue = Venue.GetAll()[0];

      Assert.Equal(testVenue, savedVenue);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
    }
  }
}