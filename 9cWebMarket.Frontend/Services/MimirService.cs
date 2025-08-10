using GraphQL;
using Nekoyume.Model.Item;
using NineCWebMarket.Frontend.Api;
using NineCWebMarket.Frontend.Models.Mimir;

namespace NineCWebMarket.Frontend.Services;

public class MimirService : IMarketService
{
    private readonly IMimirClient _mimirClient;

    public MimirService(IMimirClient mimirClient)
    {
        _mimirClient = mimirClient;
    }
    public async Task<IReadOnlyList<ProductItem>> GetItemProductsAsync(string planet, int skip, int take, ItemSubType itemSubType,
        ProductSortBy sortBy, SortDirection sortDirection, string avatarAddress = "")
    {
        var itemProductRequest = new GraphQLRequest
        {
            Query = GetItemProductQuery(take),
            OperationName = "GetItemProducts",
            Variables = new
            {
                skip,
                itemSubType = itemSubType.ToString().ToUpper(),
                sortBy,
                sortDirection,
            }
        };
        var client = _mimirClient.GetClient(planet);
        try
        {
            var graphQlResponse = await client.SendQueryAsync<GetItemProductsResponse>(itemProductRequest);
            return graphQlResponse.Data?.Products?.Items ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }
    
    // take is not a graphql variable because of fieldCosts....
    private static string GetItemProductQuery(int take) =>
        $$"""
        query GetItemProducts( $skip : Int $itemSubType: ItemSubType, $sortBy : ProductSortBy , $sortDirection: SortDirection ) {
            products(skip: $skip, take: {{take}}, filter: { itemSubType: $itemSubType, 
                itemType: EQUIPMENT productType: NON_FUNGIBLE
                sortBy: $sortBy sortDirection: $sortDirection
            }) {
                items {
                    avatarAddress
                    combatPoint
                    crystal
                    crystalPerPrice
                    productsStateAddress
                    unitPrice
                    object {
                        ... ItemProduct
                    }
                }
            }
        }

        fragment ItemProduct on ItemProduct {
            itemCount
            productId
            productType
            registeredBlockIndex
            sellerAgentAddress
            sellerAvatarAddress
            tradableItem {
                elementalType
                grade
                id
                itemSubType
                itemType
                ... Weapon
                ... Armor
                ... Belt
                ... Necklace
                ... Ring
        
            }
            price {
                decimalPlaces
                quantity
                rawValue
                ticker
            }
        }
        fragment Weapon on Weapon {
            elementalType
            equipped
            exp
            grade
            id
            itemId
            itemSubType
            itemType
            level
            madeWithMimisbrunnrRecipe
            optionCountFromCombination
            requiredBlockIndex
            setId
            spineResourcePath
            skills {
                ... Skills
            }
            statsMap {
                ... Stats
            }
        }
        fragment Armor on Armor{
            elementalType
            equipped
            exp
            grade
            id
            itemId
            itemSubType
            itemType
            level
            madeWithMimisbrunnrRecipe
            optionCountFromCombination
            requiredBlockIndex
            setId
            spineResourcePath
            skills {
                ... Skills
            }
            statsMap {
                ... Stats
            }
        }
        fragment Belt on Belt {
            elementalType
            equipped
            exp
            grade
            id
            itemId
            itemSubType
            itemType
            level
            madeWithMimisbrunnrRecipe
            optionCountFromCombination
            requiredBlockIndex
            setId
            spineResourcePath
            skills {
                ... Skills
            }
            statsMap {
                ... Stats
            }
        }
        fragment Necklace on Necklace{
            elementalType
            equipped
            exp
            grade
            id
            itemId
            itemSubType
            itemType
            level
            madeWithMimisbrunnrRecipe
            optionCountFromCombination
            requiredBlockIndex
            setId
            spineResourcePath
            skills {
                ... Skills
            }
            statsMap {
                ... Stats
            }
        }
        fragment Ring on Ring {
            elementalType
            equipped
            exp
            grade
            id
            itemId
            itemSubType
            itemType
            level
            madeWithMimisbrunnrRecipe
            optionCountFromCombination
            requiredBlockIndex
            setId
            spineResourcePath
            skills {
                ... Skills
            }
            statsMap {
                ... Stats
            }
        }

        fragment Stats on StatMap{
            value {
                key
                value {
                    additionalValue
                    baseValue
                    statType
                }
            }
        }

        fragment Skills on Skill {
            chance
            power
            referencedStatType
            statPowerRatio
            skillRow {
                combo
                cooldown
                elementalType
                hitCount
                id
                skillCategory
                skillTargetType
                skillType
            }
        }

        """;
}