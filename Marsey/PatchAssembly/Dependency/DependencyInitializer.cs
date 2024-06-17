using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HarmonyLib;

namespace Marsey.PatchAssembly.Dependency;

public static class DependencyInitializer
{
    public static void Initialize()
    {
        DependencyManager.RecheckDependencies();
    }

    public static bool TryFetchDependencies(Assembly patch, out HashSet<MarseyDependency> dependencies)
    {
        dependencies = [];
        Type? DepManifest = patch.GetType("MarseyDependencies");
        if (DepManifest == null)
            return false;

        List<(string, Version?, bool)>? deps = AccessTools.Field(DepManifest, "Dependencies").GetValue(DepManifest) as List<(string, Version?, bool)>;

        if (deps == null)
            return false;

        // I aint replacing this var
        foreach (var depdata in deps)
        {
            MarseyDependency dep = new MarseyDependency(depdata.Item1, depdata.Item2, depdata.Item3);
            dependencies.Add(dep);
        }

        return true;
    }
}
