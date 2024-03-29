﻿using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
  public interface IAccountService
  {
    Task<bool> UserExists(string username);
    Task<UserUpdateDto> GetUserByUserNameAsync(string username);
    Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
    Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
    Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
  }
}
