using System;
using System.IO;
using System.IO.Pipes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SquarePixel.Interface;
using SquarePixel.Models;

namespace SquarePixel.Services;

public class InferenceService: IInferenceService
{
    private SettingService<Setting> _settingService;
    
    public InferenceService(SettingService<Setting> settingService)
    {
        _settingService = settingService ?? throw new ArgumentNullException(nameof(settingService));
    }
    
    public async Task<InferenceResponse?> GenerateImageCaption(MemoryStream image, CancellationToken ct)
    {
        var prediction = string.Empty;
        using var client = new HttpClient();

        var settingInfer = "http://127.0.0.1:8000/predict/";

        var content = new ByteArrayContent(image.ToArray());
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        
        //todo: handle server issues
        
        try
        {
            var response = await client.PostAsync(settingInfer, content, ct);
            response.EnsureSuccessStatusCode();
            prediction = await response.Content.ReadAsStringAsync(ct);
        }
        catch (Exception )
        {
             //narrow down exception
             return null;
        }
        
        return JsonSerializer.Deserialize<InferenceResponse>(prediction);
    }

}