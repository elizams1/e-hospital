using BATCH336B.AddOns;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.Controllers
{
    public class HistoryCustomerController : Controller
    {
        private readonly HistoryCustomerModel? historyCustomerModel;
        private readonly ProfileModel? profileModel;
        private readonly RegisterModel? registerModel;
        private readonly int pageSize;
        private IWebHostEnvironment webHostEnv;

        private readonly string imageFolder;
        private VMResponse? response = new VMResponse();


        public HistoryCustomerController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            historyCustomerModel = new HistoryCustomerModel(_config);
            profileModel = new ProfileModel(_config, _webHostEnv);
            registerModel = new RegisterModel(_config);
            imageFolder = _config["ImagesFolder"];

            pageSize = int.Parse(_config["PageSize"]);
        }
        public IActionResult Index(long id)
        {
            VMMProfile data = profileModel.GetById(id);
            ViewBag.ImgFolder = imageFolder;
            return View(data);
        }
        public IActionResult ProfileMenu(VMMProfile data)
        {

            return View(data);
        }

        public IActionResult test()
        {
            return View();
        }
        public IActionResult ValidasiCetakResep(int id)
        {
            VMMHistoryCustomer? data = historyCustomerModel?.GetById(id);
            ViewBag.Title = "Cetak Resep";

            return View(data);
        }

        public IActionResult RiwayatKedatangan(string? filter, string? orderBy, int? pageNumber, int? currPageSize)
        {
            List<VMMHistoryCustomer>? dataCustomer = new List<VMMHistoryCustomer>();

            

            if (filter == null)
            {
                dataCustomer = historyCustomerModel?.GetAll();
            }
            else
            {
                dataCustomer = historyCustomerModel?.GetByFilter(filter);
            }

            if (dataCustomer == null)
            {
                dataCustomer = new List<VMMHistoryCustomer>();
            }

            //Order Data Process
            switch (orderBy)
            {
                case "tgl_dibuat_desc":
                    dataCustomer = dataCustomer?.OrderByDescending(p => p.Appointment.CreatedOn).ToList();
                    break;
                case "tgl_dibuat":
                    dataCustomer = dataCustomer?.OrderBy(p => p.Appointment.CreatedOn).ToList();
                    break;
                case "nama_pasien_desc":
                    dataCustomer = dataCustomer?.OrderByDescending(p => p.Biodatum.Fullname).ToList();
                    break;
                case "nama_pasien":
                    dataCustomer = dataCustomer?.OrderBy(p => p.Biodatum.Fullname).ToList();
                    break;
                case "tgl_kedatangan_desc":
                    dataCustomer = dataCustomer?.OrderByDescending(p => p.Appointment.AppointmentDate).ToList();
                    break;
                default:
                    dataCustomer = dataCustomer?.OrderBy(p => p.Appointment.AppointmentDate).ToList();
                    break;

            }

            ViewBag.OrderBy = orderBy;
            ViewBag.Filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);

            //Update the Data Sort Order
            
            ViewBag.TglKedatanganDesc = string.IsNullOrEmpty(orderBy) ? "tgl_kedatangan_desc" : "";
            ViewBag.NamaPasienDesc = orderBy == "nama_pasien" ? "nama_pasien_desc" : "nama_pasien";
            ViewBag.TglDibuatDesc = orderBy == "tgl_dibuat" ? "tgl_dibuat_desc" : "tgl_dibuat";
            ViewBag.NamaPasien = "nama_pasien";
            ViewBag.TglDibuat = "tgl_dibuat";
            ViewBag.OrderStock = orderBy == "stock" ? "stock_desc" : "stock";

            return View(Pagination<VMMHistoryCustomer>.Create(dataCustomer, pageNumber ?? 1, ViewBag.PageSize));
        }

    }
}
