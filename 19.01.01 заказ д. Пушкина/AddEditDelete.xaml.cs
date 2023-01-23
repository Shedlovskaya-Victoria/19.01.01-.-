using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _19._01._01_заказ_д._Пушкина
{
    /// <summary>
    /// Логика взаимодействия для AddEditDelete.xaml
    /// </summary>
    public partial class AddEditDelete : Window 
    {
        private DataContractJsonSerializer jsonSerializer =
            new DataContractJsonSerializer(typeof(ObservableCollection<Accounting>));
        public CustomCommand SaveAccounting { get; set; }
        public CustomCommand CancleAccounting { get; set; }
        public Database Database { get; set; }
        public Accounting Accounting1 { get; set; }

        public AddEditDelete(Accounting accounting, Database database, bool isEdit, Accounting OldSelectedAccounting = null)
        {
            InitializeComponent();
            DataContext = this;
            Database = database;
            Accounting1 = accounting;

            SaveAccounting = new CustomCommand(() =>
            {
                if(!isEdit)
                    database.AddAccounting(Accounting1);
                else
                {
                    OldSelectedAccounting.Detail = accounting.Detail;
                    OldSelectedAccounting.NumberCabinet = accounting.NumberCabinet;
                    OldSelectedAccounting.Price = accounting.Price;
                    database.SaveMethod();
                }
                Close();
            });
            CancleAccounting = new CustomCommand(() =>
            {
                Close();
            });
        }
       
    }
}

