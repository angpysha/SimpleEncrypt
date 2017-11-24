using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleEncrypt
{
  public class Encryptor
  {
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
            int data;

            while ((data = fsInput.ReadByte()) != -1)
            {
              cs.WriteByte((byte)data);

            }
          }
        }
      }
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
            int data;

            while ((data=cs.ReadByte()) != -1)
            {
              fileOut.WriteByte((byte)data);
            }
          }
        }
      }
    }
  }
}