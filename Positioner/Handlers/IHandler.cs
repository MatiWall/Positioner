using Positioner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Positioner.Models;
using System.Runtime.CompilerServices;
namespace Positioner.Serives
{
    public interface IEntityHandler
    {
        public string Kind { get; }
        public string Version { get; }
        public void Handle(IEntity entity);
    }
}
