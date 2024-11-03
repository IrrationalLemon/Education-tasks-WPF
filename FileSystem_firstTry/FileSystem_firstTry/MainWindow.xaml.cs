using System;
using System.Collections.Generic;
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
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace FileSystem_firstTry
{

    public class ShowInList
    {
        public string NameFile { get; set; }
        public string TypeFile { get; set; }
    }

    public partial class MainWindow : Window
    {

        DriveInfo[] drives = DriveInfo.GetDrives();
        public ObservableCollection<ShowInList> FileEntries { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            FileEntries = new ObservableCollection<ShowInList>();
            DirectoryList.ItemsSource = FileEntries;
            LoadDrives();
        }

        void LoadDrives()
        {
            foreach(DriveInfo drive in drives)
            {
                if(drive.IsReady)
                {
                    ComboBoxDrives.ItemsSource = drives;
                }
            }
        }

        private void ComboBoxDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectDrive();
        }

        void SelectDrive()
        {
            if (ComboBoxDrives.SelectedItem != null)
            {
                var selectedDrive = ComboBoxDrives.SelectedItem.ToString();
                LoadFiles(selectedDrive);
            }
        }

        void LoadFiles (string drive)
        {
            FileEntries.Clear(); // Очистить предыдущие элементы

            try
            {
                // Получаем директории в выбранном диске
                string[] directories = Directory.GetDirectories(drive);
                foreach (string directory in directories)
                {
                    // Добавляем директорию в ListView
                    FileEntries.Add(new ShowInList
                    {
                        NameFile = new DirectoryInfo(directory).Name,
                        TypeFile = "Папка"
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке директорий: " + ex.Message);
            }
        }
    }
}
