using BepInEx;
using GorillaLocomotion;
using HarmonyLib;

[BepInPlugin("org.ivy.gtag.gripmonke", "GripMonke", "2.1.1")]
public class Plugin : BaseUnityPlugin
{
  private static Harmony harmony;

  [HarmonyPatch(typeof (Player), "GetSlidePercentage")]
  private class slidepatch
  {
  }
}
