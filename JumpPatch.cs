using GorillaLocomotion;
using HarmonyLib;

namespace ModMenuPatch.HarmonyPatches
{
  [HarmonyPatch(typeof (Player))]
  [HarmonyPatch]
  internal class JumpPatch
  {
    public static bool ResetSpeed;

    private static void Prefix()
    {
    }
  }
}
