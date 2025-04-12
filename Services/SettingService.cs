using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akavache;

namespace SquarePixel.Services;

public class SettingService<T> where T : new()
{
    //todo: move to program cs
    const string name = "SquarePixel";
    
    public async Task<T?> GetSettingAsync(CancellationToken ct)
    {

        T? setting;
        
        try
        {
            setting = await BlobCache.UserAccount.GetObject<T>(name);
        }
        catch (KeyNotFoundException)
        {
            setting = new T();
            await SaveSettingAsync(setting, ct);
        }
        
       
        return setting;
    }

    public async Task SaveSettingAsync(T setting, CancellationToken ct)
    {
        await BlobCache.UserAccount.InsertObject(name, setting);
    }
}