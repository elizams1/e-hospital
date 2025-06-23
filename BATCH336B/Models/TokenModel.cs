using BATCH336B.DataModel;
using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class TokenModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public TokenModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMTToken>? GetAll()
        {
            List<VMTToken>? dataToken = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataToken = JsonConvert.DeserializeObject<List<VMTToken>?>(
                            JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Token API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataToken;
        }

        public VMResponse? GetByEmail(string email)
        {

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token/GetByEmail/" + email).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTToken>(
                            JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Token API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }

            return apiResponse;
        }
        public VMTToken GetById(int id)
        {
            VMTToken? dataToken = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataToken = JsonConvert.DeserializeObject<VMTToken>(
                            JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Token API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataToken;
        }

        public VMTToken GetByUserId(int userid)
        {
            VMTToken? dataToken = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Token/GetByUserId/" + userid).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataToken = JsonConvert.DeserializeObject<VMTToken>(
                            JsonConvert.SerializeObject(apiResponse.data)
                            );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Token API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataToken;
        }

        public VMResponse? Create(VMTToken data)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.PostAsync($"{apiUrl}/api/Token", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK || apiResponse.StatusCode != HttpStatusCode.Created)
                    {
                        throw new Exception(apiResponse.message);

                    }
                }
                else
                {
                    apiResponse.StatusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException("Token Api can't be reached!");
                }
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = (apiResponse.StatusCode == HttpStatusCode.InternalServerError) ? HttpStatusCode.InternalServerError : apiResponse.StatusCode;
                apiResponse.message += $" {e.Message}";
            }

            return apiResponse;
        }

        public VMResponse? Update(VMTToken data)
        {
            try
            {
                //Serial dari data ke json
                //Deserial di jadikan ke object
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        httpClient.PutAsync($"{apiUrl}/api/Token", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK || apiResponse.StatusCode == HttpStatusCode.Created)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTToken?>(
                            JsonConvert.SerializeObject(apiResponse.data)
                        );
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Data API Cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $" {e.Message}";
            }

            return apiResponse;
        }
    }
}
