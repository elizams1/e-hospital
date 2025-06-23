using BATCH336B.Models;
using BATCH336B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Http;

namespace BATCH336B.Controllers
{
    public class ViewRendererController : Controller
    {
        private readonly HistoryCustomerModel? historyCustomerModel;


        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private VMResponse? response = new VMResponse();

        public ViewRendererController(IConfiguration _config , IRazorViewEngine razorViewEngine, IHttpContextAccessor httpContextAccessor, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            historyCustomerModel = new HistoryCustomerModel(_config);
            _httpContextAccessor = httpContextAccessor;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider)); ; 
        }

        public async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} not found");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }

        public async Task<IActionResult> CetakPdf(int id)
        {
            VMMHistoryCustomer? data = historyCustomerModel?.GetById(id);
            VMTPrescription? dataPres = new VMTPrescription();

            foreach (VMTPrescription customer in data.Prescription)
            {
                customer.PrintAttempt += 1;
                customer.ModifiedBy = HttpContext.Session.GetInt32("custId");
                historyCustomerModel?.Update(customer);
            }

            foreach (VMTPrescription flagAttempt in data.Prescription)
            {
                dataPres.PrintAttempt = flagAttempt.PrintAttempt;
            }

            int? flag = dataPres.PrintAttempt;

            await new BrowserFetcher().DownloadAsync();
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            using var page = await browser.NewPageAsync();

            // Render the view to HTML
            var htmlContent = await RenderViewToStringAsync("ViewRenderer/CetakPdf", data);


            await page.SetContentAsync(htmlContent); // Gunakan HTML yang dihasilkan

            var pdfOptions = new PdfOptions
            {
                Format = PaperFormat.A4
            };

            var pdf = await page.PdfDataAsync(pdfOptions);
            // Simpan file PDF
            var resepObat = $"{data.Biodatum.Fullname + "-"}{data.MedicalFacility.Name + "-"}{DateTime.Now.ToString("dd-MMM-yyyy" + "-")}{flag}";

            var fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = $"{resepObat}.pdf";
            
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return fileResult;
        }
    }
}
