﻿// <auto-generated />
using System;
using HotelDatebaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelDatebaseImplement.Migrations
{
    [DbContext(typeof(HotelDatabase))]
    [Migration("20220521154148_MigDop")]
    partial class MigDop
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelDatebaseImplement.Models.Conference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOf")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.ConferenceRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConferenceId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("RoomId");

                    b.ToTable("ConferenceRooms");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.ConferenceSeminar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConferenceId")
                        .HasColumnType("int");

                    b.Property<int>("SeminarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("SeminarId");

                    b.ToTable("ConferenceSeminars");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Headwaiter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Headwaiters");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Lunch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HeadwaiterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeadwaiterId");

                    b.ToTable("Lunches");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.LunchSeminar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LunchId")
                        .HasColumnType("int");

                    b.Property<int>("SeminarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LunchId");

                    b.HasIndex("SeminarId");

                    b.ToTable("LunchSeminars");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Organizer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizers");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<int>("SeminarId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.HasIndex("SeminarId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HeadwaiterId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeadwaiterId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.RoomLunch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LunchId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LunchId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomLunches");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Roomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateBooking")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HeadwaiterId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HeadwaiterId");

                    b.HasIndex("RoomId");

                    b.ToTable("Roomers");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Seminar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectArea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Seminars");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Conference", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Organizer", "Organizer")
                        .WithMany("Conferences")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.ConferenceRoom", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Conference", "Conference")
                        .WithMany("ConferenceRooms")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDatebaseImplement.Models.Room", "Room")
                        .WithMany("ConferenceRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conference");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.ConferenceSeminar", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Conference", "Conference")
                        .WithMany("ConferenceSeminars")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDatebaseImplement.Models.Seminar", "Seminar")
                        .WithMany("ConferenceSeminars")
                        .HasForeignKey("SeminarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conference");

                    b.Navigation("Seminar");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Lunch", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Headwaiter", "Headwaiter")
                        .WithMany("Lunches")
                        .HasForeignKey("HeadwaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Headwaiter");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.LunchSeminar", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Lunch", "Lunch")
                        .WithMany("LunchSeminars")
                        .HasForeignKey("LunchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDatebaseImplement.Models.Seminar", "Seminar")
                        .WithMany("LunchSeminars")
                        .HasForeignKey("SeminarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lunch");

                    b.Navigation("Seminar");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Participant", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Organizer", "Organizer")
                        .WithMany("Participants")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDatebaseImplement.Models.Seminar", "Seminar")
                        .WithMany("Participants")
                        .HasForeignKey("SeminarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");

                    b.Navigation("Seminar");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Room", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Headwaiter", "Headwaiter")
                        .WithMany("Rooms")
                        .HasForeignKey("HeadwaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Headwaiter");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.RoomLunch", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Lunch", "Lunch")
                        .WithMany("RoomLunches")
                        .HasForeignKey("LunchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDatebaseImplement.Models.Room", "Room")
                        .WithMany("RoomLunches")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lunch");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Roomer", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Headwaiter", "Headwaiter")
                        .WithMany("Roomers")
                        .HasForeignKey("HeadwaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDatebaseImplement.Models.Room", "Room")
                        .WithMany("Roomers")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Headwaiter");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Seminar", b =>
                {
                    b.HasOne("HotelDatebaseImplement.Models.Organizer", "Organizer")
                        .WithMany("Seminars")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Conference", b =>
                {
                    b.Navigation("ConferenceRooms");

                    b.Navigation("ConferenceSeminars");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Headwaiter", b =>
                {
                    b.Navigation("Lunches");

                    b.Navigation("Roomers");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Lunch", b =>
                {
                    b.Navigation("LunchSeminars");

                    b.Navigation("RoomLunches");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Organizer", b =>
                {
                    b.Navigation("Conferences");

                    b.Navigation("Participants");

                    b.Navigation("Seminars");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Room", b =>
                {
                    b.Navigation("ConferenceRooms");

                    b.Navigation("Roomers");

                    b.Navigation("RoomLunches");
                });

            modelBuilder.Entity("HotelDatebaseImplement.Models.Seminar", b =>
                {
                    b.Navigation("ConferenceSeminars");

                    b.Navigation("LunchSeminars");

                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}