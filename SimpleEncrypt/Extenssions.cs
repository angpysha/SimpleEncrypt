namespace SimpleEncrypt
{
  public static class Extenssions
  {
    public static string GetFileExtension(this string filepath)
    {
      var arr = filepath.Split('.');

      return arr[1];
    }
  }
}