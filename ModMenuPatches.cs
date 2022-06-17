using HarmonyLib;
using System.Reflection;

namespace ModMenuPatch.HarmonyPatches
{
  public class ModMenuPatches
  {
    private static Harmony instance;
    public const string InstanceId = "com.legoandmars.gorillatag.modmenupatch";

    public static bool IsPatched { get; private set; }

    internal static void ApplyHarmonyPatches()
    {
      if (ModMenuPatches.IsPatched)
        return;
      if (ModMenuPatches.instance == null)
        ModMenuPatches.instance = new Harmony("com.legoandmars.gorillatag.modmenupatch");
      ModMenuPatches.instance.PatchAll(Assembly.GetExecutingAssembly());
      ModMenuPatches.IsPatched = true;
    }

    internal static void RemoveHarmonyPatches()
    {
      if (ModMenuPatches.instance == null || !ModMenuPatches.IsPatched)
        return;
      ModMenuPatches.instance.UnpatchSelf();
      ModMenuPatches.IsPatched = false;
    }
  }
}
