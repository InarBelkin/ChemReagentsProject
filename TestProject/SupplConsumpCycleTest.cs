using System.Collections.Generic;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Tables;
using Moq;
using Xunit;

namespace TestProject
{
    public class SupplConsumpCycleTest
    {
        private Mock<IDbRepos> mock = new Mock<IDbRepos>();
        Mock<IJsonDb> mockJs = new Mock<IJsonDb>();

        public SupplConsumpCycleTest()
        {
        }

        [Fact]
        public void NoCycleTest()   
        {
            var r = new Reagent()
            {
                isWater = true,
                Name = "abirvalg",
                Formula = "C2O5OH",
                IsAccounted = true,
                Location = "ул. какая-то",
                Supplies = new List<Supply>()
            };
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object, mockJs.Object);
            Assert.True(supplConsump.CheckReagent());
        }

        [Fact]
        public void OneCycle()
        {
            var r = new Reagent()
            {
                isWater = true,
                Name = "abirvalg",
                Formula = "C2O5OH",
                IsAccounted = true,
                Location = "ул. какая-то",
                Supplies = new List<Supply>()
                {
                    new Supply()
                    {
                        Count = 0
                    }
                }
            };
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object, mockJs.Object);
            Assert.False(supplConsump.CheckReagent());
        }
        
        [Fact]
        public void ManyCycles()
        {
            var r = new Reagent()
            {
                isWater = true,
                Name = "abirvalg",
                Formula = "C2O5OH",
                IsAccounted = true,
                Location = "ул. какая-то",
                Supplies = new List<Supply>()
                {
                    new Supply()
                    {
                        Count = 100
                    },
                    new Supply()
                    {
                        Count = 0
                    }
                }
            };
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object, mockJs.Object);
            Assert.False(supplConsump.CheckReagent());
        }
    }
}