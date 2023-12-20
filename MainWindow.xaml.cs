using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WpfApp50
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

        private void toggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is ToggleButton toggleButton)
            {
                toggleButton.Foreground =new SolidColorBrush(Colors.Green );
                toggleBtn.IsChecked = true;
                toggleButton.Content = "on";              
            }
        }

        private void toggleBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                toggleButton.Foreground = new SolidColorBrush(Colors.White);
                toggleButton.Content = "off";
                toggleButton.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void folderBtn_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "All Files(*.*)|*.*|Text Files(*.txt)| *.txt";
             openDialog.ShowDialog();
            filePathLbl.Content = openDialog.FileName;
            using (var sr = File.OpenText(openDialog.FileName))
                {
                    textBox.Text = sr.ReadToEnd();
                }
            foreach (var item in myWrapPanel.Children)
            {
                if(item is Button bt)
                {
                    bt.Visibility=Visibility.Visible;
                }
                if(item is ToggleButton tb)
                {
                    tb.Visibility=Visibility.Visible;
                }

            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = filePathLbl.Content.ToString();
          //  save.ShowDialog();
            using (var sw = new StreamWriter(save.FileName))
            {
                sw.Write(textBox.Text);
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (toggleBtn.IsChecked == true)
            {
                saveBtn_Click(sender, e);
            }
        }
        public string copied { get; set; }
        public string selectAll { get; set; }

        private void selectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            textBox.Foreground = new SolidColorBrush(Colors.Cyan);
            selectAll = textBox.Text;
        }

        private void pasteBtn_Click(object sender, RoutedEventArgs e)
        {
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            textBox.Text += copied;
        }

        private void copyBtn_Click(object sender, RoutedEventArgs e)
        {
            copied = textBox.SelectedText;
        }

        private void cutBtn_Click(object sender, RoutedEventArgs e)
        {
            copied = textBox.SelectedText;
            var content = textBox.Text.Replace(copied,"");
            textBox.Text = content;
        }
    }
}
