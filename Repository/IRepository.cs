namespace Repository;

using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public interface IRepository<T> where T : PersistentObject
{
    IQueryable<T> Get();
    Task<IQueryable<T>> GetAsync();
    IQueryable<T> GetAll();
    Task<IQueryable<T>> GetAllAsync();
    void Create(T item);
    Task CreateAsync(T item);
    Task AddRange(IEnumerable<T> newEntities);
    void Delete(T entity);
    void Remove(T entity);
    Task RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    Task UpdateRange(IEnumerable<T> entities);
    Task<int> SaveChangesAsync();
    int SaveChanges();
}
