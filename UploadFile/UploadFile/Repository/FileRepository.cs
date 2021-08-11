using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Db;
using UploadFile.Model;

namespace UploadFile.Repository
{
    public class FileRepository
    {
        private readonly ApplicationDbContext context;

        public FileRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<FileUploadViewModel> LoadAllFiles()
        {
            var viewModel = new FileUploadViewModel();
            viewModel.FilesOnDatabase = await context.FileOnDatabaseModels.ToListAsync();
            viewModel.FilesOnFileSystem = await context.FileOnFileSystemModels.ToListAsync();
            return viewModel;
        }

    }

}
