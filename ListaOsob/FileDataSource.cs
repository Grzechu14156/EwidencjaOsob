using CsvHelper;
using ListaOsob.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaOsob
{
    public class FileDataSource<T> where T : IDomainItem
    {
        private List<T> list = new List<T>();
        private string fileName;

        public FileDataSource()
        {
            fileName = "C:\\Users\\Katarzyna\\Desktop\\ewidencja.csv";
        }
        public List<T> GetAll()
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                list = csv.GetRecords<T>().ToList();
                return list;
            }

        }

        public bool Add(T item)
        {
            List<T> list = new List<T>();
            list.Add(item);
            int newId = this.list.Max(x => x.Id) + 1;
            item.Id = newId;
            UpdateFile(list, FileMode.Append);
            return true;
        }

        private void UpdateFile(List<T> list, FileMode fileMode)
        {
            using (var stream = File.Open(fileName, fileMode))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                if (fileMode == FileMode.Append)
                    csv.Configuration.HasHeaderRecord = false;
                else
                    csv.Configuration.HasHeaderRecord = true;
                csv.WriteRecords(list);
            }
        }

        public bool Remove(int id)
        {
            var personToRemove = list.Where(x => x.Id == id).First();
            list.Remove(personToRemove);
            UpdateFile(list, FileMode.Create);
            return true;
        }

    }
}
