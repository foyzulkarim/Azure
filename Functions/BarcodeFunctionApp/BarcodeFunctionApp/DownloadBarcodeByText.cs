using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace BarcodeFunctionApp
{
    public static class DownloadBarcodeByText
    {
        [FunctionName("DownloadBarcodeByText")]

        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "DownloadBarcodeByText/{barcode}/{text}")]HttpRequestMessage req, string barcode, string text, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            if (string.IsNullOrWhiteSpace(barcode))
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Barcode can not be empty or null");
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Text can not be empty or null");
            }
            
            var response = BarcodeImageHandler.GetBarcodeImage(barcode, text);
            return response;
        }
    }
}