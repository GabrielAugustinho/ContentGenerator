﻿using ContentGenerator.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Database.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Assunto> Assunto { get; set; }
        public DbSet<ContasEnvio> ContasEnvio { get; set; }
        public DbSet<Destinos> Destinos { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Homenagem> Homenagem { get; set; }
        public DbSet<Humor> Humor { get; set; }
        public DbSet<Publicacao> Publicacao { get; set; }
        public DbSet<TipoAssunto> TipoAssunto { get; set; }
        public DbSet<TipoHomenagem> TipoHomenagem { get; set; }
        public DbSet<TipoValidacao> TipoValidacao { get; set; }
        public DbSet<WhatsApp> WhatsApp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options => options.MigrationsAssembly("ContentGenerator.Api.Database"));
        }
    }
}
