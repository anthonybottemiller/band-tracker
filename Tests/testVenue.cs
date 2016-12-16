using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest
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
  }
}