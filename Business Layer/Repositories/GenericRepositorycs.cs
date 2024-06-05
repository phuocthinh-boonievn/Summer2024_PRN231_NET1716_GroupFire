using Business_Layer.Commons;
using Business_Layer.DataAccess;
using Business_Layer.Services;
using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;


namespace Business_Layer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly FastFoodDeliveryDBContext _context;

        
        public GenericRepository(FastFoodDeliveryDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();
        

        public async Task AddAsync(TEntity entity)
        {            
            await _context.Set<TEntity>().AddAsync(entity);            
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }                

        public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _context.Set<TEntity>().CountAsync();
            var items = await _context.Set<TEntity>().Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            //var stringId = id.ToString();
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
