using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.EntityFrameworkCore;

namespace Explorer.BuildingBlocks.Infrastructure.Database;

public class JsonCrudRepo<TEntity, TDbContext> : ICrudRepository<TEntity>
    where TEntity : JsonEntity
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    private readonly DbSet<TEntity> _dbSet;

    public JsonCrudRepo(TDbContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = DbContext.Set<TEntity>();
    }

    public PagedResult<TEntity> GetPaged(int page, int pageSize)
    {
        var task = _dbSet.GetPagedById(page, pageSize);
        task.Wait();

        task.Result.Results.ForEach(x => x.FromJson());
        return task.Result;
    }

    public TEntity Get(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        entity.FromJson();
        return entity;
    }

    public TEntity Create(TEntity entity)
    {
        entity.ToJson();
        _dbSet.Add(entity);
        DbContext.SaveChanges();
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        try
        {
            entity.ToJson();
            DbContext.Update(entity);
            DbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return entity;
    }

    public List<TEntity> GetFiltered(Predicate<TEntity> predicate)
    {
        var entities = _dbSet.ToList();
        entities.ForEach(x => x.FromJson());
        return entities.FindAll(predicate);
    }

    public void Delete(long id)
    {
        var entity = Get(id);
        _dbSet.Remove(entity);
        DbContext.SaveChanges();
    }
}