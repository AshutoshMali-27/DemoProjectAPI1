using IRepositroy;
using Microsoft.Extensions.Configuration;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MenuService : IMenuService
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;
        public MenuService(IConfiguration configuration)
        {

            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DefaultConnection"];
        }
        public async Task<List<MenuItemViewModel>> GetMenuItemsAsync()
        {
            var menuItems = new List<MenuItemViewModel>();

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetMenuItems", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var menuItem = new MenuItemViewModel
                            {
                                MenuItemID = Convert.ToInt32(reader["MenuItemID"]),
                                Label = reader["Label"].ToString(),
                                Route = reader["Route"].ToString(),
                                Collapsed = Convert.ToBoolean(reader["Collapsed"]),
                                ParentID = Convert.ToInt32(reader["ParentID"])
                            };

                            menuItems.Add(menuItem);
                        }
                    }
                }
            }

            return menuItems;
        }

        public async Task<MenuItemViewModel> GetMenuItemByIdAsync(int id)
        {
            MenuItemViewModel menuItem = null;

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetMenuItemById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MenuItemID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            menuItem = new MenuItemViewModel
                            {
                                MenuItemID = Convert.ToInt32(reader["MenuItemID"]),
                                Label = reader["Label"].ToString(),
                                Route = reader["Route"].ToString(),
                                Collapsed = Convert.ToBoolean(reader["Collapsed"]),
                                ParentID = Convert.ToInt32(reader["ParentID"])
                            };
                        }
                    }
                }
            }

            return menuItem;
        }

    }
}
