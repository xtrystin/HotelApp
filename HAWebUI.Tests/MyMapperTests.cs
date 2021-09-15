using HaWebUI.Library.Models;
using HAWebUI.Helpers;
using HAWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HAWebUI.Tests
{
    public class MyMapperTests
    {
        [Fact]
        public void MapApiRoomsModelToDisplayModel_ShouldWorkForList()
        {
            // Arrange
            List<RoomModel> expected = new List<RoomModel>
            {
                new RoomModel
                {
                    Id = 1,
                    Name = "FirstRoom",
                    Status = "empty",
                    NormalPrice = 200,
                    StudentPrice = 120.25M,
                    Capacity = 2
                },
                new RoomModel
                {
                    Id = 2,
                    Name = "SecondRoom",
                    Status = "booked",
                    NormalPrice = 300,
                    StudentPrice = 220.25M,
                    Capacity = 4
                },
                new RoomModel
                {
                    Id = 3,
                    Name = "ThirdRoom",
                    Status = "empty",
                    NormalPrice = 500,
                    StudentPrice = 130.25M,
                    Capacity = 3
                }
            };

            // Act
            var actual = MyMapper.MapApiRoomModelToDisplayModel(expected);

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Count == expected.Count);
            Assert.True(actual[1].Name == expected[1].Name);
        }

        [Fact]
        public void MapApiRoomsModelToDisplayModel_ShouldFailForList()
        {
            // Arrange
            List<RoomModel> expected = null;

            // Act, Assert
            Assert.Throws<Exception>(() => MyMapper.MapApiRoomModelToDisplayModel(expected));
        }


        [Fact]
        public void MapApiRoomsModelToDisplayModel_ShouldWorkForOneRoom()
        {
            // Arrange
            RoomModel expected = new RoomModel
            {
                Id = 1,
                Name = "FirstRoom",
                Status = "empty",
                NormalPrice = 200,
                StudentPrice = 120.25M,
                Capacity = 2
            };

            // Act
            var actual = MyMapper.MapApiRoomModelToDisplayModel(expected);

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Name == "FirstRoom");
        }

        [Fact]
        public void MapApiRoomsModelToDisplayModel_ShouldFailForOneRoom()
        {
            // Arrange
            RoomModel expected = null;

            // Act, Assert
            Assert.Throws<Exception>(() => MyMapper.MapApiRoomModelToDisplayModel(expected));
        }


        [Fact]
        public void MapDisplayModelToApiRoomModel_ShouldWorkForOneRoom()
        {
            // Arrange
            RoomDisplayModel expected = new RoomDisplayModel
            {
                Id = 1,
                Name = "FirstRoom",
                Status = "empty",
                NormalPrice = 200,
                StudentPrice = 120.25M,
                Capacity = 2
            };

            // Act
            var actual = MyMapper.MapDisplayModelToApiRoomModel(expected);

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Name == "FirstRoom");
        }

        [Fact]
        public void MapDisplayModelToApiRoomModel_ShouldFailForOneRoom()
        {
            // Arrange
            RoomDisplayModel expected = null;

            // Act, Assert
            Assert.Throws<Exception>(() => MyMapper.MapDisplayModelToApiRoomModel(expected));
        }
    }
}
