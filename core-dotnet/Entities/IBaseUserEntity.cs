﻿namespace core_dotnet.Entities;

public interface IBaseUserEntity
{
    string UserId { get; set; }
    string Name { get; set; }
    string Surname { get; set; }
    string Email { get; set; }
}