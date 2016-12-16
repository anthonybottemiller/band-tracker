using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var bands = Band.GetAll();
        var venues = Venue.GetAll();
        model.Add("bands", bands);
        model.Add("venues", venues);
        return View["index.cshtml", model];
      };

      Get["/venues/new"] = _ => {
        return View["venue-form.cshtml"];
      };

      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue foundVenue = Venue.Find(parameters.id);
        var bands = foundVenue.GetBands();
        model.Add("venue", foundVenue);
        model.Add("bands", bands);
        return View["venue.cshtml", model];
      };

      Get["/bands/new"] = _ => {
        return View["band-form.cshtml"];
      };

      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band foundBand = Band.Find(parameters.id);
        var venues = foundBand.GetVenues();
        model.Add("band", foundBand);
        model.Add("venues", venues);
        return View["band.cshtml", model];
      };
    }
  }
}