using System.Collections;
using System.Collections.Frozen;
using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Options;
using NineCWebMarket.Frontend.Configuration;
using NineCWebMarket.Frontend.Models.Csv;
using NineCWebMarket.Frontend.Services.Interfaces;

namespace NineCWebMarket.Frontend.Services;

public class TickerNameService : ITickerNameService
{
    private readonly IOptions<NineChroniclesEndpoints> _endpoints;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private FrozenDictionary<string, string> _tickerNames = FrozenDictionary<string, string>.Empty;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public TickerNameService(IOptions<NineChroniclesEndpoints> endpoints, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _endpoints = endpoints;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task InitAsync()
    {
        await _semaphore.WaitAsync();
        Dictionary<string, string> tickerNames = new ();
        try
        {
            if (_tickerNames.Count > 0)
            {
                return;
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("BasePath")!);
            var response = await httpClient.GetAsync(_endpoints.Value.ItemName);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<RuneSheet>();
            foreach (var record in records)
            {
                tickerNames.TryAdd(record.Ticker, record.Name);
            }

            _tickerNames = tickerNames.ToFrozenDictionary();
        }
        finally
        {
            _semaphore.Release();
        }
    }


    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _tickerNames.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_tickerNames).GetEnumerator();
    }

    public int Count => _tickerNames.Count;

    public bool ContainsKey(string key)
    {
        if (_tickerNames.Count == 0)
        {
            _ = InitAsync();
        }

        return _tickerNames.ContainsKey(key);
    }

    public bool TryGetValue(string key, out string value)
    {
        if (_tickerNames.Count == 0)
        {
            _ = InitAsync();
        }
#nullable disable
        return _tickerNames.TryGetValue(key, out value);
#nullable enable
    }

    public string this[string key] => _tickerNames.GetValueOrDefault(key) ?? string.Empty;

    public IEnumerable<string> Keys => ((IReadOnlyDictionary<string, string>)_tickerNames).Keys;

    public IEnumerable<string> Values => ((IReadOnlyDictionary<string, string>)_tickerNames).Values;
}