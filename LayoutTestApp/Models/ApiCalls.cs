using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LayoutTestApp.Models
{
    public class ApiCalls
    {
        public async Task<JObject> GetDataFromServer(string callToServer, string methodType)
        {
            JObject returnJson = null;
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    Uri uri = new Uri(callToServer);

                    var method = new HttpMethod(methodType);
                    var request = new HttpRequestMessage(method, uri);

                    //_client.DefaultRequestHeaders.Add("X-CSRF-Token", CurrentUser.Csrf_token);
                    //_client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", CurrentUser.BasicAuthKey);

                    HttpResponseMessage response = null;
                    response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var _jsonTemp = JsonConvert.DeserializeObject<JArray>(data);
                        returnJson = new JObject(
                            new JProperty("data", _jsonTemp)
                        );
                    }
                    else
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        returnJson = new JObject(
                            new JProperty("data", null),
                            new JProperty("Reason", response.ReasonPhrase),
                            new JProperty("StatusCode", response.StatusCode)
                        );
                    }
                    _client.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"                GetDataModel:GetDataFromSystemAsync {ex.Message}");
                //Crashes.TrackError(ex);
            }
            return returnJson;
        }
    }
}
