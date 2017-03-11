using System.Web.Mvc;

namespace TravelGuide.App_Start
{
    public class ViewEnginesConfig
    {
        public static void RegisterEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}