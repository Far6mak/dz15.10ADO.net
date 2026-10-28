using Microsoft.EntityFrameworkCore;

namespace FitnessClients;

public sealed class FitnessContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        string? connectionString = Environment.GetEnvironmentVariable("FITNESS_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Не задана переменная окружения FITNESS_CONNECTION_STRING. " +
                "Укажите строку подключения к существующей базе PostgreSQL Fitnes. " +
                "Пример: Host=localhost;Port=5432;Database=Fitnes;Username=postgres;Password=ваш_пароль");
        }

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("clients");
        modelBuilder.Entity<Client>().HasKey(client => client.ClientId);
    }
}
