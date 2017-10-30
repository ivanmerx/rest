using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using REST.Entity;
using REST.Interfaces;

namespace REST.WebClient
{
    class Rest
    {
        public async Task<List<Photo>> GetPersonsListAsync(MainWindow main)
        {
            string url = "http://jsonplaceholder.typicode.com";
            var client = new RestClient(url);

            var request = new RestRequest("/photos", Method.GET);

            IRestResponse response = client.Execute(request);

            IParser parser = new JsonParser();
            return await parser.ParseStringAsync<List<Photo>>(response.Content);

        }
    }
}
