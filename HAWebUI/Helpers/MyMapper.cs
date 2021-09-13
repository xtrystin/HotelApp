using HaWebUI.Library.Models;
using HAWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Helpers
{
    public static class MyMapper
    {
        //Todo: ? make generic func / use AutoMapper ?
        public static List<RoomDisplayModel> MapRoomModelToDisplayModel(List<RoomModel> rooms)
        {
            List<RoomDisplayModel> output = new List<RoomDisplayModel>();

            foreach (var room in rooms)
            {
                var mappedRoom = new RoomDisplayModel
                {
                    Id = room.Id,
                    Name = room.Name,
                    TypeId = room.RoomType.Id,
                    TypeName = room.RoomType.TypeName,
                    NormalPrice = room.RoomType.NormalPrice,
                    StudentPrice = room.RoomType.StudentPrice,
                    Capacity = room.RoomType.Capacity,
                    Status = room.Status
                };

                output.Add(mappedRoom);
            }

            return output;
        }

        public static RoomDisplayModel MapRoomModelToDisplayModel(RoomModel room)
        {
            var output = new RoomDisplayModel
            {
                Id = room.Id,
                Name = room.Name,
                TypeId = room.RoomType.Id,
                TypeName = room.RoomType.TypeName,
                NormalPrice = room.RoomType.NormalPrice,
                StudentPrice = room.RoomType.StudentPrice,
                Capacity = room.RoomType.Capacity,
                Status = room.Status
            };

            return output;
        }

        public static RoomModel MapDisplayModelToApiModel(RoomDisplayModel room)
        {
            var output = new RoomModel()
            {
                Id = room.Id,
                Name = room.Name,
                Status = room.Status,
                RoomType = new RoomType()
                {
                    //Id = room.TypeId,
                    TypeName = room.TypeName,
                    NormalPrice = room.NormalPrice,
                    StudentPrice = room.StudentPrice,
                    Capacity = room.Capacity
                }
            };

            return output;
        }
    }
}
