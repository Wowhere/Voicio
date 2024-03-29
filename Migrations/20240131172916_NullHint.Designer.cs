﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using voicio.Models;

#nullable disable

namespace voicio.Migrations
{
    [DbContext(typeof(HelpContext))]
    [Migration("20240131172916_NullHint")]
    partial class NullHint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("voicio.Models.Hint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("HintText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hints");
                });

            modelBuilder.Entity("voicio.Models.HintTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HintId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HintId");

                    b.HasIndex("TagId");

                    b.ToTable("HintTag");
                });

            modelBuilder.Entity("voicio.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TagText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("voicio.Models.HintTag", b =>
                {
                    b.HasOne("voicio.Models.Hint", "Hint")
                        .WithMany("HintTag")
                        .HasForeignKey("HintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("voicio.Models.Tag", "Tag")
                        .WithMany("HintTag")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hint");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("voicio.Models.Hint", b =>
                {
                    b.Navigation("HintTag");
                });

            modelBuilder.Entity("voicio.Models.Tag", b =>
                {
                    b.Navigation("HintTag");
                });
#pragma warning restore 612, 618
        }
    }
}
