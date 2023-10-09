using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace CogDockerTest.Controllers;

[Route("[controller]")]
public class CogController(ILogger<CogController> logger)
{
    [HttpGet]
    public async Task<string> Get()
    {
        const string SpeechKey = "a9158ea12cf449e39ee2c9a1d2746311";
        const string SpeechRegion = "uksouth";
        const string Voice = "en-US-JennyNeural";
        const string Text = "Hey there how are you?";

        var speechConfig = SpeechConfig.FromSubscription(SpeechKey, SpeechRegion);

        speechConfig.SpeechSynthesisVoiceName = Voice;

        using var stream = AudioOutputStream.CreatePullStream();
        using var audioConfig = AudioConfig.FromStreamOutput(stream);
        logger.LogInformation("Text={Text}", Text);
        using var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig);

        var response = await speechSynthesizer.SpeakTextAsync(Text);
        logger.LogInformation("Duration={AudioDuration} ResultId={ResultId}", response.AudioDuration,
            response.ResultId);

        return "response";
    }
}