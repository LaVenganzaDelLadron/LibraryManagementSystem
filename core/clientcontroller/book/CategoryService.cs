using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;
using System.IO;
using LibraryManagementSystem.core;


namespace LibraryManagementSystem.core.clientcontroller.book
{
    internal class CategoryService
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("category.json");

        public static CategoryResponse GetAllCategories()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new CategoryResponse
                    {
                        Status = "FAILED",
                        Message = "Categories database not found"
                    };
                }

                var categories = JsonConvert.DeserializeObject<List<Category>>(
                    File.ReadAllText(filePath)
                );

                if (categories == null || categories.Count == 0)
                {
                    return new CategoryResponse
                    {
                        Status = "FAILED",
                        Message = "No categories available",
                        Categories = new List<Category>()
                    };
                }

                return new CategoryResponse
                {
                    Status = "SUCCESS",
                    Categories = categories
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Category service error: {ex.Message}");
                return new CategoryResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }
    }
}
