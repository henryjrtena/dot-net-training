using System.Security.Cryptography;

namespace TodoList.Api.Security;

public class FileRsaKeyProvider : IRsaKeyProvider
{
    private readonly IWebHostEnvironment _environment;

    public FileRsaKeyProvider(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public RSA CreatePrivateRsa()
    {
        var rsa = RSA.Create();
        var path = Path.Combine(_environment.ContentRootPath, "keys", "jwt-private.pem");
        rsa.ImportFromPem(File.ReadAllText(path));
        return rsa;
    }
}
