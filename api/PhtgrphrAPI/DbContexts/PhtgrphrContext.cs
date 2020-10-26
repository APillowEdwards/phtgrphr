using Microsoft.EntityFrameworkCore;
using PhtgrphrAPI.Models;
using System;
using System.Reflection;

namespace PhtgrphrAPI.DbContexts
{
    public class PhtgrphrContext : DbContext
    {
        public PhtgrphrContext(DbContextOptions<PhtgrphrContext> options) : base(options)
        {
        }

        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryAccessToken> GalleryAccessTokens { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccessToken> UserAccessTokens { get; set; }
    }
}