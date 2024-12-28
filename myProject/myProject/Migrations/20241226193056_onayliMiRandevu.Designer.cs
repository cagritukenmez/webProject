﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using myProject.Models;

#nullable disable

namespace myProject.Migrations
{
    [DbContext(typeof(myyDbContext))]
    [Migration("20241226193056_onayliMiRandevu")]
    partial class onayliMiRandevu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("myProject.Models.Berbersalonu", b =>
                {
                    b.Property<int>("salonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("salonID"));

                    b.Property<TimeOnly>("baslangicSaati")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("bitisSaati")
                        .HasColumnType("time");

                    b.Property<string>("salonAd")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("salonAdres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("salonEposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("salonNumara")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("salonID");

                    b.ToTable("BerberSalonları");
                });

            modelBuilder.Entity("myProject.Models.Hizmetler", b =>
                {
                    b.Property<int>("hizmetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("hizmetID"));

                    b.Property<string>("hizmetAdi")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("hizmetFiyat")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("hizmetSuresi")
                        .HasColumnType("time");

                    b.Property<int>("personelID")
                        .HasColumnType("int");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.HasKey("hizmetID");

                    b.HasIndex("personelID");

                    b.HasIndex("salonID");

                    b.ToTable("Hizmetler");
                });

            modelBuilder.Entity("myProject.Models.Kullanıcı", b =>
                {
                    b.Property<int>("kullaniciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("kullaniciID"));

                    b.Property<string>("eposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("kullaniciSifre")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("kullaniciID");

                    b.ToTable("Kullanıcı");
                });

            modelBuilder.Entity("myProject.Models.Personeller", b =>
                {
                    b.Property<int>("personelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("personelID"));

                    b.Property<string>("eposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("isim")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.Property<string>("soyisim")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("telNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("personelID");

                    b.HasIndex("salonID");

                    b.ToTable("Personeller");
                });

            modelBuilder.Entity("myProject.Models.Randevu", b =>
                {
                    b.Property<int>("randevuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("randevuID"));

                    b.Property<int>("hizmetID")
                        .HasColumnType("int");

                    b.Property<int>("kullaniciID")
                        .HasColumnType("int");

                    b.Property<bool>("onaylimi")
                        .HasColumnType("bit");

                    b.Property<int>("personelID")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("randevuSaati")
                        .HasColumnType("time");

                    b.Property<DateTime>("randevuTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.HasKey("randevuID");

                    b.HasIndex("hizmetID");

                    b.HasIndex("kullaniciID");

                    b.HasIndex("personelID");

                    b.HasIndex("salonID");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("myProject.Models.UygunTarih", b =>
                {
                    b.Property<int>("tarihID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tarihID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("salonID")
                        .HasColumnType("int");

                    b.HasKey("tarihID");

                    b.HasIndex("salonID");

                    b.ToTable("UygunTarih");
                });

            modelBuilder.Entity("myProject.Models.Hizmetler", b =>
                {
                    b.HasOne("myProject.Models.Personeller", "Personel")
                        .WithMany("Hizmetler")
                        .HasForeignKey("personelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("myProject.Models.Berbersalonu", "Berbersalonu")
                        .WithMany("Hizmetler")
                        .HasForeignKey("salonID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Berbersalonu");

                    b.Navigation("Personel");
                });

            modelBuilder.Entity("myProject.Models.Personeller", b =>
                {
                    b.HasOne("myProject.Models.Berbersalonu", "Berbersalonu")
                        .WithMany("Personeller")
                        .HasForeignKey("salonID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Berbersalonu");
                });

            modelBuilder.Entity("myProject.Models.Randevu", b =>
                {
                    b.HasOne("myProject.Models.Hizmetler", "hizmet")
                        .WithMany()
                        .HasForeignKey("hizmetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("myProject.Models.Kullanıcı", "kullanici")
                        .WithMany("Randevular")
                        .HasForeignKey("kullaniciID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("myProject.Models.Personeller", "personel")
                        .WithMany("Randevular")
                        .HasForeignKey("personelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("myProject.Models.Berbersalonu", "Berbersalonu")
                        .WithMany("Randevular")
                        .HasForeignKey("salonID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Berbersalonu");

                    b.Navigation("hizmet");

                    b.Navigation("kullanici");

                    b.Navigation("personel");
                });

            modelBuilder.Entity("myProject.Models.UygunTarih", b =>
                {
                    b.HasOne("myProject.Models.Berbersalonu", "Berbersalonu")
                        .WithMany("UygunTarih")
                        .HasForeignKey("salonID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Berbersalonu");
                });

            modelBuilder.Entity("myProject.Models.Berbersalonu", b =>
                {
                    b.Navigation("Hizmetler");

                    b.Navigation("Personeller");

                    b.Navigation("Randevular");

                    b.Navigation("UygunTarih");
                });

            modelBuilder.Entity("myProject.Models.Kullanıcı", b =>
                {
                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("myProject.Models.Personeller", b =>
                {
                    b.Navigation("Hizmetler");

                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}