﻿using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;
using System;
using System.Collections.Generic;

namespace ProEventos.Domain.Identity
{
  public class User : IdentityUser<Guid>
  {
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public Titulo Titulo { get; set; }
    public string Descricao { get; set; }
    public Funcao Funcao { get; set; }
    public string ImagemURL { get; set; }
    public IEnumerable<UserRole> UserRoles { get; set; }
  }
}