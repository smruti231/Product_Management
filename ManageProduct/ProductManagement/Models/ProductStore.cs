using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ProductManagement.Models
    {
    [Table("tblProduct")]
    public class Product
        {
        [Key]
        public int pId { get; set; }
        public string pName { get; set; } = string.Empty;
        public int pPrice { get; set; }
        public int pRating { get; set; }
        public int pStock { get; set; }
        }

    class ProductContext : DbContext
        {
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"Data Source=W-674PY03-2;Initial Catalog=SMRUTI_db;Persist Security Info=True;User ID=SA;Password=Password@123456-=; TrustServerCertificate = True";
            optionsBuilder.UseSqlServer(connectionString);
            }

        }

    interface IProductManager
        {
        void AddNewProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(int id, Product product);
        List<Product> GetAllProducts();
        Product GetProducts(int id);
        List<Product> GetAllProducts(string name);
        }

    class ProductManager : IProductManager
        {
        private ProductContext context;
        public ProductManager()
            {
            context = new ProductContext();
            }
        public  void AddNewProduct(Product product)
            {
            context.Products.Add(product);
            context.SaveChanges();
            }

        public void DeleteProduct(int id)
            {

            var product = context.Products.Find(id);

            if (product == null)
                {
                throw new Exception("Product not found to delete");
                }
            context.Products.Remove(product);
            context.SaveChanges();
            }

        public List<Product> GetAllProducts()
            {
            return context.Products.ToList();
            }

        public List<Product> GetAllProducts(string name)
            {
            return context.Products.Where((p) => p.pName.Contains(name)).ToList();
            }

        public Product GetProducts(int id)
            {
            return context.Products.Find(id) ?? throw new Exception("Product not found");
            }

        public void UpdateProduct(int id, Product product)
            {
            var found = context.Products.Find(id);
            if (found != null)
                {
                found.pName = product.pName;
                found.pPrice = product.pPrice;
                found.pRating = product.pRating;
                found.pStock = product.pStock;
                context.SaveChanges();
                }
            else
                throw new Exception("Product not found to Update");
            }
        }
    }