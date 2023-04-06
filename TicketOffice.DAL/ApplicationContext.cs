using TicketOffice.Common.Models;
using Microsoft.EntityFrameworkCore;
using TicketOffice.Models;

namespace TicketOffice.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    }
}
