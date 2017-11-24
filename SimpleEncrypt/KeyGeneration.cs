using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncrypt
{
  public class KeyGeneration
  {

    public string Key => GetHardDrivesInfo();

    private string GetHardDrivesInfo()
    {
      ManagementObjectSearcher moSearcher = 
        new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
      string no = "";
      foreach (var mo in moSearcher.Get())
      {
        no +=mo["SerialNumber"].ToString();
      }

      return no;
    }
  }
}
