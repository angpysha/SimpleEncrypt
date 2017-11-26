using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleEncrypt
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
      var argsCount = e.Args.Length;
      if (argsCount > 0)
      {
        var path = e.Args[0];
       MixedLibrary.ApiManaged.AllocConsoleEx();
        Encryptor encryptor = new Encryptor();

        encryptor.EncryptionProgressEvent += delegate (float progress, int type)
        {
          Console.SetCursorPosition(0, 1);
          Console.Write(new String(' ', Console.BufferWidth));
          if (type == 0)
            Console.Write($@"Encryption progress {(int)progress} %");
          else
            Console.Write($@"Decryption progress {(int)progress} %");
        };
        if (path.Contains(".encr"))
        {
          Console.WriteLine($"File to decrypt: {path}");
          encryptor.DecryptFile(path);
        }
        else
        {
          Console.WriteLine($"File to encrypt: {path}");
          encryptor.EncryptFile(path);
        }
        Console.ReadKey();
        Application.Current.Shutdown();
      }
    }
  }
}
