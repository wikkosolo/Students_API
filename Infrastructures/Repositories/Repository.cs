﻿using Domain;
using Infrastructures.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories;

public class Repository<T>(AppDbContext dbContext)
            : IRepository<T> where T
            : BaseEntity
{
    protected readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
