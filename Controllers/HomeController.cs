using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UploadMultipleFiles.Models;

namespace UploadMultipleFiles.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Upload()
    {
        return View(new UploadMultipleFilesViewModel());
    }

    [HttpPost]
    public IActionResult Upload(UploadMultipleFilesViewModel model)
    {
        model.IsSuccess = false;

        if (ModelState.IsValid)
        {
            model.IsResponse = true;

            if (model.Files != null && model.Files.Count > 0)
            {
                foreach (var file in model.Files)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles");

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string fileNameWithPath = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                model.IsSuccess = true;
                model.Message = "Upload de arquivos feito com sucesso.";
            }
            else
            {
                model.Message = "Favor escolher os arquivos!";
            }
        }
        else
        {
            model.Message = "Ops! Algo deu errado.";
        }

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
