﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace api.dev_cart.Entity
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base(ConnectionString())
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        protected static string ConnectionString()
        {
            string dataSource = ConfigurationManager.AppSettings["DataSource"];
            string initialCatalog = ConfigurationManager.AppSettings["InitialCatalog"];
            string userId = ConfigurationManager.AppSettings["UserId"];
            string password = ConfigurationManager.AppSettings["Password"];
            
            string ConnectionString = ConfigurationManager.ConnectionStrings["Entities"].ConnectionString.Replace("data source=localhost", $"data source={dataSource}");
            ConnectionString = ConnectionString.Replace("initial catalog=CMS_Dev-Cart", $"initial catalog={initialCatalog}");
            ConnectionString = ConnectionString.Replace("user id=admin", $"user id={userId}");
            ConnectionString = ConnectionString.Replace("password=root", $"password={password}");
            return ConnectionString;
        }
    
        public virtual DbSet<cat_About_Us_Sections> cat_About_Us_Sections { get; set; }
        public virtual DbSet<cat_Admin_Login> cat_Admin_Login { get; set; }
        public virtual DbSet<cat_Carts> cat_Carts { get; set; }
        public virtual DbSet<cat_Configurations> cat_Configurations { get; set; }
        public virtual DbSet<cat_Coupons> cat_Coupons { get; set; }
        public virtual DbSet<cat_Notice_Privacy> cat_Notice_Privacy { get; set; }
        public virtual DbSet<cat_Offers_Image> cat_Offers_Image { get; set; }
        public virtual DbSet<cat_Orders> cat_Orders { get; set; }
        public virtual DbSet<cat_Product_Galery_Images> cat_Product_Galery_Images { get; set; }
        public virtual DbSet<cat_Sepomex> cat_Sepomex { get; set; }
        public virtual DbSet<cat_Slider_Images> cat_Slider_Images { get; set; }
        public virtual DbSet<cat_Social_Media> cat_Social_Media { get; set; }
        public virtual DbSet<cat_Categories> cat_Categories { get; set; }
        public virtual DbSet<cat_Reviews> cat_Reviews { get; set; }
        public virtual DbSet<vw_Orders> vw_Orders { get; set; }
        public virtual DbSet<vw_Sepomex_Info> vw_Sepomex_Info { get; set; }
        public virtual DbSet<cat_Products> cat_Products { get; set; }
        public virtual DbSet<vw_Products> vw_Products { get; set; }
        public virtual DbSet<cat_Specific_Rules> cat_Specific_Rules { get; set; }
        public virtual DbSet<cat_Banks> cat_Banks { get; set; }
    }
}
