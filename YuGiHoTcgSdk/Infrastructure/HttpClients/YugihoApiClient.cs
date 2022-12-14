namespace YuGiHoTcgSdk.Infrastructure.HttpClients
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Base;
    using Features.CacheManager;
    using Newtonsoft.Json;

    public class YugihoApiClient
    {
        private readonly HttpClient _client;
        private readonly Uri _baseUri = new Uri("https://db.ygoprodeck.com/api/v7/");
        private readonly ResourceCacheManager _resourceCache = new ResourceCacheManager();
        private readonly ResourceListCacheManager _resourceListCache = new ResourceListCacheManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="YugihoApiClient"/>
        /// </summary>
        public YugihoApiClient()
        {
            _client = new HttpClient() { BaseAddress = _baseUri };
        }

        /// <summary>
        /// Constructor with message handler 
        /// </summary>
        /// <param name="messageHandler">Message handler implementation</param>
        public YugihoApiClient(HttpMessageHandler messageHandler)
        {
            _client = new HttpClient(messageHandler) { BaseAddress = _baseUri };
        }

        /// <summary>
        /// Construct accepting directly a HttpClient. Useful when used in projects where
        /// IHttpClientFactory is used to create and configure HttpClient instances with different policies.
        /// See https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        /// </summary>
        /// <param name="httpClient">HttpClient implementation. Should include api key in header else you will be rate limited. See docs</param>
        public YugihoApiClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _client.BaseAddress = _baseUri;
        }

        /// <summary>
        /// Close resources
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
            _resourceCache.Dispose();
            _resourceListCache.Dispose();
        }


        /// <summary>
        /// Clears all cached data for both resources and resource lists
        /// </summary>
        public void ClearCache()
        {
            _resourceCache.ClearAll();
            _resourceListCache.ClearAll();
        }

        /// <summary>
        /// Clears the cached data for a specific resource
        /// </summary>
        /// <typeparam name="T">The type of cache</typeparam>
        public void ClearResourceCache<T>()
            where T : ResourceBase
        {
            _resourceCache.Clear<T>();
        }

        /// <summary>
        /// Clears the cached data for all resource types
        /// </summary>
        public void ClearResourceCache()
        {
            _resourceCache.ClearAll();
        }

        /// <summary>
        /// Clears the cached data for all resource lists
        /// </summary>
        public void ClearResourceListCache()
        {
            _resourceListCache.ClearAll();
        }

        /// <summary>
        /// Clears the cached data for a specific resource list
        /// </summary>
        /// <typeparam name="T">The type of cache</typeparam>
        public void ClearResourceListCache<T>()
            where T : ResourceBase
        {
            _resourceListCache.Clear<T>();
        }

        /// <summary>
        /// Gets an array returned of resource data
        /// </summary>
        /// <typeparam name="T">The type of resource</typeparam>
        /// <returns>The paged resource object</returns>
        public async Task<List<T>> GetArrayResourceAsync<T>() where T : ResourceBase
        {
            string url = GetApiEndpointString<T>();
            return await GetAsync<List<T>>(url, CancellationToken.None);
        }

        public async Task<T> GetSetInfoResourceAsync<T>(IDictionary<string, string> filters) where T : ResourceBase
        {
            string url = GetApiEndpointString<T>();
            return await GetAsync<T>(AddQueryFilterParamsToUrl(url, filters), CancellationToken.None);
        }

        /// <summary>
        /// Gets a single page of unnamed resource data
        /// </summary>
        /// <typeparam name="T">The type of resource</typeparam>
        /// <param name="cancellationToken">Cancellation token for the request; not utilitized if data has been cached</param>
        /// <returns>The paged resource object</returns>
        public Task<ApiResourceList<T>> GetApiResourceAsync<T>(CancellationToken cancellationToken = default)
            where T : ApiResource
        {
            string url = GetApiEndpointString<T>();
            return InternalGetApiResourcePageAsync<T>(AddPaginationParamsToUrl(url), cancellationToken);
        }

        /// <summary>
        /// Gets the specified page of unnamed resource data
        /// </summary>
        /// <typeparam name="T">The type of resource</typeparam>
        /// <param name="take">The number of cards to return</param>
        /// <param name="skip">Page offset/skip</param>
        /// <param name="cancellationToken">Cancellation token for the request; not utilitized if data has been cached</param>
        /// <returns>The paged resource object</returns>
        public Task<ApiResourceList<T>> GetApiResourceAsync<T>(int take, int skip, CancellationToken cancellationToken = default)
            where T : ApiResource
        {
            string url = GetApiEndpointString<T>();
            return InternalGetApiResourcePageAsync<T>(AddPaginationParamsToUrl(url, take, skip), cancellationToken);
        }

        /// <summary>
        /// Gets the specified page of unnamed resource data
        /// </summary>
        /// <typeparam name="T">The type of resource</typeparam>
        /// <param name="filters">Dictionary of filters based on data fields. e.g name=base </param>
        /// <param name="cancellationToken">Cancellation token for the request; not utilitized if data has been cached</param>
        /// <returns>The paged resource object</returns>
        public Task<ApiResourceList<T>> GetApiResourceAsync<T>(IDictionary<string, string> filters, CancellationToken cancellationToken = default)
            where T : ApiResource
        {
            string url = GetApiEndpointString<T>();
            return InternalGetApiResourcePageAsync<T>(AddQueryFilterParamsToUrl(url, filters), cancellationToken);
        }

        /// <summary>
        /// Gets the specified page of unnamed resource data
        /// </summary>
        /// <typeparam name="T">The type of resource</typeparam>
        /// <param name="take">The number of cards to return</param>
        /// <param name="skip">Page offset/skip</param>
        /// <param name="filters">Dictionary of filters based on data fields. e.g name=base </param>
        /// <param name="cancellationToken">Cancellation token for the request; not utilitized if data has been cached</param>
        /// <returns>The paged resource object</returns>
        public Task<ApiResourceList<T>> GetApiResourceAsync<T>(int take, int skip, IDictionary<string, string> filters, CancellationToken cancellationToken = default)
            where T : ApiResource
        {
            string url = GetApiEndpointString<T>();
            return InternalGetApiResourcePageAsync<T>(AddQueryFilterParamsToUrl(url, filters, take, skip), cancellationToken);
        }

        private async Task<ApiResourceList<T>> InternalGetApiResourcePageAsync<T>(string url, CancellationToken cancellationToken)
            where T : ApiResource
        {
            var resources = _resourceListCache.GetApiResourceList<T>(url);
            if (resources == null)
            {
                resources = await GetAsync<ApiResourceList<T>>(url, cancellationToken);
                _resourceListCache.Store(url, resources);
            }
            else
            {
                // we do this as a marker that the cache is used, useful for debugging
                resources.FromMemoryCache = true;
            }

            return resources;
        }

        /// <summary>
        /// Handles all outbound API requests to the Pokemon API server and deserializes the response
        /// </summary>
        private async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

            #if DEBUG
            // For debugging respose pre deserialisation
            var type = typeof(T);
            var responseStr = response.Content.ReadAsStringAsync().Result;
            #endif

            // TODO: Return Error msg on bad req
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return DeserializeStream<T>(await response.Content.ReadAsStreamAsync());
                }
            }

            return DeserializeStream<T>(await response.Content.ReadAsStreamAsync());
        }

        /// <summary>
        /// Handles deserialization of a given stream to a given type
        /// </summary>
        private T DeserializeStream<T>(System.IO.Stream stream)
        {
            using var sr = new System.IO.StreamReader(stream);
            using JsonReader reader = new JsonTextReader(sr);
            var serializer = JsonSerializer.Create();
            return serializer.Deserialize<T>(reader);
        }

        private static string AddPaginationParamsToUrl(string uri, int? pageSize = null, int? page = null)
        {
            var queryParameters = new Dictionary<string, string>();

            // TODO consider to always set the pageSize parameter when not present to the default "20"
            // in order to have a single cached resource list for requests with explicit or implicit default take
            if (pageSize.HasValue)
            {
                queryParameters.Add(nameof(pageSize), pageSize.Value.ToString());
            }

            if (page.HasValue)
            {
                queryParameters.Add(nameof(page), page.Value.ToString());
            }

            return QueryHelpers.AddQueryString(uri, queryParameters);
        }

        private static string AddQueryFilterParamsToUrl(string uri, IDictionary<string, string> filterQuery, int? num = null, int? offset = null)
        {
            var queryParameters = new Dictionary<string, string>();

            // TODO consider to always set the pageSize parameter when not present to the default "20"
            // in order to have a single cached resource list for requests with explicit or implicit default take
            if (num.HasValue)
            {
                // Page size
                queryParameters.Add(nameof(num), num.Value.ToString());
            }

            if (offset.HasValue)
            {
                // page number
                queryParameters.Add(nameof(offset), offset.Value.ToString());
            }

            return QueryHelpers.AddQueryFiltersString(uri, queryParameters, filterQuery);
        }

        private static string GetApiEndpointString<T>()
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty("ApiEndpoint", BindingFlags.Static | BindingFlags.NonPublic);
            return propertyInfo.GetValue(null).ToString();
        }

        private static bool IsApiEndpointCaseSensitive<T>()
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty("IsApiEndpointCaseSensitive", BindingFlags.Static | BindingFlags.NonPublic);
            return propertyInfo == null ? false : (bool)propertyInfo.GetValue(null);
        }
    }
}