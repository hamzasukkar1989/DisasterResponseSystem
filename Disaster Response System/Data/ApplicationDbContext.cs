using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DisasterResponseSystem.Models;

namespace DisasterResponseSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DonorEntities> DonorEntities { set; get; }
        public DbSet<FinancialAids> FinancialAids { get; set; }
        public DbSet<TotalFinancialAids> TotalFinancialAids { get; set; }
        public DbSet<inkindAids> inkindAids { get; set; }
        public DbSet<InkindTypes> InkindTypes { get; set; }
        public DbSet<AffectedIndividualsRequests> AffectedIndividualsRequests { get; set; }
        public DbSet<ProvidingAids> ProvidingAids { get; set; }
        public DbSet<AffectedIndividualsInkind> AffectedIndividualsInkinds { get; set; }
    }
}
