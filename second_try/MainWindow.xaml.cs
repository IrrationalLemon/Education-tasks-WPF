using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

namespace second_try
{
    public class EntryMoney
    {
        public string Euro { get; set; }
        public double TotalSumm { get; set; }
    }
    public partial class MainWindow : Window
    {
        public ObservableCollection<EntryMoney> MoneyEntries { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MoneyEntries = new ObservableCollection<EntryMoney>();
            MoneyList.ItemsSource = MoneyEntries;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MoneyInput.Text) && double.TryParse(MoneyInput.Text, out double euro))
            {
                double summHrivna = euro * 44;
                if (summHrivna > 0)
                {
                    MoneyEntries.Add(new EntryMoney { Euro = MoneyInput.Text, TotalSumm = summHrivna });
                    MoneyInput.Clear();
                }
                else
                {
                    MessageBox.Show("Введите положительное число");
                }
            }
            else
            {
                MessageBox.Show("Ошибочка");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EntryMoney selectedEntry = MoneyList.SelectedItem as EntryMoney;
            if (selectedEntry != null)
            {
                MoneyEntries.Remove(selectedEntry);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EntryMoney selectedEntry = MoneyList.SelectedItem as EntryMoney;
            if (selectedEntry != null && !string.IsNullOrWhiteSpace(MoneyInput.Text))
            {
                if (double.TryParse(MoneyInput.Text, out double euro))
                {
                    
                    double summHrivna = euro * 44;
                    if(summHrivna > 0)
                    {
                        MoneyEntries.Remove(selectedEntry);
                        MoneyEntries.Add(new EntryMoney { Euro = MoneyInput.Text, TotalSumm = summHrivna });
                        MoneyInput.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Введите положительно число");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при вводе евро");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для обновления и проверьте ввод евро");
            }
        }
    }
}
