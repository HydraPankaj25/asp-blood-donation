using BloodDonation.DAL.Entities.BloodStorage;
using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Entities.Donor;
using BloodDonation.DAL.Entities.States;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.AppContext
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        

        public DbSet<DonorDetailsEntity> DonorDetails { get; set; }

        public DbSet<DonorDonationDetailsEntity> DonorDonationDetails { get;set; }

        public DbSet<BloodStorageEntity> BloodStorageDetails { get; set; }

        public DbSet<DoctorEntity> DoctorDetails { get; set; }

        public DbSet<State> states { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DonorDetailsEntity>().HasKey(d => d.Id);

            builder.Entity<DonorDetailsEntity>().HasIndex(d => d.DonorEmail).IsUnique();


            builder.Entity<State>().Property(a=>a.id).ValueGeneratedNever();    


            builder.Entity<DonorDonationDetailsEntity>().HasKey(d => d.Id);




            builder.Entity<BloodStorageEntity>().HasKey(d => d.Id);




            builder.Entity<DoctorEntity>().HasKey(d => d.Id);

            builder.Entity<DoctorEntity>().HasIndex(d=>d.DoctorEmail).IsUnique();




            builder.Entity<DonorDonationDetailsEntity>().HasOne(e => e.DonorDetails).WithMany(e => e.DonorDonationDetails).HasForeignKey(e => e.DonorId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DonorDonationDetailsEntity>().HasOne(e => e.DoctorDetails).WithMany(e => e.DonorDonationDetails).HasForeignKey(e => e.DoctorId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
