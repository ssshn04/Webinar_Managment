using System;
using System.Threading.Tasks;
using WebinarManagement.Data;
using WebinarManagement.Models;
using WebinarManagement.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IRepository<User> _userRepository;
    private IRepository<Participant> _participantRepository;
    private IRepository<Webinar> _webinarRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);
    public IRepository<Participant> ParticipantRepository => _participantRepository ??= new GenericRepository<Participant>(_context);
    public IRepository<Webinar> WebinarRepository => _webinarRepository ??= new GenericRepository<Webinar>(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
