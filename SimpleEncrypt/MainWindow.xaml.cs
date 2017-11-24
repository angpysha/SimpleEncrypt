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
    Encryptor encryptor = new Encryptor();
    public MainWindow()
    {
      InitializeComponent();
      ProgressBar.Maximum = 100;
      encryptor.EncryptionProgressEvent += EncryptorOnEncryptionProgressEvent;
      encryptor.EncryptionProgressedEvent += EncryptorOnEncryptionProgressedEvent;
    }

    private void EncryptorOnEncryptionProgressedEvent(bool result)
    {
      ProgressBar.Value = 0;
    }

    private void EncryptorOnEncryptionProgressEvent(float progress)
    {
      ProgressBar.Value = progress;
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
      ProgressBar.Value = 0;
      if (!string.IsNullOrWhiteSpace(FilePath.Text))
        encryptor.EncryptFile(FilePath.Text);
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      ProgressBar.Value = 0;
      if (!string.IsNullOrWhiteSpace(FilePath.Text))
        encryptor.DecryptFile(FilePath.Text);
    }
  }
}
