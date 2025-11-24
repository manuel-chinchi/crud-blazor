using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace crud_blazor.Server.Repositories
{
    public class JSONRepository<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;

        public JSONRepository(string fileName)
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", $"{fileName}");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            PrepareFile();
        }

        public void Add(T obj)
        {
            var items = this.GetItems().ToList();

            if (items.Count == 0)
            {
                items.Add(obj);
            }
            else
            {
                var idProperty = typeof(T).GetProperty("Id");
                var maxId = items.Max(item => (int)idProperty.GetValue(item));

                maxId = maxId < 0 ? 0 : maxId;
                idProperty.SetValue(obj, maxId + 1);
                items.Add(obj);
            }

            SaveAll(items);
        }

        public void Delete(int id)
        {
            // SOLUCIÓN: Convertir a List ANTES de hacer Remove
            var items = this.GetItems().ToList();

            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException($"El tipo {typeof(T).Name} no tiene una propiedad 'Id'");

            var itemToRemove = items.FirstOrDefault(item => (int)idProperty.GetValue(item) == id);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                this.SaveAll(items);
            }
        }

        public T Get(int id)
        {
            var items = this.GetItems();
            var idProperty = typeof(T).GetProperty("Id");

            var item = items.FirstOrDefault(item => (int)idProperty.GetValue(item) == id);
            return item;
        }

        public IEnumerable<T> GetItems()
        {
            var json = File.ReadAllText(_filePath, Encoding.GetEncoding("iso-8859-1"));

            if (string.IsNullOrEmpty(json))
                json = "[]";

            var items = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);

            return items ?? new List<T>();
        }

        public void Update(T obj)
        {
            var items = this.GetItems().ToList();

            var idProperty = typeof(T).GetProperty("Id");

            var itemId = (int)idProperty.GetValue(obj);
            var itemIndex = items.FindIndex(item => (int)idProperty.GetValue(item) == itemId);

            if (itemId != -1)
            {
                items[itemIndex] = obj;
            }

            SaveAll(items);
        }

        #region Private Methods

        private void PrepareFile()
        {
            if (!File.Exists(_filePath))
            {
                var directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(_filePath, "[]");
            }
        }

        private void SaveAll(List<T> items)
        {
            var json = JsonSerializer.Serialize(items, _jsonOptions);
            File.WriteAllText(this._filePath, json, Encoding.GetEncoding("iso-8859-1"));
        }

        #endregion
    }
}
