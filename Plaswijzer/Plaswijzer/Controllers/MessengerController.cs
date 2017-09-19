using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plaswijzer.BotData;
using Plaswijzer.MessengerManager;
using Plaswijzer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Plaswijzer.BotData.MessengerData;

namespace Plaswijzer.Controllers
{

    [Route("api/[controller]")]
    public class MessengerController : Controller
    {
        private readonly ILogger<MessengerController> _logger;
        private IMessageHandler mhandler;
        private IPayloadHandler phandler;

        public MessengerController(ILogger<MessengerController> logger, IPayloadHandler phandler, IMessageHandler mhandler)
        {
            this.mhandler = mhandler;
            this.phandler = phandler;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var allUrlKeyValues = Request.Query;
            if (allUrlKeyValues["hub.mode"] == "subscribe" && allUrlKeyValues["hub.verify_token"] == "lab9kplaswijzer")
            {
                _logger.LogInformation("Messenger GET verification received");

                var returnVal = allUrlKeyValues["hub.challenge"];
                return Json(int.Parse(returnVal));
            }
            return NotFound();
        }


        [HttpPost]
        public ActionResult Post([FromBody] MessengerData data)
        {

            Task.Factory.StartNew(() =>
            {
                foreach (var entry in data.entry)
                {
                    foreach (var message in entry.messaging)
                    {
                        // Check current message if text is recognized and sets corresponding payload
                        Messaging currentMessage = mhandler.MessageRecognized(message);
                        if (currentMessage.postback != null)
                        {
                            _logger.LogInformation("Messenger postback data received");
                            phandler.handle(message);
                        }
                        // Check current message if it has an attachment (location)
                        else if (currentMessage?.message?.attachments != null)
                        {
                            try
                            {
                                Attachment locationAtt = currentMessage?.message?.attachments[0];
                                Coordinates coords = locationAtt.payload?.coordinates;
                                string lang = uData.GetLanguage(currentMessage.sender.id);
                                if (string.IsNullOrWhiteSpace(lang))
                                    lang = "";
                                if (!uData.WantsToilet(message.sender.id))
                                {
                                    currentMessage.postback = new Postback { payload = $"DEVELOPER_DEFINED_COORDINATES°{coords.lon}:{coords.lat}°{lang}" };
                                    _logger.LogInformation($"Messenger locationdata received, toilet: false, lat: {coords.lat}, long {coords.lon}");
                                    phandler.handle(message);
                                }
                                else
                                {
                                    currentMessage.postback = new Postback { payload = $"GET_TOILET°{coords.lon}:{coords.lat}°{lang}" };
                                    _logger.LogInformation($"Messenger locationdata received, toilet: true, lat: {coords.lat}, long {coords.lon}");
                                    phandler.handle(message);
                                }
                                uData.Remove(message.sender.id); //Remove the user from the set
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Messenger text data received");
                            mhandler.CheckForKnowText(currentMessage);
                        }

                            if (string.IsNullOrWhiteSpace(message?.message?.text))
                            continue;

                        var msg = "You said: " + message.message.text;
                        var json = $@" {{recipient: {{  id: {message.sender.id}}},message: {{text: ""{msg}"" }}}}";
                        Console.Write("Message: " + msg + " to id: " + message.sender.id + "\n");
                        String res = PostRawAsync("https://graph.facebook.com/v2.6/me/messages?access_token=EAAbZA6U4S0GABAFLNHPyXF6RFGNWE05TOGIUOWmmajr56WqpaCqV73YPumqQDMLfRLmRt9aUxtdjPZAZBKibIOt7ZAgRBChCZBQMKHtQBEohZCZAV2KDSiJbn4XwrQyAWG161lVHvgYLtB1OykZBeAATMKvNhPyRt3ogr9PbZAgZBx4QZDZD", json).Result;
                    }
                }
            });

            return Ok();
        }

        private async Task<String> PostRawAsync(string url, string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var requestWriter = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                requestWriter.Write(data);
            }

            var response = (HttpWebResponse)await request.GetResponseAsync();
            if (response == null)
                throw new InvalidOperationException("GetResponse returns null");

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

    }
}


