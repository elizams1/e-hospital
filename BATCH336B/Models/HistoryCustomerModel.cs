using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Collections;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Org.BouncyCastle.Crypto.Engines;
using System;
using Microsoft.AspNetCore.Mvc.Razor;

namespace BATCH336B.Models
{
    public class HistoryCustomerModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private VMResponse? apiResponse = new VMResponse();
        private HttpContent content;
        private string jsonData;

        public HistoryCustomerModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        /*public List<VMMBiodatum>? GetAllBiodata()
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
        public List<VMMCustomer>? GetAllCustomer()
        {
            List<VMMCustomer>? dataCustomer = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Customer").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataCustomer = JsonConvert.DeserializeObject<List<VMMCustomer>?>(
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
                    throw new Exception("Customer API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataCustomer;
        }

        public List<VMMCustomerMember>? GetAllCustomerMember()
        {
            List<VMMCustomerMember>? dataCustomerMember = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/CustomerMember").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataCustomerMember = JsonConvert.DeserializeObject<List<VMMCustomerMember>?>(
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
                    throw new Exception("Customer Member API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataCustomerMember;
        }

        public List<VMMCustomerRelation>? GetAllCustomerRelation()
        {
            List<VMMCustomerRelation>? dataCustomerRelation = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/CustomerRelation").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataCustomerRelation = JsonConvert.DeserializeObject<List<VMMCustomerRelation>?>(
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
                    throw new Exception("Customer Relation API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataCustomerRelation;
        }

        public List<VMMDoctor>? GetAllDoctor()
        {
            List<VMMDoctor>? dataDoctor = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Doctor").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataDoctor = JsonConvert.DeserializeObject<List<VMMDoctor>?>(
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
                    throw new Exception("Doctor API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataDoctor;
        }

        public List<VMTCurrentDoctorSpecialization>? GetAllCurrentDoctorSpecialization()
        {
            List<VMTCurrentDoctorSpecialization>? dataCurrentDoctorSpecialization = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/CurrentDoctorSpecialization").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataCurrentDoctorSpecialization = JsonConvert.DeserializeObject<List<VMTCurrentDoctorSpecialization>?>(
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
                    throw new Exception("CurrentDoctorSpecialization API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataCurrentDoctorSpecialization;
        }

        public List<VMMSpecialization>? GetAllSpecialization()
        {
            List<VMMSpecialization>? dataSpecialization = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Specialization").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataSpecialization = JsonConvert.DeserializeObject<List<VMMSpecialization>?>(
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
                    throw new Exception("Specialization API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataSpecialization;
        }

        public List<VMTDoctorOffice>? GetAllDoctorOffice()
        {
            List<VMTDoctorOffice>? dataDoctorOffice = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/DoctorOffice").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataDoctorOffice = JsonConvert.DeserializeObject<List<VMTDoctorOffice>?>(
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
                    throw new Exception("DoctorOffice API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataDoctorOffice;
        }

        public List<VMMMedicalFacility>? GetAllMedicalFacility()
        {
            List<VMMMedicalFacility>? dataMedicalFacility = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalFacility").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMedicalFacility = JsonConvert.DeserializeObject<List<VMMMedicalFacility>?>(
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
                    throw new Exception("MedicalFacility API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMedicalFacility;
        }

        public List<VMMMedicalFacilityCategory>? GetAllMedicalFacilityCategory()
        {
            List<VMMMedicalFacilityCategory>? dataMedicalFacilityCategory = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalFacilityCategory").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMedicalFacilityCategory = JsonConvert.DeserializeObject<List<VMMMedicalFacilityCategory>?>(
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
                    throw new Exception("MedicalFacilityCategory API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMedicalFacilityCategory;
        }

        public List<VMTAppointment>? GetAllAppointment()
        {
            List<VMTAppointment>? dataAppointment = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Appointment").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataAppointment = JsonConvert.DeserializeObject<List<VMTAppointment>?>(
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
                    throw new Exception("Appointment API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataAppointment;
        }

        public List<VMTAppointmentDone>? GetAllAppointmentDone()
        {
            List<VMTAppointmentDone>? dataAppointmentDone = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/AppointmentDone").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataAppointmentDone = JsonConvert.DeserializeObject<List<VMTAppointmentDone>?>(
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
                    throw new Exception("AppointmentDone API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataAppointmentDone;
        }

        public List<VMTDoctorOfficeTreatment>? GetAllDoctorOfficeTreatment()
        {
            List<VMTDoctorOfficeTreatment>? dataDoctorOfficeTreatment = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/DoctorOfficeTreatment").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataDoctorOfficeTreatment = JsonConvert.DeserializeObject<List<VMTDoctorOfficeTreatment>?>(
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
                    throw new Exception("DoctorOfficeTreatment API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataDoctorOfficeTreatment;
        }

        public List<VMTDoctorTreatment>? GetAllDoctorTreatment()
        {
            List<VMTDoctorTreatment>? dataDoctorTreatment = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/DoctorTreatment").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataDoctorTreatment = JsonConvert.DeserializeObject<List<VMTDoctorTreatment>?>(
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
                    throw new Exception("DoctorTreatment API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataDoctorTreatment;
        }

        public List<VMTPrescription>? GetAllPrescription()
        {
            List<VMTPrescription>? dataPrescription = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/Prescription").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataPrescription = JsonConvert.DeserializeObject<List<VMTPrescription>?>(
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
                    throw new Exception("Prescription API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataPrescription;
        }

        public List<VMMMedicalItem>? GetAllMedicalItem()
        {
            List<VMMMedicalItem>? dataMedicalItem = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalItem").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMedicalItem = JsonConvert.DeserializeObject<List<VMMMedicalItem>?>(
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
                    throw new Exception("MedicalItem API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMedicalItem;
        }

        public List<VMMMedicalItemSegmentation>? GetAllMedicalItemSegmentation()
        {
            List<VMMMedicalItemSegmentation>? dataMedicalItemSegmentation = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalItemSegmentation").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMedicalItemSegmentation = JsonConvert.DeserializeObject<List<VMMMedicalItemSegmentation>?>(
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
                    throw new Exception("MedicalItemSegmentation API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMedicalItemSegmentation;
        }
        public List<VMMMedicalItemCategory>? GetAllMedicalItemCategory()
        {
            List<VMMMedicalItemCategory>? dataMedicalItemCategory = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/MedicalItemCategory").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataMedicalItemCategory = JsonConvert.DeserializeObject<List<VMMMedicalItemCategory>?>(
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
                    throw new Exception("MedicalItemCategory API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataMedicalItemCategory;
        }*/

        public VMMHistoryCustomer GetById(int id)
        {
            VMMHistoryCustomer? dataHistoryCustomer = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer/GetById/" + id).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataHistoryCustomer = JsonConvert.DeserializeObject<VMMHistoryCustomer>(
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
                    throw new Exception("History Customer API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return dataHistoryCustomer;
        }

        public List<VMMHistoryCustomer>? GetAll()
        {
            List<VMMHistoryCustomer>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer").Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMHistoryCustomer>?>(
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
                    throw new Exception("HistoryCustomer API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }

        public List<VMMHistoryCustomer>? GetByFilter(string? filter)
        {
            List<VMMHistoryCustomer>? data = null;

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(httpClient.GetStringAsync(apiUrl + "/api/HistoryCustomer/GetByFilter/" + filter).Result);

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMMHistoryCustomer>?>(
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
                    throw new Exception("HistoryCustomer Filter API Cannot be reached!");
                }

            }
            catch (Exception ex)
            {
            }

            return data;
        }

        public VMResponse? Update(VMTPrescription data)
        {
            try
            {
                //Serial dari data ke json
                //Deserial di jadikan ke object
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(
                        httpClient.PutAsync($"{apiUrl}/api/HistoryCustomer", content).Result.Content.ReadAsStringAsync().Result
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTPrescription?>(
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
