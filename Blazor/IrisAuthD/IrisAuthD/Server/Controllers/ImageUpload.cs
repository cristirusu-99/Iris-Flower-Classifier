using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IrisAuthD.Shared;
using System;

namespace IrisAuthD.Server.Controllers
{
    [ApiController]
	[Route("api/upload")]
	public class ImageController : ControllerBase
	{
		private readonly IWebHostEnvironment env;

		public ImageController(IWebHostEnvironment env)
		{
			this.env = env;
		}

		[HttpPost]
		public async Task Post([FromBody] ImageFile[] files)
		{
			foreach (var file in files)
			{
				var buf = Convert.FromBase64String(file.base64data);
				if (file.fileName.Equals("Iris Setosa"))
					await System.IO.File.WriteAllBytesAsync(env.ContentRootPath + "\\Images\\" + "\\Iris Setosa\\" + file.fileName + ".jpg", buf);
				if (file.fileName.Equals("Iris Versicolor"))
					await System.IO.File.WriteAllBytesAsync(env.ContentRootPath + "\\Images\\" + "\\Iris Versicolor\\" + file.fileName + ".jpg", buf);
				if (file.fileName.Equals("Iris Virginica"))
					await System.IO.File.WriteAllBytesAsync(env.ContentRootPath + "\\Images\\" + "\\Iris Virginica\\" + file.fileName + ".jpg", buf);

			}
		}
	}
}


