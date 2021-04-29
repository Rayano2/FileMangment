using FileManagerApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagementController : ControllerBase
    {
        private readonly FileManagerDbContext _context;

        public FileManagementController(FileManagerDbContext context)
        {
            _context = context;
        }
        
        
        [HttpPost("UploadSingleFile")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file==null)
            {
                return BadRequest();
            }
            string uploadsFolder = Directory.GetCurrentDirectory()+"\\UploadFile";
            string fileName = file.FileName;  
            string filePath = uploadsFolder+"\\"+fileName;
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
            Model.FileInfo info = new Model.FileInfo();
            info.FilePath = filePath;
            info.FileName = fileName;
            _context.FileInfos.Add(info);
            _context.SaveChanges();
            // Process uploaded files

            return Ok(new { count = 1, path = filePath });
        }
    }
    public static class Common
    {
        public static string FetchUniquePath(this string directoryPath, string imageName)
        {
            string extension = Path.GetExtension(imageName);
            string fileName = DateTime.UtcNow.Ticks.ToString();

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            int i = 0;
            while (System.IO.File.Exists(directoryPath + "/" + fileName + i + extension))
                i++;

            return (fileName + i + extension);
        }
    }
}
