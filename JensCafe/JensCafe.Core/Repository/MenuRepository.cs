using JensCafe.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace JensCafe.Core.Repository
{
    public class MenuRepository
    {
        private static List<MenuGroup> menuGroups = new List<MenuGroup>
        {
            new MenuGroup
            {
                MenuGroupId = 1,
                Title = "Breakfast",
                ImagePath = "imagePath",
                MenuItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        ItemId = 1,
                        Available = true,
                        ShortDescription = "French Toast - Vegetarian",
                        Description = "Breakfast isn't Complete without the Perfect Cinnamon Brioche French Toast. " +
                                      "Our French Toast comes with Fresh Berries and Powdered Sugar",
                        GroupName = "Breakfast",
                        ImagePath = "frenchToast",
                        Ingredients = new List<string> {"Brioche", "Eggs", "Cream", "Cinnamon", "Fresh Berries"},
                        IsFavorite = false,
                        Name = "Brioche French Toast",
                        PrepTime = 15,
                        Price = 10.99
                    },
                    new MenuItem
                    {
                        ItemId = 2,
                        Available = true,
                        ShortDescription = "Veggie Omelet - Vegetarian, Gluten-free",
                        Description = "Healthy Mediterranean Veggie Omelet with Feta Cheese",
                        GroupName = "Breakfast",
                        ImagePath = "omelet",
                        Ingredients = new List<string>{"Eggs", "Spinach", "Green Onion", "Tomatoes", "Feta Cheese"},
                        IsFavorite = false,
                        Name = "Mediterranean Omelet",
                        PrepTime = 10,
                        Price = 8.99
                    },
                    new MenuItem
                    {
                        ItemId = 3,
                        Available = true,
                        ShortDescription = "Caprese Eggs Benedict - Vegetarian",
                        Description = "Rich Caprese Eggs Benedict With Smoked Mozzarella",
                        GroupName = "Breakfast",
                        ImagePath = "eggsBenedict",
                        Ingredients = new List<string>{"Eggs", "Spinach", "Smoked Caprese", "Milk", "Tomatoes", "English Muffin"},
                        IsFavorite = false,
                        Name = "Caprese Eggs Benedict",
                        PrepTime = 15,
                        Price = 9.99
                    }
                }
            },
             new MenuGroup
            {
                MenuGroupId = 2,
                Title = "Lunch",
                ImagePath = "imagePath",
                MenuItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        ItemId = 4,
                        Available = true,
                        ShortDescription = "Hummus Wrap - Vegetarian, Low carb",
                        Description = "Heavenly Low carb Hummus Veggie Wrap",
                        GroupName = "Lunch",
                        ImagePath = "hummusWrap",
                        Ingredients = new List<string> {"Spinach Tortilla", "Fresh Greens", "Onion", "Tomatoes", "Hummus"},
                        IsFavorite = false,
                        Name = "Hummus Veggie Wrap",
                        PrepTime = 5,
                        Price = 8.99
                    },
                    new MenuItem
                    {
                        ItemId = 5,
                        Available = true,
                        ShortDescription = "Potato Soup - Vegetarian",
                        Description = "Hearty Vegetarian Potato Soup with Vegetable Broth and Carrots",
                        GroupName = "Lunch",
                        ImagePath = "potatoSoup",
                        Ingredients = new List<string>{"Milk", "Onions", "Potatoes", "Carrots", "Vegetable Broth"},
                        IsFavorite = false,
                        Name = "Vegetarian Potato Soup",
                        PrepTime = 5,
                        Price = 5.99
                    },
                    new MenuItem
                    {
                        ItemId = 6,
                        Available = true,
                        ShortDescription = "Mediterranean Tofu Salad - Vegetarian, Gluten-free",
                        Description = "Light and Tasty Mediterranean Tofu Salad with House Balsamic Dressing",
                        GroupName = "Lunch",
                        ImagePath = "tofuSalad",
                        Ingredients = new List<string>{"Tofu", "Mixed Greens", "Onion", "Sun-dried Tomatoes", "Balsamic Dressing"},
                        IsFavorite = false,
                        Name = "Mediterranean Tofu Salad",
                        PrepTime = 15,
                        Price = 10.89
                    }
                }
            }
        };

        public IEnumerable<MenuItem> GetAllMenuItems()
        {
            IEnumerable<MenuItem> menuItems = menuGroups.SelectMany(menuGroup => menuGroup.MenuItems);
            return menuItems;
        }

        public MenuItem GetMenuItemById(int menuItemId)
        {
            IEnumerable<MenuItem> menuItems = menuGroups
                .SelectMany(menuGroup => menuGroup.MenuItems, (menuGroup, menuItem) => new { menuGroup, menuItem })
                .Where(t => t.menuItem.ItemId == menuItemId)
                .Select(t => t.menuItem);

            return menuItems.FirstOrDefault();
        }

        public IEnumerable<MenuGroup> GetMenuGroups()
        {
            return menuGroups;
        }

        public IEnumerable<MenuItem> GetItemsForGroup(int groupId)
        {
            var group = menuGroups.FirstOrDefault(x => x.MenuGroupId == groupId);
            return group?.MenuItems;
        }

        public IEnumerable<MenuItem> GetFavoriteItems()
        {
            IEnumerable<MenuItem> menuItems = menuGroups
                .SelectMany(menuGroup => menuGroup.MenuItems, (menuGroup, menuItem) => new { menuGroup, menuItem })
                .Where(t => t.menuItem.IsFavorite)
                .Select(t => t.menuItem);

            return menuItems;
        }
    }
}