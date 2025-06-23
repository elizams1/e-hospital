using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace BATCH336B.Models
{
    public class MedicalItemModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;

        public MedicalItemModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMMedicalItem>? GetByFilter(string? segmen, string? filter, int? minPrice, int? maxPrice,string? cat)
        {
            List<VMMMedicalItem>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    //httpClient.GetStringAsync($"{apiUrl}/api/MedicalItem/GetByFilter/{segmen}/{filter}/{minPrice}/{maxPrice}/{cat}").Result
                    httpClient.GetStringAsync($"{apiUrl}/api/MedicalItem/GetByFilter?segmen={segmen}&filter={filter}&minPrice={minPrice}&maxPrice={maxPrice}&cat={cat}").Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItem>?>(
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
                    throw new Exception("Medical Item api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }

        public List<VMMMedicalItem>? GetAll()
        {
            List<VMMMedicalItem>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    httpClient.GetStringAsync($"{apiUrl}/api/MedicalItem").Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItem>?>(
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
                    throw new Exception("Medical Item api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }

        public VMMMedicalItem? GetById(int id)
        {
            VMMMedicalItem? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItem/Get/" + id).Result
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMMedicalItem?>(
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
                    throw new Exception("Medical Item api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Catch IN");
            }

            return data;
        }
    }
}
