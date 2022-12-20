using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Base.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDBContext _context;
        public EntityBaseRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            /* or
                        var delData=await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
                         _context.Set<T>().Remove(delData);
                      await  _context.SaveChangesAsync();

             */
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var Result = await _context.Set<T>().ToListAsync();
            return Result;

           
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            //Getting data from 1 entity
            IQueryable<T> query = _context.Set<T>();
            /*we want to get all the properties  from includeProperties array so aggregate thefirst parameter  of aggregate
             is going to be query itself and the second parameter is going to be the function that is going to be use 
            all includeProperties  
            */
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
           
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var Data = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return Data;

          
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry =  _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            /* 
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
              _context.Set<T>().Update(entity);
             await _context.SaveChangesAsync();

             */
        }
    }
}
