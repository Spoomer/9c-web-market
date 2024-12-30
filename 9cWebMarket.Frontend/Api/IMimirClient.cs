using GraphQL.Client.Http;

namespace NineCWebMarket.Frontend.Api;

public interface IMimirClient
{
    GraphQLHttpClient GetClient(string planet);
}