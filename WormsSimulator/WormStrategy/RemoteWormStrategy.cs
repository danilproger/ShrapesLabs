using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CS_lab.Models;

namespace CS_lab.WormStrategy
{
    public class RemoteWormStrategy: IWormStrategy
    {
        private readonly string _baseUrl;
        
        public RemoteWormStrategy(string ip, string port)
        {
            _baseUrl = $"http://{ip}:{port}/";
        }
        public WormGameStep NextStep(Worm worm, World world, int gameState)
        {
            var wormName = worm.Name;
            var jsonBody = JsonSerializer.Serialize(world);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var requestUrl = $"{_baseUrl}worms/{wormName}/getAction?step={gameState}&run={0}";
            
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

            using (var client = new HttpClient(clientHandler))
            {
                Console.WriteLine(jsonBody);
                var task = client.PostAsync(requestUrl, content);
                var response = task.Result;
                
                var result = response.Content.ReadAsStringAsync().Result;
                var behaviour = JsonSerializer.Deserialize<WormGameStep>(result);
                return behaviour;
            }
        }
    }
}