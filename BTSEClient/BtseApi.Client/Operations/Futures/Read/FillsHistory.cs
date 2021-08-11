﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using RestSharp;

using BtseApi.Client.DataClasses.Futures;
using BtseApi.Client.ApiSettings;
using BtseApi.Client.Helpers;

namespace BtseApi.Client.Operations.Futures.Read
{
    public static class FillsHistory
    {
        private static string url = "/api/v2.1/user/trade_history";

        /// <summary>
        /// Returns user fills history.
        /// API returns a maximum of 500 records at each time.
        /// To paginate beyond the 500 records, you will need to specify the beforeSerialId or afterSerialId field.
        /// </summary>
        /// <param name="symbol">Symbol representing the market (eg. BTCPFC, ETHPFC)</param>
        /// <param name="startTime">The earliest timestamp to return result for</param>
        /// <param name="endTime">The latest timestamp to return result for</param>
        /// <param name="beforeSerialId">Query items before the ID number</param>
        /// <param name="afterSerialId">Query items after the ID number</param>
        /// <param name="count">Number of requested items, default = 10, max = 500</param>
        /// <param name="includeOld">Include trades older than 7 days, default = false</param>
        /// <returns></returns>
        public static string Execute(
            string symbol = null,
            long? startTime = null,
            long? endTime = null,
            long? beforeSerialId = null,
            long? afterSerialId = null,
            int? count = null,
            bool? includeOld = null
            )
        {
            var client = Helper.GetClient(url);

            var request = new RestRequest(Method.GET);
            Helper.AddRequestAuth(request, url, string.Empty);

            if (symbol != null)
            {
                request.AddParameter("symbol", symbol, ParameterType.QueryString);
            }

            if (startTime.HasValue)
            {
                request.AddParameter("startTime", symbol, ParameterType.QueryString);
            }

            if (endTime.HasValue)
            {
                request.AddParameter("endTime", symbol, ParameterType.QueryString);
            }

            if (beforeSerialId.HasValue)
            {
                request.AddParameter("beforeSerialId", symbol, ParameterType.QueryString);
            }

            if (afterSerialId.HasValue)
            {
                request.AddParameter("afterSerialId", symbol, ParameterType.QueryString);
            }

            if (count.HasValue)
            {
                request.AddParameter("count", symbol, ParameterType.QueryString);
            }

            if (includeOld.HasValue)
            {
                request.AddParameter("includeOld", symbol, ParameterType.QueryString);
            }

            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        public static List<TradeHistoryResponse> ExecuteObj(
            string symbol = null,
            long? startTime = null,
            long? endTime = null,
            long? beforeSerialId = null,
            long? afterSerialId = null,
            int? count = null,
            bool? includeOld = null
        )
        {
            var json = Execute(symbol, startTime, endTime, beforeSerialId,
                            afterSerialId, count, includeOld);

            var result =
            JsonSerializer.Deserialize<List<TradeHistoryResponse>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
