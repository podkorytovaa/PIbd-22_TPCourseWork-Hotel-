using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BindingModels;
using HotelDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDatebaseImplement.Implements
{
    public class RoomStorage : IRoomStorage
    {
        public List<RoomViewModel> GetFullList()
        {
            using var context = new HotelDatabase();

            return context.Rooms
                .Include(rec => rec.RoomLunches)
                .ThenInclude(rec => rec.Lunch)
                .ToList()
                .Select(CreateModel)
                .ToList();

        }

        public List<RoomViewModel> GetFilteredList(RoomBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            return context.Rooms
            .Include(rec => rec.RoomLunches)
            .ThenInclude(rec => rec.Lunch)
            .Where(rec => rec.Number.Contains(model.Number))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public RoomViewModel GetElement(RoomBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();

            var room = context.Rooms
           .Include(rec => rec.RoomLunches)
          .ThenInclude(rec => rec.Lunch)
          .FirstOrDefault(rec => rec.Id == model.Id || rec.Number == model.Number);
            return room != null ? CreateModel(room) : null;

        }

        public void Insert(RoomBindingModel model)
        {
            using var context = new HotelDatabase();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                Room room = new Room
                {
                    Number = model.Number,
                    Level = model.Level,
                    //RoomerId = model.RoomerId,
                    HeadwaiterId = model.HeadwaiterId
                };
                context.Rooms.Add(room);
                context.SaveChanges();
                CreateModel(model, room, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }


        }


        public void Update(RoomBindingModel model)//
        {
            using var context = new HotelDatabase();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var element = context.Rooms.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }


        }

        public void Delete(RoomBindingModel model)//
        {
            using var context = new HotelDatabase();

            Room element = context.Rooms.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Rooms.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }

        }

        private Room CreateModel(RoomBindingModel model, Room room, HotelDatabase context)
        {
            room.Number = model.Number;
            room.Level = model.Level;
            room.HeadwaiterId = model.HeadwaiterId;

            if (model.Id.HasValue)
            {
                var roomLunches = context.RoomLunches.Where(rec =>
               rec.RoomId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.RoomLunches.RemoveRange(roomLunches.ToList());
                context.SaveChanges();
            }
            // добавили новые
            foreach (var rl in model.RoomLunches)
            {
                context.RoomLunches.Add(new RoomLunch
                {
                    RoomId = room.Id,
                    LunchId = rl.Key,
                });
                context.SaveChanges();
            }
            return room;
        }

        private static RoomViewModel CreateModel(Room room)
        {
            return new RoomViewModel
            {
                Id = room.Id,
                Number = room.Number,
                Level = room.Level,
                HeadwaiterId = room.HeadwaiterId,
                //RoomerId = room.RoomerId,
                //RoomRoomers = room.RoomRoomers
                //    .ToDictionary(recRR => recRR.RoomerId, recRR => (recRR.Roomer?.FullName))
                RoomLunches = room.RoomLunches
                    .ToDictionary(recRL => recRL.LunchId, recRL => (recRL.Lunch?.Name))
            };
        }
    }
}
