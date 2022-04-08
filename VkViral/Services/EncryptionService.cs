using System.Security.Cryptography;
using System.Text;

namespace VkViral.Services;

public class EncryptionService
{
    private readonly IConfiguration _configuration;

    public EncryptionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Encrypt(string item)
    {
        var bytes = Encoding.UTF8.GetBytes(item);

        var md5 = MD5.Create();
        var keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(_configuration["Encryption:Key"]));
        md5.Clear();

        var tripleDes = TripleDES.Create();
        tripleDes.Key = keyBytes;
        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Padding = PaddingMode.PKCS7;

        var encryptor = tripleDes.CreateEncryptor();
        var result = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        tripleDes.Clear();
        return Convert.ToBase64String(result, 0, result.Length);
    }

    public string Decrypt(string item)
    {
        var bytes = Convert.FromBase64String(item);
        
        var md5 = MD5.Create();
        var keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(_configuration["Encryption:Key"]));
        md5.Clear();

        var tripleDes = TripleDES.Create();
        tripleDes.Key = keyBytes;
        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Padding = PaddingMode.PKCS7;

        var decryptor = tripleDes.CreateDecryptor();
        byte[] resultArray = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        tripleDes.Clear();

        return Encoding.UTF8.GetString(resultArray);
    }
}