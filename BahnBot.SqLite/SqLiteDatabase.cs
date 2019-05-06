using BahnBot.Core;
using BahnBot.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BahnBot.SqLite
{
    public class SqLiteDatabase : BahnBotDatabase
    {
        static SqLiteDatabase() => SQLitePCL.Batteries.Init();

        public SqLiteDatabase(DbContextOptions options) : base(options)
        {

        }

        public override BahnBotDatabase GetEnsureDatabase(string source)
        {
            var db = GetDatabase(source);
            db.Database.EnsureCreated();
            return db;
        }

        public static SqLiteDatabase GetDatabase(string source)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite($"Data Source={source}");
            return new SqLiteDatabase(builder.Options);
        }
    }
}
