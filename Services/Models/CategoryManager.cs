using Entities.Models;
using Services.Contracts;
using Repositories.Contracts;
namespace Services.Models;

public class CategoryManager : ICategoryService
{
    private readonly IRepositoryManager _manager;

    public CategoryManager(IRepositoryManager manager)
    {
        _manager= manager;
    }

    public IEnumerable<Category> GetAllCategories(bool trackChanges)
    {
        return _manager.Category.GetAllCategories(trackChanges);
    }
    public Category ? GetOneCategory(int id , bool trackChanges)
    {
        var category = _manager.Category.GetOneCategory(id, false);

         if(category==null)
            throw new Exception("This Category is not founded");
        else
            return category;
    }
}