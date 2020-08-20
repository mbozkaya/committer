using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CommitterService.Enum.Constant;

namespace CommitterService
{
    public interface ISlackService
    {
        public void WriteMessage(string message, SlackType channel = SlackType.Info);
    }
    public class SlackService : ISlackService
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public SlackService()
        {
            string token = "xoxp-1296544803815-1323913382657-1312486540226-73ed7111778b8581458746f47a3528a5";
            _url = "https://slack.com/api/chat.postMessage";
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async void WriteMessage(string message, SlackType channel = SlackType.Info)
        {
            var postObject = new { channel = $"#{channel}", text = message };
            var json = JsonConvert.SerializeObject(postObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PostAsync(_url, content);
        }
    }
}
