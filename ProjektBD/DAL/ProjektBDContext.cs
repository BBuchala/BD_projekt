using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektBD.Model;

namespace ProjektBD.DAL
{
    class ProjektBDContext : DbContext
    {
        public ProjektBDContext() : base("ProjektBD")                    // Connection String. Tak będzie nazywała się utworzona baza
        {
            Database.SetInitializer<ProjektBDContext>(new ProjektBDInitializer());
        }

        public DbSet<Użytkownik>            Użytkownicy         { get; set; }
        public DbSet<Rozmowa>               Rozmowy             { get; set; }
        public DbSet<Wiadomość>             Wiadomości          { get; set; }
        public DbSet<Administrator>         Administratorzy     { get; set; }
        public DbSet<Student>               Studenci            { get; set; }
        public DbSet<Prowadzący>            Prowadzący          { get; set; }
        public DbSet<Zakład>                Zakłady             { get; set; }
        public DbSet<Ocena>                 Oceny               { get; set; }
        public DbSet<Projekt>               Projekty            { get; set; }
        public DbSet<Przedmiot>             Przedmioty          { get; set; }
        public DbSet<PrzedmiotObieralny>    PrzedmiotyObieralne { get; set; }
        public DbSet<Raport>                Raporty             { get; set; }
        public DbSet<Zgłoszenie>            Zgłoszenia          { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Dzięki temu nazwy tabel nie zostaną zmnogowane ("Rozmowas" itp.)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Mapowanie relacji M - N z nadaniem nazwy tabeli pośredniczącej
            modelBuilder.Entity<Rozmowa>()
                .HasMany(t => t.Użytkownicy)
                .WithMany(t => t.Rozmowy)
                .Map(m =>
                {
                    m.ToTable("Prowadzone_rozmowy");
                    m.MapLeftKey("RozmowaID");
                    m.MapRightKey("UżytkownikID");
                });

            modelBuilder.Entity<Przedmiot>()
                .HasMany(t => t.Studenci)
                .WithMany(t => t.Przedmioty)
                .Map(m =>
                {
                    m.ToTable("Przedmioty_studenci");
                    m.MapLeftKey("PrzedmiotID");
                    m.MapRightKey("StudentID");
                });
        }
    }
}
