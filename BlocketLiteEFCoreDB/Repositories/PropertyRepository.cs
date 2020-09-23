using BlocketLiteEFCoreDB.DbContexts;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using System;

namespace BlocketLiteEFCoreDB.Repositories
{
    public class PropertyRepository : Repository<PropertyType>, IPropertyRepository
    {
        private readonly BlocketLiteContext _context;

        public PropertyRepository(BlocketLiteContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

