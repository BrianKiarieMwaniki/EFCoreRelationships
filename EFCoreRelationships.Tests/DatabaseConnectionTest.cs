using EFCoreRelationships.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
            _app = new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            
                        });
                    });
        }

        [Fact]
        public void TestDBConnection()
        {
            using (var scope = _app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DbContext>();

                var isCreated = context.Database.EnsureCreated();

                Assert.True(isCreated);
            }
        }
    }
}
