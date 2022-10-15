﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Base
{
    /// <summary>
    /// The base class for all resource lists
    /// </summary>
    public abstract class ResourceList<T> where T : ResourceBase
    {
        ///// <summary>
        ///// The number of cards results from this API request
        ///// </summary>
        //public int Count { get; set; }

        ///// <summary>
        ///// The total number of available cards from this API request
        ///// </summary>
        //public int TotalCount { get; set; }

        ///// <summary>
        ///// The current page
        ///// </summary>
        //public string Page { get; set; }

        ///// <summary>
        ///// The size of the page
        ///// </summary>
        //public string PageSize { get; set; }

        /// <summary>
        /// The object that holds pagination information. Will only populate if request is paginated
        /// </summary>
        [JsonProperty("meta")]
        public Meta Metadata { get; set; }

        /// <summary>
        /// Marker to show if response is from cache
        /// </summary>
        public bool FromMemoryCache { get; set; } = false;

        public string Error { get; set; }
    }

    /// <summary>
    /// The paging object for un-named resources
    /// </summary>
    /// <typeparam name="T">The type of the paged resource</typeparam>
    public class ApiResourceList<T> : ResourceList<T> where T : ApiResource
    {
        /// <summary>
        /// A list of un-named API resources.
        /// </summary>
        [JsonProperty("data")]
        public List<T> Results { get; set; }
    }
}