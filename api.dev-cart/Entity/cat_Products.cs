//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class cat_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cat_Products()
        {
            this.cat_Product_Galery_Images = new HashSet<cat_Product_Galery_Images>();
            this.cat_Reviews = new HashSet<cat_Reviews>();
        }
    
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public Nullable<decimal> Product_Discount { get; set; }
        public Nullable<int> Category_Id { get; set; }
        public string Product_Img { get; set; }
        public string Product_Description { get; set; }
        public string Product_Configurations { get; set; }
        public Nullable<System.DateTime> Product_Creation_Date { get; set; }
        public bool Product_Released { get; set; }
        public int Product_Stock { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cat_Product_Galery_Images> cat_Product_Galery_Images { get; set; }
        public virtual cat_Categories cat_Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cat_Reviews> cat_Reviews { get; set; }
    }
}
