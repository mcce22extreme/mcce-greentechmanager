using AutoMapper;
using FakeItEasy;
using GreenTechManager.Core.Entities;
using GreenTechManager.Operators;
using GreenTechManager.WindParks.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GreenTechManagaer.Operators.Tests
{
    public abstract class TestBase
    {
        protected static IMapper Mapper { get; }

        protected static string UserName { get; } = Make.String();

        static TestBase()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Operator).Assembly);
                cfg.AddMaps(typeof(AuditEntry).Assembly);
                cfg.AllowNullCollections = true;
            });

            Mapper = config.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            var dbContext = CreateDbContext();

            // Reset in memory databsae
            dbContext.Database.EnsureDeleted();
        }

        protected AppDbContext CreateDbContext()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder();
            dbOptionsBuilder.UseInMemoryDatabase("testdb");

            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            A.CallTo(() => httpContextAccessor.HttpContext.User.Identity.Name).Returns(UserName);

            var dbContext = new AppDbContext(dbOptionsBuilder.Options, httpContextAccessor);

            return dbContext;
        }
    }
}
