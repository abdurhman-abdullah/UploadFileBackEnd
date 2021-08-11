using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Db;
using UploadFile.Model;
using UploadFile.Repository;

namespace UploadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public FileController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            FileRepository fileRepository = new FileRepository(this.context);
            var fileuploadViewModel = await context.FileOnFileSystemModels.ToListAsync();
            return Ok(fileuploadViewModel);
        }

        [HttpGet]
        [Route("Id")]
        public async Task<IActionResult> IndexById(int id)
        {
            var fileuploadViewModel = await context.FileOnFileSystemModels.FirstOrDefaultAsync(i => i.Id == id);
            return Ok(fileuploadViewModel);
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new FileOnFileSystemModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = description,
                        FilePath = filePath
                    };
                    context.FileOnFileSystemModels.Add(fileModel);
                    context.SaveChanges();
                }
            }
            return Ok(new { name = files.Select(i => i.FileName) });
        }

        [HttpPost]
        [Route("DownLoad")]
        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {
            var file = await context.FileOnFileSystemModels.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(file.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, file.FileType, file.Name + file.Extension);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFileFromFileSystem(int id)
        {
            var file = await context.FileOnFileSystemModels.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            context.FileOnFileSystemModels.Remove(file);
            context.SaveChanges();
           
            return RedirectToAction("Index");
        }
    }
}
