﻿// <auto-generated />
using System;
using LDS_2425.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LDS_2425.Migrations
{
    [DbContext(typeof(MachineHubContext))]
    [Migration("20241027124423_ThirdVersionSchema")]
    partial class ThirdVersionSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LDS_2425.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LDS_2425.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.HasIndex("ReceiptId")
                        .IsUnique();

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("FavoritesPage");
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPageLoan_Listing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FavoritesPageId")
                        .HasColumnType("int");

                    b.Property<int>("Loan_ListingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Loan_ListingId");

                    b.ToTable("FavoritesPageLoan_Listing");
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPageMachine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FavoritesPageId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MachineId");

                    b.ToTable("FavoritesPageMachine");
                });

            modelBuilder.Entity("LDS_2425.Models.Loan_Listing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateListed")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<bool>("TransportNecessary")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("WorkerAvailable")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("YearManufacture")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("Loan_Listings");
                });

            modelBuilder.Entity("LDS_2425.Models.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateOnly>("Year_of_Manufacture")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("LDS_2425.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyNif")
                        .HasColumnType("int");

                    b.Property<int>("CompanyPhone")
                        .HasColumnType("int");

                    b.Property<int?>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReceipt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Loan_ListingId")
                        .HasColumnType("int");

                    b.Property<int?>("MachineId")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Loan_ListingId");

                    b.HasIndex("MachineId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCartLoan_Listing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FavoritesPageId")
                        .HasColumnType("int");

                    b.Property<int>("Loan_ListingId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FavoritesPageId");

                    b.HasIndex("Loan_ListingId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartLoan_ListingConnections");
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCartMachine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FavoritesPageId")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FavoritesPageId");

                    b.HasIndex("MachineId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartMachineConnections");
                });

            modelBuilder.Entity("LDS_2425.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FavoritesPageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LDS_2425.Models.Contract", b =>
                {
                    b.HasOne("LDS_2425.Models.Loan_Listing", "Listing")
                        .WithMany("Contracts")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LDS_2425.Models.Receipt", "Receipt")
                        .WithOne("Contract")
                        .HasForeignKey("LDS_2425.Models.Contract", "ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPage", b =>
                {
                    b.HasOne("LDS_2425.Models.User", "User")
                        .WithOne("FavoritesPage")
                        .HasForeignKey("LDS_2425.Models.FavoritesPage", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPageLoan_Listing", b =>
                {
                    b.HasOne("LDS_2425.Models.Loan_Listing", null)
                        .WithMany("FavoritesPages")
                        .HasForeignKey("Loan_ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPageMachine", b =>
                {
                    b.HasOne("LDS_2425.Models.Machine", null)
                        .WithMany("FavoritesPages")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LDS_2425.Models.Loan_Listing", b =>
                {
                    b.HasOne("LDS_2425.Models.Category", "Category")
                        .WithMany("LoanListings")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LDS_2425.Models.User", "Owner")
                        .WithMany("LoanedIn")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LDS_2425.Models.User", "User")
                        .WithMany("LoanedOut")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Owner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LDS_2425.Models.Machine", b =>
                {
                    b.HasOne("LDS_2425.Models.Category", "Category")
                        .WithMany("Machines")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LDS_2425.Models.Receipt", b =>
                {
                    b.HasOne("LDS_2425.Models.Loan_Listing", "Loan_Listing")
                        .WithMany()
                        .HasForeignKey("Loan_ListingId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LDS_2425.Models.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LDS_2425.Models.User", "Owner")
                        .WithMany("ReceiptsOwner")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LDS_2425.Models.User", "User")
                        .WithMany("ReceiptsUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Loan_Listing");

                    b.Navigation("Machine");

                    b.Navigation("Owner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCart", b =>
                {
                    b.HasOne("LDS_2425.Models.User", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("LDS_2425.Models.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCartLoan_Listing", b =>
                {
                    b.HasOne("LDS_2425.Models.FavoritesPage", null)
                        .WithMany("LoanListings")
                        .HasForeignKey("FavoritesPageId");

                    b.HasOne("LDS_2425.Models.Loan_Listing", null)
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("Loan_ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LDS_2425.Models.ShoppingCart", null)
                        .WithMany("LoanListings")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCartMachine", b =>
                {
                    b.HasOne("LDS_2425.Models.FavoritesPage", null)
                        .WithMany("Machines")
                        .HasForeignKey("FavoritesPageId");

                    b.HasOne("LDS_2425.Models.Machine", null)
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LDS_2425.Models.ShoppingCart", null)
                        .WithMany("Machines")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LDS_2425.Models.Category", b =>
                {
                    b.Navigation("LoanListings");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("LDS_2425.Models.FavoritesPage", b =>
                {
                    b.Navigation("LoanListings");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("LDS_2425.Models.Loan_Listing", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("FavoritesPages");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("LDS_2425.Models.Machine", b =>
                {
                    b.Navigation("FavoritesPages");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("LDS_2425.Models.Receipt", b =>
                {
                    b.Navigation("Contract");
                });

            modelBuilder.Entity("LDS_2425.Models.ShoppingCart", b =>
                {
                    b.Navigation("LoanListings");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("LDS_2425.Models.User", b =>
                {
                    b.Navigation("FavoritesPage")
                        .IsRequired();

                    b.Navigation("LoanedIn");

                    b.Navigation("LoanedOut");

                    b.Navigation("ReceiptsOwner");

                    b.Navigation("ReceiptsUser");

                    b.Navigation("ShoppingCart")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
