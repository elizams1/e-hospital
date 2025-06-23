using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class BiodataModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public BiodataModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMBiodatum>? GetAll()
        {
            List<VMMBiodatum>? dataBiodata = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Biodata").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataBiodata = JsonConvert.DeserializeObject<List<VMMBiodatum>?>(
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
                    throw new Exception("Biodata API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataBiodata;
        }

        public VMMBiodatum GetById(int id)
        {
            VMMBiodatum? dataBiodata = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Biodata/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataBiodata = JsonConvert.DeserializeObject<VMMBiodatum>(
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
                    throw new Exception("Biodata API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataBiodata;
        }
        public VMResponse? Update(VMMBiodatum data)
        {
            try
            {
                //Serial dari data ke json
                //Deserial di jadikan ke object
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        httpClient.PutAsync($"{apiUrl}/api/Biodata", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK || apiResponse.StatusCode == HttpStatusCode.Created)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMBiodatum?>(
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
