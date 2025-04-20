using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SquarePixel.Interface;
using SquarePixel.Models;
using SquarePixel.Models.AI;
using SquarePixel.Util;

namespace SquarePixel.Services;

public class InferenceService(SettingService<SquareSetting> settingService) : IInferenceService
{
    private SettingService<SquareSetting> _settingService = settingService ?? throw new ArgumentNullException(nameof(settingService));
    
    //todo: save inference server address in setting and have const endpoints for each below
    
    public async Task<InferenceResponse?> GenerateImageCaption(MemoryStream image, CancellationToken ct)
    {
        var prediction = string.Empty;
        using var client = new HttpClient();

        return new InferenceResponse
        {
            Caption = Extensions.GenerateRandomString(50)[0]
        };
        
        var settingInfer = "http://127.0.0.1:8000/caption/";

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

    public async Task<string[]> GenerateImageTags(MemoryStream image, string imagePath, CancellationToken ct)
    {
        
        //todo: connect to server and infer add data to below

        var tags = new []{"Car", "Jet", "Boat"};
        await CacheImageTagsAsync(tags, ct);
        return tags;
    }


    private async Task CacheImageTagsAsync(IEnumerable<string> data, CancellationToken ct)
    {
       
        

        //todo: save to blob
    }
    
    
    
    /// <summary>
    /// Streams Conversation with LLM
    /// </summary>
    public async Task ChatBotConversationAsync(string userPrompt, string context)
    {
        
    }
}