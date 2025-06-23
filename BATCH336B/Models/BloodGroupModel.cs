using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class BloodGroupModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse;
        private string jsonData;
        private HttpContent content;
        

        public BloodGroupModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public List<VMMBloodGroup>? GetAll()
        {

            List<VMMBloodGroup>? data = null;
           
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/BloodGroup").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMBloodGroup>?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Category api cannot be reached!");
                }
            }
            catch (Exception ex)
            {
               
                
            }

            return data;
        }

        public List<VMMBloodGroup>? GetByFilter(string filter)
        {
            List<VMMBloodGroup>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/BloodGroup/Get/" + filter).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMBloodGroup>?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Blood Group api cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                
            }
            return data;
        }

        public VMMBloodGroup? GetById(long id)
        {

            VMMBloodGroup? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/BloodGroup/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMBloodGroup?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Blood Group api cannot be reached!");
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public async Task<VMResponse> CreateAsync(VMMBloodGroup data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PostAsync($"{apiUrl}/api/BloodGroup", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMBloodGroup?>(
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
                    throw new Exception("Blood Group API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> UpdateAsync(VMMBloodGroup data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/BloodGroup", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMBloodGroup?>(
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
                    throw new Exception("Blood Group API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> DeleteAsync(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.DeleteAsync($"{apiUrl}/api/BloodGroup?id={id}&userId={userId}").Result.Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Blood Group API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }

            return apiResponse;
        }

        public VMResponse? GetByCode(string code)
        {

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/BloodGroup/GetByCode/" + code).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMBloodGroup>(
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
                    throw new Exception("BloodGroup API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }

            return apiResponse;
        }
    }
}
