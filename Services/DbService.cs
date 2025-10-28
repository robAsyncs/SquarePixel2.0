using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SquarePixel.Models.Entities;

namespace SquarePixel.Services;

public class DbService(IDbContextFactory<SquareDbContext> dbContextFactory)
{
    private readonly IDbContextFactory<SquareDbContext> _contextFactory = dbContextFactory ?? throw new ArgumentNullException();

    public async Task RetrieveCaptionsAsync()
    {
        
    }
    
    public async Task SaveCaptionAsync(Guid id, string caption, CancellationToken ct)
    {
        
    }

    public async Task<IEnumerable<Photo>> RetrieveImagesAsync(CancellationToken ct )
    {
        await using var db = await _contextFactory.CreateDbContextAsync(ct);
        return await db.Photos.ToListAsync(ct);
    }

    public async Task SaveUniqueImagesAsync(IEnumerable<Photo> photos, CancellationToken ct)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(ct);

        try
        {
            await db.Photos.AddRangeAsync(photos, ct);
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            //log and throw to global ex handler
        }

    }
}