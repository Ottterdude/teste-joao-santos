using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Domain.Interfaces;
using Prova_CRM_Joao_Santos.Infra;

namespace Prova_CRM_Joao_Santos.Infrastructure.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly VestibularContext _vestibularContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(VestibularContext context)
        {
            _vestibularContext = context;
            _dbSet = context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _vestibularContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _vestibularContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _vestibularContext.SaveChanges();
            }
        }
    }
}
