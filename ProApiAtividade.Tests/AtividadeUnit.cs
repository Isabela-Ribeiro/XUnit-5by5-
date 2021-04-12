using Microsoft.EntityFrameworkCore;
using ProjApiAtividade.Api.Controllers;
using ProjApiAtividade.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProApiAtividade.Tests
{
    public class AtividadeUnit
    {
        private DbContextOptions<AtividadeContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AtividadeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AtividadeContext(options))
            {
                context.Atividade.Add(new Atividade { Id = 1, Descricao = "Arroz", DataInicio = DateTime.Now, DataFim = DateTime.Now, Responsavel = new Responsavel { Id = 1, Nome = "Isabela" }}); 
                context.Atividade.Add(new Atividade { Id = 2, Descricao = "feijao", DataInicio = DateTime.Now, DataFim = DateTime.Now, Responsavel = new Responsavel { Id = 2, Nome = "Joao" } });
                context.Atividade.Add(new Atividade { Id = 3, Descricao = "Carne", DataInicio = DateTime.Now, DataFim = DateTime.Now, Responsavel = new Responsavel { Id = 3, Nome = "Pedro" } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                IEnumerable<Atividade> atividades = atividadeController.GetAtividade().Result.Value;

                Assert.Equal(3, atividades.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                int atividadeId = 2;
                AtividadeController clientController = new AtividadeController(context);
                Atividade atividade = clientController.GetAtividade(atividadeId).Result.Value;
                Assert.Equal(2, atividade.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Atividade atividade = new Atividade()
            {
                Id = 4,
                Descricao = "Alface",
                DataInicio = DateTime.Now,
                DataFim =DateTime.Now,
                Responsavel = new Responsavel {Id=4,Nome="maria" }
            };

            using (var context = new AtividadeContext(options))
            {
                AtividadeController clientController = new AtividadeController(context);
                Atividade ativ = clientController.PostAtividade(atividade).Result.Value;
                Assert.Equal(5, ativ.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Atividade atividade = new Atividade()
            {
                Id = 3,
                Descricao = "Alface",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now,
                Responsavel = new Responsavel {Id=3,Nome="Beatriz"}
            
            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade ativ = atividadeController.PutAtividade(3, atividade).Result.Value;
                Assert.Equal("Alface", ativ.Descricao);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadeContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade ativ = atividadeController.DeleteAtividade(2).Result.Value;
                Assert.Null(ativ);
            }
        }
    }
}
