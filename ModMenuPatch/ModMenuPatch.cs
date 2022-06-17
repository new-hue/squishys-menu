using BepInEx;
using BepInEx.Configuration;
using ModMenuPatch.HarmonyPatches;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using Utilla;

namespace ModMenuPatch
{
  [Description("HauntedModMenu")]
  [BepInPlugin("org.legoandmars.gorillatag.modmenupatch", "Mod Menu Patch", "1.0.0")]
  [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
  [ModdedGamemode]
  public class ModMenuPatch : BaseUnityPlugin
  {
    public static bool allowSpaceMonke = true;
    public static ConfigEntry<float> multiplier;
    public static ConfigEntry<float> speedMultiplier;
    public static ConfigEntry<float> jumpMultiplier;
    public static ConfigEntry<bool> RandomColor;
    public static ConfigEntry<float> CycleSpeed;
    public static ConfigEntry<float> GlowAmount;
    public static ConfigEntry<float> sp;
    public static ConfigEntry<float> dp;
    public static ConfigEntry<float> ms;
    public static ConfigEntry<Color> rc;
    internal static object randomColor;
    internal static object glowAmount;

    private void OnEnable()
    {
      ModMenuPatches.ApplyHarmonyPatches();
      ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "ModMonkeyPatch.cfg"), true);
      ModMenuPatch.ModMenuPatch.speedMultiplier = configFile.Bind<float>("Configuration", "SpeedMultiplier", 100f, "How much to multiply the speed. 10 = 10x higher jumps");
      ModMenuPatch.ModMenuPatch.jumpMultiplier = configFile.Bind<float>("Configuration", "JumpMultiplier", 1.5f, "How much to multiply the jump height/distance by. 10 = 10x higher jumps");
      ModMenuPatch.ModMenuPatch.RandomColor = configFile.Bind<bool>("rgb_monke", "RandomColor", false, "Whether to cycle through colours of rainbow or choose random colors");
      ModMenuPatch.ModMenuPatch.CycleSpeed = configFile.Bind<float>("rgb_monke", "CycleSpeed", 0.004f, "The speed the color cycles at each frame (1=Full colour cycle). If random colour is enabled, this is the time in seconds before switching color");
      ModMenuPatch.ModMenuPatch.GlowAmount = configFile.Bind<float>("rgb_monke", "GlowAmount", 1f, "The brightness of your monkey. The higher the value, the more emissive your monkey is");
      ModMenuPatch.ModMenuPatch.sp = configFile.Bind<float>("Configuration", "Spring", 10f, "spring");
      ModMenuPatch.ModMenuPatch.dp = configFile.Bind<float>("Configuration", "Damper", 30f, "damper");
      ModMenuPatch.ModMenuPatch.ms = configFile.Bind<float>("Configuration", "MassScale", 12f, "massscale");
      ModMenuPatch.ModMenuPatch.rc = configFile.Bind<Color>("Configuration", "webColor", Color.white, "webcolor hex code");
    }

    private void OnDisable() => ModMenuPatches.RemoveHarmonyPatches();

    [ModdedGamemodeJoin]
    private void RoomJoined() => ModMenuPatch.ModMenuPatch.allowSpaceMonke = true;

    [ModdedGamemodeLeave]
    private void RoomLeft() => ModMenuPatch.ModMenuPatch.allowSpaceMonke = true;
  }
}
