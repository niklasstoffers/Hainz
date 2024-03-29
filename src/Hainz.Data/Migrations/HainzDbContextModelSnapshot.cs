﻿// <auto-generated />
using Hainz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hainz.Data.Migrations
{
    [DbContext(typeof(HainzDbContext))]
    partial class HainzDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Hainz.Data.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelFlags")
                        .HasColumnType("integer");

                    b.Property<decimal>("DiscordChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Hainz.Data.Entities.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "DefaultActivityType",
                            Value = "Playing"
                        },
                        new
                        {
                            Id = 2,
                            Name = "DefaultActivityName",
                            Value = "Development"
                        },
                        new
                        {
                            Id = 3,
                            Name = "DefaultStatus",
                            Value = "AFK"
                        },
                        new
                        {
                            Id = 4,
                            Name = "SendDMUponBan",
                            Value = "True"
                        },
                        new
                        {
                            Id = 5,
                            Name = "CommandPrefix",
                            Value = "!"
                        });
                });

            modelBuilder.Entity("Hainz.Data.Entities.Guild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("DiscordGuildId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Hainz.Data.Entities.GuildSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GuildId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("GuildSettings");
                });

            modelBuilder.Entity("Hainz.Data.Entities.GuildSetting", b =>
                {
                    b.HasOne("Hainz.Data.Entities.Guild", "Guild")
                        .WithMany("GuildSettings")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("Hainz.Data.Entities.Guild", b =>
                {
                    b.Navigation("GuildSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
