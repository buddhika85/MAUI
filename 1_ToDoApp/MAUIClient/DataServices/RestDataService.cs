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

        public async Task AddAsync(ToDo toDo)
        {
            if (!IsInternetAvailable())
            {
                Debug.WriteLine("----> No Internet Access");
                return;
            }
            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseAddress}/todo", content);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("----> Not Http 2xx responses");
                    return;
                }
                Debug.WriteLine("----> Successfully added to do");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----> Exception: {ex?.Message}");
                return;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (!IsInternetAvailable())
            {
                Debug.WriteLine("----> No Internet Access");
                return;
            }
            try
            {
               
                var response = await _httpClient.DeleteAsync($"{_baseAddress}/todo/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("----> Not Http 2xx responses");
                    return;
                }
                Debug.WriteLine($"----> Successfully deleted to do with ID {id}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----> Exception: {ex?.Message}");
                return;
            }
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

        public async Task UpdateAsync(ToDo toDo)
        {
            if (!IsInternetAvailable())
            {
                Debug.WriteLine("----> No Internet Access");
                return;
            }
            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseAddress}/todo/{toDo.Id}", content);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("----> Not Http 2xx responses");
                    return;
                }
                Debug.WriteLine("----> Successfully updated to do");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----> Exception: {ex?.Message}");
                return;
            }
        }
    }
}
