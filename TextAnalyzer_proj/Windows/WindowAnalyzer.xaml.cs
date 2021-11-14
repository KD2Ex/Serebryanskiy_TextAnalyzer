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
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace TextAnalyzer.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAnalyzer.xaml
    /// </summary>
    public partial class WindowAnalyzer : Window
    {

        private bool[] parameters;
        private string selectedLang;
        public WindowAnalyzer(bool[] parameters, string selectedLang)
        {
            InitializeComponent();
            this.parameters = parameters;
            this.selectedLang = selectedLang;

           
        }

        
        private int[] Count(bool[] parameters, string selectedLang)
        {
            int[] result = { 0, 0, 0, 0 };
            if (selectedLang == "Русский")
            {
                foreach (char c in textBoxMain.Text)
                {
                    string symbol = c.ToString();
                    if (parameters[0] && Regex.IsMatch(symbol, @"[аоиеёяуэыюАОИЕЁЯУЭЫЮ]")) result[0]++;
                    if (parameters[1] && Regex.IsMatch(symbol, @"[бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ]")) result[1]++;
                    if (parameters[2] && Char.IsDigit(c)) result[2]++;
                    if (parameters[3] && Char.IsPunctuation(c)) result[3]++;
                }
            } 
            else
            {
                foreach (char c in textBoxMain.Text)
                {
                    string symbol = c.ToString();
                    if (parameters[0] && Regex.IsMatch(symbol, @"[aeiouyAEIOUY]")) result[0]++;
                    if (parameters[1] && Regex.IsMatch(symbol, @"[bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ]")) result[1]++;
                    if (parameters[2] && Char.IsDigit(c)) result[2]++;
                    if (parameters[3] && Char.IsPunctuation(c)) result[3]++;
                }
            }
            return result;
        }



        void listBoxClear()
        {
            listBoxResult.Items.Clear();
        }

        /*int parametersCount(bool[] parameters)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (parameters[i]) count++;
            }
            return count;
        }*/

        private void buttonAnalyseOK_Click(object sender, RoutedEventArgs e)
        {
            listBoxClear();
            int[] result = Count(parameters, selectedLang);
            /*if (parameters[0]) listBoxResult.Items.Add("Гласных: ");
            if (parameters[1]) listBoxResult.Items.Add("Согласных: ");
            if (parameters[2]) listBoxResult.Items.Add("Цифр: ");
            if (parameters[3]) listBoxResult.Items.Add("Знаков препинания: ");*/
            //string[] result = { "123", "1", "2" };

            for (int i = 0; i < 4; i++) 
            {
                if (parameters[i])
                {
                    switch (i)
                    {
                        case 0:
                            listBoxResult.Items.Add("Гласных: ");
                            break;
                        case 1:
                            listBoxResult.Items.Add("Согласных: ");
                            break;
                        case 2:
                            listBoxResult.Items.Add("Цифр: ");
                            break;
                        case 3:
                            listBoxResult.Items.Add("Знаков препинания: ");
                            break;
                        default:
                            break;
                    }
                    listBoxResult.Items[listBoxResult.Items.Count - 1] += result[i].ToString();
                }
            }
            /*for (int i = 0; i < 4; i++)
            {
                if (result[i] == 0) listBoxResult.Items.RemoveAt(i);
            }*/
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            listBoxClear();
            textBoxMain.Clear();
        }

        private void buttonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                textBoxFilePath.Text = openFileDialog.FileName;
                textBoxFilePath.IsEnabled = false;
                textBoxMain.Text = File.ReadAllText(textBoxFilePath.Text, Encoding.GetEncoding("UTF-8"));
                Count(parameters, selectedLang);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBoxMain.Text + "\nРезультат:\n"
                    + string.Join("\n", listBoxResult.Items.Cast<String>()));
            }
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
