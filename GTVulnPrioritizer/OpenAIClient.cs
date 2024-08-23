using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;


namespace GTVulnPrioritizer
{

    public class OpenAIClient
    {
        private static readonly HttpClient client = new HttpClient();
        private const string openAIEmbedingsEndpoint = "https://api.openai.com/v1/embeddings";
        private const string openAIChatEndpoint = "https://api.openai.com/v1/chat/completions";

        private string apiKey;

        public OpenAIClient(string apiKey)
        {
            this.apiKey = apiKey;
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }
        public async Task<string> GetCompletionAsync(string prompt, string context)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are an assistant that helps prioritize software vulnerabilities based on their severity, EPPS, cvss and the description of the vulnerbility" },
                    new { role = "user", content = "Please prioritize the following vulnerabilities in order of importance from most to least important:  with the following context: " + context + " .Please output the information in structured JSON format without using markdown code blocks" }
    }
};
var jsonBody = JsonConvert.SerializeObject(requestBody);
var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
try
{
    var response = await client.PostAsync(openAIChatEndpoint, content);
    response.EnsureSuccessStatusCode();
    var responseBody = await response.Content.ReadAsStringAsync();
                var completionResponse = JsonConvert.DeserializeObject<CompletionResponse>(responseBody);
                return completionResponse.choices[0].message.content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCompletionAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<float>> GetEmbeddingAsync(string text, string model = "text-embedding-ada-002")
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text cannot be null or empty", nameof(text));
            }

            var requestBody = new
            {
                input = text,
                model = model
            };

            var jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(openAIEmbedingsEndpoint, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var embeddingResponse = JsonConvert.DeserializeObject<EmbeddingResponse>(responseBody);
                return embeddingResponse.data[0].embedding;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetEmbeddingAsync: {ex.Message}");
                return null;
            }
        }


        public class EmbeddingResponse
        {
            public List<EmbeddingData> data { get; set; }
        }

        public class EmbeddingData
        {
            public List<float> embedding { get; set; }
        }

        public class CompletionResponse
        {
            public List<Choice> choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string content { get; set; }

        

        }
    }
}