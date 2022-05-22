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
    public class LunchStorage : ILunchStorage
    {
        public List<LunchViewModel> GetFullList()//
        {
            using var context = new HotelDatabase();

            return context.Lunches
                .Include(rec => rec.LunchSeminars)
                .ThenInclude(rec => rec.Seminar)
                .ToList()
               .Select(CreateModel)
                .ToList();

        }

        public List<LunchViewModel> GetFilteredList(LunchBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            return context.Lunches
            .Include(rec => rec.LunchSeminars)
            .ThenInclude(rec => rec.Seminar)
            .Where(rec => (rec.HeadwaiterId == model.HeadwaiterId))
            .ToList().Select(CreateModel)
           .ToList();

        }

        public LunchViewModel GetElement(LunchBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            var lunch = context.Lunches
              .Include(rec => rec.LunchSeminars)
              .ThenInclude(rec => rec.Seminar)
              .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return lunch != null ? CreateModel(lunch) : null;

        }

        public void Insert(LunchBindingModel model)//
        {
            using var context = new HotelDatabase();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                Lunch lunch = new Lunch
                {
                    Name = model.Name,
                    Dish = model.Dish,
                    Drink = model.Drink,
                    HeadwaiterId = model.HeadwaiterId
                };
                context.Lunches.Add(lunch);
                context.SaveChanges();
                CreateModel(model, lunch, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }


        }

        public void Update(LunchBindingModel model)//
        {
            using var context = new HotelDatabase();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var element = context.Lunches.FirstOrDefault(rec => rec.Id ==
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

        public void Delete(LunchBindingModel model)//
        {
            using var context = new HotelDatabase();

            Lunch element = context.Lunches.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Lunches.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }

        }

        private Lunch CreateModel(LunchBindingModel model, Lunch lunch, HotelDatabase context)//
        {
            lunch.Name = model.Name;
            lunch.Dish = model.Dish;
            lunch.Drink = model.Drink;
            lunch.HeadwaiterId = model.HeadwaiterId;

            if (model.LunchSeminars != null)
            {
                if (model.Id.HasValue)
                {
                    var lunchSeminars = context.LunchSeminars.Where(rec =>
                   rec.LunchId == model.Id.Value).ToList();
                    //удалили все
                    context.LunchSeminars.RemoveRange(lunchSeminars);
                    context.SaveChanges();
                }
                // добавили новые
                foreach (var ls in model.LunchSeminars)
                {
                    context.LunchSeminars.Add(new LunchSeminar
                    {
                        LunchId = lunch.Id,
                        SeminarId = ls.Key,
                    });
                    context.SaveChanges();
                }
            }
            return lunch;
        }

        private static LunchViewModel CreateModel(Lunch lunch)
        {
            return new LunchViewModel
            {
                Id = lunch.Id,
                Name = lunch.Name,
                Dish = lunch.Dish,
                Drink = lunch.Drink,
                HeadwaiterId = lunch.HeadwaiterId,
                LunchSeminars = lunch.LunchSeminars
                .ToDictionary(recLS => recLS.SeminarId,
                recLS => (recLS.Seminar?.Name))
            };
        }
    }
}
