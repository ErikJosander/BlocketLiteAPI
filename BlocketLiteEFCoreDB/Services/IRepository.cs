using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlocketLiteEFCoreDB.Services
{
    /// <summary>
    /// A generic> interface. Has a couple of generic methodes.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the <see cref="TEntity"/> when a correct <paramref name="id"/> is supplied. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <typeparamref name="TEntity"/>.</returns>
        TEntity Get(int id);

        /// <summary>
        /// Gets the <see cref="TEntity"/> when a correct <paramref name="id"/> is supplied. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <typeparamref name="TEntity"/>.</returns>
        TEntity Get(string id);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of the <see cref="TEntity"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TEntity> GetAll();

        // Do we need this?
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Adds a <see cref="TEntity"/> to the DB.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds a <see cref="IEnumerable{T}"/> of <see cref="TEntity"/> to the DB.
        /// </summary>
        /// <param name="entities"></param>
        void AddRage(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes a <see cref="TEntity"/> from the DB.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);


        /// <summary>
        /// Removes an <see cref="IEnumerable{T}"/> of <see cref="TEntity"/> from the DB.
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Saves changes made in the <see cref="DbContexts"/> to the DB.
        /// </summary>
        void Save();

    }
}
