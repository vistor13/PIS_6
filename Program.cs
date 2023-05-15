using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ExternalServiceExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "d938542a2df1afde45cceb56aeb8ebb5"; // Ваш API-ключ
            string city = "Korosten"; // Назва міста

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Обробка JSON-відповіді
                    JsonDocument jsonDocument = JsonDocument.Parse(responseBody);
                    JsonElement root = jsonDocument.RootElement;

                    // Отримання погодних даних
                    double temperature = root.GetProperty("main").GetProperty("temp").GetDouble();
                    double humidity = root.GetProperty("main").GetProperty("humidity").GetDouble();
                    string cityName = root.GetProperty("name").GetString();

                    // Перетворення температури з Кельвінів на градуси Цельсія
                    double temperatureCelsius = temperature - 273.15;

                    Console.WriteLine($"City: {cityName}");
                    Console.WriteLine($"Temperature: {temperatureCelsius} °C");
                    Console.WriteLine($"Humidity: {humidity}%");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Console.ReadLine();
            }
        }
    }
}



