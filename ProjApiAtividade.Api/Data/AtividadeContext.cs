using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjApiAtividade.Api.Models;

    public class AtividadeContext : DbContext
    {
        public AtividadeContext (DbContextOptions<AtividadeContext> options)
            : base(options)
        {
        }

        public DbSet<ProjApiAtividade.Api.Models.Atividade> Atividade { get; set; }
    public DbSet<ProjApiAtividade.Api.Models.Responsavel> Responsaveis { get; set; }
}
