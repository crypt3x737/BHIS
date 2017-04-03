using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Security.Cryptography;
using System.Windows.Shapes;

namespace BHIS
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

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                address.Text = dlg.FileName.ToString();
            }
            else
                MessageBox.Show("fail");
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            string line;
            long i = 0;
            if (hash.Text.Length == 32)
            {
                try
                {   // Open the text file using a stream reader.
                    StreamReader sr = new StreamReader(address.Text);
                    while ((line = sr.ReadLine()) != null)
                    {
                        i++;
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash1 = GetMd5Hash(md5Hash, line);
                            if(hash1.Equals(hash.Text))
                            {
                                decrypt.Text = line;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ex.Message);
                }
            }
            else
                MessageBox.Show("Invalid Hash\n");

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }


}
