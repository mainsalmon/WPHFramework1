using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{

    /// <summary>
    /// Persist data in local json files named using the id value in folders named with the plural of the entity name
    /// </summary>
    public class JsonRepository<T> : IRepository<T> where T : IEntity
    {

        private const string _rootPath = @"C:\WPHFrameworkData\";
        private string _folderPath;

        public JsonRepository()
        {
            string entityName = typeof(T).Name;
            _folderPath = string.Format("{0}{1}s\\", _rootPath, entityName); 

              // ensure the folder exists
            if (Directory.Exists(_folderPath) == false)
            {
                Directory.CreateDirectory(_folderPath);
            }
        
        }

        public void Save(T entity)
        {
            string path = MakeFullPath(entity.ID);

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            serializer.WriteObject(ms, entity);
            ms.Position = 0;

            // overwrite existing file
            using (FileStream fs = File.Create(path))
            {
                ms.WriteTo(fs);
            }
        }

        public void Delete(T entity)
        {
            string path = MakeFullPath(entity.ID);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public IEnumerable<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var allEntities = GetAll().ToList();
            var rslt = allEntities.Where(predicate.Compile()).ToList();
            return rslt;
        }

        public IEnumerable<T> GetAll()
        {
            List<T> entities = new List<T>();
            DirectoryInfo di = new DirectoryInfo(_folderPath);
            FileInfo[] files = di.GetFiles("*.json");

            foreach (FileInfo file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file.Name);
                int id = -1;
                int.TryParse(name, out id);
                if (id != -1)
                {
                    T entity = GetById(id);
                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
            }
            return entities;
        }

        public T GetById(int id)
        {
            string path = MakeFullPath(id);
            if (File.Exists(path) == false)
            {
                return default(T);
            }

            string text = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream(bytes);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            T entity = (T)serializer.ReadObject(ms);

            return entity;
        }

        private string MakeFullPath(int id)
        {
            return string.Format("{0}{1}.json", _folderPath, id);
        }

        /// <summary>
        /// Figure out the next ID to assign based on the existing file names 
        /// </summary>
        public int GetNextId()
        {
            DirectoryInfo di = new DirectoryInfo(_folderPath);
            IEnumerable<FileInfo> files = di.GetFiles("*.json");
            var names = files.Select(f => f.Name).ToList();
            int max = names.Select(n => n.Split('.')).Max(i => Convert.ToInt32( i[0]));

            return max + 1;
        }
    }
}
