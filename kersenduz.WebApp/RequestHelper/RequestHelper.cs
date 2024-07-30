using kersenduz.WebApp.RequestHelper.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace kersenduz.WebApp.RequestHelper;

public class RequestHelper : IRequestHelper
{
    /// <summary>
    /// Constructor da base url alır. Requestlerin endpoint i dir.
    /// Örn:http://localhost:55245/api/
    /// </summary>
    public string _baseUrl = null;

    /// <summary>
    /// Request için gerekli header
    /// </summary>
    public IRequestHelperHeader _headers;

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient _client;

    public RequestHelper(string baseUrl, IRequestHelperHeader headers)
    {
        _baseUrl = baseUrl;
        _headers = headers;
    }

    public RequestHelper(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    /// <summary>
    /// Constructor da base url tanımlanmamış ise url in base url ile verildiği varsayılır.
    /// Örn:http://localhost:55245/api/Todo
    /// </summary>
    public RequestHelper()
    {
    }

    /// <summary>
    /// Constructor da HttpClient alan method.
    /// </summary>
    public RequestHelper(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Constructor da HttpClient alan method.
    /// </summary>
    public RequestHelper(HttpClient client, IRequestHelperHeader headers)
    {
        _client = client;
        _headers = headers;
    }

    /// <summary>
    /// Request typeların belirlendiği enum.
    /// </summary>
    private enum RequestType
    {
        Get,
        Post,
        Put,
        Delete
    }

    /// <summary>
    /// Get isteklerinin yapılacağı method.
    /// </summary>
    /// <typeparam name="T">İstenen geri dönüş tipi</typeparam>
    /// <param name="url">Base url den sonra gelen ilgili methodun yolu veya yolun tamamı</param>
    /// <returns></returns>
    public RequestHelperResult<T> Get<T>(string url)
    {
        return BaseRequest<T>(RequestType.Get, url, null);
    }

    /// <summary>
    /// Post isteklerinin yapılacağı method.
    /// </summary>
    /// <typeparam name="T">İstenen geri dönüş tipi</typeparam>
    /// <param name="url">Base url den sonra gelen ilgili methodun yolu veya yolun tamamı</param>
    /// <param name="prms">Gönderilecek parametreler</param>
    /// <returns></returns>
    public RequestHelperResult<T> Post<T>(string url, dynamic prms)
    {
        return BaseRequest<T>(RequestType.Post, url, prms);
    }

    /// <summary>
    /// Put isteklerinin yapılacağı method.
    /// </summary>
    /// <typeparam name="T">İstenen geri dönüş tipi</typeparam>
    /// <param name="url">Base url den sonra gelen ilgili methodun yolu veya yolun tamamı</param>
    /// <param name="prms">Gönderilecek parametreler</param>
    /// <returns></returns>
    public RequestHelperResult<T> Put<T>(string url, dynamic prms)
    {
        return BaseRequest<T>(RequestType.Put, url, prms);
    }

    /// <summary>
    /// Delete isteklerinin yapılacağı method.
    /// </summary>
    /// <typeparam name="T">İstenen geri dönüş tipi</typeparam>
    /// <param name="url">Base url den sonra gelen ilgili methodun yolu veya yolun tamamı</param>
    /// <returns></returns>
    public RequestHelperResult<T> Delete<T>(string url)
    {
        return BaseRequest<T>(RequestType.Delete, url, null);
    }

    /// <exclude />
    private RequestHelperResult<T> BaseRequest<T>(RequestType requestType, string url, dynamic prms)
    {
        RequestHelperResult<T> res = new RequestHelperResult<T>();
        try
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url null olamaz!");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client;
            if (_client != null)
                client = _client;
            else
                client = HttpClientFactory.Create(clientHandler);

            if (_headers != null && _headers.Headers != null)
            {
                foreach (var header in _headers.Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            if (client.Timeout != Timeout.InfiniteTimeSpan)
                client.Timeout = Timeout.InfiniteTimeSpan;

            string fullUrl = url;
            if (!url.StartsWith("http") && !string.IsNullOrWhiteSpace(_baseUrl))
            {
                fullUrl = _baseUrl + url;
            }
            HttpResponseMessage result = new HttpResponseMessage();
            if (requestType == RequestType.Get)
            {
                result = client.GetAsync(fullUrl, System.Threading.CancellationToken.None).GetAwaiter().GetResult();
            }
            else if (requestType == RequestType.Post)
            {
                if (prms == null)
                    throw new ArgumentNullException("prms null olamaz!");

                var json = JsonConvert.SerializeObject(prms);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                result = client.PostAsync(fullUrl, stringContent).GetAwaiter().GetResult();
            }
            else if (requestType == RequestType.Put)
            {
                if (prms == null)
                    throw new ArgumentNullException("prms null olamaz!");

                var json = JsonConvert.SerializeObject(prms);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                result = client.PutAsync(fullUrl, stringContent).GetAwaiter().GetResult();
            }
            else if (requestType == RequestType.Delete)
            {
                result = client.DeleteAsync(fullUrl).GetAwaiter().GetResult();
            }

            res.StatusCode = result.StatusCode;

            if (result.IsSuccessStatusCode)
            {
                if (typeof(T).IsValueType || typeof(T) == typeof(string))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    res.Result = (T)conv.ConvertFromString(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
                else if (typeof(T).IsClass || typeof(T).IsInterface)
                {
                    res.Result = JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
            }
            else
            {
                throw new Exception(result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
        catch (Exception ex)
        {
            res.Error = ex;
        }

        return res;
    }
}
