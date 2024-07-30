namespace kersenduz.WebApp.RequestHelper.Interfaces;

public interface IRequestHelperHeader
{
    List<KeyValuePair<string, string>> Headers { get; set; }

    void Add(string key, string value);
    void Clear();
    void Remove(string key);
}
