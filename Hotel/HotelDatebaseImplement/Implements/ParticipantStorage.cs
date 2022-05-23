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
    public class ParticipantStorage : IParticipantStorage
    {
        public List<ParticipantViewModel> GetFullList()
        {
            using var context = new HotelDatabase();
            return context.Participants
                .Include(rec => rec.Seminar)
                .Select(CreateModel)
                .ToList();
        }

        public List<ParticipantViewModel> GetFilteredList(ParticipantBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            return context.Participants
                .Include(rec => rec.Seminar)
                .Where(rec => (rec.Id == model.Id) || rec.OrganizerId == model.OrganizerId)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public ParticipantViewModel GetElement(ParticipantBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new HotelDatabase();
            var participant = context.Participants.Include(rec => rec.Seminar).FirstOrDefault(rec => rec.Id == model.Id);
            return participant != null ? CreateModel(participant) : null;
        }

        public void Insert(ParticipantBindingModel model)
        {
            using var context = new HotelDatabase();
            context.Participants.Add(CreateModel(model, new Participant()));
            context.SaveChanges();
        }

        public void Update(ParticipantBindingModel model)
        {
            using var context = new HotelDatabase();
            var participant = context.Participants.FirstOrDefault(rec => rec.Id == model.Id);
            if (participant == null)
            {
                throw new Exception("Участник не найден");
            }
            CreateModel(model, participant);
            context.SaveChanges();
        }

        public void Delete(ParticipantBindingModel model)
        {
            using var context = new HotelDatabase();
            Participant participant = context.Participants.FirstOrDefault(rec => rec.Id == model.Id);
            if (participant != null)
            {
                context.Participants.Remove(participant);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Участник не найден");
            }
        }

        private Participant CreateModel(ParticipantBindingModel model, Participant participant)
        {
            participant.FullName = model.FullName;
            participant.Status = model.Status;
            participant.SeminarId = model.SeminarId; 
            participant.OrganizerId = model.OrganizerId;
            return participant;
        }

        private static ParticipantViewModel CreateModel(Participant participant)
        {
            return new ParticipantViewModel
            {
                Id = participant.Id,
                FullName = participant.FullName,
                Status = participant.Status,
                SeminarId = participant.SeminarId,
                SeminarName = participant.Seminar.Name,
                OrganizerId = participant.OrganizerId
            };
        }
    }
}
