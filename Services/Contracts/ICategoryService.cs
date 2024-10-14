using Entities.Models;
namespace Services.Contracts;


public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories(bool trackChanges);
    Category ? GetOneCategory(int id , bool trackChanges);
}