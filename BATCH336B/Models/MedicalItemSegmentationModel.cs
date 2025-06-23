using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class MedicalItemSegmentationModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;

        public MedicalItemSegmentationModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMMedicalItemSegmentation>? GetAll()
        {
            List<VMMMedicalItemSegmentation>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItemSegmentation").Result
                );

                if(apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItemSegmentation>?>(
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
                    throw new Exception("Medical item category api cannot be reached!");
                }

            } catch (Exception ex)
            {
                throw new Exception("Catch IN");
            }

            return data;
        }

        public List<VMMMedicalItemSegmentation>? GetByFilter(string filter)
        {
            List<VMMMedicalItemSegmentation>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItemSegmentation/GetByFilter/" + filter).Result
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItemSegmentation>?>(
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
                    throw new Exception("Medical Item Segmentation api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }

        public VMMMedicalItemSegmentation? GetById(int id)
        {
            VMMMedicalItemSegmentation? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItemSegmentation/Get/" + id).Result
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMMedicalItemSegmentation?>(
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
                    throw new Exception("Medical Item Segmentation api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Catch IN");
            }

            return data;
        }

        public async Task<VMResponse> CreateAsync(VMMMedicalItemSegmentation data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PostAsync($"{apiUrl}/api/MedicalItemSegmentation", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalItemSegmentation?>(
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
                    throw new Exception("Medical Item Segmentation API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> UpdateAsync(VMMMedicalItemSegmentation data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/MedicalItemSegmentation", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalItemSegmentation?>(
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
                    throw new Exception("Medical Item Segmentation API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }

            return apiResponse;
        }

        public async Task<VMResponse> DeleteAsync(int id, int userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                       await httpClient.DeleteAsync($"{apiUrl}/api/MedicalItemSegmentation?id={id}&userId={userId}").Result
                       .Content
                       .ReadAsStringAsync()
                    );

                if (apiResponse == null)
                {
                    throw new Exception("Segmentation API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }
    }
}
