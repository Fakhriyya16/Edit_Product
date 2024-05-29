using Fiorello_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Social> Socials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<SliderInfo>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Expert>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Social>().HasQueryFilter(m => !m.SoftDeleted);

            modelBuilder.Entity<Social>().HasData(
            new Social
            {
                Id = 1,
                Name = "Twitter",
                Link = "https://x.com/home?lang=ru",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            },
            new Social
            {
                Id = 2,
                Name = "Instagram",
                Link = "https://x.com/home?lang=ru",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            },
            new Social
            {
                Id = 3,
                Name = "Tumblr",
                Link = "https://x.com/home?lang=ru",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            },
            new Social
            {
                Id = 4,
                Name = "Pinterest",
                Link = "https://x.com/home?lang=ru",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            }
            );


            modelBuilder.Entity<Setting>().HasData(
            new Setting
            {
                Id = 1,
                Key = "HeaderLogo",
                Value = "logo.png",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            },
            new Setting
            {
                Id = 2,
                Key = "Phone",
                Value = "23145",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            },
            new Setting
            {
                Id = 3,
                Key = "Address",
                Value = "28 May",
                SoftDeleted = false,
                CreatedDate = DateTime.Now
            }
        );

            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    Id = 1,
                    Title = "Flower Power",
                    Description = "Class aptent taciti sociosqu ad litora torquent per conubia\r\n                                    nostra, per",
                    Image = "blog-feature-img-1.jpg",
                    CreatedDate = DateTime.Now
                },
                new Blog
                {
                    Id = 2,
                    Title = "Local Florists",
                    Description = "Class aptent taciti sociosqu ad litora torquent per conubia\r\n                                    nostra, per",
                    Image = "blog-feature-img-3.jpg",
                    CreatedDate = DateTime.Now
                },
                new Blog
                {
                    Id = 3,
                    Title = "Flower Beauty",
                    Description = "Class aptent taciti sociosqu ad litora torquent per conubia\r\n                                    nostra, per",
                    Image = "blog-feature-img-4.jpg",
                    CreatedDate = DateTime.Now
                }
            );
        }

    }
}
