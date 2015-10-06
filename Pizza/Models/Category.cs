using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public string addition1 { get; set; }
        public string addition2 { get; set; }
        public List<Item> items { get; set; }


    }

    public class CategoryViewModelList
    {
        public List<Category> items { get; set; }
        public int countItems { get; set; }
    }
}