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
    private ISettingService<Setting> _settingService;
    
    public InferenceService()
    {
      
    }
    
    public async Task<ModelPrediction?> PredictImageTag(MemoryStream image, CancellationToken ct)
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
        catch (Exception ex)
        {
             //narrow down exception
        }
        
        return JsonSerializer.Deserialize<ModelPrediction>(prediction);
    }

    public class ModelPrediction
    {
        public string? Caption { get; set; }
    }
    
    public async Task<string> GenerateImageCaption(CancellationToken ct)
    {
        return string.Empty;
    }
    
}