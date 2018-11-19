using System;
using System.Collections.Generic;
using Fusion.Face.WebAdmin.Biz;
using Fusion.Face.WebAdmin.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Fusion.Face.WebAdmin.Models;

namespace Fusion.Face.WebAdmin.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow(3, 3)]
        [DataRow(2, 2)]
        public void TestMethod1(int x, int y)
        {
        }

        [TestMethod]
        public void Test_GroupBiz_Search()
        {
            //Init Data
            List<GroupMaster> groups = new List<GroupMaster>();
            groups.Add(new GroupMaster() { Name = "Group A", Description = "Description A" });
            groups.Add(new GroupMaster() { Name = "Group B", Description = "Description B" });

            //Setup Mock
            Mock<IDataBox<GroupMaster>> mockGroups = new Mock<IDataBox<GroupMaster>>();
            mockGroups.Setup(g => g.Queryable()).Returns(groups.AsQueryable());

            Mock<IDataBoxes> mockDataBoxes = new Mock<IDataBoxes>();
            mockDataBoxes.Setup(d => d.Groups).Returns(mockGroups.Object);

            //Arrange
            GroupBiz biz = new GroupBiz(mockDataBoxes.Object);

            //Act
            SearchResult result = biz.Search(new SearchInfo() { search = new DtSearch() { value = "B" }, start = 0, length = 10 });

            //Assert
            Assert.AreEqual(1, result.data.Length);
            GroupInfo[] groupInfos = result.data as GroupInfo[];

            Assert.AreEqual(groups.First().Name, groupInfos.First().Name);

            mockGroups.VerifyAll();
            mockDataBoxes.VerifyAll();
        }
    }
}
