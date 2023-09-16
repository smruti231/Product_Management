using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagement.Models
    {
    class Program
        {
        static void Main(string[] args)
            {
            IProductManager productManager = new ProductManager();

            while (true)
                {
                Console.Clear();
                Console.WriteLine("Product Management System");
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. View All Products");
                Console.WriteLine("5. Search Products by Name");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string choice;

                try
                    {
                    choice = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(choice))
                        {
                        Console.WriteLine("Please enter a valid choice.");
                        continue;
                        }

                    switch (choice)
                        {
                        case "1":
                            AddProduct(productManager);
                            break;
                        case "2":
                            UpdateProduct(productManager);
                            break;
                        case "3":
                            DeleteProduct(productManager);
                            break;
                        case "4":
                            ViewAllProducts(productManager);
                            break;
                        case "5":
                            SearchProductsByName(productManager);
                            break;
                        case "6":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                        }
                    }
                catch (Exception ex)
                    {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                }
            }

        private static void SearchProductsByName(IProductManager productManager)
            {
            Console.Write("Enter product name to search: ");
            string name = Console.ReadLine();
            List<Product> products = productManager.GetAllProducts(name);

            if (products.Any())
                {
                Console.WriteLine("Matching products:");
                foreach (var product in products)
                    {
                    Console.WriteLine($"{product.pId}: {product.pName}, Price: {product.pPrice}, Rating: {product.pRating}, Stock: {product.pStock}");
                    }
                }
            else
                {
                Console.WriteLine("No matching products found.");
                }

            }

        private static void ViewAllProducts(IProductManager productManager)
            {
            List<Product> products = productManager.GetAllProducts();
            Console.WriteLine("List of all products:");
            foreach (var product in products)
                {
                Console.WriteLine($"{product.pId}: {product.pName}, Price: {product.pPrice}, Rating: {product.pRating}, Stock: {product.pStock}");
                }

            }

        private static void DeleteProduct(IProductManager productManager)
            {
            Console.Write("Enter the ID of the product to delete: ");
            int id = int.Parse(Console.ReadLine());
            Product existingProduct = productManager.GetProducts(id);

            if (existingProduct != null)
                {
                productManager.DeleteProduct(id);
                Console.WriteLine("Product deleted successfully.");
                }
            else
                {
                Console.WriteLine("Product not found.");
                }
            }

        private static void UpdateProduct(IProductManager productManager)
            {
            Console.Write("Enter the ID of the product to update: ");
            int id = int.Parse(Console.ReadLine());
            Product existingProduct = productManager.GetProducts(id);

            if (existingProduct != null)
                {
                Console.WriteLine($"Updating product: {existingProduct.pName}");
                Console.Write("New Name: ");
                string name = Console.ReadLine();
                Console.Write("New Price: ");
                int price = int.Parse(Console.ReadLine());
                Console.Write("New Rating: ");
                int rating = int.Parse(Console.ReadLine());
                Console.Write("New Stock: ");
                int stock = int.Parse(Console.ReadLine());

                existingProduct.pName = name;
                existingProduct.pPrice = price;
                existingProduct.pRating = rating;
                existingProduct.pStock = stock;

                productManager.UpdateProduct(id, existingProduct);
                Console.WriteLine("Product updated successfully.");
                }
            else
                {
                Console.WriteLine("Product not found.");
                }

            }

        private static void AddProduct(IProductManager productManager)
            {
            Console.WriteLine("Enter product details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            int price = int.Parse(Console.ReadLine());
            Console.Write("Rating: ");
            int rating = int.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            Product newProduct = new Product
                {
                pName = name,
                pPrice = price,
                pRating = rating,
                pStock = stock
                };

            productManager.AddNewProduct(newProduct);
            Console.WriteLine("Product added successfully.");

            }

        // The rest of your code for AddProduct, UpdateProduct, DeleteProduct, ViewAllProducts, and SearchProductsByName methods remains the same.
        // Ensure you have implemented these methods as shown in your original code.
        }
    }
