using System.Collections.Generic;

namespace SmartWallet.DAL.Repository;

public interface IRepository<T> where T: class
{
    void Add(T entity);
    void Remove(T entity);
    IEnumerable<T> GetAll();
    T Get(int id);
    void Update(T entity);
}