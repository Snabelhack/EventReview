using EventReview.Shared.Models;
using EventReview.Shared.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Configuration;
using System.Xml;

namespace EventReview.BackendAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IRepository<Event>>(provider => 
            {
                string endpointUri = "https://localhost:8081";
                string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                var client = new CosmosClient(endpointUri, primaryKey);

                var databaseName = "reviewDB";
                var containerName = "reviewContainer";

                var database = client.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();

                var container = database.Database.CreateContainerIfNotExistsAsync(containerName, "/partitionKey").GetAwaiter().GetResult();

                return new CosmosDbRepository<Event>(container.Container);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}