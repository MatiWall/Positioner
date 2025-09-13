using Positioner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Positioner.Repositories
{
    public class FileEntityRepository : IEntityRepository
    {
        public readonly string _path;
        public FileEntityRepository(string path) {
            _path = path;


        }

        private string FileName(string id)
        {
            return System.IO.Path.Combine(_path, $"{id}.json");
        }

        public List<IEntity> GetAll() {

            if (!Directory.Exists(_path)) {
                throw new DirectoryNotFoundException($"Directory {_path} does not exists");
            }


            List<IEntity> entities = new List<IEntity>();
            foreach (var file in Directory.GetFiles(_path, "*.json")) { 
                string jsonString = File.ReadAllText(file);  
                
                entities.Add(ParseEntity(jsonString));

            }

            return entities;


        }

        public IEntity Get(string id) { 
            string file = FileName(id);
            string jsonString = File.ReadAllText(file);
            return ParseEntity(jsonString);
        }
        private IEntity ParseEntity(string jsonString)
        {
            using var doc = JsonDocument.Parse(jsonString);
            var kind = doc.RootElement.GetProperty("kind").GetString();
            var version = doc.RootElement.GetProperty("version").GetString();

            IEntity? entity = kind switch
            {
                "Browser" => JsonSerializer.Deserialize<Entity<BrowserSpecV1>>(jsonString),
                _ => null
            };

            if (entity == null) {
                throw new Exception("Invalid Entity");
            }

            return entity;
        }

    }
}
