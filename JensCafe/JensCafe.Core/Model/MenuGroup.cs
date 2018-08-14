using System.Collections.Generic;

namespace JensCafe.Core.Model
{
    public class MenuGroup
    {
        public int MenuGroupId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}