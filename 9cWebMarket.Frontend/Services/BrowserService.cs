using Microsoft.JSInterop;

namespace NineCWebMarket.Frontend.Services;

public class BrowserService
{
    private readonly IJSRuntime _jsRuntime;

    public BrowserService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public async Task<int> GetInnerWidthAsync()
    {
        return await _jsRuntime.InvokeAsync<int>("helper.getInnerWidth");
    }

    public async Task<bool> IsMobileWidthAsync()
    {
        return await GetInnerWidthAsync() < 640 ? true : false;
    }



}