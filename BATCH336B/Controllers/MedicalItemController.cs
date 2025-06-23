using BATCH336B.AddOns;
using BATCH336B.DataModel;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using NuGet.Configuration;
using System.Drawing.Printing;
using System.Net;

namespace BATCH336B.Controllers
{
    public class MedicalItemController : Controller
    {
        private readonly MedicalItemModel medItem;
        private readonly MedicalItemCategoryModel medItemCategory;
        private VMResponse? response;

        private string images;
        private readonly int pageSize;

        public MedicalItemController(IConfiguration _config)
        {
            //medItem = new MedicalItemModel(_config);
            medItem = new MedicalItemModel(_config);
            medItemCategory = new MedicalItemCategoryModel(_config);
            images = _config["ImagesFolder"];
            
            pageSize = 4;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            ViewBag.Title = "Cari Obat & Alat Kesehatan";
            ViewBag.MedItemCat = medItemCategory.GetAll();

            return View();
        }

        public IActionResult FilteredItem(string? segmen, string? filter, int? minPrice, int? maxPrice, string? cat, int? pageNumber, int? currPageSize)
        {
            //ViewBag.MedItemCat = medItemCategory.GetAll();

            List<VMMMedicalItem>? data = new List<VMMMedicalItem>();
            data = medItem.GetByFilter(segmen, filter, minPrice, maxPrice, cat);

            //data = medItem.GetAll();

            ViewBag.Filter = filter;
            ViewBag.Segmen = segmen;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Cat = cat;
            ViewBag.Images = images;

            ViewBag.PageSize = (currPageSize ?? pageSize);

            return View(Pagination<VMMMedicalItem>.Create(data, pageNumber ?? 1, ViewBag.PageSize));

            //return View(data);
        }
        public IActionResult Save(int totProduct, int estPrice)
        {
            HttpContext.Session.SetInt32("totProduct", totProduct);
            HttpContext.Session.SetInt32("estPrice", estPrice);

            //if (HttpContext.Session.GetInt32("totProduct") != null && HttpContext.Session.GetInt32("estPrice") != null)
            //{

            //}
            return RedirectToAction("FilteredItems");
        }

        public IActionResult Details(int id)
        {
            VMMMedicalItem? data = medItem.GetById(id);

            ViewBag.Title = "Detail Produk Kesehatan";

            return View(data);
        }
    }
}
