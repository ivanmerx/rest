using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using REST.Entity;
using REST.WebClient;
namespace REST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetPersonListAndPopulateAsync()
        {
            App.Database.ResetTable();
            Console.WriteLine("Is current thread in background: " + Thread.CurrentThread.IsBackground);

            Rest webClient = new Rest();
            List<Photo> data = await webClient.GetPersonsListAsync(this);
            if (data != null && data.Count != 0)
            {
                if (!String.IsNullOrEmpty(selectid.Text))
                {
                    ObservableCollection<Photo> pl = new ObservableCollection<Photo>(data);
                    await App.Database.SaveItemsAsync(data);
                    ListBox.ItemsSource = App.Database.GetItemsNotDoneAsync2(Int32.Parse(selectid.Text)).Result;
                }
                else
                {
                    ObservableCollection<Photo> pl = new ObservableCollection<Photo>(data);
                    await App.Database.SaveItemsAsync(data);
                    ListBox.ItemsSource = App.Database.GetItemsNotDoneAsync().Result;
                }
            }
        }

        private async void GetData_Click2(object sender, RoutedEventArgs e)
        {
            GetPersonListAndPopulateAsync();
        }
    }
}
