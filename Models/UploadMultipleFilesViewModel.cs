using System.ComponentModel.DataAnnotations;

namespace UploadMultipleFiles.Models;

public class UploadMultipleFilesViewModel
{
    [Required(ErrorMessage = "Favor escolher os arquivos!")]
    public List<IFormFile>? Files { get; set; }

    public bool IsResponse { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
