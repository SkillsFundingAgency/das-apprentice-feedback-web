namespace SFA.DAS.ApprenticeFeedback.Web.NServiceBusListener.Extensions
{
    public static class ConnectionStringExtensions
    {
        public static string FormatConnectionString(this string connectionString)
        {
            return connectionString.Replace("Endpoint=sb://", string.Empty).TrimEnd('/');
        }
    }
}
