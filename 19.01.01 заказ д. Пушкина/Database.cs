using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace _19._01._01_заказ_д._Пушкина
{
    

    public class Database
    {
        private ObservableCollection<Accounting> Accountings = 
            new ObservableCollection<Accounting>();
        private DataContractJsonSerializer jsonSerializer =
            new DataContractJsonSerializer(typeof(ObservableCollection<Accounting>));
        private string path = "Cabinet.db";
        public Database()
        {
            LoadMethod();
        }

        public ObservableCollection<Accounting> ReturnCollection()
        {
            return Accountings;
        }

        public void AddAccounting(Accounting accounting)
        {
            Accountings.Add(accounting);
            SaveMethod();
        }

        public void SaveMethod()
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                jsonSerializer.WriteObject(fs, Accountings);
            }
        }
        public void LoadMethod()
        {
            if (File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    Accountings = (ObservableCollection<Accounting>)jsonSerializer.ReadObject(fs);
                }
            }

        }
    }
   
    
}
