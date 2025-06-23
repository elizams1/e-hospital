using BATCH336B.AddOns;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.Controllers
{
    public class CourierController : Controller
    {
        private readonly CourierModel? courier;
        private VMResponse response = new VMResponse();
        private readonly int pageSize;

        public CourierController(IConfiguration _config)
        {
            courier = new CourierModel(_config);
            pageSize = int.Parse(_config["PageSize"]);

        }
        public IActionResult Index(string? filter, string? orderBy, int? pageNumber, int? pageIndex)//, string? orderBy, int? pageNumber, int? currPageSize)
        {
            List<VMMCourier> data = (filter == null) ? courier.GetAll() : courier.GetBy(filter);

            ViewBag.Title = "Courier Index";
            ViewBag.filter = filter;

            ViewBag.OrderBy = orderBy;
            ViewBag.Filter = filter;

            //  ViewBag.PageSize = currPageSize ?? pageSize;
            // return View(data);
            return View(Pagination<VMMCourier>.Create(data, pageNumber ?? 1, pageSize));
        }


        public IActionResult Add()
        {
            ViewBag.Title = "Create New Courier";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMMCourier data)
        {
            response = await courier.CreateAsync(data);
            return response;
        }


        public IActionResult Edit(int id)
        {
            VMMCourier? data = courier.GetById(id);
            ViewBag.Title = "Courier Edit";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Edit(VMMCourier data)
        {
            response = await courier.UpdateAsync(data);

            return response;
        }


        public IActionResult Delete(int id)
        {
            VMMCourier data = courier.GetById(id);
            ViewBag.Title = "Delete Courier Confirmation";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(int id, int userId)
        {
            response = await courier.DeleteAsync(id, userId);
            return response;
        }
    }
}
