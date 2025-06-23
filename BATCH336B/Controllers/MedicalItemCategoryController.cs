using BATCH336B.AddOns;
using BATCH336B.DataModel;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BATCH336B.Controllers
{
    public class MedicalItemCategoryController : Controller
    {
        private readonly MedicalItemCategoryModel? medItemCategory;
        private VMResponse? response;
        private readonly int pageSize;

        public MedicalItemCategoryController(IConfiguration _config)
        {
            medItemCategory = new MedicalItemCategoryModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }

        public IActionResult Index(string? filter, int? pageNumber, int? currPageSize)
        {
            List<VMMMedicalItemCategory>? data = new List<VMMMedicalItemCategory>();
            
            data = (filter == null)
                ? medItemCategory.GetAll()
                : medItemCategory.GetByFilter(filter);

            ViewBag.Filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);

            return View(Pagination<VMMMedicalItemCategory>.Create(data, pageNumber ?? 1, ViewBag.PageSize));


        }

        public IActionResult Add()
        {
            ViewBag.Title = "Tambah Kategori Produk Kesehatan";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Add(VMMMedicalItemCategory data)
        {
            response = await medItemCategory.CreateAsync(data);

            return response;
        }

        public IActionResult Edit(int id)
        {
            VMMMedicalItemCategory? data = medItemCategory.GetById(id);

            ViewBag.Title = "Edit Kategori Produk Kesehatan";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse> Edit(VMMMedicalItemCategory data)
        {
            response = await medItemCategory.UpdateAsync(data);

            return response;
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Category Confirmation";
            return View(id);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(int id, int userId)
        {
            response = await medItemCategory.DeleteAsync(id, userId);
            return response;
        }
    }
}
