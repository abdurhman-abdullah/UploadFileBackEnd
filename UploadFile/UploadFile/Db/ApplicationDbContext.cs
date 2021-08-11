using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;

namespace UploadFile.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<FileOnFileSystemModel> FileOnFileSystemModels { get; set; }
        public DbSet<FileOnDatabaseModel> FileOnDatabaseModels { get; set; }
    }
}
