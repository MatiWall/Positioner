using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Positioner.Models;
using System.Drawing;


namespace Positioner.Serives
{
    internal class BrowserSetupServiceV1: IEntityHandler
    {
        public string Kind => "Browser";
        public string Version => "V1";

        public void Handle(IEntity entity) {

            if (entity.Spec is not BrowserSpecV1 spec)
            {
                throw new InvalidOperationException("Invalid spec type for BrowserSetupServiceV1");
            }

            if (spec.Urls == null || spec.Urls.Count == 0)
            {
                Console.WriteLine("No urls specified in spec");
                return;
            }

            Position pos = spec.Position;


            string url = spec.Urls[0];


        }
    }
}
