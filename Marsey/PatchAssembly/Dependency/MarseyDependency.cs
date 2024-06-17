using System.Reflection;

namespace Marsey.PatchAssembly.Dependency;

public class MarseyDependency
{
    public string asmname { get; private set; }
    public string? path { get; set; } // Known only the present dependencies
    public bool? missing { get; set; } // Known only to requested dependencies
    public Version? version { get; private set; }
    public bool supercede { get; private set; }

    // Created by patch dll's MarseyDependency when looking for dependencies
    public MarseyDependency(string name, Version? ver, bool super = false)
    {
        asmname = name;
        version = ver;
        supercede = super;
    }

    // Created by dlls we find in the Dependencies folder
    public MarseyDependency(Assembly asm, bool super = false)
    {
        asmname = asm.GetName().Name ?? "dependency";
        path = asm.Location;
        version = asm.GetName().Version;
        supercede = super;
    }
}
