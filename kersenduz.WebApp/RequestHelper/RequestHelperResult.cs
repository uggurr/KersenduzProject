using System.Net;

namespace kersenduz.WebApp.RequestHelper;

public class RequestHelperResult<T>
{
    public HttpStatusCode StatusCode { get; internal set; }
    public bool IsSuccess { get { return this.Error == null; } }
    public Exception Error { get; internal set; }
    public T Result { get; internal set; }
}
