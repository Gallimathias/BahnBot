using BahnBot.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BahnBot.Core.Model
{
    public abstract class BahnBotDatabase : DbContext
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public BahnBotDatabase() : base()
        {
        }
        public BahnBotDatabase(DbContextOptions options) : base(options)
        {
        }

        public abstract BahnBotDatabase GetEnsureDatabase(string source);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
