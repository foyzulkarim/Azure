using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using Aspose.BarCode;

namespace BarcodeFunctionApp
{
    public class BarcodeImageHandler
    {
        public static HttpResponseMessage GetBarcodeImage(string barcode, string text)
        {
            string uploadPath = "";
            uploadPath = Path.GetTempPath();
            string filename = uploadPath + "/" + barcode + ".png";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            GenerateBarcode(barcode, text, filename);
            var response = GenerateHttpResponse(filename);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            return response;
        }

        private static HttpResponseMessage GenerateHttpResponse(string filename)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            Stream stream = new MemoryStream(File.ReadAllBytes(filename));
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }

        private static void GenerateBarcode(string barCode, string text, string filename)
        {
            BarCodeBuilder builder = new BarCodeBuilder(barCode);
            Caption captionBelow = new Caption(text)
            {
                TextAlign = StringAlignment.Center,
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            builder.CaptionBelow = captionBelow;
            builder.Save(filename, ImageFormat.Png);
        }
    }
}