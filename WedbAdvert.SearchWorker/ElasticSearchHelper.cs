using System;
using Microsoft.Extensions.Configuration;
using Nest;

namespace WedbAdvert.SearchWorker
{
    public static class ElasticSearchHelper
    {
        private static IElasticClient _client;

        public static IElasticClient GetInstance(IConfiguration config)
        {
            if (_client == null)
            {
                var url = config.GetSection("ES").GetValue<string>("url");
                var settings = new ConnectionSettings(new Uri(url))
                    .DefaultIndex("adverts")
                    .DefaultTypeName("advert")
                    .DefaultMappingFor<AdvertType>(m => m.IdProperty(x => x.Id));
                _client = new ElasticClient(settings);
            }

            return _client;
        }
    }
}
