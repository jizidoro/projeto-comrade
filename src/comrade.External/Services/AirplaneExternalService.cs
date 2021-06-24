#region

using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using comrade.External.Bases;
using comrade.External.Interfaces;

#endregion

namespace comrade.External.Services
{
    public class AirplaneExternalService : IAirplaneExternalService
    {
        public AirplaneExternalService()
        {
        }

        public void BaAtributoArvoreFamiliaListar()
        {
            WebRequest request =
                WebRequest.Create("http://172.16.50.97/erp-backend/produto/api/v1/BaAtributoArvoreFamilia/obter-por-nome/bus");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                options.Converters.Add(new JsonStringEnumConverter());

                try
                {
                    var result = JsonSerializer.Deserialize<ListResultDto<EntityDto>>(responseFromServer, options);
                    var oto = result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            response.Close();
        }
    }
}