using WebinarManagement.Models;
using Microsoft.EntityFrameworkCore;
using WebinarManagement.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace WebinarManagement.Services
{
    public class UserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users.FirstOrDefault(x => x.UserName == userName);
        }

        public async Task<List<Webinar>> GetUserWebinarsAsync(int userId)
        {
            var participants = await _unitOfWork.ParticipantRepository.GetAllAsync();
            var userParticipants = participants.Where(p => p.UserID == userId).ToList();

            var webinars = new List<Webinar>();
            foreach (var participant in userParticipants)
            {
                var webinar = await _unitOfWork.WebinarRepository.GetByIdAsync(participant.WebinarID);
                if (webinar != null)
                {
                    webinars.Add(webinar);
                }
            }

            return webinars;
        }

        public async Task<List<Webinar>> GetSpeakerWebinarAsync(int speakerId)
        {
            var webinars = await _unitOfWork.WebinarRepository.GetAllAsync();
            return webinars.Where(w => w.SpeakerID == speakerId).ToList();
        }

        public async Task<bool> IsParticipantAsync(int userId, int webinarId)
        {
            var participants = await _unitOfWork.ParticipantRepository.GetAllAsync();
            return participants.Any(p => p.UserID == userId && p.WebinarID == webinarId);
        }

        public async Task AddParticipantAsync(int userId, int webinarId)
        {
            if (!await IsParticipantAsync(userId, webinarId))
            {
                var participant = new Participant
                {
                    UserID = userId,
                    WebinarID = webinarId
                };

                await _unitOfWork.ParticipantRepository.AddAsync(participant);
            }
        }

        public async Task RemoveParticipantAsync(int userId, int webinarId)
        {
            var participant = await _unitOfWork.ParticipantRepository.FirstOrDefaultAsync(p => p.UserID == userId && p.WebinarID == webinarId);
            if (participant != null)
            {
                await _unitOfWork.ParticipantRepository.DeleteAsync(participant.ParticipantID);
                await _unitOfWork.SaveAsync(); 
            }
        }

    }
}
