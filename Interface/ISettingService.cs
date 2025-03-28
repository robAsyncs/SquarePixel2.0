using System.Threading;
using System.Threading.Tasks;

namespace SquarePixel.Interface;

public interface ISettingService<T>
{
    public Task<T> GetSettingAsync(CancellationToken ct);
    public Task<T> SaveSettingAsync(CancellationToken ct);
}