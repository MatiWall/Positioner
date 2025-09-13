using Positioner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            foreach (var url in spec.Urls)
            {
                System.Diagnostics.Process.Start(url);  
            }
        }
    }
}
