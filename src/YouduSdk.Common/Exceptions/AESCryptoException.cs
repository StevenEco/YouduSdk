namespace YouduSdk.Common.Exceptions;
public class AESCryptoException(string message, Exception innerException) : GeneralEntAppException(message, innerException)
{
}