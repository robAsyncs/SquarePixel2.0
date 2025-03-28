using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SquarePixel.Services;
using Stream = System.IO.Stream;

namespace SquarePixel.Interface;

public interface IInferenceService
{
    Task<InferenceService.ModelPrediction> PredictImageTag(MemoryStream image, CancellationToken ct);
    Task<string> GenerateImageCaption(CancellationToken ct);

}