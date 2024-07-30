namespace kersenduz.WebApp.RequestHelper.Interfaces;

public interface IRequestHelper
{
    RequestHelperResult<T> Get<T>(string url);

    RequestHelperResult<T> Post<T>(string url, dynamic prms);

    RequestHelperResult<T> Put<T>(string url, dynamic prms);

    RequestHelperResult<T> Delete<T>(string url);
}
