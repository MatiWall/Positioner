using Positioner.Models;
using Positioner.Repositories;
using Xunit;

namespace Positioner.Tests
{
    public class FileEntityRepositoryTests
    {
        private readonly string _tmpDir;
        private readonly FileEntityRepository _repo;

        public FileEntityRepositoryTests(){
            _tmpDir = "./TestData";
            
            Directory.CreateDirectory(_tmpDir);

            _repo = new Repositories.FileEntityRepository(_tmpDir);
            
        }

        [Fact]
        public void TestGetAll()
        {
            List<IEntity> entities = _repo.GetAll();

            foreach (IEntity entity in entities)
            {
                Assert.Equal("uuid", entity.Metadata.Id);
            }
        }
    }
}