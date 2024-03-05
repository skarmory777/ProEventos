using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ProEventos.Domain.Identity
{
  public class Role : IdentityRole<Guid>
  {
    public List<UserRole> UserRoles { get; set; }
  }
}