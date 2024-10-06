using System.Text.Json.Serialization;

namespace NineCWebMarket.Frontend.Models.Api;

public record Planet(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("genesisHash")] string GenesisHash,
    [property: JsonPropertyName("genesisUri")] string GenesisUri,
    [property: JsonPropertyName("9cscanUrl")] string NineCScanUrl,
    [property: JsonPropertyName("rpcEndpoints")] RpcEndpoints RpcEndpoints,
    [property: JsonPropertyName("bridges")] IReadOnlyDictionary<string, Bridge> Bridges
);

public record RpcEndpoints(
    [property: JsonPropertyName("dp.gql")] IReadOnlyList<string> DpGql,
    [property: JsonPropertyName("9cscan.rest")] IReadOnlyList<string> NineCScanRest,
    [property: JsonPropertyName("headless.gql")] IReadOnlyList<string> HeadlessGql,
    [property: JsonPropertyName("headless.grpc")] IReadOnlyList<string> HeadlessGrpc,
    [property: JsonPropertyName("market.rest")] IReadOnlyList<string> MarketRest,
    [property: JsonPropertyName("world-boss.rest")] IReadOnlyList<string> WorldBossRest,
    [property: JsonPropertyName("patrol-reward.gql")] IReadOnlyList<string> PatrolRewardGql,
    [property: JsonPropertyName("arena.gql")] IReadOnlyList<string> ArenaGql
);

public record Bridge(
    [property: JsonPropertyName("agent")] string Agent,
    [property: JsonPropertyName("avatar")] string Avatar
);



