using HAApi.Library.Models;
using HAApi.Library.Models.EFModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions options) : base(options) { }

        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
