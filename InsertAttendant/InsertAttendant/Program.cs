using InsertAttendant.Model;
using Lime.Protocol;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace InsertAttendant
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite a chave do bot");
            var key = Console.ReadLine();

            Console.WriteLine("Digite o caminho do arquivo com os usuários:");
            var file = Console.ReadLine();

            var text = System.IO.File.ReadAllLines(file);

            foreach (var item in text)
            {
                var splitedText = item.Split(',');
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://msging.net");
                    client.DefaultRequestHeaders.Add("Authorization",key);

                    var request = new DelegationRequest()
                    {
                        id = EnvelopeId.NewId(),
                        method = "set",
                        resource = new ResourceDelegation()
                        {
                            envelopeTypes = new System.Collections.Generic.List<string>() { "command" },
                            target = $"{HttpUtility.UrlEncode(splitedText[0])}@blip.ai"
                        },
                        to = "postmaster@msging.net",
                        type = "application/vnd.lime.delegation+json",
                        uri = "/delegations"
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.Default, "application/json");
                    var responsePost = client.PostAsync("/commands", content).Result;

                    var requestAdd = new AddRequest()
                    {
                        id = EnvelopeId.NewId(),
                        method = "set",
                        resource = new Resource()
                        {
                            identity = $"{HttpUtility.UrlEncode(splitedText[0])}@blip.ai",
                            teams = splitedText[1].Split(';').ToList()
                        },
                        to = "postmaster@desk.msging.net",
                        type = "application/vnd.iris.desk.attendant+json",
                        uri = "/attendants"
                    };

                    content = new StringContent(JsonConvert.SerializeObject(requestAdd), Encoding.Default, "application/json");
                    responsePost = client.PostAsync("/commands", content).Result;

                }
            }



        }
    }
}
