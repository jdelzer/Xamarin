using JensCafe.Core.Model;
using JensCafe.Core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace JensCafe.Core.Service
{
    public class MenuDataService
    {
        private static MenuRepository menuRepository = new MenuRepository();

        public List<MenuItem> GetAllItems()
        {
            return menuRepository.GetAllMenuItems().ToList();
        }

        public List<MenuGroup> GetGroupedItems()
        {
            return menuRepository.GetMenuGroups().ToList();
        }

        public List<MenuItem> GetItemsForGroup(int groupId)
        {
            return menuRepository.GetItemsForGroup(groupId).ToList();
        }

        public List<MenuItem> GetFavoriteItems()
        {
            return menuRepository.GetFavoriteItems().ToList();
        }

        public MenuItem GetItemById(int itemId)
        {
            return menuRepository.GetMenuItemById(itemId);
        }
    }
}