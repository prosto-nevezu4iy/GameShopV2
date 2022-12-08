using Core.Entities;
using Core.Interfaces;

namespace Catalog.Contracts.Entities
{
    public class Genre : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public Genre(string name)
        {
            Name = name;
        }
    }
}