using System.Collections.Generic;

namespace TS.Common.Entities
{
    /// <summary>
    /// Endpoint information
    /// </summary>
    public interface IEndpointEntity : IEntity
    {
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public string POSType { get; set; }
        public string Directory { get; set; }
        public string UrlPath { get; set; }
        public string Method { get; set; }
        public bool IsPeriodRelated { get; set; }
        public IDictionary<string, string> Body { get; set; }
    }
}