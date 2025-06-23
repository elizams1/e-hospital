using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class RegisterModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse;
        private HttpContent content;

        public RegisterModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public VMResponse? Create(VMMRegister data)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    httpClient.PostAsync($"{apiUrl}/api/Register", content).Result.Content.ReadAsStringAsync().Result
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
                    throw new ArgumentNullException("Register Api can't be reached!");
                }
            }
            catch (Exception e)
            {
                apiResponse.StatusCode = (apiResponse.StatusCode == HttpStatusCode.InternalServerError) ? HttpStatusCode.InternalServerError : apiResponse.StatusCode;
                apiResponse.message += $" {e.Message}";
            }

            return apiResponse;
        }

        public VMResponse? GetByEmail(string email)
        {

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Register/GetByEmail/" + email).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMUser>(
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
                    throw new Exception("User API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }

            return apiResponse;
        }
        public VMResponse SendEmail (string email, int otp)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.PostAsync(apiUrl + $"/api/Register/SendEmail/{email}/{otp}", content).Result.Content.ReadAsStringAsync().Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMUser>(
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
                    throw new Exception("User API Cannot be reached!");
                }
            }
            catch(Exception ex)
            {
                apiResponse.StatusCode=HttpStatusCode.InternalServerError;
                apiResponse.message = ex.Message;
            }

            return apiResponse;
        }
    }
}
