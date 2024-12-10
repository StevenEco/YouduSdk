namespace YouduSdk.Common.Exceptions;
public class SignatureException(string message, System.Exception innerException) : GeneralEntAppException(message, innerException)
{
}
