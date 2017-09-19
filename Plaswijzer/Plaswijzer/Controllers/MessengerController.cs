using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plaswijzer.BotData;
using Plaswijzer.Data;
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
   
        private IMessageHandler mhandler;
        private IPayloadHandler phandler;
        private UserTemp utemp;

        public MessengerController(IPayloadHandler phandler, IMessageHandler mhandler, IUserTemp utemp)
        {
            this.mhandler = mhandler;
            this.phandler = phandler;
            this.utemp = (UserTemp) utemp;

        }

        [HttpGet]
        public ActionResult Get()
        {
            var allUrlKeyValues = Request.Query;
            if (allUrlKeyValues["hub.mode"] == "subscribe" && allUrlKeyValues["hub.verify_token"] == "lab9kplaswijzer")
            {

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
                        Console.Write("in postback");
                        // Check current message if text is recognized and sets corresponding payload
                        /*Messaging currentMessage = mhandler.MessageRecognized(message);
                        if (currentMessage.postback != null)
                        {
                            phandler.handle(message);
                        }
                        // Check current message if it has an attachment (location)
                        else if (currentMessage?.message?.attachments != null)
                        {
                            try
                            {
                                Console.Write("in attachment");
                                Attachment locationAtt = currentMessage?.message?.attachments[0];
                                Coordinates coords = locationAtt.payload?.coordinates;
                                string lang = utemp.GetLanguage(currentMessage.sender.id);
                                if (string.IsNullOrWhiteSpace(lang))
                                    lang = "";
                                //toiletten nog afhandelen
                                /*
                                else
                                {
                                    currentMessage.postback = new Postback { payload = $"GET_TOILET°{coords.lon}:{coords.lat}°{lang}" };
                                    _logger.LogInformation($"Messenger locationdata received, toilet: true, lat: {coords.lat}, long {coords.lon}");
                                    phandler.handle(message);
                                }
                                
                                utemp.Remove(message.sender.id); //Remove the user from the set
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else
                        {*/
                            //for testing: papegaai!
                            if (string.IsNullOrWhiteSpace(message?.message?.text))
                                continue;

                            var msg = "You said: " + message.message.text;
                            var json = $@" {{recipient: {{  id: {message.sender.id}}},message: {{text: ""{msg}"" }}}}";
                            Console.Write("Message: " + msg + " to id: " + message.sender.id + "\n");
                            String res = PostRawAsync("https://graph.facebook.com/v2.6/me/messages?access_token=EAAbZA6U4S0GABAFLNHPyXF6RFGNWE05TOGIUOWmmajr56WqpaCqV73YPumqQDMLfRLmRt9aUxtdjPZAZBKibIOt7ZAgRBChCZBQMKHtQBEohZCZAV2KDSiJbn4XwrQyAWG161lVHvgYLtB1OykZBeAATMKvNhPyRt3ogr9PbZAgZBx4QZDZD", json).Result;
                            
                            //mhandler.CheckForKnowText(currentMessage);
                        //}

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


