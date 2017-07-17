using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace BarcodeFunctionApp
{
    public static class DownloadBarcodeByProductId
    {
        [FunctionName("DownloadBarcodeByProductId")]

        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "DownloadBarcodeByProductId/{id}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            if (string.IsNullOrWhiteSpace(id))
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Id can not be empty or null");
            }
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Id must be a GUID or UUID");
            }

            BusinessDbContext businessDbContext = new BusinessDbContext();
            var model = businessDbContext.ProductDetails.Find(id);
            HttpResponseMessage response;
            if (model == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
            string barCode = model.BarCode;
            string text = $"{model.Name} \n MRP: {model.SalePrice} Tk";
            response = BarcodeImageHandler.GetBarcodeImage(barCode, text);
            return response;
        }
    }
}