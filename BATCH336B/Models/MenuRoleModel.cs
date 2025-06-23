using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace BATCH336B.Models
{
    public class MenuRoleModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();

        private HttpContent content;
        private string jsonData;

        public MenuRoleModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMMenuRole>? GetAll()
        {
            List<VMMMenuRole>? dataMenuRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MenuRole").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMenuRole = JsonConvert.DeserializeObject<List<VMMMenuRole>?>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMenuRole;
        }

        public VMMMenuRole GetByMenuId(int menuId)
        {
            VMMMenuRole? dataMenuRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MenuRole/GetByMenuId/" + menuId).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMenuRole = JsonConvert.DeserializeObject<VMMMenuRole>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMenuRole;
        }

        public VMMMenuRole GetByRoleId(int id)
        {
            VMMMenuRole? dataMenuRole = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Role/GetByRoleId/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMenuRole = JsonConvert.DeserializeObject<VMMMenuRole>(
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
                    throw new Exception("Role API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMenuRole;
        }
    }
}
