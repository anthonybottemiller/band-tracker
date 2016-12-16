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
    }
  }
}