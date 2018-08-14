using System.Collections.Generic;

namespace JensCafe.Core.Model
{
    public class MenuItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public int PrepTime { get; set; }
        public List<string> Ingredients { get; set; }
        public bool IsFavorite { get; set; }
        public string GroupName { get; set; }
    }
}