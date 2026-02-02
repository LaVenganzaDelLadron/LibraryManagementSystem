using LibraryManagementSystem.model;
using LibraryManagementSystem.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.controller.book
{
    internal class CategoryController
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("category.json");

        private List<LibraryManagementSystem.model.Category> LoadCategories()
        {
            if (!File.Exists(filePath))
            {
                return new List<LibraryManagementSystem.model.Category>();
            }
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<LibraryManagementSystem.model.Category>>(json) ?? new List<LibraryManagementSystem.model.Category>();
        }

        private void SaveCategories(List<LibraryManagementSystem.model.Category> categories)
        {
            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public bool AddCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            var categories = LoadCategories();
            if (categories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            categories.Add(new LibraryManagementSystem.model.Category(name));
            SaveCategories(categories);
            return true;
        }

        public List<LibraryManagementSystem.model.Category> GetAllCategories()
        {
            return LoadCategories();
        }

        public bool DeleteCategory(Guid id)
        {
            var categories = LoadCategories();
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return false;
            }

            categories.Remove(category);
            SaveCategories(categories);
            return true;
        }
    }
}
