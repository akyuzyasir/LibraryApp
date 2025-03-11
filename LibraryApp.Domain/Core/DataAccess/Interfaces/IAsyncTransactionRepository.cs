﻿using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryApp.Domain.Core.DataAccess.Interfaces;

public interface IAsyncTransactionRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<IExecutionStrategy> CreateExecutionStrategy();
}
