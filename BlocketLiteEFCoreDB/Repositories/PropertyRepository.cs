using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;

namespace BlocketLiteEFCoreDB.Repositories
{
    /// <summary>
    /// Repository that implements the <see cref="IPropertyRepository"/>
    /// </summary>
    public class PropertyRepository : Repository<PropertyType>, IPropertyRepository
    {
        private readonly BlocketLiteContext _context;

        // Constructor
        public PropertyRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

