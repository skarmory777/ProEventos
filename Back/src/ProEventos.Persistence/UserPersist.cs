using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
  public class UserPersist : GeralPersist, IUserPersist
  {
    private readonly ProEventosContext _context;
    public UserPersist(ProEventosContext context) : base(context)
    {
      _context = context;
    }

    public async Task<User> GetUserByIdAsync(Guid Id)
    {
      return await _context.Users.FindAsync(Id);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
      return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
      return await _context.Users.ToListAsync();
    }
  }
}
