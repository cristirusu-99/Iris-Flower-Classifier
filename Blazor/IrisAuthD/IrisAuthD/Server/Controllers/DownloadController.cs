using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IrisAuthD.Server.Util;
using System.IO;

namespace IrisAuthD.Server.Controllers
{
    [Route("api/download")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private IWebHostEnvironment HostEnvironment { get; }

        private Stream GetZipStream()
        {

            List<ZipItem> zipItems = new List<ZipItem>();
            try{
                var fileInfo = this.HostEnvironment.ContentRootFileProvider.GetFileInfo("resources/files_to_zip/ImageProcessing.exe");
                zipItems.Add(new ZipItem("ImageProcessing.exe", System.IO.File.OpenRead(fileInfo.PhysicalPath)));
                fileInfo = this.HostEnvironment.ContentRootFileProvider.GetFileInfo("resources/files_to_zip/ImageProcessing.exe.config");
                zipItems.Add(new ZipItem("ImageProcessing.exe", System.IO.File.OpenRead(fileInfo.PhysicalPath)));
                System.Console.WriteLine("salut");
                return Zipper.Zip(zipItems);
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return Zipper.Zip(zipItems);
        }

        public PictureController(IWebHostEnvironment hostEnvironment)
        {
            HostEnvironment = hostEnvironment;
        }

        [HttpGet("util1")]
        public async Task<IActionResult> GetUtil1()
        {
            var zipStream = GetZipStream();
            System.Console.WriteLine("Test");
            try{
                return File(zipStream, "application/octet-stream");
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return File(zipStream, "application/octet-stream");
        }

        [HttpGet("util2")]
        public async Task<IActionResult> GetUtil2()
        {
            var fileInfo = this.HostEnvironment.ContentRootFileProvider.GetFileInfo("resources/Distance Calculator.zip");
            var archiveBytes = await System.IO.File.ReadAllBytesAsync(fileInfo.PhysicalPath);
            return this.File(archiveBytes, "application/zip");
        }
    }
}
