using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SquarePixel.Models;
using SquarePixel.Services;
using Stream = System.IO.Stream;

namespace SquarePixel.Interface;

public interface IInferenceService
{
    Task<InferenceResponse> GenerateImageCaption(MemoryStream image, CancellationToken ct);

}