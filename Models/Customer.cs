using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    // ---------------- CUSTOMER ----------------
    public class Customer
    {
        public int CustomerId { get; set; }
       // public int CustomerName { get; set; }

        [Required, StringLength(200)]
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; }

        // NOTE: don't store plain text passwords in production.
        // Store a password hash (and salt) instead.
        [Required]
        public string Password { get; set; }

        // Use an interface type and initialize the collection to avoid null refs.
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }

    // ---------------- PRODUCT ----------------
    public class Product
    {
        public int ProductId { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        // specify precision/scale in EF Core fluent API or data annotation if needed
        public decimal Price { get; set; }

        // initialize collection
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

    // ---------------- ORDER ----------------
    public class Order
    {
        public int OrderId { get; set; }

        // prefer UTC in servers; you can use DateTime.UtcNow when creating orders
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // FK and navigation
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // initialize collection
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

    // ---------------- ORDER ITEM ----------------
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        // FK and navigation
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
