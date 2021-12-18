using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Tables;
using Moq;
using Xunit;

namespace TestProject
{
    public class SupplConsumpCrudTests
    {
        private Mock<IDbRepos> mock = new Mock<IDbRepos>();


        [Fact]
        public void CheckIsWater() //1
        {
            var r = new Reagent()
            {
                isWater = false,
                isAlwaysWater = true,
                Name = "abirvalg",
                Formula = "C2O5OH",
                IsAccounted = false,
            };
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object, mockJs.Object);
            Assert.False(supplConsump.CheckReagent());
        }
        
        [Fact]
        public void CheckLength() //2
        {
           
            var r = new Reagent()
            {
                isWater = true,
                isAlwaysWater = true,
                Name = "",
                Formula = "C2O5OH",
                IsAccounted = false,
            };
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object,mockJs.Object);
            Assert.False(supplConsump.CheckReagent());
        }
        
        [Fact]
        public void CheckRusFormula() //3
        {

            var r = new Reagent()
            {
                isWater = false,
                isAlwaysWater = false,
                Name = "abirvalg",
                Formula = "Русский",
                IsAccounted = false,
            };
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object,mockJs.Object);
            
            Assert.False(supplConsump.CheckReagent());
        }
                
        [Fact]
        public void CheckLenghtLocation() //4
        {
          
            var r = new Reagent()
            {
                isWater = true,
                Name = "abirvalg",
                Formula = "C2O5OH",
                IsAccounted = true,
                Location = "1"
            };
            
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object,mockJs.Object);
            Assert.False(supplConsump.CheckReagent());
        }
        
        [Fact]
        public void CheckAccounting() //5
        {
            var r = new Reagent()
            {
                isWater = true,
                Name = "abirvalg",
                Formula = "C2O5OH",
                IsAccounted = false,
            };
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object,mockJs.Object);
            Assert.True(supplConsump.CheckReagent());
        }
        
        [Fact]
        public void CheckInnerSuppliesTrue() //6
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
                        Count = 10
                    }
                }
            };
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object,mockJs.Object);
            Assert.True(supplConsump.CheckReagent());
        }
        
        [Fact]
        public void CheckInnerSuppliesFalse() //7
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
            Mock<IJsonDb> mockJs = new Mock<IJsonDb>();
            mockJs.Setup(foo => foo.GetReagent()).Returns(r);
            var supplConsump = new SupplConsumpCrud(mock.Object,mockJs.Object);
            Assert.False(supplConsump.CheckReagent());
        }

      
    }
}