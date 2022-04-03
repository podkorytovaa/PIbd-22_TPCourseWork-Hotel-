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
    public class HeadwaiterStorage : IHeadwaiterStorage
    {
        public List<HeadwaiterViewModel> GetFullList()//
        {
            using var context = new HotelDatabase();

            return context.Headwaiters
               .Select(rec => new HeadwaiterViewModel
               {
                   Id = rec.Id,
                   FullName = rec.FullName,
                   PhoneNumber = rec.PhoneNumber,
                   Login = rec.Login,
                   Password = rec.Password
               })
                .ToList();

        }
        
        public List<HeadwaiterViewModel> GetFilteredList(HeadwaiterBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            return context.Headwaiters.Include(x => x.Lunches).Include(x => x.Rooms).Include(x => x.Roomers)
            .Where(rec => rec.Login == model.Login && rec.Password == rec.Password)
            .Select(rec => new HeadwaiterViewModel
            {
                Id = rec.Id,
                FullName = rec.FullName,
                PhoneNumber = rec.PhoneNumber,
                Login = rec.Login,
                Password = rec.Password
            })
            .ToList();
        }


        public HeadwaiterViewModel GetElement(HeadwaiterBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new HotelDatabase();

            var _headwaiter = context.Headwaiters.Include(x => x.Lunches)
                .Include(x => x.Rooms).Include(x => x.Roomers)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Login == model.Login);
            return _headwaiter != null ? new HeadwaiterViewModel
            {
                Id = _headwaiter.Id,
                FullName = _headwaiter.FullName,
                PhoneNumber = _headwaiter.PhoneNumber,
                Login = _headwaiter.Login,
                Password = _headwaiter.Password
            } :
            null;

        }

        public void Insert(HeadwaiterBindingModel model)//
        {
            using var context = new HotelDatabase();

            context.Headwaiters.Add(CreateModel(model, new Headwaiter()));
            context.SaveChanges();

        }

        public void Update(HeadwaiterBindingModel model)//
        {
            using var context = new HotelDatabase();

            var element = context.Headwaiters.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Метрдотель не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();

        }

        public void Delete(HeadwaiterBindingModel model)//
        {
            using var context = new HotelDatabase();

            var element = context.Headwaiters.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Headwaiters.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Метрдотель не найден");
            }

        }

        private Headwaiter CreateModel(HeadwaiterBindingModel model, Headwaiter _headwaiter)//
        {
            _headwaiter.FullName = model.FullName;
            _headwaiter.PhoneNumber = model.PhoneNumber;
            _headwaiter.Login = model.Login;
            _headwaiter.Password = model.Password;
            return _headwaiter;
        }
    }
}
