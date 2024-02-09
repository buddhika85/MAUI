using MAUIClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAUIClient.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.2.2.2:5113" : "https://localhost:7113";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        protected internal bool IsInternetAvailable()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }

        public Task AddAsync(ToDo toDo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToDo>> GetAllToDosAsync()
        {
            List<ToDo> todos = new();
            if (!IsInternetAvailable()) 
            {
                Debug.WriteLine("----> No Internet Access");
                return todos;
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseAddress}/todo");
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("----> Not Http 2xx responses");
                    return todos;
                }

                string content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSerializerOptions);
                if (result == null)
                {
                    Debug.WriteLine("----> Deserialization resulted in null");
                    return todos;
                }
                return todos;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----> Exception: {ex?.Message}");
                return todos;
            }
        }

        public Task UpdateAsync(ToDo toDo)
        {
            throw new NotImplementedException();
        }
    }
}
