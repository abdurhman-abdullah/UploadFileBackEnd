// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UploadFile.Db;

namespace UploadFile.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210810092727_Initail")]
    partial class Initail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UploadFile.Model.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UploadedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileModels");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FileModel");
                });

            modelBuilder.Entity("UploadFile.Model.FileOnDatabaseModel", b =>
                {
                    b.HasBaseType("UploadFile.Model.FileModel");

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.HasDiscriminator().HasValue("FileOnDatabaseModel");
                });

            modelBuilder.Entity("UploadFile.Model.FileOnFileSystemModel", b =>
                {
                    b.HasBaseType("UploadFile.Model.FileModel");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("FileOnFileSystemModel");
                });
#pragma warning restore 612, 618
        }
    }
}
