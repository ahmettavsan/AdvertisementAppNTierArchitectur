using AdvertisementApp.Common;
using AppAdvertisement.DataAccess.Contexts;
using AppAdvertisement.DataAccess.Interfaces;
using AppAdvertisement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DataAccess.Repositories
{
    public class Repository<T>:IRepository<T> where T : BaseEntity
    {
        private readonly AdvertisementContext _context;

        public Repository(AdvertisementContext context)
        {
            _context = context;
        }
        //bütün veriyi getirme
        //bütün veriyi sıralayarak getirme
        //bütün veriyi filter getirme
        //asnotracking
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector,OrderByType orderByType=OrderByType.Desc)
        {
            return orderByType == OrderByType.Asc ? await _context.Set<T>().OrderBy(selector).AsNoTracking().ToListAsync() : await _context.Set<T>().OrderByDescending(selector).ToListAsync();
        }
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T,bool>> filter,Expression<Func<T,TKey>>selector,OrderByType orderByType = OrderByType.Desc)
        {
            return orderByType==OrderByType.Asc?await _context.Set<T>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync():
                await _context.Set<T>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }
        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> GetByFilterAsync(Expression<Func<T,bool>>filter,bool asNoTracking=false)
        {
            return !asNoTracking?await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter):
                await _context.Set<T>().SingleOrDefaultAsync(filter);
        }
        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void Update(T entity,T unChanged)
        {
            _context.Entry(unChanged).CurrentValues.SetValues(entity);
        }







    }
}
