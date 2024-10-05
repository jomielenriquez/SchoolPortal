using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Portal.Data.Entities;
using Portal.Data.Interface;

namespace Portal.Controllers
{
    public class FileController : Controller
    {
        private readonly IBaseRepository<FileStorage> _baseRepository;
        public FileController(IBaseRepository<FileStorage> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                // Convert the uploaded file to a Base64 string
                using (var memoryStream = new MemoryStream())
                {
                    await uploadedFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);

                    // Store the Base64 string in the database
                    FileStorage file = new FileStorage()
                    {
                        FileName = uploadedFile.FileName,
                        FileData = base64String
                    };

                    _baseRepository.Save(file);
                }

                return RedirectToAction("Files", "Admin"); // Redirect or return a view
            }
            return BadRequest("No file uploaded");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var file = _baseRepository.GetWithId(id);
            if(file != null)
            {
                byte[] fileBytes = Convert.FromBase64String(file.FileData);

                return File(fileBytes, "application/octet-stream", file.FileName);
            }
            return NotFound();
        }
    }
}
