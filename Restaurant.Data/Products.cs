namespace Restaurant.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int SortOrder { get; set; }

        [StringLength(512)]
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public int CategoryID { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
