using BATCH336B.DataAccess;
using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336B.Controllers
{
    public class BloodGroupController : Controller
    {
        private readonly BloodGroupModel? bloodGroup;
        private VMResponse? response;

        public BloodGroupController(IConfiguration _config)
        {
            bloodGroup = new BloodGroupModel(_config);
        }

//        public IActionResult Index(string? filter)
//        {
//            List<VMMBloodGroup>? data = new List<VMMBloodGroup>();

        public IActionResult Index(string? filter)
        {
            List<VMMBloodGroup>? data = new List<VMMBloodGroup>();

            if (filter != null)
            {
                if(filter == "?")
                {
                    data = bloodGroup.GetByFilter("%3F");
                }
                else if (filter == "+")
                {
                    data = bloodGroup.GetByFilter("%2B");
                }
                else
                {
                    data = bloodGroup.GetByFilter(filter);
                }
                
            }
            else
            {
                data = bloodGroup.GetAll();
            }
            ViewBag.Filter = filter;


            return View(data);
        }

        [HttpPost]
        public VMResponse? CheckCode(string code)
        {
            response = bloodGroup.GetByCode(code);
            return response;
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Tambah Golongan Darah";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMMBloodGroup data)
        {
            response = await bloodGroup.CreateAsync(data);

            return response;
        }

        public IActionResult Edit(long id)
        {
            VMMBloodGroup? data = bloodGroup.GetById(id);
            ViewBag.Title = "Edit Golongan Darah";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Edit(VMMBloodGroup data)
        {

            response = await bloodGroup.UpdateAsync(data);

            return response;
        }

        public IActionResult Delete(long id)
        {
            VMMBloodGroup? data = bloodGroup.GetById(id);
            ViewBag.Title = "Hapus Golongan Darah";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(long id, long userId)
        {
            response = await bloodGroup.DeleteAsync(id, userId);

            return response;
        }

    }
}
