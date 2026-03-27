using System.Security.Cryptography;

namespace TodoList.Api.Security;

public interface IRsaKeyProvider
{
    RSA CreatePrivateRsa();
}
