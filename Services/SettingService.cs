using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akavache;

namespace SquarePixel.Services;

public class SettingService<T> where T : new()
{
    public async Task<T?> GetAsync<T>(CancellationToken ct)
    {

       var setting = await BlobCache.UserAccount.GetObject<T>(BlobCache.ApplicationName)
            .Catch((Exception _) => 
                Observable.Empty<T>())
            .FirstOrDefaultAsync();

        return setting;
    }

    public async Task SaveSettingAsync(T setting) =>
        await BlobCache.UserAccount.InsertObject(BlobCache.ApplicationName, setting);
}