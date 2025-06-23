using BATCH336B.AddOns;
using BATCH336B.DataModel;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BATCH336B.Controllers
{
    public class MedicalItemSegmentationController : Controller
    {
        private readonly MedicalItemSegmentationModel? medItemSegmentation;
        private VMResponse? response;
        private readonly int pageSize;

        public MedicalItemSegmentationController(IConfiguration _config)
        {
            medItemSegmentation = new MedicalItemSegmentationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }

        public IActionResult Index(string? filter, int? pageNumber, int? currPageSize)
        {
            List<VMMMedicalItemSegmentation>? data = new List<VMMMedicalItemSegmentation>();
            
            data = (filter == null)
                ? medItemSegmentation.GetAll()
                : medItemSegmentation.GetByFilter(filter);

            ViewBag.Filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);

            return View(Pagination<VMMMedicalItemSegmentation>.Create(data, pageNumber ?? 1, ViewBag.PageSize));


        }

        public IActionResult Add()
        {
            ViewBag.Title = "Tambah Kategori Produk Kesehatan";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Add(VMMMedicalItemSegmentation data)
        {
            response = await medItemSegmentation.CreateAsync(data);

            return response;
        }

        public IActionResult Edit(int id)
        {
            VMMMedicalItemSegmentation? data = medItemSegmentation.GetById(id);

            ViewBag.Title = "Edit Kategori Produk Kesehatan";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse> Edit(VMMMedicalItemSegmentation data)
        {
            response = await medItemSegmentation.UpdateAsync(data);

            return response;
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Segmentation Confirmation";
            return View(id);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(int id, int userId)
        {
            response = await medItemSegmentation.DeleteAsync(id, userId);
            return response;
        }
    }
}
