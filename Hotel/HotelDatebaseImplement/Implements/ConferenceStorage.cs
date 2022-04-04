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
    public class ConferenceStorage : IConferenceStorage
    {
        public List<ConferenceViewModel> GetFullList()
        {
            using var context = new HotelDatabase();
            return context.Conferences
                .Include(rec => rec.ConferenceSeminars)
                .ThenInclude(rec => rec.Seminar)
                .Include(rec => rec.ConferenceRooms)
                .ThenInclude(rec => rec.Room)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<ConferenceViewModel> GetFilteredList(ConferenceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            return context.Conferences
                .Include(rec => rec.ConferenceSeminars)
                .ThenInclude(rec => rec.Seminar)
                .Include(rec => rec.ConferenceRooms)
                .ThenInclude(rec => rec.Room)
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public ConferenceViewModel GetElement(ConferenceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            var conference = context.Conferences
                .Include(rec => rec.ConferenceSeminars)
                .ThenInclude(rec => rec.Seminar)
                .Include(rec => rec.ConferenceRooms)
                .ThenInclude(rec => rec.Room)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return conference != null ? CreateModel(conference) : null;
        }

        public void Insert(ConferenceBindingModel model)
        {
            using var context = new HotelDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Conference conference = new Conference
                {
                    Name = model.Name,
                    DataOf = model.DataOf,
                    NumberOfRooms = model.NumberOfRooms,
                    OrganizerId = model.OrganizerId
                };
                context.Conferences.Add(conference);
                context.SaveChanges();
                CreateModel(model, conference, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ConferenceBindingModel model)
        {
            using var context = new HotelDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var conference = context.Conferences.FirstOrDefault(rec => rec.Id == model.Id);
                if (conference == null)
                {
                    throw new Exception("Конференция не найдена");
                }
                CreateModel(model, conference, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(ConferenceBindingModel model)
        {
            using var context = new HotelDatabase();
            Conference conference = context.Conferences.FirstOrDefault(rec => rec.Id == model.Id);
            if (conference != null)
            {
                context.Conferences.Remove(conference);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Конференция не найдена");
            }
        }

        private Conference CreateModel(ConferenceBindingModel model, Conference conference, HotelDatabase context)
        {
            conference.Name = model.Name;
            conference.DataOf = model.DataOf;
            conference.NumberOfRooms = model.NumberOfRooms;
            conference.OrganizerId = model.OrganizerId;

            if (model.Id.HasValue)
            {
                var conferenceRooms = context.ConferenceRooms.Where(rec => rec.ConferenceId == model.Id.Value).ToList();
                // удаляем те, которых нет в модели
                context.ConferenceRooms.RemoveRange(conferenceRooms.Where(rec => !model.ConferenceRooms.ContainsKey(rec.RoomId)).ToList());
                context.SaveChanges();
                // убираем повторы
                foreach (var conferenceRoom in conferenceRooms)
                {
                    if (model.ConferenceRooms.ContainsKey(conferenceRoom.RoomId))
                    {
                        model.ConferenceRooms.Remove(conferenceRoom.RoomId);
                    }
                }
                context.SaveChanges();
            }
            // добавляем новые
            foreach (var cr in model.ConferenceRooms)
            {
                context.ConferenceRooms.Add(new ConferenceRoom
                {
                    ConferenceId = conference.Id,
                    RoomId = cr.Key
                });
                context.SaveChanges();
            } 
            return conference;
        }

        private static ConferenceViewModel CreateModel(Conference conference)
        {
            return new ConferenceViewModel
            {
                Id = conference.Id,
                Name = conference.Name,
                DataOf = conference.DataOf,
                NumberOfRooms = conference.NumberOfRooms,
                OrganizerId = conference.OrganizerId,
                ConferenceRooms = conference.ConferenceRooms
                .ToDictionary(recCR => recCR.RoomId,
                recCR => (recCR.Room?.Number))
            };
        }
    }
}
