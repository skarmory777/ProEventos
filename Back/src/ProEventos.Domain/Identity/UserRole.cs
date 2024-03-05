using Microsoft.AspNetCore.Identity;
using System;

namespace ProEventos.Domain.Identity
{
  public class UserRole : IdentityUserRole<Guid>
  {
    public User User { get; set; }
    public Role Role { get; set; }
  }
}