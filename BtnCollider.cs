using UnityEngine;

namespace ModMenuPatch.HarmonyPatches
{
  internal class BtnCollider : MonoBehaviour
  {
    public string relatedText;

    private void OnTriggerEnter(Collider collider)
    {
      if (MenuPatch.page2 == 0 && Time.frameCount >= MenuPatch.framePressCooldown + 30)
      {
        MenuPatch.Toggle(this.relatedText);
        MenuPatch.framePressCooldown = Time.frameCount;
      }
      if (MenuPatch.page2 == 1 && Time.frameCount >= MenuPatch.framePressCooldown + 30)
      {
        MenuPatch.Toggle2(this.relatedText);
        MenuPatch.framePressCooldown = Time.frameCount;
      }
      if (MenuPatch.page2 != 2 || Time.frameCount < MenuPatch.framePressCooldown + 30)
        return;
      MenuPatch.Toggle3(this.relatedText);
      MenuPatch.framePressCooldown = Time.frameCount;
    }
  }
}
