
namespace Positioner.Models
{
    internal interface IEntityRepository
    {
        //void Add(Entity entity);

        IEntity Get(string id);

        List<IEntity> GetAll();

        //Entity Update(Entity entity);
    }
}
