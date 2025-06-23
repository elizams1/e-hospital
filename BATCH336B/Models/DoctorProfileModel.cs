using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BATCH336B.Models
{
    public class DoctorProfileModel
    {
        private readonly string apiUrl;
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private string jsonData;
        private HttpContent content;

        public DoctorProfileModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public VMDoctorProfile? GetDoctorById(long id)
        {
            VMDoctorProfile? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/DoctorProfile/GetDoctorById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMDoctorProfile?>(JsonConvert.SerializeObject(apiResponse.data));
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
            catch(Exception ex)
            {

            }
            return data;
        }
        public VMCustomerBuatJanji? GetCustomerById(long id)
        {
            VMCustomerBuatJanji? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/DoctorProfile/GetCustomerById/" + id).Result);
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMCustomerBuatJanji?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Cuastomer api cannot be reached!");
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        
        public List<VMTTAppointmnet>? GetAllAppointment(long? id, string? tgl)
        {
            List<VMTTAppointmnet>? data = null;

            try
            {
                
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/DoctorProfile/GetAllAppointment?id={id}&tgl={tgl}").Result);
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTTAppointmnet>?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Appointment api cannot be reached!");
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<VMTTAppointmnet>? GetAllAppointmentBySche(long? id, string? tgl)
        {
            List<VMTTAppointmnet>? data = null;

            try
            {

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync($"{apiUrl}/api/DoctorProfile/GetAllAppointmentBySche?id={id}&tgl={tgl}").Result);
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTTAppointmnet>?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Appointment api cannot be reached!");
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public async Task<VMResponse> CreateAsync(VMTTAppointmnet data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PostAsync($"{apiUrl}/api/DoctorProfile", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTTAppointmnet?>(
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
                    throw new Exception("Appointment API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }
    }
}
