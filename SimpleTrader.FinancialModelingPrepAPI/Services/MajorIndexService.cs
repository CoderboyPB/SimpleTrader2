﻿using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepHttpClient client;

        public MajorIndexService(FinancialModelingPrepHttpClient client)
        {
            this.client = client;
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            string uriSuffix = GetUriSuffix(indexType);
            string uri = $"majors-indexes/{uriSuffix}";

            MajorIndex majorIndex = await client.GetAsync<MajorIndex>(uri);
            majorIndex.Type = indexType;

            return majorIndex;
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new Exception("MajorIndexType has not a suffix defined");
            }
        }
    }
}
