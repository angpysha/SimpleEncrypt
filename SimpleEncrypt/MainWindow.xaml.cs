﻿using System;
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
using Microsoft.Win32;

namespace SimpleEncrypt
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    OpenFileDialog dialog = new OpenFileDialog();
    SaveFileDialog save = new SaveFileDialog();
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      dialog.Filter = "All files|*.*";
      dialog.Title = "Please select your file";
      dialog.ShowDialog();

      FilePath.Text = dialog?.FileName ?? "";

    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      var encryptor = new Encryptor();
      if (!string.IsNullOrWhiteSpace(FilePath.Text))
        encryptor.EncryptFile(FilePath.Text);
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      var encryptor = new Encryptor();
      if (!string.IsNullOrWhiteSpace(FilePath.Text))
        encryptor.DecryptFile(FilePath.Text);
    }
  }
}
