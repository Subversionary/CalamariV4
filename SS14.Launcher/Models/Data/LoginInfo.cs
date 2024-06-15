using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SS14.Launcher.Models.Data;

public class LoginInfo : ReactiveObject
{
    [Reactive]
    public Guid UserId { get; set; }
    [Reactive]
    public string Username { get; set; } = default!;
    [Reactive]
    public LoginToken Token { get; set; }

    public string HWID { get; set; } = ""; // Empty by default

    // We are not subtle whatsoever with empty GUID fingerprints
    public string GUIDFingerprint { get; set; } = "";

    public override string ToString()
    {
        return $"{Username}/{UserId}";
    }
}
