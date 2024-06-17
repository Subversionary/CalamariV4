using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Marsey.Config;
using Marsey.Misc;

namespace Marsey.PatchAssembly.Dependency;

/// <summary>
/// Manages 3rd party dependencies
/// </summary>
/// <note>
/// This is done lunatically
/// </note>
public static class DependencyManager
{
    private static List<MarseyDependency> _dependencies = new List<MarseyDependency>();

    public static void RecheckDependencies()
    {
        _dependencies.Clear();
        FillDependencies();
    }

    private static void FillDependencies()
    {
        List<string> e = FileHandler.GetAssemblyPaths([MarseyVars.MarseyDependencyFolder]);

        foreach (string deppath in e)
        {
            Assembly foo = FileHandler.CreateAssembly(deppath);
            MarseyDependency dep = new MarseyDependency(foo);
            _dependencies.Add(dep);
        }

        MarseyLogger.Log(MarseyLogger.LogType.TRCE, $"Initialized {_dependencies.Count} dependencies.");
    }

    /// <summary>
    /// Check if we have the dependency
    /// </summary>
    /// <param name="dependency">Dependency were checking against</param>
    /// <returns>true if found</returns>
    /// <note>
    /// This also sets the path in the requested dependency if found
    /// </note>
    private static bool MatchDependency(MarseyDependency dependency)
    {
        foreach (MarseyDependency d in _dependencies)
        {
            if (d.asmname == dependency.asmname && (dependency.version == null || d.version == dependency.version))
            {
                dependency.path = d.path;
                return true;
            }
        }

        MarseyLogger.Log(MarseyLogger.LogType.DEBG, $"Missing dependency: {dependency.asmname}, version {dependency.version}");
        return false;
    }

    /// <summary>
    /// Check if dll has its dependencies satisfied
    /// </summary>
    /// <param name="required">Required dependencies</param>
    /// <param name="missing">Missing dependencies assemblynames</param>
    /// <returns></returns>
    public static bool SatisfiedDeps(HashSet<MarseyDependency> required, [NotNullWhen(false)] out HashSet<MarseyDependency>? missing)
    {
        missing = required;

        foreach (MarseyDependency dep in required)
        {
            if (MatchDependency(dep))
                missing.Remove(dep);
        }

        if (missing.Count != 0)
        {
            foreach (MarseyDependency dep in missing)
            {
                dep.missing = true;
            }
            return false;
        }

        missing = null;
        return true;
    }

}
