using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Repositories;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// Interface that defins contracts for the <see cref="PropertyRepository"/>
    /// </summary>
    public interface IPropertyRepository : IRepository<PropertyType>
    {
    }
}
