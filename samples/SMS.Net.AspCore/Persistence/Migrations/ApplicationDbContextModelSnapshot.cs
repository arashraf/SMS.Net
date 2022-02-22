﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMS.Net.Persistence;

#nullable disable

namespace SMS.Net.AspCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsClient", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.Property<string>("ConnectionId")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("RavenSmsClients");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsClientPhoneNumber", b =>
                {
                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.HasKey("PhoneNumber");

                    b.HasIndex("ClientId");

                    b.ToTable("RavenSmsClientPhoneNumber");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.Property<DateTimeOffset>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("JobQueueId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("RavenSmsMessages");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsMessageSendAttempt", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Errors")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("varchar(4000)");

                    b.Property<string>("MessageId")
                        .IsRequired()
                        .HasColumnType("varchar(17)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("RavenSmsMessageSendAttempt");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsClientPhoneNumber", b =>
                {
                    b.HasOne("SMS.Net.Channel.RavenSMS.Entities.RavenSmsClient", null)
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsMessage", b =>
                {
                    b.HasOne("SMS.Net.Channel.RavenSMS.Entities.RavenSmsClient", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsMessageSendAttempt", b =>
                {
                    b.HasOne("SMS.Net.Channel.RavenSMS.Entities.RavenSmsMessage", "Message")
                        .WithMany("SendAttempts")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsClient", b =>
                {
                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("SMS.Net.Channel.RavenSMS.Entities.RavenSmsMessage", b =>
                {
                    b.Navigation("SendAttempts");
                });
#pragma warning restore 612, 618
        }
    }
}
