using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;
using System;

namespace ProEventos.Persistence
{
  public class ProEventosContext : IdentityDbContext<User, Role, Guid,
                                                     IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
                                                     IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
  {
    public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Lote> Lotes { get; set; }
    public DbSet<Palestrante> Palestrantes { get; set; }
    public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
    public DbSet<RedeSocial> RedeSociais { get; set; }

    // muitos para muitos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserRole>(userRole =>
      {
        userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

        userRole.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        userRole.HasOne(ur => ur.User)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
      }
      );
      modelBuilder.Entity<PalestranteEvento>()
                .HasKey(palestranteEvento => new { palestranteEvento.EventoId, palestranteEvento.PalestranteId });

      modelBuilder.Entity<Evento>()
              .HasMany(e => e.RedesSociais)
              .WithOne(rs => rs.Evento)
              .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Palestrante>()
              .HasMany(e => e.RedesSociais)
              .WithOne(rs => rs.Palestrante)
              .OnDelete(DeleteBehavior.Cascade);
    }

  }
}