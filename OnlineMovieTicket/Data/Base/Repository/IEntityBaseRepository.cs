using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Base.Repository
{
   public  interface IEntityBaseRepository<T> where T:class ,IEntityBase,new()
    {
        Task<IEnumerable<T>> GetAllAsync();
       
        /*this will return Inumumerable but metthod have parameter(params) ,expression which is linq expression,expression
         has a function ie:(params Expression<Func<entityType, object>>[]) to pass more than one expression so array[]
        ,naming thisparameter as includeProperties
         For GetAllMovies 
         */
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T ,Object>>[] includeProperties);

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);

    }
}
