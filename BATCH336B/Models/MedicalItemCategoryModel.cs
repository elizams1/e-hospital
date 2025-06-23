using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class MedicalItemCategoryModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse;
        private HttpContent content;
        private string jsonData;

        public MedicalItemCategoryModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMMedicalItemCategory>? GetAll()
        {
            List<VMMMedicalItemCategory>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory").Result
                );

                if(apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItemCategory>?>(
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
            }

            return data;
        }

        public List<VMMMedicalItemCategory>? GetByFilter(string filter)
        {
            List<VMMMedicalItemCategory>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory/GetByFilter/" + filter).Result
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMMedicalItemCategory>?>(
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
                    throw new Exception("Medical Item Category api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }

        public VMMMedicalItemCategory? GetById(int id)
        {
            VMMMedicalItemCategory? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory/Get/" + id).Result
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMMedicalItemCategory?>(
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
                    throw new Exception("Medical Item Category api cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Catch IN");
            }

            return data;
        }

        public async Task<VMResponse> CreateAsync(VMMMedicalItemCategory data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PostAsync($"{apiUrl}/api/MedicalItemCategory", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalItemCategory?>(
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
                    throw new Exception("Medical Item Category API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> UpdateAsync(VMMMedicalItemCategory data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/MedicalItemCategory", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMMedicalItemCategory?>(
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
                    throw new Exception("Medical Item Category API cannot be reached");
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
                       await httpClient.DeleteAsync($"{apiUrl}/api/MedicalItemCategory?id={id}&userId={userId}").Result
                       .Content
                       .ReadAsStringAsync()
                    );

                if (apiResponse == null)
                {
                    throw new Exception("Category API cannot be reached");
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
