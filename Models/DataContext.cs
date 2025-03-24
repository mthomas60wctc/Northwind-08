using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public void AddCategory(Category category)
    {
        this.Add(category);
        this.SaveChanges();
    }
    public void DeleteCategory(Category category)
    {
        this.Remove(category);
        this.SaveChanges();
    }
    public void AddProduct(Product product)
    {
        this.Add(product);
        this.SaveChanges();
    }
    public void DeleteProduct(Product product)
    {
        this.Remove(product);
        this.SaveChanges();
    }
    public void AddCustomer(Customer customer){
        this.Add(customer);
        this.SaveChanges();
    }
}