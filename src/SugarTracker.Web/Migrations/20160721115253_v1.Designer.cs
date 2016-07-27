using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SugarTracker.Web.DataContext;

namespace SugarTracker.Web.Migrations
{
    [DbContext(typeof(SugarTrackerDbContext))]
    [Migration("20160721115253_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SugarTracker.Web.Entities.RawReading", b =>
                {
                    b.Property<int>("RawReadingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FromPhoneNumber");

                    b.Property<string>("Message");

                    b.Property<DateTime>("ReadingTime");

                    b.HasKey("RawReadingId");

                    b.ToTable("RawReadings");
                });

            modelBuilder.Entity("SugarTracker.Web.Entities.Reading", b =>
                {
                    b.Property<int>("ReadingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<int>("RawReadingId");

                    b.Property<DateTime>("ReadingTime");

                    b.Property<int>("Type");

                    b.Property<int>("UserId");

                    b.Property<double>("Value");

                    b.HasKey("ReadingId");

                    b.ToTable("Readings");
                });
        }
    }
}
