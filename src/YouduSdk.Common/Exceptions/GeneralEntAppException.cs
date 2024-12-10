namespace YouduSdk.Common.Exceptions;
public class GeneralEntAppException(string message, Exception innerException) : Exception(message, innerException)
{
}
