using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartWallet.DAL.Repository;

public class Repository<T> : IRepository<T> where T: class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet;
    }

    public T Get(int id)
    {
        return _dbSet.Find(id);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
}