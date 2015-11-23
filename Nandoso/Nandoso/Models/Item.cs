using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nandoso.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public bool vegetarian { get; set; }
        public bool glutenFree { get; set; }
        //public string image { get; set; }
        public double discount { get; set; }
    }
}