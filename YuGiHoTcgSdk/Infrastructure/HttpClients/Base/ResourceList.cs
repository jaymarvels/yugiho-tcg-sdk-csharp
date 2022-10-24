using Newtonsoft.Json;
using System.Collections.Generic;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Base
{
    /// <summary>
    /// The base class for all resource lists
    /// </summary>
    public abstract class ResourceList<T> where T : ResourceBase
    {
        /// <summary>
        /// The object that holds pagination information. Will only populate if request is paginated
        /// </summary>
        [JsonProperty("meta")]
        public Meta Metadata { get; set; }

        /// <summary>
        /// Marker to show if response is from memory cache
        /// </summary>
        public bool FromMemoryCache { get; set; } = false;

        /// <summary>
        /// Will show error from api server
        /// </summary>
        public string Error { get; set; }
    }

    /// <summary>
    /// The object named resources that have a data property
    /// </summary>
    /// <typeparam name="T">The type of the resource</typeparam>
    public class ApiResourceList<T> : ResourceList<T> where T : ApiResource
    {
        /// <summary>
        /// A list of un-named API resources.
        /// </summary>
        [JsonProperty("data")]
        public List<T> Results { get; set; }
    }
}