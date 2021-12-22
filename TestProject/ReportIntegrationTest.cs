using System;
using BLL.Models.OtherModels;
using BLL.Services.BigServices;
using DAL.Interfaces;
using DAL.Tables;
using Moq;
using Xunit;

namespace TestProject
{
    public class ReportIntegrationTest
    {
        private readonly Mock<IDbRepos> mockRepos = new Mock<IDbRepos>();
        private readonly Mock<IRepository<Supply>> mockSupply = new Mock<IRepository<Supply>>();
        private readonly ReportService reportServ;


        public ReportIntegrationTest()
        {
            mockRepos.Setup(repos => repos.Supplies).Returns(mockSupply.Object);
            reportServ = new ReportService(mockRepos.Object);
        }

        [Fact]
        public void Enable()
        {
            var m = new MonthReportLineM()
            {
                SupplyId = 1,
                AcceptWriteOff = true,
            };
            var r = new ReportM();
            var supply = new Supply();
            mockSupply.Setup(repository => repository.GetItem(1)).Returns(supply);
            reportServ.AcceptWriteOff(m, r);

            Assert.True(supply.Active == false && supply.Date_UnWrite == r.RealDate && supply.ReportId == r.Id);
        }

        [Fact]
        public void Disable()
        {
            var m = new MonthReportLineM()
            {
                SupplyId = 1,
                AcceptWriteOff = false,
            };
            var r = new ReportM();
            var supply = new Supply();
            mockSupply.Setup(repository => repository.GetItem(1)).Returns(supply);

            reportServ.AcceptWriteOff(m, r);
            Assert.True(supply.Active == true && supply.Date_UnWrite == new DateTime(2100, 1, 1) &&
                        supply.ReportId == null);
        }

        [Fact]
        public void Called()
        {
            var m = new MonthReportLineM()
            {
                SupplyId = 1,
                AcceptWriteOff = false,
            };
            var r = new ReportM();
            var supply = new Supply();
            mockSupply.Setup(repository => repository.GetItem(1)).Returns(supply);
            reportServ.AcceptWriteOff(m, r);
            mockSupply.Verify(s => s.GetItem(1));
            mockSupply.Verify(s => s.Update(It.IsAny<Supply>()));
        }


    }
}