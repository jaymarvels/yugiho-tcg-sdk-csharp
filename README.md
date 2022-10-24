
# Pokémon TCG SDK C#
A .Net wrapper for the Yu-Gi-Ho API at [ygoprodeck.com/api-guide/](https://ygoprodeck.com/api-guide/).

Targets .Net Standard 2.0+.

<!--![workflow](https://github.com/jaymarvels/yugiho-tcg-sdk-csharp/actions/workflows/main.yml/badge.svg)-->
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/jaymarvels/yugiho-tcg-sdk-csharp/CI%20-%20Build%20to%20Nuget?label=CI%20-%20Build&style=flat-square)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/jaymarvels/yugiho-tcg-sdk-csharp?display_name=tag)
![Nuget](https://img.shields.io/nuget/v/YuGiHoTcgSdk?style=flat-square)

# Use
Please check [ygoprodeck.com/api-guide/](https://ygoprodeck.com/api-guide/) for information around rate limits, images and downloading of data.
```cs
// instantiate client
YugihoApiClient pokeClient = new YugihoApiClient();
```
Internally, `YugihoApiClient` uses an instance of the `HttpClient` class. As such, instances of `YugihoApiClient` are [meant to be instantiated once and re-used throughout the life of an application.](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netcore-3.1#remarks)

There are additional `YugihoApiClient` constructors that support your own httpclients as well as `HttpMessageHandler`. This is especially useful when used in projects where [IHttpClientFactory is used to create and configure HttpClient instances with different policies](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0).
```c#
// your httpclient (factory, named clients, typed clients, generated clients)
var client = _httpClientFactory.CreateClient();

// instantiate client
YugihoApiClient yugClient = new YugihoApiClient(client);
```

## Method Definitions
```c#
// gets all cards regardless of type
var card = await yugClient.GetApiResourceAsync<Card>();

// with pagination
var card = await yugClient.GetApiResourceAsync<Card>(take: 10, skip: 2);

// Monster Cards
// There are multiple types for each monster type e.g. Effect Monster or Spirit Monster, as well as All Monsters, see below docs for all possible types
var card = await yugClient.GetApiResourceAsync<AllMonsters>();
var card = await yugClient.GetApiResourceAsync<AllMonsters>(take: 10, skip: 2);

var card = await yugClient.GetApiResourceAsync<EffectMonster>();
var card = await yugClient.GetApiResourceAsync<EffectMonster>(take: 10, skip: 2);

var card = await yugClient.GetApiResourceAsync<LinkMonster>();
var card = await yugClient.GetApiResourceAsync<LinkMonster>(take: 10, skip: 2);

// Skill Cards
var card = await yugClient.GetApiResourceAsync<SkillCard>();
var card = await yugClient.GetApiResourceAsync<SkillCard>(take: 10, skip: 2);

// Spell Cards
var card = await yugClient.GetApiResourceAsync<SpellCard>();
var card = await yugClient.GetApiResourceAsync<SpellCard>(take: 10, skip: 2);

// Token Cards
var card = await yugClient.GetApiResourceAsync<TokenCard>();
var card = await yugClient.GetApiResourceAsync<TokenCard>(take: 10, skip: 2);

// Trap Cards
var card = await yugClient.GetApiResourceAsync<TrapCard>();
var card = await yugClient.GetApiResourceAsync<TrapCard>(take: 10, skip: 2);

// Set Info
// This does require a parameter
var card = await yugClient.GetSetInfoResourceAsync<SetInfo>();

// Sets
//Set Info
var card = await yugClient.GetArrayResourceAsync<Sets>();
```
## Filter Definitions
Most card types (Monster, Skill, Spell, Token, Trap) have some helpful filter extensions that cover off a lot of the usual filter needs. These can be stacked also. There are some very specific ones i.e. attack filters, and a generic catch all filter `AddFilter` which allows you to pass in the nameof a model value
```c#
CardFilterBuilder.CreateCardFilter()
```
e.g
```c#
var filter = CardFilterBuilder.CreateCardFilter().AddName("3-Hump Lacooda");
```
Or
```c#
var filter = CardFilterBuilder.CreateCardFilter().AddFilter("3-Hump Lacooda", nameof(Monster.Name));

// Stacked filters
var filter = CardFilterBuilder.CreateCardFilter()
    .AddName("3-Hump Lacooda")
    .AddName("4-Starred Ladybug of Doom");
```
Please see offical documentation for more advance filters until these are added in as extension.

For example, if you don't want to use any extension methods you can create your own filter from scratch. 
Be aware that the key needs to match the json fields in the api return. 
Also for multiple matches (so OR filter) this is done by seperating with a comma, no spaces as the example of "name" shows below.
```c#
 var filter = new Dictionary<string, string>
 {
     {"name", "3-Hump Lacooda,4-Starred Ladybug of Doom"},
     {"race", "Beast"}
 };
```
#### Using Filters
Once you have built up the filter you can pass it into your call method. Pagination is still supported
```c#
var filter = CardFilterBuilder.CreateCardFilter().AddName("3-Hump Lacooda");
var card = await yugClient.GetApiResourceAsync<AllMonsters>(filter);
// Pagination
var cards = await yugClient.GetApiResourceAsync<AllMonsters>(10, 2, filter);
```
## String Method Definitions
As these lists as small and of type List<T> these will return all.
##### Archetypes
```c#
var types = await pokeClient.GetArrayResourceAsync<Archetypes>();
```
##### Sets
```c#
var sets = await pokeClient.GetArrayResourceAsync<Sets>();
```
##### Set Info
Set Info requires a parameter passed through
```c#
var dicObj = new Dictionary<string, string> {{"setcode", "SDY-046"}};
var setinfo = await pokeClient.GetSetInfoResourceAsync<SetInfo>(dicObj);
```

## Caching
Every resource response is automatically cached in memory, making all subsequent requests for the same resource (url matching) pull cached data. Example:
```c#
// this will fetch the data from the API  
var filter = CardFilterBuilder.CreateCardFilter().AddName("3-Hump Lacooda");
var card = await yugClient.GetApiResourceAsync<AllMonsters>(filter);

// another call to the same resource will fetch from the cache
var filter = CardFilterBuilder.CreateCardFilter().AddName("3-Hump Lacooda");
var card = await yugClient.GetApiResourceAsync<AllMonsters>(filter);  
```
This can be confirmed by:
```c#
var fromCache = card.FromCache;
```

To clear the cache of data:
```c#
// clear all caches for both resources and pages
pokeClient.ClearCache();
```
Additional overloads are provided to allow for clearing the individual caches for resources, as well as by type of cache.

## Class Definitions
```C#
Card
TrapCard
PokemonCard
TokenCard
SpellCard
Sets
SetInfo
Archetypes
AllMonsters
-----------
NormalMonster
NormalTunerMonster
EffectMonster
TunerMonster
FlipMonster
FlipEffectMonster
FlipTunerEffectMonster
SpiritMonster
UnionEffectMonster
GeminiMonster
PendulumEffectMonster
PendulumNormalMonster
PendulumTunerEffectMonster
RitualMonster
RitualEffectMonster
ToonMonster
FusionMonster
SynchroMonster
SynchroTunerMonster
SynchroPendulumEffectMonster
XYZMonster
XYZPendulumEffectMonster
LinkMonster
PendulumFlipEffectMonster
PendulumEffectFusionMonster

``` 
##### Card

```C#
string Id 
string Name 
string Type 
string Desc 
string Race 
string Archetype 
List<SetInfo> CardSets 
List<CardImage> CardImages 
List<CardPrice> CardPrices 
int? Atk 
int? Def 
int? Level 
string Attribute 
Banlist BanlistInfo 
int? Scale 
int? Linkval 
List<string> Linkmarkers 
``` 

#### Monster (type) Card
```c#
string Id 
string Name 
string Type 
string Desc 
int Atk 
string Race 
string Attribute 
string Archetype 
int Linkval 
List<string> Linkmarkers 
List<SetInfo> CardSets 
List<CardImage> CardImages 
List<CardPrice> CardPrices 
Banlist BanlistInfo 
```

##### TrapCard
```c#
string Id 
string Name 
string Type 
string Desc 
string Race 
List<SetInfo> CardSets 
List<CardImage> CardImages 
List<CardPrice> CardPrices 
string Archetype 
Banlist BanlistInfo 
```
##### TokenCard
````c#
string Id 
string Name 
string Type 
string Desc 
int Atk 
int Def 
int Level 
string Race 
string Attribute 
string Archetype 
List<SetInfo> CardSets 
List<CardImage> CardImages 
List<CardPrice> CardPrices 
````
##### SpellCard
```c#
string Id 
string Name 
string Type 
string Desc 
string Race 
string Archetype 
List<SetInfo> CardSets 
List<CardImage> CardImages 
List<CardPrice> CardPrices 
Banlist BanlistInfo 
```    
##### SkillCard
```c#
string Id 
string Name 
string Type 
string Desc 
string Race 
List<CardImage> CardImages 
List<CardPrice> CardPrices 
List<SetInfo> CardSets 
string Archetype 
```
##### Sets
```c#
string Id
string SetName 
string SetCode 
long NumOfCards 
DateTime? TcgDate 
```
##### SetInfo
```c#
string Id 
string SetName 
string SetCode 
string SetRarity 
string SetRarityCode 
string SetPrice 
```
##### CardImage
```c#
long Id 
Uri ImageUrl 
Uri ImageUrlSmall 
```
##### CardPrice
```c#
string CardmarketPrice 
string TcgplayerPrice 
string EbayPrice 
string AmazonPrice 
string CoolstuffincPrice 
```
##### Banlist
```c#
string BanOcg 
string BanTcg 
string BanGoat 
```


## Contributing

* -   Open an issue
    -   Describe what the SDK is missing and what changes you'd like to see implemented
    -   **Ask clarifying questions**
* Fork it (click the Fork button at the top of the page)
* Create your feature branch (git checkout -b my-new-feature)
* Commit your changes (git commit -am 'Add some feature')
* Push to the branch (git push origin my-new-feature)
* Create a new Pull Request to ```develop```

#### Mentions
- Caching and some api handling used/inspired from [PokeApiNet](https://github.com/mtrdp642/PokeApiNet) under the MIT license
- Inspired based off my other SDK [PokemonTCG SDK](https://github.com/PokemonTCG/pokemon-tcg-sdk-csharp) under MIT license
