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

namespace TextAnalyzer
{


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            bool[] parameters = { 
                checkBox1.IsChecked.Value, 
                checkBox2.IsChecked.Value, 
                checkBox3.IsChecked.Value, 
                checkBox4.IsChecked.Value 
            };
            string selectedLang = comboBoxSelectLang.Text;

            Windows.WindowAnalyzer windowAnalyzer = new Windows.WindowAnalyzer(parameters, selectedLang);
            windowAnalyzer.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
