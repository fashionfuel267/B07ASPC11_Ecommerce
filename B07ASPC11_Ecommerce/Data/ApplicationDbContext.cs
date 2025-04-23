using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B07ASPC11_Ecommerce.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category>Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
public class Product
{
    public int Id { get; set; }
    [ForeignKey("Category")]
    public int CatId { get; set; }
    [StringLength(20)]
    public string Name { get; set; }
    [StringLength(120)]
    [ValidateNever]
    public string ImagePath { get; set; }
    [NotMapped]
    [ValidateNever]
    public IFormFile Image { get; set; }
    [MaxLength]
    public string Description { get; set; }
    public Category Category { get; set; }

}

public class Category
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    [StringLength(20)]
    public string Name { get; set; }
    [StringLength(120)]
    [ValidateNever]
    public string ImagePath { get; set; }
    [NotMapped]
    [ValidateNever]
    public IFormFile Image { get; set; }

}