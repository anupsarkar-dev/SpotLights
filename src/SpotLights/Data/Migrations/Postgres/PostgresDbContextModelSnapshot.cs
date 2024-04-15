// <auto-generated />
using System;
using SpotLights.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SpotLights.Data.Migrations.Postgres
{
  [DbContext(typeof(PostgresDbContext))]
  partial class PostgresDbContextModelSnapshot : ModelSnapshot
  {
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "7.0.5")
          .HasAnnotation("Relational:MaxIdentifierLength", 63);

      NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

      modelBuilder.Entity("SpotLights.Identity.UserInfo", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasMaxLength(128)
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<int>("AccessFailedCount")
                      .HasColumnType("integer");

            b.Property<string>("Avatar")
                      .HasMaxLength(1024)
                      .HasColumnType("character varying(1024)");

            b.Property<string>("Bio")
                      .HasMaxLength(2048)
                      .HasColumnType("character varying(2048)");

            b.Property<string>("ConcurrencyStamp")
                      .IsConcurrencyToken()
                      .HasMaxLength(64)
                      .HasColumnType("character varying(64)");

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasColumnOrder(0)
                      .HasDefaultValueSql("now()");

            b.Property<string>("Email")
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<bool>("EmailConfirmed")
                      .HasColumnType("boolean");

            b.Property<string>("Gender")
                      .HasMaxLength(32)
                      .HasColumnType("character varying(32)");

            b.Property<bool>("LockoutEnabled")
                      .HasColumnType("boolean");

            b.Property<DateTimeOffset?>("LockoutEnd")
                      .HasColumnType("timestamp with time zone");

            b.Property<string>("NickName")
                      .IsRequired()
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<string>("NormalizedEmail")
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<string>("NormalizedUserName")
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<string>("PasswordHash")
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<string>("PhoneNumber")
                      .HasMaxLength(32)
                      .HasColumnType("character varying(32)");

            b.Property<bool>("PhoneNumberConfirmed")
                      .HasColumnType("boolean");

            b.Property<string>("SecurityStamp")
                      .HasMaxLength(32)
                      .HasColumnType("character varying(32)");

            b.Property<int>("State")
                      .HasColumnType("integer");

            b.Property<bool>("TwoFactorEnabled")
                      .HasColumnType("boolean");

            b.Property<int>("Type")
                      .HasColumnType("integer");

            b.Property<DateTime>("UpdatedAt")
                      .HasColumnType("timestamp with time zone")
                      .HasColumnOrder(1);

            b.Property<string>("UserName")
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.HasKey("Id");

            b.HasIndex("NormalizedEmail")
                      .HasDatabaseName("EmailIndex");

            b.HasIndex("NormalizedUserName")
                      .IsUnique()
                      .HasDatabaseName("UserNameIndex");

            b.ToTable("Users", (string)null);
          });

      modelBuilder.Entity("SpotLights.Newsletters.Newsletter", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("now()");

            b.Property<int>("PostId")
                      .HasColumnType("integer");

            b.Property<bool>("Success")
                      .HasColumnType("boolean");

            b.Property<DateTime>("UpdatedAt")
                      .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.HasIndex("PostId");

            b.ToTable("Newsletters");
          });

      modelBuilder.Entity("SpotLights.Newsletters.Subscriber", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("Country")
                      .HasMaxLength(120)
                      .HasColumnType("character varying(120)");

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("now()");

            b.Property<string>("Email")
                      .IsRequired()
                      .HasMaxLength(160)
                      .HasColumnType("character varying(160)");

            b.Property<string>("Ip")
                      .HasMaxLength(80)
                      .HasColumnType("character varying(80)");

            b.Property<string>("Region")
                      .HasMaxLength(120)
                      .HasColumnType("character varying(120)");

            b.Property<DateTime>("UpdatedAt")
                      .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.ToTable("Subscribers");
          });

      modelBuilder.Entity("SpotLights.Options.OptionInfo", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("now()");

            b.Property<string>("Key")
                      .IsRequired()
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<DateTime>("UpdatedAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<string>("Value")
                      .IsRequired()
                      .HasColumnType("text");

            b.HasKey("Id");

            b.HasIndex("Key")
                      .IsUnique();

            b.ToTable("Options", (string)null);
          });

      modelBuilder.Entity("SpotLights.Shared.Category", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("Content")
                      .IsRequired()
                      .HasMaxLength(120)
                      .HasColumnType("character varying(120)");

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("now()");

            b.Property<string>("Description")
                      .HasMaxLength(255)
                      .HasColumnType("character varying(255)");

            b.HasKey("Id");

            b.ToTable("Categories");
          });

      modelBuilder.Entity("SpotLights.Shared.Post", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("Content")
                      .IsRequired()
                      .HasColumnType("text");

            b.Property<string>("Cover")
                      .HasMaxLength(160)
                      .HasColumnType("character varying(160)");

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("now()");

            b.Property<string>("Description")
                      .IsRequired()
                      .HasMaxLength(450)
                      .HasColumnType("character varying(450)");

            b.Property<int>("PostType")
                      .HasColumnType("integer");

            b.Property<DateTime?>("PublishedAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<string>("Slug")
                      .IsRequired()
                      .HasMaxLength(160)
                      .HasColumnType("character varying(160)");

            b.Property<int>("State")
                      .HasColumnType("integer");

            b.Property<string>("Title")
                      .IsRequired()
                      .HasMaxLength(160)
                      .HasColumnType("character varying(160)");

            b.Property<DateTime>("UpdatedAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<int>("UserId")
                      .HasColumnType("integer");

            b.Property<int>("Views")
                      .HasColumnType("integer");

            b.HasKey("Id");

            b.HasIndex("Slug")
                      .IsUnique();

            b.HasIndex("UserId");

            b.ToTable("Posts", (string)null);
          });

      modelBuilder.Entity("SpotLights.Shared.PostCategory", b =>
          {
            b.Property<int>("PostId")
                      .HasColumnType("integer");

            b.Property<int>("CategoryId")
                      .HasColumnType("integer");

            b.HasKey("PostId", "CategoryId");

            b.HasIndex("CategoryId");

            b.ToTable("PostCategories", (string)null);
          });

      modelBuilder.Entity("SpotLights.Storages.Storage", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("ContentType")
                      .IsRequired()
                      .HasMaxLength(128)
                      .HasColumnType("character varying(128)");

            b.Property<DateTime>("CreatedAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("now()");

            b.Property<DateTime?>("DeletedAt")
                      .HasColumnType("timestamp with time zone");

            b.Property<bool>("IsDeleted")
                      .HasColumnType("boolean");

            b.Property<long>("Length")
                      .HasColumnType("bigint");

            b.Property<string>("Name")
                      .IsRequired()
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<string>("Path")
                      .IsRequired()
                      .HasMaxLength(2048)
                      .HasColumnType("character varying(2048)");

            b.Property<string>("Slug")
                      .IsRequired()
                      .HasMaxLength(2048)
                      .HasColumnType("character varying(2048)");

            b.Property<int>("Type")
                      .HasColumnType("integer");

            b.Property<int>("UserId")
                      .HasColumnType("integer");

            b.HasKey("Id");

            b.HasIndex("UserId");

            b.ToTable("Storages", (string)null);
          });

      modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("ClaimType")
                      .HasMaxLength(16)
                      .HasColumnType("character varying(16)");

            b.Property<string>("ClaimValue")
                      .HasMaxLength(256)
                      .HasColumnType("character varying(256)");

            b.Property<int>("UserId")
                      .HasColumnType("integer");

            b.HasKey("Id");

            b.HasIndex("UserId");

            b.ToTable("UserClaim", (string)null);
          });

      modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
          {
            b.Property<string>("LoginProvider")
                      .HasColumnType("text");

            b.Property<string>("ProviderKey")
                      .HasColumnType("text");

            b.Property<string>("ProviderDisplayName")
                      .HasMaxLength(128)
                      .HasColumnType("character varying(128)");

            b.Property<int>("UserId")
                      .HasColumnType("integer");

            b.HasKey("LoginProvider", "ProviderKey");

            b.HasIndex("UserId");

            b.ToTable("UserLogin", (string)null);
          });

      modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
          {
            b.Property<int>("UserId")
                      .HasColumnType("integer");

            b.Property<string>("LoginProvider")
                      .HasColumnType("text");

            b.Property<string>("Name")
                      .HasColumnType("text");

            b.Property<string>("Value")
                      .HasMaxLength(1024)
                      .HasColumnType("character varying(1024)");

            b.HasKey("UserId", "LoginProvider", "Name");

            b.ToTable("UserToken", (string)null);
          });

      modelBuilder.Entity("SpotLights.Newsletters.Newsletter", b =>
          {
            b.HasOne("SpotLights.Shared.Post", "Post")
                      .WithMany()
                      .HasForeignKey("PostId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Post");
          });

      modelBuilder.Entity("SpotLights.Shared.Post", b =>
          {
            b.HasOne("SpotLights.Identity.UserInfo", "User")
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("User");
          });

      modelBuilder.Entity("SpotLights.Shared.PostCategory", b =>
          {
            b.HasOne("SpotLights.Shared.Category", "Category")
                      .WithMany("PostCategories")
                      .HasForeignKey("CategoryId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("SpotLights.Shared.Post", "Post")
                      .WithMany("PostCategories")
                      .HasForeignKey("PostId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Category");

            b.Navigation("Post");
          });

      modelBuilder.Entity("SpotLights.Storages.Storage", b =>
          {
            b.HasOne("SpotLights.Identity.UserInfo", "User")
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("User");
          });

      modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
          {
            b.HasOne("SpotLights.Identity.UserInfo", null)
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();
          });

      modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
          {
            b.HasOne("SpotLights.Identity.UserInfo", null)
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();
          });

      modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
          {
            b.HasOne("SpotLights.Identity.UserInfo", null)
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();
          });

      modelBuilder.Entity("SpotLights.Shared.Category", b =>
          {
            b.Navigation("PostCategories");
          });

      modelBuilder.Entity("SpotLights.Shared.Post", b =>
          {
            b.Navigation("PostCategories");
          });
#pragma warning restore 612, 618
    }
  }
}
