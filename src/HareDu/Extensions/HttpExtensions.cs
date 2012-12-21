namespace HareDu
{
    using System.Net.Http;

    public static class HttpExtensions
    {
        public static T GetResponse<T>(this HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content.ReadAsAsync<T>().Result;
        }
    }
}