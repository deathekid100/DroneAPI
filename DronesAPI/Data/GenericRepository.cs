﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace DronesAPI.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DroneDBContext _context;

        public GenericRepository(DroneDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<T> GetAll() => _context.Set<T>();

        public T Get(int id) => _context.Set<T>().Find(id);

        public T Find(Expression<Func<T, bool>> match) => _context.Set<T>().SingleOrDefault(match);

        public ICollection<T> FindAll(Expression<Func<T, bool>> match) => _context.Set<T>().Where(match).ToList();

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);

        public async Task<ICollection<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T t) => await _context.Set<T>().AddAsync(t);

        public async Task<T> FindAsync(Expression<Func<T, bool>> match) => await _context.Set<T>().SingleOrDefaultAsync(match);

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match) => await _context.Set<T>().Where(match).ToListAsync();

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task UpdateAsync(T t, object key)
        {
            T exist = null;

            if (key.GetType() == typeof(int[]))
            {
                exist = await _context.Set<T>().FindAsync(((int[])key)[0], ((int[])key)[1]);
            }
            else
            {
                exist = await _context.Set<T>().FindAsync(key);
            }
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
            }
        }

        public void Update(T t) => _context.Set<T>().Update(t);

        public async Task<int> CountAsync() => await _context.Set<T>().CountAsync();

        public async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().Where(predicate).ToListAsync();

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        public async Task AddRangeAsync(List<T> t) => await _context.Set<T>().AddRangeAsync(t);

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}
