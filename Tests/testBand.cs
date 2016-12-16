using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTest
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

  }
}