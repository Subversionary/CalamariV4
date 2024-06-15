using System.Net.Http;
using Serilog;

namespace SS14.Launcher.Models.Data;
/// <summary>
/// Why:
/// https://github.com/space-wizards/SS14.Web/blob/dc3c648b904b39ee545354f655842f972ba620d6/SS14.Auth/Controllers/AuthApiController.cs#L57
/// </summary>
public class Fingerprinting
{
    private string _fingerprint = "";

    public string GetFingerprint()
    {
        return _fingerprint;
    }

    public void SetFingerprint(string fingerprint)
    {
        _fingerprint = fingerprint;
    }
}

// TODO: Persistence
public static class FingerprintUpdater
{
    public static void UpdateFingerprint(Fingerprinting fingerprinting, HttpClient httpClient, string newFingerprint)
    {
        Log.Debug($"Changing fingerprint to {newFingerprint}");
        fingerprinting.SetFingerprint(newFingerprint);
        UpdateHttpClientFingerprint(httpClient, newFingerprint);
    }

    private static void UpdateHttpClientFingerprint(HttpClient httpClient, string fingerprint)
    {
        if (httpClient.DefaultRequestHeaders.Contains("SS14-Launcher-Fingerprint"))
        {
            httpClient.DefaultRequestHeaders.Remove("SS14-Launcher-Fingerprint");
        }
        httpClient.DefaultRequestHeaders.Add("SS14-Launcher-Fingerprint", fingerprint);
    }
}
