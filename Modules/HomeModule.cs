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

      Post["/venues/new"] = _ => {
        string newName = Request.Form["venue-name"];
        Venue newVenue = new Venue(newName);
        newVenue.Save();
        return View["new-venue-confirm.cshtml"];
      };

      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue foundVenue = Venue.Find(parameters.id);
        var allBands = Band.GetAll();
        var bands = foundVenue.GetBands();
        model.Add("venue", foundVenue);
        model.Add("bands", allBands);
        model.Add("bandsPlayedVenue", bands);
        return View["venue.cshtml", model];
      };

      Post["/venues/{id}/add_band"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        Band foundBand = Band.Find(Request.Form["band-id"]);
        foundVenue.AddBand(foundBand);

        Dictionary<string, object> model = new Dictionary<string, object>();
        var allBands = Band.GetAll();
        var bands = foundVenue.GetBands();
        model.Add("venue", foundVenue);
        model.Add("bands", allBands);
        model.Add("bandsPlayedVenue", bands);
        return View["venue.cshtml", model];
      };

      Get["/bands/new"] = _ => {
        return View["band-form.cshtml"];
      };

      Post["/bands/new"] = _ => {
        string newName = Request.Form["band-name"];
        Band newBand = new Band(newName);
        newBand.Save();
        return View["new-band-confirm.cshtml"];
      };

      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band foundBand = Band.Find(parameters.id);
        var venuesPlayed = foundBand.GetVenues();
        var allVenues = Venue.GetAll();
        model.Add("band", foundBand);
        model.Add("venues", allVenues);
        model.Add("venuesPlayed", venuesPlayed);
        return View["band.cshtml", model];
      };

      Post["/bands/{id}/add_venue"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        Venue foundVenue = Venue.Find(Request.Form["venue-id"]);
        foundBand.AddVenue(foundVenue);

        Dictionary<string, object> model = new Dictionary<string, object>();
        var allVenues = Venue.GetAll();
        var venuesPlayedByBand = foundBand.GetVenues();
        model.Add("band", foundBand);
        model.Add("venues", allVenues);
        model.Add("venuesPlayed", venuesPlayedByBand);
        return View["band.cshtml", model];
      };
    }
  }
}