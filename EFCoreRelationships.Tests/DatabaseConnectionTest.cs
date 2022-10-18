using EFCoreRelationships.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationships.Tests
{
    public class DatabaseConnectionTest
    {
        private readonly WebApplicationFactory<Program> _app;

        public DatabaseConnectionTest()
        {
            _app = new WebApplicationFactory<Program>();

        }

        [Fact]
        public void TestDBConnection()
        {
            using (var scope = _app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();

                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;

                var context = new DataContext(options);

                var isCreated = context.Database.EnsureCreated();

                var isConnectionSuccessful = context.Database.CanConnect();

                Assert.True(isConnectionSuccessful);

                Assert.True(isCreated);
            }

        }
    }
}
