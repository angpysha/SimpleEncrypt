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
    SaveFileDialog saveDialog = new SaveFileDialog();

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

    private void EncryptorOnEncryptionProgressEvent(float progress,int type)
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
    /// <summary>
    /// Action by pression Encrypt button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      ProgressBar.Value = 0;
      if (SaveBox.IsChecked != null && SaveBox.IsChecked.Value)
      {
        var extension = dialog.FileName.GetFileExtension();
        saveDialog.Filter = $"Encrypted files|*.{extension}.encr";
        //saveDialog.FileName = dialog.FileName.Split('.')[0];
        var result = saveDialog.ShowDialog();

        if (result != null && (!string.IsNullOrWhiteSpace(FilePath.Text) && result.Value))
          encryptor.EncryptFile(FilePath.Text,saveDialog.FileName);
      }
      else
      {
          if (!string.IsNullOrWhiteSpace(FilePath.Text))
            encryptor.EncryptFile(FilePath.Text);
      }

    }

    /// <summary>
    /// Action pressing Decrypt button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      ProgressBar.Value = 0;
      if (SaveBox.IsChecked != null && SaveBox.IsChecked.Value)
      {
        var fileEncr = dialog.FileName.Replace(".encr", "");
        var ext = fileEncr.GetFileExtension();

        saveDialog.Filter = $"Decrypted file|*.{ext}";
        var result = saveDialog.ShowDialog();

        if (result != null && (!string.IsNullOrWhiteSpace(FilePath.Text) && result.Value))
          encryptor.DecryptFile(FilePath.Text,saveDialog.FileName);
      }
      else
      {
        if (!string.IsNullOrWhiteSpace(FilePath.Text))
          encryptor.DecryptFile(FilePath.Text);
      }

    }
  }
}
