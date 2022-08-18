﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.WebApi.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public string ImageUrl { get; set; }
        public int ProductCount { get; set; }
        public bool HasSubCategories { get; set; }
    }
}