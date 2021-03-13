using CsvHelper;
using Model.Entities;
using Model.Utils;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class StockService : IStockService
    {
        private string baseURI = "https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv";
        public async Task<string> SendRequestAsync(string command)
        {
            var url = String.Format(baseURI, command);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponse = await response.Content.ReadAsStreamAsync();
                            if (apiResponse != null)
                            {
                                return ReadStreamResponse(apiResponse, command);
                            }
                        }
                        return CommandNotValidMessage(command);
                    }
                }
            } catch (Exception)
            {
                return Constants.UnavailableService;
            }
        }

        private string ReadStreamResponse(Stream stream, string command)
        {
            try
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<Stock>().FirstOrDefault();
                        if (records.Open.Equals(Constants.StockNoDataFromCommandText))
                        {
                            return CommandNotValidMessage(command);
                        }
                        else
                        {
                            return CommandMessageResult(command.ToUpper(), records.Open);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CommandNotValidMessage(string command) 
            => String.Format("{0} is not a valid command", command);

        private string CommandMessageResult(string command, string price)
            => String.Format("{0} quote is ${1} per share", command.ToUpper(), price);
    }
}
