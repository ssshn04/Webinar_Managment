using System;
using System.Threading.Tasks;
using WebinarManagement.Models;
using WebinarManagement.Services;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Participant> ParticipantRepository { get; }
    IRepository<Webinar> WebinarRepository { get; }

    Task SaveAsync();
}
