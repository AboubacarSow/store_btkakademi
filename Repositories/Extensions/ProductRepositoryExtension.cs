using Entities.Models;

namespace Repositories.Extensions;

public static class ProductRepositoryExtension 
{

    public static IQueryable<Product> FilterByCategoryId(this IQueryable <Product> products, int? catergoryId)
     {
        if(catergoryId is null)
            return products;
        else
            return products.Where(p => p.CategoryId.Equals(catergoryId));
     }
    public static IQueryable<Product > FilterBySearchTerm(this IQueryable <Product> products, string ? searchTerm)
     {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return products;
        else
        {
            return products?.Where(p=> p.ProductName.ToLower()
                            .Contains(searchTerm.ToLower()));
        }
        
     }
    public static IQueryable<Product> FilterByPrice(this IQueryable<Product> products,
     int? minPrice, int? maxPrice, bool isValidPrice)
     {
        if(isValidPrice)
            return products.Where(p => p.Price >= minPrice && p.Price<= maxPrice);
        else
            return products;
     }
    public static IQueryable< Product > ToPaginate (this IQueryable<Product > products , 
        int pageNumber , int pageSize)
      {
         return products
                 .Skip((pageNumber-1)*pageSize)
                 .Take(pageSize);

      }
}