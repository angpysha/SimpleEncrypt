using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleEncrypt
{
  public class Encryptor
  {
    public delegate void EncryptionProgress(float progress,int type);
    public event EncryptionProgress EncryptionProgressEvent;

    public delegate void EncryptionProgressed(bool result);

    public event EncryptionProgressed EncryptionProgressedEvent;
    private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

    public void EncryptFile(string path)
    {

      var keygen = new KeyGeneration();

      UnicodeEncoding ue = new UnicodeEncoding();

      var keyBytes = ue.GetBytes(keygen.Key);
      string initVector = "HR$2pIjHR$2pIj12";
      Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(keygen.Key,SALT);


      var vecBytes = ue.GetBytes(initVector);

      string cryptedFile = path + ".encr";

      using (FileStream fileStream = new FileStream(cryptedFile, FileMode.Create))
      {
        RijndaelManaged rijndaelManaged = new RijndaelManaged();

        using (CryptoStream cs = new CryptoStream(fileStream,
          rijndaelManaged.CreateEncryptor(deriveBytes.GetBytes(32), deriveBytes.GetBytes(16)), CryptoStreamMode.Write))
        {
          using (FileStream fsInput = new FileStream(path,FileMode.Open))
          {
            byte[] buffer = new byte[32*1024];
            int data;
            while ((data = fsInput.Read(buffer, 0, buffer.Length)) > 0)
            {
              cs.Write(buffer,0,data);
              float progress = ((float)fsInput.Position / (float)fsInput.Length) * 100;
              OnEncryptionProgressEvent(progress,0);
            }
            //while ((data = fsInput.ReadByte()) != -1)
            //{
            //  cs.WriteByte((byte)data);
            //  float progress = ((float)fsInput.Position / (float)fsInput.Length)*100;
            //  OnEncryptionProgressEvent(progress);
            //}
          fsInput.CopyTo(cs);
          }
        }
      }
     // OnEncryptionProgressedEvent(true);
    }

    public void DecryptFile(string path)
    {
      var keygen = new KeyGeneration();

      UnicodeEncoding ue = new UnicodeEncoding();

      var keyBytes = ue.GetBytes(keygen.Key);
      string initVector = "HR$2pIjHR$2pIj12";
      Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(keygen.Key, SALT);


      var vecBytes = ue.GetBytes(initVector);

      var dencryptedFile = path.Replace(".encr","");

      using (FileStream fileStream = new FileStream(path, FileMode.Open))
      {
        RijndaelManaged rijndaelManaged = new RijndaelManaged();

        using (CryptoStream cs = new CryptoStream(fileStream, rijndaelManaged
          .CreateDecryptor(deriveBytes.GetBytes(32), deriveBytes.GetBytes(16)), CryptoStreamMode.Read))
        {
          using (FileStream fileOut = new FileStream(dencryptedFile,FileMode.Create))
          {
            //while ((data=cs.ReadByte()) != -1)
            //{
            // /* fileOut.WriteByte((byte)data);*/
            //  float progress = ((float)fileStream.Position / (float)fileStream.Length) * 100;
            //  OnEncryptionProgressEvent(progress);
            //}
            byte[] buffer = new byte[32 * 1024];
            int data;
            while ((data = cs.Read(buffer, 0, buffer.Length)) > 0)
            {
             fileOut.Write(buffer, 0, data);
              float progress = ((float)fileStream.Position / (float)fileStream.Length) * 100;
              OnEncryptionProgressEvent(progress,1);
            }
          }
        }
      }
    //  OnEncryptionProgressedEvent(true);

    }

    protected virtual void OnEncryptionProgressEvent(float progress,int type)
    {
      EncryptionProgressEvent?.Invoke(progress,type);
    }

    protected virtual void OnEncryptionProgressedEvent(bool result)
    {
      EncryptionProgressedEvent?.Invoke(result);
    }
  }
}