using Livit.Common.Models;
using Livit.Common.Repository;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Livit.Common.Tests.Repository
{
    public class DbFixture : IDisposable
    {
        public DbFixture()
        {
            var dbFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            Db = dbFactory.Open();
            Db.CreateTableIfNotExists<LeaveRequest>();
        }

        public IDbConnection Db { get; set; }

        public void Dispose()
        {
            Db.DropTable<LeaveRequest>();
            Db.Dispose();
        }
    }


    public class LeaveRequestRepositoryTests : IClassFixture<DbFixture>
    {
        readonly DbFixture _fixture;
        public LeaveRequestRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void WithNoItems_GetAllCountShouldReturnZero()
        {
            var repository = new LeaveRequestRepository(_fixture.Db);
            var count = repository.GetAll().Count();

            Assert.Equal(0, count);
        }

        [Fact]
        public void AddingOneItem_GetAllShouldReturnOneItem()
        {
            var request = new LeaveRequest
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "johndoe@gmail.com",
                            StartDate = DateTime.Today,
                            EndDate = DateTime.Today.AddDays(3),
                            DateTimeRequested = DateTime.Now
                        };
            _fixture.Db.Insert(request);

            var repository = new LeaveRequestRepository(_fixture.Db);
            var results = repository.GetAll();

            Assert.Equal(1, results.Count());
            
        }

        [Fact]
        public void AddingAnotherItem_Add_ShouldInsertNewRow()
        {
            var request = new LeaveRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@gmail.com",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(5),
                DateTimeRequested = DateTime.Now
            };
            
            var repository = new LeaveRequestRepository(_fixture.Db);
            var initialCount = repository.GetAll().Count();

            repository.Add(request);

            Assert.Equal(initialCount + 1, repository.GetAll().Count());


        }

        [Fact]
        public void Always_GetAllShouldReturnDbSelectOrderedByDescendingRequestDate()
        {
           
            var repository = new LeaveRequestRepository(_fixture.Db);
            Assert.Equal(_fixture.Db.Select<LeaveRequest>().Count, repository.GetAll().Count());
            
        }



    }
}
