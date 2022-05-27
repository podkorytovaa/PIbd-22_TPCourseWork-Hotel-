using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BindingModels;
using HotelDatebaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HotelDatebaseImplement.Implements
{
    public class SeminarStorage : ISeminarStorage
    {
        public List<SeminarViewModel> GetFullList()
        {
            using var context = new HotelDatabase();
            return context.Seminars
                .Include(rec => rec.ConferenceSeminars)
                .ThenInclude(rec => rec.Conference)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<SeminarViewModel> GetFilteredList(SeminarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            return context.Seminars
                .Include(rec => rec.ConferenceSeminars)
                .ThenInclude(rec => rec.Conference)
                .Where(rec => (rec.Name.Contains(model.Name)) || rec.OrganizerId == model.OrganizerId)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public SeminarViewModel GetElement(SeminarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            var seminar = context.Seminars
                .Include(rec => rec.ConferenceSeminars)
                .ThenInclude(rec => rec.Conference)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return seminar != null ? CreateModel(seminar) : null;
        }

        public void Insert(SeminarBindingModel model)
        {
            using var context = new HotelDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Seminar seminar = new Seminar
                {
                    Name = model.Name,
                    SubjectArea = model.SubjectArea,
                    OrganizerId = model.OrganizerId
                };
                context.Seminars.Add(seminar);
                context.SaveChanges();
                CreateModel(model, seminar, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(SeminarBindingModel model)
        {
            using var context = new HotelDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var seminar = context.Seminars.FirstOrDefault(rec => rec.Id == model.Id);
                if (seminar == null)
                {
                    throw new Exception("Семинар не найден");
                }
                CreateModel(model, seminar, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(SeminarBindingModel model)
        {
            using var context = new HotelDatabase();
            Seminar seminar = context.Seminars.FirstOrDefault(rec => rec.Id == model.Id);
            if (seminar != null)
            {
                //отвязывание конференций
                var seminarConference = context.ConferenceSeminars.Where(rec => rec.SeminarId == model.Id.Value).ToList();
                context.ConferenceSeminars.RemoveRange(seminarConference.ToList());
                context.SaveChanges();
                //удаление постояльцев
                var seminarParticipant = context.Participants.Where(rec => rec.SeminarId == model.Id.Value);
                foreach (var sem in seminarParticipant)
                {
                    sem.Seminar = null;
                }
                context.SaveChanges();
                context.Seminars.Remove(seminar);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Семинар не найден");
            }
        }

        private Seminar CreateModel(SeminarBindingModel model, Seminar seminar, HotelDatabase context)
        {
            seminar.Name = model.Name;
            seminar.SubjectArea = model.SubjectArea;
            seminar.OrganizerId = model.OrganizerId;

            if (model.Id.HasValue)
            {
                var semiarConferences = context.ConferenceSeminars.Where(rec => rec.SeminarId == model.Id.Value).ToList();
                context.ConferenceSeminars.RemoveRange(semiarConferences);
                context.SaveChanges();
            }
            // добавляем новые
            foreach (var sc in model.SeminarConferences)
            {
                context.ConferenceSeminars.Add(new ConferenceSeminar
                {
                    SeminarId = seminar.Id,
                    ConferenceId = sc.Key
                });
                context.SaveChanges();
            }
            return seminar;
        }

        private static SeminarViewModel CreateModel(Seminar seminar)
        {
            return new SeminarViewModel
            {
                Id = seminar.Id,
                Name = seminar.Name,
                SubjectArea = seminar.SubjectArea,
                OrganizerId = seminar.OrganizerId,
                SeminarConferences = seminar.ConferenceSeminars
                    .ToDictionary(recSC => recSC.ConferenceId, recSC => (recSC.Conference?.Name))
            };
        }
    }
}
