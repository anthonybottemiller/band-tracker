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

    [Fact]
    public void Venue_Save_SaveAssignsIdToObject()
    {
      Venue testVenue = new Venue("Roseland");
      testVenue.Save();

      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      Assert.Equal(result, testId);
    }

    [Fact]
    public void Venue_Find_FindVenueInDatabase()
    {
      Venue testVenue = new Venue("Roseland");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.GetId());

      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Venue_Delete_DeleteVenueInDatabase()
    {
      Venue testVenueZero = new Venue("Roseland");
      testVenueZero.Save();
      Venue testVenueOne = new Venue("Crystal Ballroom");
      testVenueOne.Save();

      Venue.Delete(testVenueOne.GetId());
      List<Venue> testList = new List<Venue>{testVenueZero};
      var foundVenues = Venue.GetAll();

      Assert.Equal(testList, foundVenues);
    }

    [Fact]
    public void Venue_UpdateName_UpdateVenueInDatabase()
    {
      Venue testVenue = new Venue("Roseland");
      testVenue.Save();
      string newName = "Roseworld";

      testVenue.UpdateName(newName);

      string result = testVenue.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void Venue_AddBand_AddAssociationsThatBandsPlayedVenue()
    {
      Venue testVenue = new Venue("Roseland");
      testVenue.Save();

      Band testBandZero = new Band("The Soft Pack");
      testBandZero.Save();

      Band testBandOne = new Band("Coheed and Cambria");
      testBandOne.Save();

      testVenue.AddBand(testBandZero);
      testVenue.AddBand(testBandOne);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBandZero, testBandOne};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Venue_GetBands_ReturnsAllBandsThatPlayedVenue()
    {
      Venue testVenue = new Venue("Roseland");
      testVenue.Save();

      Band testBandZero = new Band("The Soft Pack");
      testBandZero.Save();

      Band testBandOne = new Band("Coheed and Cambria");
      testBandOne.Save();

      testVenue.AddBand(testBandZero);
      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBandZero};

      Assert.Equal(testList, savedBands);
    }
    
    public void Dispose()
    {
      Venue.DeleteAll();
    }
  }
}