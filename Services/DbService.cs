using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SquarePixel.Models.Entities;

namespace SquarePixel.Services;

public class DbService
{
    public DbService()
    {
        
    }
    

    public async Task RetrieveCaptionsAsync()
    {
        
    }
    
    public async Task SaveCaptionAsync(Guid id, string caption, CancellationToken ct)
    {
        
    }

    public async Task<IEnumerable<Photo>> RetrieveImagesAsync()
    {
        return new List<Photo>();
    }

    public async Task SaveImageMetaAsync(IEnumerable<Photo> photos, CancellationToken ct)
    {
        
    }
}