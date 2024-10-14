using Entities.Models;
using Repositories.Contracts;

namespace Repositories.Models;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Category> GetAllCategories(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public Category? GetOneCategory(int id ,bool trackChanges)
    {
        return FindByCondition(c=>c.CategoryId.Equals(id),trackChanges);
    }
}