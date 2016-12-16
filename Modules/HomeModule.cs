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

      Get["/venue/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue foundVenue = Venue.Find(parameters.id);
        var bands = foundVenue.GetBands();
        model.Add("venue", foundVenue);
        model.Add("bands", bands);
        return View["venue.cshtml", model];
      };
    }
  }
}