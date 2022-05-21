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
                .Where(rec => rec.OrganizerId == model.OrganizerId)
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
            var seminar = context.Seminars.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return seminar != null ? CreateModel(seminar) : null;
        }

        public void Insert(SeminarBindingModel model)
        {
            using var context = new HotelDatabase();
            context.Seminars.Add(CreateModel(model, new Seminar()));
            context.SaveChanges();
        }

        public void Update(SeminarBindingModel model)
        {
            using var context = new HotelDatabase();
            var seminar = context.Seminars.FirstOrDefault(rec => rec.Id == model.Id);
            if (seminar == null)
            {
                throw new Exception("Семинар не найден");
            }
            CreateModel(model, seminar);
            context.SaveChanges();
        }

        public void Delete(SeminarBindingModel model)
        {
            using var context = new HotelDatabase();
            Seminar seminar = context.Seminars.FirstOrDefault(rec => rec.Id == model.Id);
            if (seminar != null)
            {
                context.Seminars.Remove(seminar);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Семинар не найден");
            }
        }

        private Seminar CreateModel(SeminarBindingModel model, Seminar seminar)
        {
            seminar.Name = model.Name;
            seminar.SubjectArea = model.SubjectArea;
            seminar.OrganizerId = model.OrganizerId;
            return seminar;
        }

        private static SeminarViewModel CreateModel(Seminar seminar)
        {
            return new SeminarViewModel
            {
                Id = seminar.Id,
                Name = seminar.Name,
                SubjectArea = seminar.SubjectArea,
                OrganizerId = seminar.OrganizerId
            };
        }
    }
}
