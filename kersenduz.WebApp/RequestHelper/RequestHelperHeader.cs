using kersenduz.WebApp.RequestHelper.Interfaces;

namespace kersenduz.WebApp.RequestHelper;

public class RequestHelperHeader : IRequestHelperHeader
{
    public RequestHelperHeader()
    {
        Headers = new List<KeyValuePair<string, string>>();
    }
    public RequestHelperHeader(List<KeyValuePair<string, string>> headers)
    {
        Headers = headers;
    }
    public List<KeyValuePair<string, string>> Headers { get; set; }

    public void Add(string key, string value)
    {
        var header = Headers.FirstOrDefault(x => x.Key == key);
        if (!header.Equals(default(KeyValuePair<string, string>)))
        {
            Headers.Remove(header);
        }
        Headers.Add(new KeyValuePair<string, string>(key, value));
    }

    public void Remove(string key)
    {
        var header = Headers.FirstOrDefault(x => x.Key == key);
        if (!header.Equals(default(KeyValuePair<string, string>)))
        {
            Headers.Remove(header);
        }
    }

    public void Clear()
    {
        Headers = new List<KeyValuePair<string, string>>();
    }
}
