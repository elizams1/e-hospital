using BATCH336B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BATCH336B.Models
{
    public class ProfileModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private readonly string imageFolder;

        private VMResponse? apiResponse;
        private IWebHostEnvironment webHostEnv;
        private string jsonData;
        private HttpContent content;

        public ProfileModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            apiUrl = _config["ApiUrl"];
            webHostEnv = _webHostEnv;
            imageFolder = _config["ImagesFolder"];
        }

        public VMMProfile? GetById(long id)
        {
            VMMProfile? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Profile/GetById/" + id).Result);

                if(apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMMProfile?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Profile api cannot be reached!");
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public async Task<VMResponse> UpdateAsync(VMMProfile data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/Profile/UpdateAkun", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMProfile?>(
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
                    throw new Exception("Profile API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse> UpdateEmailAsync(VMMProfile data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/Profile/UpdateEmail", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMProfile?>(
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
                    throw new Exception("Profile API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public async Task<VMResponse?> UpdatePhotoProfileAsync(VMMProfile data)
        {
            try
            {
                if(data.ImageFile != null)
                {
                    if(data.ImagePath != null)
                    {
                        DeleteOldImage(data.ImagePath);
                    }

                    data.ImagePath = UploadFile(data.ImageFile);

                    //using(MemoryStream mStream = new())
                    //{
                    //    data.ImageFile.CopyTo(mStream);
                    //    data.Image = mStream.ToArray();
                    //}

                    data.ImageFile = null;
                    
                }
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/Profile/UpdatePhotoProfile", content)
                    ).Content.ReadAsStringAsync()
                 );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.Created || apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    apiResponse.StatusCode = HttpStatusCode.NotFound;
                    throw new ArgumentNullException("Profile API cannot be reached");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"[ProductModel-{DateTime.Now}]\n" + $"Message: {e.Message}\n" + $"InnerException: {e.InnerException}");
            }
            return apiResponse;
        }

        public string? UploadFile(IFormFile? imageFile)
        {
            string uniqueFileName = "";
            string uploadFolder = "";
            if (imageFile != null)
            {

                uploadFolder = $"{webHostEnv.WebRootPath}\\{imageFolder}\\";
                uniqueFileName = $"{Guid.NewGuid()}-{imageFile.FileName}";

                //Upload Process
                using (FileStream fileStream = new FileStream($"{uploadFolder}{uniqueFileName}", FileMode.CreateNew))
                {
                    imageFile.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }

        public bool DeleteOldImage(string oldImageFileName)
        {
            try
            {
                //Check if existing file is exist
                oldImageFileName = $"{webHostEnv.WebRootPath}\\{imageFolder}\\{oldImageFileName}";

                if (File.Exists(oldImageFileName))
                {
                    File.Delete(oldImageFileName);
                }
                else
                {
                    throw new ArgumentNullException("File does not exist");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<VMResponse> UpdatePasswordAsync(VMMProfile data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                    await (
                        await httpClient.PutAsync($"{apiUrl}/api/Profile/UpdatePassword", content)
                    ).Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created || apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMProfile?>(
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
                    throw new Exception("Profile API cannot be reached");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public VMResponse SendOtp (int userId, string email, int otp, string expiredToken)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.PostAsync($"{apiUrl}/api/Profile/SendOtp/{userId}/{email}/{otp}/{expiredToken}", content).Result.Content.ReadAsStringAsync().Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMProfile>(
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
                    throw new Exception("Profile API Cannot be reached!");
                }
            }
            catch (Exception e)
            {
                apiResponse.message += $"{e.Message}";
                apiResponse.data = null;
            }
            return apiResponse;
        }

        public VMResponse UpdateToken( string email, int userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(httpClient.PostAsync($"{apiUrl}/api/Profile/UpdateToken/{email}/{userId}", content).Result.Content.ReadAsStringAsync().Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMMProfile>(
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
                    throw new Exception("Profile API Cannot be reached!");
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
