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
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        public FileController(IBaseRepository<FileStorage> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            long MaxFileSizeInBytes = 5 * 1024 * 1024; // 2MB
            if (uploadedFile.Length > MaxFileSizeInBytes)
            {
                return BadRequest("File size exceeds 2MB.");
            }

            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                if (!Directory.Exists(_storagePath))
                {
                    Directory.CreateDirectory(_storagePath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedFile.FileName);
                var filePath = Path.Combine(_storagePath, fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(stream);
                }

                FileStorage file = new FileStorage()
                {
                    FileName = fileName,
                    FileType = (int)GetFileTypeByExtension(uploadedFile.FileName),
                    FileDownloadName = uploadedFile.FileName,
                };

                _baseRepository.Save(file);

                return RedirectToAction("Files", "Admin"); // Redirect or return a view
            }
            return BadRequest("No file uploaded");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var file = _baseRepository.GetWithId(id);
            if (file != null)
            {
                // Combine the directory with the file name to get the full file path
                var filePath = Path.Combine(_storagePath, file.FileName);

                // Check if the file exists in the directory
                if (System.IO.File.Exists(filePath))
                {
                    // Read the file bytes from the disk
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                    // Return the file as a download
                    return File(fileBytes, "application/octet-stream", file.FileDownloadName);
                }

                return NotFound();
            }

            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var file = _baseRepository.GetWithId(id);
            if (file != null)
            {
                // Delete the file from the physical storage
                var filePath = Path.Combine(_storagePath, file.FileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // Delete the file from the folder
                }

                // Delete the file from the database
                Guid[] fileToDelete = new Guid[] { id };
                _baseRepository.DeleteWithIds(fileToDelete);

                return RedirectToAction("Files", "Admin");
            }

            // Handle the case when the file does not exist in the database
            return RedirectToAction("Files", "Admin");
        }
        public FileType GetFileTypeByExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                // Image types
                ".jpg" or ".jpeg" => FileType.Image,
                ".png" => FileType.Image,
                ".gif" => FileType.Image,

                // Document types
                ".pdf" => FileType.Pdf,
                ".doc" or ".docx" => FileType.Document,
                ".xls" or ".xlsx" => FileType.Excel,

                // Video types
                ".mp4" => FileType.Video,
                ".avi" => FileType.Video,
                ".mov" => FileType.Video,
                ".wmv" => FileType.Video,
                ".mkv" => FileType.Video,

                // Default for unknown types
                _ => FileType.Other
            };
        }
        [HttpGet]
        public IActionResult LoadPartial(Guid id)
        {
            var file = _baseRepository.GetWithId(id);
            //var model = new YourModel { Title = "Sample Title", Description = "This content is loaded into the modal." };
            //return PartialView("_SamplePartial", model);
            return PartialView("Modals/_File", file);
        }
    }
    public enum FileType
    {
        Image = 1,
        Pdf = 2,
        Document = 3,
        Excel = 4,
        Video = 5,
        Other = 6
    }
}
