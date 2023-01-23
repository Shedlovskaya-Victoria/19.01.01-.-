using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace _19._01._01_заказ_д._Пушкина
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Accounting> accountings = new ObservableCollection<Accounting>();
        private string accountingsCount;
        private ObservableCollection<Accounting> accountingsCollectionCount = new ObservableCollection<Accounting>();

        public ObservableCollection<Accounting> AccountingsCollectionCount { 
            get => accountingsCollectionCount;
            set
            {
                accountingsCollectionCount = value;
                SignalChenged();
            }

        }
        public Accounting SelectedAccounting { get; set; }
        public ObservableCollection<Accounting> Accountings
        {
            get => accountings;
            set
            {
                accountings = value;
                SignalChenged();
            }
        }
        public CustomCommand AddAccounting { get; set; }
        public CustomCommand EditAccounting { get; set; }
        public CustomCommand DeleteAccounting { get; }
        public CustomCommand CountAccounting { get; set; }
        public Double Summ { get; set; }
        public string AccountingsCount
        {
            get => accountingsCount;
            set
            {
                accountingsCount = value;
            }
        }
        public Database Database { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Database = new Database();
            Database.LoadMethod();

            Accountings = Database.ReturnCollection();

            AddAccounting = new CustomCommand(() =>
            {
                AddEditDelete addEditDelete = new AddEditDelete(
                    new Accounting(),
                    Database,
                    false);
                addEditDelete.ShowDialog();
                Accountings = Database.ReturnCollection();
                //Accountings = new ObservableCollection<Accounting>();
                //SaveMethod();
                //Accountings.Add(new Accounting()
                //{
                //    NumberCabinet = 0,
                //    Price = 0,
                //    Detail = "пример"
                //});
            });
            EditAccounting = new CustomCommand(() =>
            {
                if (SelectedAccounting != null)
                {
                    Accounting accounting = new Accounting
                    {
                        Detail = SelectedAccounting.Detail,
                        NumberCabinet = SelectedAccounting.NumberCabinet,
                        Price = SelectedAccounting.Price
                    };
                    AddEditDelete addEditDelete = new AddEditDelete(
                        accounting,
                        Database, true, SelectedAccounting);
                    addEditDelete.ShowDialog();
                    Accountings = new ObservableCollection<Accounting>();
                    Accountings = Database.ReturnCollection();
                    //Database.SaveMethod();
                }
            });
            DeleteAccounting = new CustomCommand(() =>
            {
                if (SelectedAccounting != null)
                {
                    Accountings.Remove(SelectedAccounting);
                    Database.SaveMethod();
                }
                Accountings = new ObservableCollection<Accounting>();
                Accountings = Database.ReturnCollection();
            });
            CountAccounting = new CustomCommand(() =>
            {
                Summ = 0;
                double c = 0, median = 0;
                foreach (Accounting accountingCount in Accountings)
                {
                    Summ += accountingCount.Price;
                    c++;
                }
                SignalChenged(nameof(Summ));
                median = Summ / c;
                AccountingsCollectionCount = new ObservableCollection<Accounting>();
                foreach (Accounting accountingCount in Accountings)
                {
                    if (accountingCount.Price > median)
                    {
                        AccountingsCollectionCount.Add(new Accounting()
                        {
                           NumberCabinet = accountingCount.NumberCabinet,
                           Price = accountingCount.Price,
                           Detail = accountingCount.Detail
                          });
                    }
                }
            });
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void SignalChenged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}


