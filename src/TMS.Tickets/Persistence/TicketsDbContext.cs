using Microsoft.EntityFrameworkCore;
using TMS.Tickets.Persistence.Rows;

namespace TMS.Tickets.Persistence
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext(DbContextOptions options) : base(options)
        {
        }

        internal DbSet<TicketRow> Tickets { get; set; }
        internal DbSet<UserRow> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestionBaseConfiguration).Assembly); // todo zrobić konfigurację
            base.OnModelCreating(modelBuilder);
        }
    }
}