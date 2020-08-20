using CommitterService.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CommitterService.Enum.Constant;

namespace CommitterService
{
    public interface ISlackService
    {
        public void WriteMessage(string message, SlackType channel = SlackType.Info);
        public void Slack(Exception exp, string pretext = "");
    }
    public class SlackService : ISlackService
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public SlackService()
        {
            string token = ConfigurationManager.AppSettings.Get("SlackToken");
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

        public void Slack(Exception exp, string pretext = "")
        {

            List<SlackDto.Attachment> attachmentList = new List<SlackDto.Attachment>();
            SlackDto.Attachment attachment = new SlackDto.Attachment();
            attachment.fields = new List<SlackDto.Field>();
            attachment.fields.Add(new SlackDto.Field { title = "Message", value = exp.Message != null ? exp.Message.ToString().Trim() : "" });
            attachment.fields.Add(new SlackDto.Field { title = "Method Name", value = new StackTrace(new StackFrame(2)).GetFrame(0).ToString().Trim() });
            attachment.fields.Add(new SlackDto.Field { title = "InnerException", value = exp.InnerException != null ? exp.InnerException.ToString().Trim() : "" });
            attachment.fields.Add(new SlackDto.Field { title = "Source", value = exp.Source != null ? exp.Source.ToString().Trim() : "" });
            attachment.fields.Add(new SlackDto.Field { title = "Data", value = (exp.Data != null ? exp.Data.ToString() : "").Trim() });
            attachment.fields.Add(new SlackDto.Field { title = "StackTrace", value = exp.StackTrace != null ? exp.StackTrace.ToString().Trim() : "" });
            attachment.fields.Add(new SlackDto.Field { title = "HResult", value = exp.HResult.ToString().Trim() });
            attachment.fields.Add(new SlackDto.Field { title = "TargetSite", value = exp.TargetSite != null ? exp.HResult.ToString().Trim() : "" });

            attachment.pretext = pretext;
            attachment.title = "Commiter Service";
            attachment.title_link = "https://github.com/mbozkaya/Committer";

            attachmentList.Add(attachment);
            string serializedContent = JsonConvert.SerializeObject(attachmentList.ToArray());
            WriteMessage(serializedContent, SlackType.Error);
        }
    }
}
