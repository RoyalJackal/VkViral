namespace VkViral.Helpers;

public static class HttpClientHelper
{
    public static HttpClient GetClient()
    {
        var httpClientHandler = new HttpClientHandler();

        httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, certificate, chain, sslPolicyErrors) => true;

        return new HttpClient(httpClientHandler);
    }
}