using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BindingModels;
using HotelDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelDatebaseImplement.Implements
{
    public class OrganizerStorage : IOrganizerStorage
    {
        public List<OrganizerViewModel> GetFullList()
        {
            using var context = new HotelDatabase();
            return context.Organizers
                .Select(CreateModel)
                .ToList();
        }

        public List<OrganizerViewModel> GetFilteredList(OrganizerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            return context.Organizers
                .Include(rec => rec.Conferences)
                .Include(rec => rec.Seminars)
                .Include(rec => rec.Participants)
                .Where(rec => rec.Login == model.Login && rec.Password == rec.Password)
                .Select(CreateModel)
                .ToList();
        }

        public OrganizerViewModel GetElement(OrganizerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            var organizer = context.Organizers
                .Include(rec => rec.Conferences)
                .Include(rec => rec.Seminars)
                .Include(rec => rec.Participants)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Login == model.Login);
            return organizer != null ? CreateModel(organizer) : null;
        }

        public void Insert(OrganizerBindingModel model)
        {
            using var context = new HotelDatabase();
            context.Organizers.Add(CreateModel(model, new Organizer()));
            context.SaveChanges();
        }

        public void Update(OrganizerBindingModel model)
        {
            using var context = new HotelDatabase();
            var organizer = context.Organizers.FirstOrDefault(rec => rec.Id == model.Id);
            if (organizer == null)
            {
                throw new Exception("Организатор не найден");
            }
            CreateModel(model, organizer);
            context.SaveChanges();
        }

        public void Delete(OrganizerBindingModel model)
        {
            using var context = new HotelDatabase();
            var organizer = context.Organizers.FirstOrDefault(rec => rec.Id == model.Id);
            if (organizer == null)
            {
                context.Organizers.Remove(organizer);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Организатор не найден");
            }
        }

        private Organizer CreateModel(OrganizerBindingModel model, Organizer organizer)
        {
            organizer.FullName = model.FullName;
            organizer.PhoneNumber = model.PhoneNumber;
            organizer.Login = model.Login;
            organizer.Password = model.Password;
            return organizer;
        }

        private static OrganizerViewModel CreateModel(Organizer organizer)
        {
            return new OrganizerViewModel
            {
                Id = organizer.Id,
                FullName = organizer.FullName,
                PhoneNumber = organizer.PhoneNumber,
                Login = organizer.Login,
                Password = organizer.Password
            };
        }
    }
}
