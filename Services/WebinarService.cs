using System.Collections.Generic;
using System.Threading.Tasks;
using WebinarManagement.Models;

namespace WebinarManagement.Services
{
    public class WebinarService
    {
        private readonly IRepository<Webinar> _webinarRepository;
        private readonly IRepository<Participant> _participantRepository;

        public WebinarService(IRepository<Webinar> webinarRepository, IRepository<Participant> participantRepository)
        {
            _webinarRepository = webinarRepository;
            _participantRepository = participantRepository;
        }

        public async Task<List<Webinar>> GetWebinarsAsync()
        {
            return await _webinarRepository.GetAllAsync();
        }

        public async Task<Webinar> GetWebinarByIdAsync(int id)
        {
            return await _webinarRepository.GetByIdAsync(id);
        }

        public async Task AddWebinarAsync(Webinar webinar, int speakerId)
        {
            await _webinarRepository.AddAsync(webinar);
            Participant participant = new Participant
            {
                UserID = speakerId,
                WebinarID = webinar.WebinarID
            };
            await _participantRepository.AddAsync(participant);
        }

        public async Task UpdateWebinarAsync(Webinar webinar)
        {
            await _webinarRepository.UpdateAsync(webinar);
        }

        public async Task DeleteWebinarAsync(int id)
        {
            await _webinarRepository.DeleteAsync(id);
        }
    }
}
