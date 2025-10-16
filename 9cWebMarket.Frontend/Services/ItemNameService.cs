using System.Collections;
using System.Collections.Frozen;
using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Options;
using NineCWebMarket.Frontend.Configuration;
using NineCWebMarket.Frontend.Models.Csv;
using NineCWebMarket.Frontend.Services.Interfaces;

namespace NineCWebMarket.Frontend.Services;

public class ItemNameService : IItemNameService
{
    private readonly IOptions<NineChroniclesEndpoints> _endpoints;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private FrozenDictionary<int, string> _itemNames = FrozenDictionary<int, string>.Empty;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public ItemNameService(IOptions<NineChroniclesEndpoints> endpoints, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _endpoints = endpoints;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task InitAsync()
    {
        await _semaphore.WaitAsync();
        Dictionary<int, string> itemNames = new Dictionary<int, string>();
        try
        {
            if (_itemNames.Count > 0)
            {
                return;
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("BasePath")!);
            
            await AddItemNamesFromRemoteCsv(httpClient, itemNames);
            await AddItemNames(httpClient, itemNames);
            await AddItemNamesFromEquipmentItem(httpClient, itemNames);

            _itemNames = itemNames.ToFrozenDictionary();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task AddItemNamesFromRemoteCsv(HttpClient httpClient, Dictionary<int, string> itemNames)
    {
        var response = await httpClient.GetAsync(_endpoints.Value.RemoteCsv);
        if (!response.IsSuccessStatusCode)
        {
            return;
        }
        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<ItemName>();
        foreach (var record in records)
        {
            if (!record.Key.StartsWith("ITEM_NAME_") && !record.Key.StartsWith("ITEM_NAME_CUSTOM_"))
            {
                continue;
            }
            
            if (int.TryParse(record.Key.Split('_')[^1], CultureInfo.InvariantCulture, out var key))
            {
                itemNames.TryAdd(key, record.English);
            }
        }
    }

    private async Task AddItemNamesFromEquipmentItem(HttpClient httpClient, Dictionary<int, string> itemNames)
    {
        var response = await httpClient.GetAsync(_endpoints.Value.EquipmentItem);
        if (!response.IsSuccessStatusCode)
        {
            return;
        }
        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<EquipmentItem>();
        foreach (var record in records)
        {
            itemNames.TryAdd(record.Id, record.Name);
        }
    }

    private async Task AddItemNames(HttpClient httpClient, Dictionary<int, string> itemNames)
    {
        var response = await httpClient.GetAsync(_endpoints.Value.ItemName);
        if (!response.IsSuccessStatusCode)
        {
            return;
        }
        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<ItemName>();
        foreach (var record in records)
        {
            if (int.TryParse(record.Key[10..], CultureInfo.InvariantCulture, out var key))
            {
                itemNames.TryAdd(key, record.English);
            }
        }
    }


    public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
    {
        return _itemNames.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_itemNames).GetEnumerator();
    }

    public int Count => _itemNames.Count;

    public bool ContainsKey(int key)
    {
        if (_itemNames.Count == 0)
        {
            _ = InitAsync();
        }

        return _itemNames.ContainsKey(key);
    }

    public bool TryGetValue(int key, out string value)
    {
        if (_itemNames.Count == 0)
        {
            _ = InitAsync();
        }
#nullable disable
        return _itemNames.TryGetValue(key, out value);
#nullable enable
    }

    public string this[int key] => _itemNames.GetValueOrDefault(key) ?? string.Empty;

    public IEnumerable<int> Keys => ((IReadOnlyDictionary<int, string>)_itemNames).Keys;

    public IEnumerable<string> Values => ((IReadOnlyDictionary<int, string>)_itemNames).Values;
}