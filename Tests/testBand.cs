using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void BandsEmptyAtFirst()
    {
      int result = Band.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Band_Equal_ReturnsTrueIfNamesAreSame_True()
    {
      Band firstBand = new Band("The Soft Pack");
      Band secondBand = new Band("The Soft Pack");

      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Band_Save_SavesBandToDatabase()
    {
      Band testBand = new Band("The Soft Pack");
      testBand.Save();

      Band savedBand = Band.GetAll()[0];

      Assert.Equal(testBand, savedBand);
    }

    [Fact]
    public void Band_Save_SaveAssignsIdToObject()
    {
      Band testBand = new Band("The Soft Pack");
      testBand.Save();

      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      Assert.Equal(result, testId);
    }

    [Fact]
    public void Band_Find_FindBandInDatabase()
    {
      Band testBand = new Band("The Soft Pack");
      testBand.Save();

      Band foundBand = Band.Find(testBand.GetId());

      Assert.Equal(testBand, foundBand);
    }

    [Fact]
    public void Band_AddVenue_AddAssociationThatBandPlayedVenue()
    {
      Band testBand = new Band("The Roseland");
      testBand.Save();

      Venue testVenueZero = new Venue("Roseland");
      testVenueZero.Save();

      Venue testVenueOne = new Venue("Crystal Ballroom");
      testVenueOne.Save();

      testBand.AddVenue(testVenueZero, testVenueOne);
      List<Venue> savedVenues = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenueZero};

      Assert.Equal(testList, savedVenues);
    }

    [Fact]
    public void Band_GetVenues_AddAssociationThatBandPlayedVenue()
    {
      Band testBand = new Band("The Roseland");
      testBand.Save();

      Venue testVenueZero = new Venue("Roseland");
      testVenueZero.Save();

      Venue testVenueOne = new Venue("Crystal Ballroom");
      testVenueOne.Save();

      testBand.AddVenue(testVenueZero);
      List<Venue> savedVenues = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenueZero};

      Assert.Equal(testList, savedVenues);
    }

    public void Dispose()
    {
      Band.DeleteAll();
    }
  }
}