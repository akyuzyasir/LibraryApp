﻿namespace LibraryApp.Domain.Core.Entities.Interfaces;

public interface ICreateableEntity : IEntity
{
    string CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }
}
