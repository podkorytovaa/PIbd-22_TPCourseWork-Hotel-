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
    public class RoomerStorage : IRoomerStorage
    {
        public List<RoomerViewModel> GetFullList()//
        {
            using var context = new HotelDatabase();
            return context.Roomers
            .Include(rec => rec.Room)
            .Include(rec => rec.Headwaiter)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<RoomerViewModel> GetFilteredList(RoomerBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            return context.Roomers
            .Include(rec => rec.Room)
            .Include(rec => rec.Headwaiter)
            .Where(rec => rec.Id == model.Id)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public RoomerViewModel GetElement(RoomerBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            var roomer = context.Roomers
            .Include(rec => rec.Room)
            .Include(rec => rec.Headwaiter)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return roomer != null ? CreateModel(roomer) : null;
        }

        public void Insert(RoomerBindingModel model)//
        {
            using var context = new HotelDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Roomers.Add(CreateModel(model, new Roomer()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(RoomerBindingModel model)//
        {
            using var context = new HotelDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Roomers.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(RoomerBindingModel model)//
        {
            using var context = new HotelDatabase();
            Roomer element = context.Roomers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Roomers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }


        private Roomer CreateModel(RoomerBindingModel model, Roomer roomer)
        {

            roomer.FullName = model.FullName;
            roomer.PhoneNumber = model.PhoneNumber;
            roomer.DateBooking = model.DateBooking;
            roomer.HeadwaiterId = model.HeadwaiterId;
            roomer.RoomId = model.RoomId; //
            return roomer;
        }

        private static RoomerViewModel CreateModel(Roomer roomer)
        {
            return new RoomerViewModel
            {
                Id = roomer.Id,
                FullName = roomer.FullName,
                PhoneNumber = roomer.PhoneNumber,
                DateBooking = roomer.DateBooking,
                HeadwaiterId = roomer.HeadwaiterId,
                RoomId = roomer.RoomId //
            };
        }
    }
}
