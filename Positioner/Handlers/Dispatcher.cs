using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Positioner.Models;

namespace Positioner.Serives
{
    internal class Dispatcher
    {
        private Dictionary<string, IEntityHandler> _handlers = new();

        public void Register(IEntityHandler handler)
        {
            string key = GenerateKey(handler.Kind, handler.Version);
            _handlers[key] = handler;
        }

        public void Dispatch(IEntity entity)
        {
            string key = GenerateKey(entity.Kind, entity.Version);

            if (_handlers.ContainsKey(key))
            {
                _handlers[key].Handle(entity);
            }
            else
            {
                throw new Exception($"No handler registered for {key}");
            }
        }

        private string GenerateKey(string kind, string version) {
            return $"{kind}:{version}";
        }
    }
}
