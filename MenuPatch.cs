using ExitGames.Client.Photon;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace ModMenuPatch.HarmonyPatches
{
  [HarmonyPatch(typeof (Player))]
  [HarmonyPatch]
  internal class MenuPatch
  {
    public static bool ResetSpeed = false;
    private static string[] buttons = new string[14]
    {
      "Leave Lobby",
      "Tag Gun [D] [NW]",
      "Speed Boost [D?]",
      "Tag All [D] [NW]",
      "Turn Off Tag Freeze",
      "Toggle Beacon",
      "Platforms",
      "NoClip [D]",
      "TP Gun [D]",
      "RGB Monke",
      "Super Monke [D]",
      "Grip Monke",
      "Better Slide Control [D]",
      ">>>>>>>>>>"
    };
    private static bool?[] buttonsActive = new bool?[14]
    {
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false),
      new bool?(false)
    };
    private static bool gripDown;
    private static bool grip;
    private static GameObject menu;
    private static GameObject canvasObj;
    private static GameObject reference;
    public static int framePressCooldown;
    private static GameObject pointer;
    private static bool gravityToggled;
    private static bool flying;
    private static int btnCooldown;
    private static int speedPlusCooldown;
    private static int speedMinusCooldown;
    private static float? maxJumpSpeed;
    private static float? jumpMultiplier;
    private static Vector3? leftHandOffsetInitial;
    private static Vector3? rightHandOffsetInitial;
    private static float? maxArmLengthInitial;
    private static Color color;
    private static float updateTimer;
    private static float updateRate;
    private static float timer;
    private static float hue;
    private static Harmony harmony;
    private static Vector3 scale;
    private static bool gripDown_left;
    private static bool gripDown_right;
    private static bool once_left;
    private static bool once_right;
    private static bool once_left_false;
    private static bool once_right_false;
    private static bool once_networking;
    private static GameObject[] jump_left_network;
    private static GameObject[] jump_right_network;
    private static GameObject jump_left_local;
    private static GameObject jump_right_local;
    private static bool flag2;
    private static bool flag1;
    public static float triggerpressed;
    public static float lefttriggerpressed;
    public static bool cangrapple;
    public static bool canleftgrapple;
    public static bool wackstart;
    public static bool start;
    public static bool inAllowedRoom;
    public static float maxDistance;
    public static float Spring;
    public static float Damper;
    public static float MassScale;
    public static Vector3 grapplePoint;
    public static Vector3 leftgrapplePoint;
    public static SpringJoint joint;
    public static SpringJoint leftjoint;
    public static LineRenderer lr;
    public static LineRenderer leftlr;
    public static Color grapplecolor;
    private static bool ghostToggled;
    private static bool up;
    private static bool down;
    private static bool trigger;
    private static float myVarY1;
    private static float myVarY2;
    private static bool fastr;
    private static float timeSinceLastChange;
    private static bool reset;
    private static float gainSpeed;
    private static bool gain;
    private static bool less;
    private static string[] buttons2;
    private static bool?[] buttonsActive2;
    private static bool page;
    private static bool dont;
    private static int x;
    private static int y;
    private static GameObject C4;
    private static float BoomGrip;
    private static float SpawnGrip;
    private static bool spawned;
    private static GameObject dagger;
    public static int currentPage;
    private static bool verified;
    private static int pbtnCooldown;
    private static float m;
    private static bool inRoom;
    private static GameObject katanae;
    private static float? jumpMultiplierxd;
    private static object index;
    public static int BlueMaterial;
    public static int TransparentMaterial;
    public static int LavaMaterial;
    public static int RockMaterial;
    public static int DefaultMaterial;
    public static int NeonRed;
    public static int RedTransparent;
    public static int self;
    private static bool noClipDisabledOneshot;
    private static bool noClipEnabledAtLeastOnce;
    public static bool modmenupatch;
    private static bool once_networkingd;
    private static GameObject pointerthing;
    private static GameObject[] kunai_network;
    private static GameObject[] knife_network;
    private static Vector3? checkpointPos;
    private static bool checkpointTeleportAntiRepeat;
    private bool onceEnabled;
    private bool onceDisabled;
    private static int layers;
    private static Vector3 head_direction;
    private static Vector3 roll_direction;
    private static Vector2 left_joystick;
    private static float acceleration;
    private static float maxs;
    private static float distance;
    private static float multiplier;
    private static float speed;
    private static bool Start;
    private static GradientColorKey[] colorKeys;
    private static bool speed1;
    private static bool canGrapple;
    public bool hauntedModMenuEnabled = true;
    private static bool canPull;
    public static Vector3 grappleDirection;
    public static Color grapplecolor2;
    private static string[] randomNames;
    private static bool checkedProps;
    private static bool teleportGunAntiRepeat;
    private static bool foundPlayer;
    public static bool umbrellaOpened;
    public static bool canFreeze;
    public static Vector3 lastVel;
    public static Vector3 lastAngVel;
    private static string[] buttons3;
    private static bool?[] buttonsActive3;
    public static int page2;
    private static bool antiRepeat;
    private static bool modEnabled = true;
    private static bool passThrough;
    private static bool inPrivate = PhotonNetwork.InRoom;
    private static bool netOffDocumented = false;
    private static bool netOff = true;
    private static bool netOffKey = false;
    private static Vector3 recordedPos;
    private static GameObject TreeRoom;
    private static GameObject Forest1;
    private static GameObject Forest2;
    private static GameObject Caves;
    private static GameObject Canyons;
    private static GameObject City1;
    private static GameObject City2;
    private static GameObject City3;
    private static GameObject City4;
    private static GameObject Mountain1;
    private static GameObject Mountain2;
    private static GameObject NetworkingTrigger;
    private static GameObject QuitBox;
    public static bool isModEnabled;
    private static GameObject banana;
    private static Vector3 gravityWas;

    private static void Prefix()
    {
      int num1 = ModMenuPatch.ModMenuPatch.allowSpaceMonke ? 1 : 0;
      try
      {
        if (MenuPatch.btnCooldown > 0 && Time.frameCount > MenuPatch.btnCooldown)
        {
          MenuPatch.btnCooldown = 0;
          MenuPatch.buttonsActive2[12] = new bool?(false);
          MenuPatch.buttonsActive2[13] = new bool?(false);
          MenuPatch.buttonsActive[13] = new bool?(false);
          MenuPatch.buttonsActive3[11] = new bool?(false);
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          if (MenuPatch.page2 == 0)
            MenuPatch.Draw();
          if (MenuPatch.page2 == 1)
            MenuPatch.Draw2();
          if (MenuPatch.page2 == 2)
            MenuPatch.Draw3();
        }
        bool? nullable1 = MenuPatch.buttonsActive[13];
        bool flag1 = true;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
        {
          if (MenuPatch.btnCooldown == 0)
          {
            MenuPatch.btnCooldown = Time.frameCount + 1;
            ++MenuPatch.page2;
          }
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          MenuPatch.Draw2();
        }
        nullable1 = MenuPatch.buttonsActive2[12];
        bool flag2 = true;
        bool flag3 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue;
        if (flag3)
        {
          if (MenuPatch.btnCooldown == 0)
          {
            MenuPatch.btnCooldown = Time.frameCount + 1;
            --MenuPatch.page2;
          }
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          MenuPatch.Draw();
        }
        nullable1 = MenuPatch.buttonsActive2[13];
        bool flag4 = true;
        if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
        {
          if (MenuPatch.btnCooldown == 0)
          {
            MenuPatch.btnCooldown = Time.frameCount + 1;
            ++MenuPatch.page2;
          }
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          MenuPatch.Draw3();
        }
        nullable1 = MenuPatch.buttonsActive3[11];
        bool flag5 = true;
        if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
        {
          if (MenuPatch.btnCooldown == 0)
          {
            MenuPatch.btnCooldown = Time.frameCount + 1;
            --MenuPatch.page2;
          }
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          MenuPatch.Draw2();
        }
        if (!MenuPatch.maxJumpSpeed.HasValue)
          MenuPatch.maxJumpSpeed = new float?(Player.Instance.maxJumpSpeed);
        if (!MenuPatch.jumpMultiplier.HasValue)
          MenuPatch.jumpMultiplier = new float?(Player.Instance.jumpMultiplier);
        if (!MenuPatch.maxArmLengthInitial.HasValue)
        {
          MenuPatch.maxArmLengthInitial = new float?(Player.Instance.maxArmLength);
          MenuPatch.leftHandOffsetInitial = new Vector3?(Player.Instance.leftHandOffset);
          MenuPatch.rightHandOffsetInitial = new Vector3?(Player.Instance.rightHandOffset);
        }
        GameObject gameObject = GameObject.Find("Shoulder Camera");
        Camera camera = Object.op_Inequality((Object) gameObject, (Object) null) ? gameObject.GetComponent<Camera>() : (Camera) null;
        List<InputDevice> inputDeviceList1 = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList1);
        InputDevice inputDevice1 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice1).TryGetFeatureValue(CommonUsages.secondaryButton, ref MenuPatch.gripDown);
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList1);
        InputDevice inputDevice2 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice2).TryGetFeatureValue(CommonUsages.secondaryButton, ref MenuPatch.gripDown);
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList1);
        InputDevice inputDevice3 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice3).TryGetFeatureValue(CommonUsages.trigger, ref MenuPatch.SpawnGrip);
        InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList1);
        InputDevice inputDevice4 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice4).TryGetFeatureValue(CommonUsages.grip, ref MenuPatch.BoomGrip);
        InputDevice inputDevice5 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice5).TryGetFeatureValue(CommonUsages.gripButton, ref MenuPatch.gain);
        InputDevice inputDevice6 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.triggerButton, ref MenuPatch.less);
        inputDevice6 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.primaryButton, ref MenuPatch.reset);
        inputDevice6 = inputDeviceList1[0];
        ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.secondaryButton, ref MenuPatch.fastr);
        if (MenuPatch.gripDown && Object.op_Equality((Object) MenuPatch.menu, (Object) null))
        {
          if (MenuPatch.page2 == 0)
            MenuPatch.Draw();
          if (MenuPatch.page2 == 1)
            MenuPatch.Draw2();
          if (MenuPatch.page2 == 2)
            MenuPatch.Draw3();
          if (Object.op_Equality((Object) MenuPatch.reference, (Object) null))
          {
            MenuPatch.reference = GameObject.CreatePrimitive((PrimitiveType) 3);
            Object.Destroy((Object) MenuPatch.reference.GetComponent<MeshRenderer>());
            MenuPatch.reference.transform.parent = Player.Instance.rightHandTransform;
            MenuPatch.reference.transform.localPosition = new Vector3(0.0f, -0.1f, 0.0f);
            MenuPatch.reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
          }
        }
        else if (!MenuPatch.gripDown && Object.op_Inequality((Object) MenuPatch.menu, (Object) null))
        {
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          Object.Destroy((Object) MenuPatch.reference);
          MenuPatch.reference = (GameObject) null;
        }
        if (MenuPatch.gripDown && Object.op_Inequality((Object) MenuPatch.menu, (Object) null))
        {
          MenuPatch.menu.transform.position = Player.Instance.leftHandTransform.position;
          MenuPatch.menu.transform.rotation = Player.Instance.leftHandTransform.rotation;
        }
        bool? nullable2 = MenuPatch.buttonsActive[0];
        bool flag6 = true;
        if (nullable2.GetValueOrDefault() == flag6 & nullable2.HasValue)
          PhotonNetwork.Disconnect();
        nullable2 = MenuPatch.buttonsActive[1];
        nullable2.GetValueOrDefault();
        int num2 = nullable2.HasValue ? 1 : 0;
        nullable2 = MenuPatch.buttonsActive[2];
        bool flag7 = true;
        if (nullable2.GetValueOrDefault() == flag7 & nullable2.HasValue)
        {
          Player.Instance.maxJumpSpeed = ModMenuPatch.ModMenuPatch.speedMultiplier.Value;
          Player.Instance.jumpMultiplier = ModMenuPatch.ModMenuPatch.jumpMultiplier.Value;
        }
        else
        {
          Player.Instance.maxJumpSpeed = MenuPatch.maxJumpSpeed.Value;
          Player.Instance.jumpMultiplier = 1.15f;
        }
        nullable2 = MenuPatch.buttonsActive[3];
        bool flag8 = true;
        if (nullable2.GetValueOrDefault() == flag8 & nullable2.HasValue && MenuPatch.btnCooldown == 0)
        {
          MenuPatch.btnCooldown = Time.frameCount + 30;
          foreach (Player player in PhotonNetwork.PlayerList)
            PhotonView.Get((Component) ((Component) GorillaGameManager.instance).GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", (RpcTarget) 2, new object[1]
            {
              (object) player
            });
          Object.Destroy((Object) MenuPatch.menu);
          MenuPatch.menu = (GameObject) null;
          MenuPatch.Draw();
        }
        nullable2 = MenuPatch.buttonsActive[4];
        bool flag9 = true;
        if (nullable2.GetValueOrDefault() == flag9 & nullable2.HasValue)
          Player.Instance.disableMovement = false;
        nullable2 = MenuPatch.buttonsActive[5];
        bool flag10 = true;
        if (nullable2.GetValueOrDefault() == flag10 & nullable2.HasValue)
        {
          foreach (VRRig vrRig in (VRRig[]) Object.FindObjectsOfType(typeof (VRRig)))
          {
            if (!vrRig.isOfflineVRRig && !vrRig.isMyPlayer && !((MonoBehaviourPun) vrRig).photonView.IsMine)
            {
              GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 2);
              Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
              Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
              Object.Destroy((Object) primitive.GetComponent<Collider>());
              primitive.transform.rotation = Quaternion.identity;
              primitive.transform.localScale = new Vector3(0.04f, 200f, 0.04f);
              primitive.transform.position = ((Component) vrRig).transform.position;
              ((Renderer) primitive.GetComponent<MeshRenderer>()).material = ((Renderer) vrRig.mainSkin).material;
              Object.Destroy((Object) primitive, Time.deltaTime);
            }
          }
        }
        nullable2 = MenuPatch.buttonsActive[6];
        bool flag11 = true;
        if (nullable2.GetValueOrDefault() == flag11 & nullable2.HasValue)
          MenuPatch.ProcessPlatformMonke();
        nullable2 = MenuPatch.buttonsActive[7];
        bool flag12 = true;
        if (nullable2.GetValueOrDefault() == flag12 & nullable2.HasValue)
          MenuPatch.ProcessNoClip();
        nullable2 = MenuPatch.buttonsActive[8];
        bool flag13 = true;
        if (nullable2.GetValueOrDefault() == flag13 & nullable2.HasValue)
          MenuPatch.ProcessTeleportGun();
        nullable2 = MenuPatch.buttonsActive[9];
        bool flag14 = true;
        if (nullable2.GetValueOrDefault() == flag14 & nullable2.HasValue)
          MenuPatch.ProcessRGB();
        nullable2 = MenuPatch.buttonsActive[10];
        bool flag15 = true;
        if (nullable2.GetValueOrDefault() == flag15 & nullable2.HasValue)
        {
          bool flag16 = false;
          bool flag17 = false;
          inputDeviceList1 = new List<InputDevice>();
          InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList1);
          inputDevice6 = inputDeviceList1[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.primaryButton, ref flag16);
          inputDevice6 = inputDeviceList1[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.secondaryButton, ref flag17);
          if (flag16)
          {
            Transform transform = ((Component) Player.Instance).transform;
            transform.position = Vector3.op_Addition(transform.position, Vector3.op_Multiply(Vector3.op_Multiply(((Component) Player.Instance.headCollider).transform.forward, Time.deltaTime), 30f));
            ((Component) Player.Instance).GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!MenuPatch.flying)
              MenuPatch.flying = true;
          }
          else if (MenuPatch.flying)
          {
            ((Component) Player.Instance).GetComponent<Rigidbody>().velocity = Vector3.op_Multiply(Vector3.op_Multiply(((Component) Player.Instance.headCollider).transform.forward, Time.deltaTime), 36f);
            MenuPatch.flying = false;
          }
        }
        nullable2 = MenuPatch.buttonsActive[12];
        bool flag18 = true;
        Player.Instance.slideControl = !(nullable2.GetValueOrDefault() == flag18 & nullable2.HasValue) ? 0.0f : 1f;
        nullable2 = MenuPatch.buttonsActive2[0];
        nullable2.GetValueOrDefault();
        int num3 = nullable2.HasValue ? 1 : 0;
        nullable2 = MenuPatch.buttonsActive2[1];
        bool flag19 = true;
        if (nullable2.GetValueOrDefault() == flag19 & nullable2.HasValue)
        {
          bool flag20 = false;
          bool flag21 = false;
          bool flag22 = false;
          inputDeviceList1 = new List<InputDevice>();
          InputDevices.GetDevices(inputDeviceList1);
          InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList1);
          inputDevice6 = inputDeviceList1[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.triggerButton, ref flag20);
          inputDevice6 = inputDeviceList1[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.gripButton, ref flag21);
          inputDevice6 = inputDeviceList1[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.secondaryButton, ref flag22);
          bool flag23;
          if (flag3)
          {
            flag23 = false;
            if (Object.op_Equality((Object) MenuPatch.pointer, (Object) null))
            {
              MenuPatch.pointer = GameObject.CreatePrimitive((PrimitiveType) 0);
              Object.Destroy((Object) MenuPatch.pointer.GetComponent<Rigidbody>());
              Object.Destroy((Object) MenuPatch.pointer.GetComponent<SphereCollider>());
              MenuPatch.pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            MenuPatch.pointer.transform.position = Player.Instance.rightHandTransform.position;
          }
          if (flag20)
          {
            MenuPatch.ProcessNoClip();
            flag23 = false;
            MenuPatch.pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            ((Component) Player.Instance).transform.position = MenuPatch.pointer.transform.position;
          }
          else if (!flag20)
            MenuPatch.pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        nullable2 = MenuPatch.buttonsActive2[2];
        bool flag24 = true;
        if (nullable2.GetValueOrDefault() == flag24 & nullable2.HasValue)
          MenuPatch.ProcessInvisPlatformMonke();
        nullable2 = MenuPatch.buttonsActive2[3];
        bool flag25 = true;
        if (nullable2.GetValueOrDefault() == flag25 & nullable2.HasValue)
          MenuPatch.UpAndDown();
        nullable2 = MenuPatch.buttonsActive2[4];
        bool flag26 = true;
        if (nullable2.GetValueOrDefault() == flag26 & nullable2.HasValue)
        {
          bool flag27 = false;
          inputDeviceList1 = new List<InputDevice>();
          InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList1);
          inputDevice6 = inputDeviceList1[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.primaryButton, ref flag27);
          if (flag27)
          {
            if (!MenuPatch.ghostToggled && ((Behaviour) GorillaTagger.Instance.myVRRig).enabled)
            {
              ((Behaviour) GorillaTagger.Instance.myVRRig).enabled = false;
              MenuPatch.ghostToggled = true;
            }
            else if (!MenuPatch.ghostToggled && !((Behaviour) GorillaTagger.Instance.myVRRig).enabled)
            {
              ((Behaviour) GorillaTagger.Instance.myVRRig).enabled = true;
              MenuPatch.ghostToggled = true;
            }
          }
          else
            MenuPatch.ghostToggled = false;
        }
        nullable2 = MenuPatch.buttonsActive2[5];
        bool flag28 = true;
        Physics.gravity = !(nullable2.GetValueOrDefault() == flag28 & nullable2.HasValue) ? Vector3.op_Multiply(Physics.gravity, 1f) : Vector3.op_Multiply(Physics.gravity, -1f);
        nullable2 = MenuPatch.buttonsActive2[6];
        bool flag29 = true;
        if (nullable2.GetValueOrDefault() == flag29 & nullable2.HasValue)
        {
          PhotonNetwork.Disconnect();
          PhotonNetwork.JoinRandomRoom();
        }
        nullable2 = MenuPatch.buttonsActive2[7];
        bool flag30 = true;
        if (nullable2.GetValueOrDefault() == flag30 & nullable2.HasValue)
          ((Collider) Player.Instance.bodyCollider).attachedRigidbody.useGravity = false;
        else
          ((Collider) Player.Instance.bodyCollider).attachedRigidbody.useGravity = true;
        nullable2 = MenuPatch.buttonsActive2[8];
        nullable2.GetValueOrDefault();
        int num4 = nullable2.HasValue ? 1 : 0;
        nullable2 = MenuPatch.buttonsActive2[9];
        bool flag31 = true;
        if (nullable2.GetValueOrDefault() == flag31 & nullable2.HasValue)
        {
          MenuPatch.updateTimer += Time.deltaTime;
          MenuPatch.color = Random.ColorHSV(0.0f, 1f, ModMenuPatch.ModMenuPatch.GlowAmount.Value, ModMenuPatch.ModMenuPatch.GlowAmount.Value, ModMenuPatch.ModMenuPatch.GlowAmount.Value, ModMenuPatch.ModMenuPatch.GlowAmount.Value);
          MenuPatch.timer = Time.time + ModMenuPatch.ModMenuPatch.CycleSpeed.Value;
          if ((double) MenuPatch.updateTimer > (double) MenuPatch.updateRate)
          {
            MenuPatch.updateTimer = 0.0f;
            GorillaTagger.Instance.UpdateColor(MenuPatch.color.r, MenuPatch.color.g, MenuPatch.color.b);
            ((MonoBehaviourPun) GorillaTagger.Instance.myVRRig).photonView.RPC("InitializeNoobMaterial", (RpcTarget) 0, new object[3]
            {
              (object) MenuPatch.color.r,
              (object) MenuPatch.color.g,
              (object) MenuPatch.color.b
            });
          }
        }
        nullable2 = MenuPatch.buttonsActive2[10];
        bool flag32 = true;
        if (nullable2.GetValueOrDefault() == flag32 & nullable2.HasValue)
        {
          MenuPatch.canFreeze = true;
          MenuPatch.ProcessTimeFreeze();
        }
        else
          MenuPatch.canFreeze = false;
        nullable2 = MenuPatch.buttonsActive2[11];
        bool flag33 = true;
        if (nullable2.GetValueOrDefault() == flag33 & nullable2.HasValue)
          MenuPatch.ProcessLongArms();
        nullable2 = MenuPatch.buttonsActive3[0];
        bool flag34 = true;
        if (nullable2.GetValueOrDefault() == flag34 & nullable2.HasValue)
        {
          if ((double) MenuPatch.SpawnGrip > 0.0 && !MenuPatch.spawned)
          {
            MenuPatch.C4 = GameObject.CreatePrimitive((PrimitiveType) 3);
            Object.Destroy((Object) MenuPatch.C4.GetComponent<Rigidbody>());
            Object.Destroy((Object) MenuPatch.C4.GetComponent<BoxCollider>());
            MenuPatch.C4.transform.position = ((Component) Player.Instance.leftHandTransform).transform.position;
            MenuPatch.C4.transform.localScale = new Vector3(0.2f, 0.1f, 0.4f);
            MenuPatch.spawned = true;
          }
          if (MenuPatch.spawned && (double) MenuPatch.BoomGrip > 0.0)
          {
            ((Component) Player.Instance).GetComponent<Rigidbody>().AddExplosionForce(50000f, MenuPatch.C4.transform.position, 10f, 2f);
            Object.Destroy((Object) MenuPatch.C4);
            MenuPatch.spawned = false;
          }
        }
        nullable2 = MenuPatch.buttonsActive3[1];
        bool flag35 = true;
        if (nullable2.GetValueOrDefault() == flag35 & nullable2.HasValue)
        {
          if (!MenuPatch.Start)
          {
            MenuPatch.multiplier = 3f;
            MenuPatch.Start = true;
          }
          InputDevices.GetDevices(inputDeviceList1);
          for (int index = 0; index < inputDeviceList1.Count; ++index)
          {
            inputDevice6 = inputDeviceList1[index];
            if (((Enum) (object) ((InputDevice) ref inputDevice6).characteristics).HasFlag((Enum) (object) (InputDeviceCharacteristics) 256))
            {
              inputDevice6 = inputDeviceList1[index];
              ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.primary2DAxis, ref MenuPatch.left_joystick);
            }
            inputDevice6 = inputDeviceList1[index];
            ((Enum) (object) ((InputDevice) ref inputDevice6).characteristics).HasFlag((Enum) (object) (InputDeviceCharacteristics) 512);
          }
          RaycastHit raycastHit;
          Physics.Raycast(((Component) Player.Instance.bodyCollider).transform.position, Vector3.down, ref raycastHit, 100f, MenuPatch.layers);
          MenuPatch.head_direction = ((Component) Player.Instance.headCollider).transform.forward;
          MenuPatch.roll_direction = Vector3.ProjectOnPlane(MenuPatch.head_direction, ((RaycastHit) ref raycastHit).normal);
          if ((double) MenuPatch.left_joystick.y != 0.0)
          {
            if ((double) MenuPatch.left_joystick.y < 0.0)
            {
              if ((double) MenuPatch.speed > -(double) MenuPatch.maxs)
                MenuPatch.speed -= MenuPatch.acceleration * Math.Abs(MenuPatch.left_joystick.y) * Time.deltaTime;
            }
            else if ((double) MenuPatch.speed < (double) MenuPatch.maxs)
              MenuPatch.speed += MenuPatch.acceleration * Math.Abs(MenuPatch.left_joystick.y) * Time.deltaTime;
          }
          else if ((double) MenuPatch.speed < 0.0)
            MenuPatch.speed += (float) ((double) MenuPatch.acceleration * (double) Time.deltaTime * 0.5);
          else if ((double) MenuPatch.speed > 0.0)
            MenuPatch.speed -= (float) ((double) MenuPatch.acceleration * (double) Time.deltaTime * 0.5);
          if ((double) MenuPatch.speed > (double) MenuPatch.maxs)
            MenuPatch.speed = MenuPatch.maxs;
          if ((double) MenuPatch.speed < -(double) MenuPatch.maxs)
            MenuPatch.speed = -MenuPatch.maxs;
          if ((double) MenuPatch.speed != 0.0 && (double) ((RaycastHit) ref raycastHit).distance < (double) MenuPatch.distance)
            ((Collider) Player.Instance.bodyCollider).attachedRigidbody.velocity = Vector3.op_Multiply(Vector3.op_Multiply(((Vector3) ref MenuPatch.roll_direction).normalized, MenuPatch.speed), MenuPatch.multiplier);
          if (Player.Instance.IsHandTouching(true) || Player.Instance.IsHandTouching(false))
            MenuPatch.speed *= 0.75f;
        }
        nullable2 = MenuPatch.buttonsActive3[2];
        bool flag36 = true;
        if (nullable2.GetValueOrDefault() == flag36 & nullable2.HasValue)
          ((Component) Player.Instance).transform.localScale = new Vector3(2f, 2f, 2f);
        else
          ((Component) Player.Instance).transform.localScale = new Vector3(1f, 1f, 1f);
        nullable2 = MenuPatch.buttonsActive3[3];
        nullable2.GetValueOrDefault();
        int num5 = nullable2.HasValue ? 1 : 0;
        nullable2 = MenuPatch.buttonsActive3[4];
        bool flag37 = true;
        if (nullable2.GetValueOrDefault() == flag37 & nullable2.HasValue)
        {
          bool flag38 = false;
          bool flag39 = false;
          List<InputDevice> inputDeviceList2 = new List<InputDevice>();
          InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList2);
          inputDevice6 = inputDeviceList2[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.triggerButton, ref flag38);
          inputDevice6 = inputDeviceList2[0];
          ((InputDevice) ref inputDevice6).TryGetFeatureValue(CommonUsages.secondaryButton, ref flag39);
          int num6 = 0;
          if (flag38)
          {
            ((Component) GorillaTagger.Instance.myVRRig).transform.position = new Vector3(100f, 100f, 100f);
            ((Behaviour) GorillaTagger.Instance.myVRRig).enabled = false;
          }
          else if (num6 < 15)
          {
            int num7 = num6 + 1;
            ((Behaviour) GorillaTagger.Instance.myVRRig).enabled = true;
          }
        }
        nullable2 = MenuPatch.buttonsActive3[5];
        bool flag40 = true;
        if (nullable2.GetValueOrDefault() == flag40 & nullable2.HasValue)
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/MOD STICKLEFT.").SetActive(true);
        else
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/MOD STICKLEFT.").SetActive(false);
        nullable2 = MenuPatch.buttonsActive3[6];
        bool flag41 = true;
        if (nullable2.GetValueOrDefault() == flag41 & nullable2.HasValue)
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/MOD STICKRIGHT.").SetActive(true);
        else
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/MOD STICKRIGHT.").SetActive(false);
        nullable2 = MenuPatch.buttonsActive3[7];
        bool flag42 = true;
        if (nullable2.GetValueOrDefault() == flag42 & nullable2.HasValue)
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/ADMINISTRATOR BADGE").SetActive(true);
        else
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/ADMINISTRATOR BADGE").SetActive(false);
        nullable2 = MenuPatch.buttonsActive3[8];
        bool flag43 = true;
        if (nullable2.GetValueOrDefault() == flag43 & nullable2.HasValue)
          ((Behaviour) camera).enabled = false;
        else
          ((Behaviour) camera).enabled = true;
        nullable2 = MenuPatch.buttonsActive3[9];
        bool flag44 = true;
        if (nullable2.GetValueOrDefault() == flag44 & nullable2.HasValue)
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/SPARKLERRIGHT.").SetActive(true);
        else
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/SPARKLERRIGHT.").SetActive(false);
        nullable2 = MenuPatch.buttonsActive3[10];
        bool flag45 = true;
        if (nullable2.GetValueOrDefault() == flag45 & nullable2.HasValue)
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/CANDY CANERIGHT.").SetActive(true);
        else
          GameObject.Find("OfflineVRRig/Actual Gorilla/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/CANDY CANERIGHT.").SetActive(false);
      }
      catch (Exception ex)
      {
        File.WriteAllText("spacemonkemodmenu_error.log", ex.ToString());
      }
    }

    private static void ProcessPlatformMonke()
    {
      MenuPatch.colorKeys[0].color = Color.red;
      MenuPatch.colorKeys[0].time = 0.0f;
      MenuPatch.colorKeys[1].color = Color.green;
      MenuPatch.colorKeys[1].time = 0.3f;
      MenuPatch.colorKeys[2].color = Color.blue;
      MenuPatch.colorKeys[2].time = 0.6f;
      MenuPatch.colorKeys[3].color = Color.red;
      MenuPatch.colorKeys[3].time = 1f;
      if (!MenuPatch.once_networking)
      {
        PhotonNetwork.NetworkingClient.EventReceived += new Action<EventData>(MenuPatch.PlatformNetwork);
        MenuPatch.once_networking = true;
      }
      List<InputDevice> inputDeviceList = new List<InputDevice>();
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList);
      InputDevice inputDevice = inputDeviceList[0];
      ((InputDevice) ref inputDevice).TryGetFeatureValue(CommonUsages.gripButton, ref MenuPatch.gripDown_left);
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList);
      inputDevice = inputDeviceList[0];
      ((InputDevice) ref inputDevice).TryGetFeatureValue(CommonUsages.gripButton, ref MenuPatch.gripDown_right);
      if (MenuPatch.gripDown_right)
      {
        if (!MenuPatch.once_right && Object.op_Equality((Object) MenuPatch.jump_right_local, (Object) null))
        {
          MenuPatch.jump_right_local = GameObject.CreatePrimitive((PrimitiveType) 3);
          MenuPatch.jump_right_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
          MenuPatch.jump_right_local.transform.localScale = MenuPatch.scale;
          MenuPatch.jump_right_local.transform.position = Vector3.op_Addition(new Vector3(0.0f, -0.0075f, 0.0f), Player.Instance.rightHandTransform.position);
          MenuPatch.jump_right_local.transform.rotation = Player.Instance.rightHandTransform.rotation;
          PhotonNetwork.RaiseEvent((byte) 70, (object) new object[2]
          {
            (object) Vector3.op_Addition(new Vector3(0.0f, -0.0075f, 0.0f), Player.Instance.rightHandTransform.position),
            (object) Player.Instance.rightHandTransform.rotation
          }, new RaiseEventOptions()
          {
            Receivers = (ReceiverGroup) 0
          }, SendOptions.SendReliable);
          MenuPatch.once_right = true;
          MenuPatch.once_right_false = false;
          MenuPatch.ColorChanger colorChanger = MenuPatch.jump_right_local.AddComponent<MenuPatch.ColorChanger>();
          colorChanger.colors = new Gradient()
          {
            colorKeys = MenuPatch.colorKeys
          };
          colorChanger.Start();
        }
      }
      else if (!MenuPatch.once_right_false && Object.op_Inequality((Object) MenuPatch.jump_right_local, (Object) null))
      {
        Object.Destroy((Object) MenuPatch.jump_right_local);
        MenuPatch.jump_right_local = (GameObject) null;
        MenuPatch.once_right = false;
        MenuPatch.once_right_false = true;
        PhotonNetwork.RaiseEvent((byte) 72, (object) null, new RaiseEventOptions()
        {
          Receivers = (ReceiverGroup) 0
        }, SendOptions.SendReliable);
      }
      if (MenuPatch.gripDown_left)
      {
        if (!MenuPatch.once_left && Object.op_Equality((Object) MenuPatch.jump_left_local, (Object) null))
        {
          MenuPatch.jump_left_local = GameObject.CreatePrimitive((PrimitiveType) 3);
          MenuPatch.jump_left_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
          MenuPatch.jump_left_local.transform.localScale = MenuPatch.scale;
          MenuPatch.jump_left_local.transform.position = Player.Instance.leftHandTransform.position;
          MenuPatch.jump_left_local.transform.rotation = Player.Instance.leftHandTransform.rotation;
          PhotonNetwork.RaiseEvent((byte) 69, (object) new object[2]
          {
            (object) Player.Instance.leftHandTransform.position,
            (object) Player.Instance.leftHandTransform.rotation
          }, new RaiseEventOptions()
          {
            Receivers = (ReceiverGroup) 0
          }, SendOptions.SendReliable);
          MenuPatch.once_left = true;
          MenuPatch.once_left_false = false;
          MenuPatch.ColorChanger colorChanger = MenuPatch.jump_left_local.AddComponent<MenuPatch.ColorChanger>();
          colorChanger.colors = new Gradient()
          {
            colorKeys = MenuPatch.colorKeys
          };
          colorChanger.Start();
        }
      }
      else if (!MenuPatch.once_left_false && Object.op_Inequality((Object) MenuPatch.jump_left_local, (Object) null))
      {
        Object.Destroy((Object) MenuPatch.jump_left_local);
        MenuPatch.jump_left_local = (GameObject) null;
        MenuPatch.once_left = false;
        MenuPatch.once_left_false = true;
        PhotonNetwork.RaiseEvent((byte) 71, (object) null, new RaiseEventOptions()
        {
          Receivers = (ReceiverGroup) 0
        }, SendOptions.SendReliable);
      }
      if (PhotonNetwork.InRoom)
        return;
      for (int index = 0; index < MenuPatch.jump_right_network.Length; ++index)
        Object.Destroy((Object) MenuPatch.jump_right_network[index]);
      for (int index = 0; index < MenuPatch.jump_left_network.Length; ++index)
        Object.Destroy((Object) MenuPatch.jump_left_network[index]);
    }

    private static void PlatformNetwork(EventData eventData)
    {
      switch (eventData.Code)
      {
        case 69:
          object[] customData1 = (object[]) eventData.CustomData;
          MenuPatch.jump_left_network[eventData.Sender] = GameObject.CreatePrimitive((PrimitiveType) 3);
          MenuPatch.jump_left_network[eventData.Sender].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
          MenuPatch.jump_left_network[eventData.Sender].transform.localScale = MenuPatch.scale;
          MenuPatch.jump_left_network[eventData.Sender].transform.position = (Vector3) customData1[0];
          MenuPatch.jump_left_network[eventData.Sender].transform.rotation = (Quaternion) customData1[1];
          MenuPatch.ColorChanger colorChanger1 = MenuPatch.jump_left_network[eventData.Sender].AddComponent<MenuPatch.ColorChanger>();
          colorChanger1.colors = new Gradient()
          {
            colorKeys = MenuPatch.colorKeys
          };
          colorChanger1.Start();
          break;
        case 70:
          object[] customData2 = (object[]) eventData.CustomData;
          MenuPatch.jump_right_network[eventData.Sender] = GameObject.CreatePrimitive((PrimitiveType) 3);
          MenuPatch.jump_right_network[eventData.Sender].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
          MenuPatch.jump_right_network[eventData.Sender].transform.localScale = MenuPatch.scale;
          MenuPatch.jump_right_network[eventData.Sender].transform.position = (Vector3) customData2[0];
          MenuPatch.jump_right_network[eventData.Sender].transform.rotation = (Quaternion) customData2[1];
          MenuPatch.ColorChanger colorChanger2 = MenuPatch.jump_left_network[eventData.Sender].AddComponent<MenuPatch.ColorChanger>();
          colorChanger2.colors = new Gradient()
          {
            colorKeys = MenuPatch.colorKeys
          };
          colorChanger2.Start();
          break;
        case 71:
          Object.Destroy((Object) MenuPatch.jump_left_network[eventData.Sender]);
          MenuPatch.jump_left_network[eventData.Sender] = (GameObject) null;
          break;
        case 72:
          Object.Destroy((Object) MenuPatch.jump_right_network[eventData.Sender]);
          MenuPatch.jump_right_network[eventData.Sender] = (GameObject) null;
          break;
      }
    }

    private static void ProcessNoClip()
    {
      bool flag = false;
      List<InputDevice> inputDeviceList = new List<InputDevice>();
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList);
      InputDevice inputDevice = inputDeviceList[0];
      ((InputDevice) ref inputDevice).TryGetFeatureValue(CommonUsages.triggerButton, ref flag);
      if (flag)
      {
        if (MenuPatch.flag2)
          return;
        foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
          ((Component) meshCollider).transform.localScale = Vector3.op_Division(((Component) meshCollider).transform.localScale, 10000f);
        MenuPatch.flag2 = true;
        MenuPatch.flag1 = false;
      }
      else
      {
        if (MenuPatch.flag1)
          return;
        foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
          ((Component) meshCollider).transform.localScale = Vector3.op_Multiply(((Component) meshCollider).transform.localScale, 10000f);
        MenuPatch.flag1 = true;
        MenuPatch.flag2 = false;
      }
    }

    private static void ProcessTeleportGun()
    {
      bool flag1 = false;
      bool flag2 = false;
      List<InputDevice> inputDeviceList = new List<InputDevice>();
      InputDevices.GetDevices(inputDeviceList);
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList);
      InputDevice inputDevice1 = inputDeviceList[0];
      ((InputDevice) ref inputDevice1).TryGetFeatureValue(CommonUsages.triggerButton, ref flag1);
      InputDevice inputDevice2 = inputDeviceList[0];
      ((InputDevice) ref inputDevice2).TryGetFeatureValue(CommonUsages.gripButton, ref flag2);
      if (!flag2)
      {
        Object.Destroy((Object) MenuPatch.pointer);
        MenuPatch.pointer = (GameObject) null;
        MenuPatch.antiRepeat = false;
      }
      else
      {
        RaycastHit raycastHit;
        Physics.Raycast(Vector3.op_Subtraction(Player.Instance.rightHandTransform.position, Player.Instance.rightHandTransform.up), Vector3.op_UnaryNegation(Player.Instance.rightHandTransform.up), ref raycastHit);
        if (Object.op_Equality((Object) MenuPatch.pointer, (Object) null))
        {
          MenuPatch.pointer = GameObject.CreatePrimitive((PrimitiveType) 0);
          Object.Destroy((Object) MenuPatch.pointer.GetComponent<Rigidbody>());
          Object.Destroy((Object) MenuPatch.pointer.GetComponent<SphereCollider>());
          MenuPatch.pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        MenuPatch.pointer.transform.position = ((RaycastHit) ref raycastHit).point;
        if (!flag1)
        {
          MenuPatch.antiRepeat = false;
        }
        else
        {
          if (MenuPatch.antiRepeat)
            return;
          ((Component) Player.Instance).transform.position = ((RaycastHit) ref raycastHit).point;
          ((Component) Player.Instance).GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
          MenuPatch.antiRepeat = true;
        }
      }
    }

    private static void ProcessRGB()
    {
      MenuPatch.updateTimer += Time.deltaTime;
      if (ModMenuPatch.ModMenuPatch.RandomColor.Value)
      {
        if ((double) Time.time > (double) MenuPatch.timer)
        {
          MenuPatch.color = Random.ColorHSV(0.0f, 1f, ModMenuPatch.ModMenuPatch.GlowAmount.Value, ModMenuPatch.ModMenuPatch.GlowAmount.Value, ModMenuPatch.ModMenuPatch.GlowAmount.Value, ModMenuPatch.ModMenuPatch.GlowAmount.Value);
          MenuPatch.timer = Time.time + ModMenuPatch.ModMenuPatch.CycleSpeed.Value;
        }
      }
      else
      {
        if ((double) MenuPatch.hue >= 1.0)
          MenuPatch.hue = 0.0f;
        MenuPatch.hue += ModMenuPatch.ModMenuPatch.CycleSpeed.Value;
        MenuPatch.color = Color.HSVToRGB(MenuPatch.hue, 1f * ModMenuPatch.ModMenuPatch.GlowAmount.Value, 1f * ModMenuPatch.ModMenuPatch.GlowAmount.Value);
      }
      if ((double) MenuPatch.updateTimer <= (double) MenuPatch.updateRate)
        return;
      MenuPatch.updateTimer = 999f;
      GorillaTagger.Instance.UpdateColor(MenuPatch.color.r, MenuPatch.color.g, MenuPatch.color.b);
      ((MonoBehaviourPun) GorillaTagger.Instance.myVRRig).photonView.RPC("InitializeNoobMaterial", (RpcTarget) 0, new object[3]
      {
        (object) MenuPatch.color.r,
        (object) MenuPatch.color.g,
        (object) MenuPatch.color.b
      });
    }

    private static void AddButton(float offset, string text)
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
      ((Collider) primitive.GetComponent<BoxCollider>()).isTrigger = true;
      primitive.transform.parent = MenuPatch.menu.transform;
      primitive.transform.rotation = Quaternion.identity;
      primitive.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
      primitive.transform.localPosition = new Vector3(0.56f, 0.0f, (float) (0.28999999165535 - (double) offset / 1.20000004768372));
      primitive.AddComponent<BtnCollider>().relatedText = text;
      int index1 = -1;
      for (int index2 = 0; index2 < MenuPatch.buttons.Length; ++index2)
      {
        if (text == MenuPatch.buttons[index2])
        {
          index1 = index2;
          break;
        }
      }
      bool? nullable = MenuPatch.buttonsActive[index1];
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
      }
      else
      {
        nullable = MenuPatch.buttonsActive[index1];
        bool flag2 = true;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        else
          primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
      }
      Text text1 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text1.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text1.text = text;
      ((Graphic) text1).color = Color.white;
      text1.fontSize = 1;
      text1.alignment = (TextAnchor) 4;
      text1.resizeTextForBestFit = true;
      text1.resizeTextMinSize = 0;
      RectTransform component = ((Component) text1).GetComponent<RectTransform>();
      ((Transform) component).localPosition = Vector3.zero;
      component.sizeDelta = new Vector2(0.2f, 0.03f);
      ((Transform) component).localPosition = new Vector3(0.064f, 0.0f, (float) (0.111000001430511 - (double) offset / 3.04999995231628));
      ((Transform) component).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
    }

    public static void Draw()
    {
      MenuPatch.menu = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) MenuPatch.menu.GetComponent<Rigidbody>());
      Object.Destroy((Object) MenuPatch.menu.GetComponent<BoxCollider>());
      Object.Destroy((Object) MenuPatch.menu.GetComponent<Renderer>());
      MenuPatch.menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
      Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
      primitive.transform.parent = MenuPatch.menu.transform;
      primitive.transform.rotation = Quaternion.identity;
      primitive.transform.localScale = new Vector3(0.1f, 1f, 2.1f);
      primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
      primitive.transform.position = new Vector3(0.05f, 0.0f, -0.05f);
      MenuPatch.canvasObj = new GameObject();
      MenuPatch.canvasObj.transform.parent = MenuPatch.menu.transform;
      Canvas canvas = MenuPatch.canvasObj.AddComponent<Canvas>();
      CanvasScaler canvasScaler = MenuPatch.canvasObj.AddComponent<CanvasScaler>();
      MenuPatch.canvasObj.AddComponent<GraphicRaycaster>();
      canvas.renderMode = (RenderMode) 2;
      canvasScaler.dynamicPixelsPerUnit = 1000f;
      Text text1 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text1.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text1.text = "Squishys Menu";
      text1.fontSize = 1;
      ((Graphic) text1).color = Color.white;
      text1.alignment = (TextAnchor) 4;
      text1.resizeTextForBestFit = true;
      text1.resizeTextMinSize = 0;
      RectTransform component1 = ((Component) text1).GetComponent<RectTransform>();
      ((Transform) component1).localPosition = Vector3.zero;
      component1.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component1).position = new Vector3(0.06f, 0.0f, 0.175f);
      ((Transform) component1).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
      for (int index = 0; index < MenuPatch.buttons.Length; ++index)
        MenuPatch.AddButton((float) index * 0.13f, MenuPatch.buttons[index]);
      Text text2 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text2.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text2.text = "By Squishy";
      text2.fontSize = 1;
      ((Graphic) text2).color = Color.white;
      text2.alignment = (TextAnchor) 0;
      text2.resizeTextForBestFit = true;
      text2.resizeTextMinSize = 0;
      RectTransform component2 = ((Component) text2).GetComponent<RectTransform>();
      ((Transform) component2).localPosition = Vector3.zero;
      component2.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component2).position = new Vector3(0.06f, -0.01f, -0.5f);
      ((Transform) component2).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
      Text text3 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text3.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text3.text = "discord.gg/exxo";
      text3.fontSize = 1;
      ((Graphic) text3).color = Color.white;
      text3.alignment = (TextAnchor) 0;
      text3.resizeTextForBestFit = true;
      text3.resizeTextMinSize = 0;
      RectTransform component3 = ((Component) text3).GetComponent<RectTransform>();
      ((Transform) component3).localPosition = Vector3.zero;
      component3.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component3).position = new Vector3(0.06f, -0.01f, -0.54f);
      ((Transform) component3).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
    }

    public static void Toggle(string relatedText)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < MenuPatch.buttons.Length; ++index2)
      {
        if (relatedText == MenuPatch.buttons[index2])
        {
          index1 = index2;
          break;
        }
      }
      if (!MenuPatch.buttonsActive[index1].HasValue)
        return;
      bool?[] buttonsActive = MenuPatch.buttonsActive;
      int index3 = index1;
      bool? nullable1 = MenuPatch.buttonsActive[index1];
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      buttonsActive[index3] = nullable2;
      Object.Destroy((Object) MenuPatch.menu);
      MenuPatch.menu = (GameObject) null;
      MenuPatch.Draw();
    }

    static MenuPatch()
    {
      MenuPatch.antiRepeat = false;
      MenuPatch.buttons3 = new string[12]
      {
        nameof (C4),
        "Gorilla Car (L Joystick)",
        "Big Monke (CS)",
        "peepee",
        "Invisible Monke (trigger)",
        "Left Stick (CS)",
        "Right Stick (CS)",
        "Admin Badge (CS)",
        "First Person Camera",
        "Sparkler",
        "Dual Candy Cane",
        "<<<<<<<<<<"
      };
      MenuPatch.buttonsActive3 = new bool?[17]
      {
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false)
      };
      MenuPatch.dagger = (GameObject) null;
      MenuPatch.currentPage = 0;
      MenuPatch.verified = false;
      MenuPatch.pbtnCooldown = 0;
      MenuPatch.m = 100f;
      MenuPatch.inRoom = true;
      MenuPatch.katanae = (GameObject) null;
      MenuPatch.jumpMultiplierxd = new float?();
      MenuPatch.BlueMaterial = 5;
      MenuPatch.TransparentMaterial = 6;
      MenuPatch.LavaMaterial = 2;
      MenuPatch.RockMaterial = 1;
      MenuPatch.DefaultMaterial = 5;
      MenuPatch.NeonRed = 3;
      MenuPatch.RedTransparent = 4;
      MenuPatch.self = 0;
      MenuPatch.noClipDisabledOneshot = false;
      MenuPatch.noClipEnabledAtLeastOnce = false;
      MenuPatch.modmenupatch = true;
      MenuPatch.pointerthing = (GameObject) null;
      MenuPatch.kunai_network = new GameObject[9999];
      MenuPatch.knife_network = new GameObject[9999];
      MenuPatch.checkpointTeleportAntiRepeat = false;
      MenuPatch.layers = 512;
      MenuPatch.acceleration = 5f;
      MenuPatch.maxs = 10f;
      MenuPatch.distance = 0.35f;
      MenuPatch.multiplier = 1f;
      MenuPatch.speed = 0.0f;
      MenuPatch.Start = false;
      MenuPatch.colorKeys = new GradientColorKey[4];
      MenuPatch.speed1 = true;
      MenuPatch.randomNames = new string[11]
      {
        "932423487",
        "234234772",
        "PBBV",
        "PBBV",
        "IS",
        "HERE",
        "RUN",
        "HIDE",
        "I",
        "AM",
        "PBBV"
      };
      MenuPatch.checkedProps = false;
      MenuPatch.teleportGunAntiRepeat = true;
      MenuPatch.foundPlayer = false;
      MenuPatch.canFreeze = false;
      MenuPatch.canGrapple = true;
      MenuPatch.canPull = true;
      MenuPatch.grapplecolor2 = Color.black;
      MenuPatch.spawned = false;
      MenuPatch.C4 = (GameObject) null;
      MenuPatch.x = 0;
      MenuPatch.y = 0;
      MenuPatch.buttons2 = new string[14]
      {
        "XXX",
        "CheckPoint (D?)",
        "Invisible Platforms",
        "Up & Down (D?)",
        "Ghost Cam",
        "Gravity Flip",
        "Server Hop",
        "Toggle Gravity",
        "XXX",
        "RGB Strobe",
        "Trigger To Freeze",
        "Long Arms",
        "<<<<<<<<<<",
        ">>>>>>>>>>"
      };
      MenuPatch.buttonsActive2 = new bool?[23]
      {
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false),
        new bool?(false)
      };
      MenuPatch.grip = false;
      MenuPatch.menu = (GameObject) null;
      MenuPatch.canvasObj = (GameObject) null;
      MenuPatch.reference = (GameObject) null;
      MenuPatch.framePressCooldown = 0;
      MenuPatch.pointer = (GameObject) null;
      MenuPatch.gravityToggled = false;
      MenuPatch.flying = false;
      MenuPatch.btnCooldown = 0;
      MenuPatch.speedPlusCooldown = 0;
      MenuPatch.speedMinusCooldown = 0;
      MenuPatch.maxJumpSpeed = new float?();
      MenuPatch.jumpMultiplier = new float?();
      MenuPatch.leftHandOffsetInitial = new Vector3?();
      MenuPatch.rightHandOffsetInitial = new Vector3?();
      MenuPatch.maxArmLengthInitial = new float?();
      MenuPatch.color = new Color(0.0f, 0.0f, 0.0f);
      MenuPatch.page = false;
      MenuPatch.dont = false;
      MenuPatch.ghostToggled = false;
      MenuPatch.myVarY1 = 0.0f;
      MenuPatch.myVarY2 = 0.0f;
      MenuPatch.fastr = false;
      MenuPatch.timeSinceLastChange = 0.0f;
      MenuPatch.reset = false;
      MenuPatch.gainSpeed = 1f;
      MenuPatch.gain = false;
      MenuPatch.less = false;
      MenuPatch.updateTimer = 0.0f;
      MenuPatch.updateRate = 0.0f;
      MenuPatch.timer = 0.0f;
      MenuPatch.hue = 0.0f;
      MenuPatch.scale = new Vector3(0.0125f, 0.28f, 0.3825f);
      MenuPatch.jump_left_network = new GameObject[9999];
      MenuPatch.jump_right_network = new GameObject[9999];
      MenuPatch.jump_left_local = (GameObject) null;
      MenuPatch.jump_right_local = (GameObject) null;
      MenuPatch.flag2 = false;
      MenuPatch.flag1 = true;
      MenuPatch.cangrapple = true;
      MenuPatch.canleftgrapple = true;
      MenuPatch.wackstart = false;
      MenuPatch.start = true;
      MenuPatch.inAllowedRoom = false;
      MenuPatch.maxDistance = 100f;
    }

    public static void ProcessCheckPoint()
    {
      List<InputDevice> inputDeviceList1 = new List<InputDevice>();
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      List<InputDevice> inputDeviceList2 = new List<InputDevice>();
      InputDevices.GetDevices(inputDeviceList2);
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList2);
      InputDevice inputDevice1 = inputDeviceList2[0];
      ((InputDevice) ref inputDevice1).TryGetFeatureValue(CommonUsages.triggerButton, ref flag1);
      InputDevice inputDevice2 = inputDeviceList2[0];
      ((InputDevice) ref inputDevice2).TryGetFeatureValue(CommonUsages.gripButton, ref flag2);
      InputDevice inputDevice3 = inputDeviceList2[0];
      ((InputDevice) ref inputDevice3).TryGetFeatureValue(CommonUsages.secondaryButton, ref flag3);
      bool flag4;
      if (flag2)
      {
        flag4 = false;
        if (Object.op_Equality((Object) MenuPatch.pointer, (Object) null))
        {
          MenuPatch.pointer = GameObject.CreatePrimitive((PrimitiveType) 0);
          Object.Destroy((Object) MenuPatch.pointer.GetComponent<Rigidbody>());
          Object.Destroy((Object) MenuPatch.pointer.GetComponent<SphereCollider>());
          MenuPatch.pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        MenuPatch.pointer.transform.position = Player.Instance.rightHandTransform.position;
      }
      if (!flag2 & flag1)
      {
        flag4 = false;
        MenuPatch.pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        MenuPatch.ProcessNoClip();
        ((Component) Player.Instance).transform.position = MenuPatch.pointer.transform.position;
      }
      else
      {
        if (flag1)
          return;
        MenuPatch.pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
      }
    }

    private static void ProcessInvisPlatformMonke()
    {
      if (!MenuPatch.once_networking)
      {
        PhotonNetwork.NetworkingClient.EventReceived += new Action<EventData>(MenuPatch.PlatformNetwork);
        MenuPatch.once_networking = true;
      }
      List<InputDevice> inputDeviceList = new List<InputDevice>();
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 324, inputDeviceList);
      InputDevice inputDevice1 = inputDeviceList[0];
      ((InputDevice) ref inputDevice1).TryGetFeatureValue(CommonUsages.gripButton, ref MenuPatch.gripDown_left);
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList);
      InputDevice inputDevice2 = inputDeviceList[0];
      ((InputDevice) ref inputDevice2).TryGetFeatureValue(CommonUsages.gripButton, ref MenuPatch.gripDown_right);
      if (MenuPatch.gripDown_right)
      {
        if (!MenuPatch.once_right && Object.op_Equality((Object) MenuPatch.jump_right_local, (Object) null))
        {
          MenuPatch.jump_right_local = GameObject.CreatePrimitive((PrimitiveType) 3);
          MenuPatch.jump_right_local.GetComponent<Renderer>().enabled = false;
          MenuPatch.jump_right_local.transform.localScale = MenuPatch.scale;
          MenuPatch.jump_right_local.transform.position = Vector3.op_Addition(new Vector3(0.0f, -0.0075f, 0.0f), Player.Instance.rightHandTransform.position);
          MenuPatch.jump_right_local.transform.rotation = Player.Instance.rightHandTransform.rotation;
          PhotonNetwork.RaiseEvent((byte) 70, (object) new object[2]
          {
            (object) Vector3.op_Addition(new Vector3(0.0f, -0.0075f, 0.0f), Player.Instance.rightHandTransform.position),
            (object) Player.Instance.rightHandTransform.rotation
          }, new RaiseEventOptions()
          {
            Receivers = (ReceiverGroup) 0
          }, SendOptions.SendReliable);
          MenuPatch.once_right = true;
          MenuPatch.once_right_false = false;
        }
      }
      else if (!MenuPatch.once_right_false && Object.op_Inequality((Object) MenuPatch.jump_right_local, (Object) null))
      {
        Object.Destroy((Object) MenuPatch.jump_right_local);
        MenuPatch.jump_right_local = (GameObject) null;
        MenuPatch.once_right = false;
        MenuPatch.once_right_false = true;
        PhotonNetwork.RaiseEvent((byte) 72, (object) null, new RaiseEventOptions()
        {
          Receivers = (ReceiverGroup) 0
        }, SendOptions.SendReliable);
      }
      if (MenuPatch.gripDown_left)
      {
        if (!MenuPatch.once_left && Object.op_Equality((Object) MenuPatch.jump_left_local, (Object) null))
        {
          MenuPatch.jump_left_local = GameObject.CreatePrimitive((PrimitiveType) 3);
          MenuPatch.jump_left_local.GetComponent<Renderer>().enabled = false;
          MenuPatch.jump_left_local.transform.localScale = MenuPatch.scale;
          MenuPatch.jump_left_local.transform.position = Player.Instance.leftHandTransform.position;
          MenuPatch.jump_left_local.transform.rotation = Player.Instance.leftHandTransform.rotation;
          PhotonNetwork.RaiseEvent((byte) 69, (object) new object[2]
          {
            (object) Player.Instance.leftHandTransform.position,
            (object) Player.Instance.leftHandTransform.rotation
          }, new RaiseEventOptions()
          {
            Receivers = (ReceiverGroup) 0
          }, SendOptions.SendReliable);
          MenuPatch.once_left = true;
          MenuPatch.once_left_false = false;
        }
      }
      else if (!MenuPatch.once_left_false && Object.op_Inequality((Object) MenuPatch.jump_left_local, (Object) null))
      {
        Object.Destroy((Object) MenuPatch.jump_left_local);
        MenuPatch.jump_left_local = (GameObject) null;
        MenuPatch.once_left = false;
        MenuPatch.once_left_false = true;
        PhotonNetwork.RaiseEvent((byte) 71, (object) null, new RaiseEventOptions()
        {
          Receivers = (ReceiverGroup) 0
        }, SendOptions.SendReliable);
      }
      if (PhotonNetwork.InRoom)
        return;
      for (int index = 0; index < MenuPatch.jump_right_network.Length; ++index)
        Object.Destroy((Object) MenuPatch.jump_right_network[index]);
      for (int index = 0; index < MenuPatch.jump_left_network.Length; ++index)
        Object.Destroy((Object) MenuPatch.jump_left_network[index]);
    }

    private static void UpAndDown()
    {
      InputDevice deviceAtXrNode1 = InputDevices.GetDeviceAtXRNode((XRNode) 5);
      ((InputDevice) ref deviceAtXrNode1).TryGetFeatureValue(CommonUsages.triggerButton, ref MenuPatch.up);
      if (MenuPatch.up)
        ((Component) Player.Instance).GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 1000f, 0.0f), (ForceMode) 5);
      InputDevice deviceAtXrNode2 = InputDevices.GetDeviceAtXRNode((XRNode) 4);
      ((InputDevice) ref deviceAtXrNode2).TryGetFeatureValue(CommonUsages.triggerButton, ref MenuPatch.down);
      if (!MenuPatch.down)
        return;
      ((Component) Player.Instance).GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -1000f, 0.0f), (ForceMode) 5);
    }

    private static void ProcessLongArms()
    {
      List<InputDevice> inputDeviceList = new List<InputDevice>();
      InputDevices.GetDevicesWithCharacteristics((InputDeviceCharacteristics) 580, inputDeviceList);
      InputDevice inputDevice1 = inputDeviceList[0];
      ((InputDevice) ref inputDevice1).TryGetFeatureValue(CommonUsages.gripButton, ref MenuPatch.gain);
      InputDevice inputDevice2 = inputDeviceList[0];
      ((InputDevice) ref inputDevice2).TryGetFeatureValue(CommonUsages.triggerButton, ref MenuPatch.less);
      InputDevice inputDevice3 = inputDeviceList[0];
      ((InputDevice) ref inputDevice3).TryGetFeatureValue(CommonUsages.primaryButton, ref MenuPatch.reset);
      InputDevice inputDevice4 = inputDeviceList[0];
      ((InputDevice) ref inputDevice4).TryGetFeatureValue(CommonUsages.secondaryButton, ref MenuPatch.fastr);
      MenuPatch.timeSinceLastChange += Time.deltaTime;
      if ((double) MenuPatch.timeSinceLastChange <= 0.200000002980232)
        return;
      Player.Instance.leftHandOffset = new Vector3(0.0f, MenuPatch.myVarY1, 0.0f);
      Player.Instance.rightHandOffset = new Vector3(0.0f, MenuPatch.myVarY2, 0.0f);
      Player.Instance.maxArmLength = 200f;
      if (MenuPatch.gain)
      {
        MenuPatch.timeSinceLastChange = 0.0f;
        MenuPatch.myVarY1 += MenuPatch.gainSpeed;
        MenuPatch.myVarY2 += MenuPatch.gainSpeed;
        if ((double) MenuPatch.myVarY1 >= 201.0)
        {
          MenuPatch.myVarY1 = 200f;
          MenuPatch.myVarY2 = 200f;
        }
      }
      if (MenuPatch.less)
      {
        MenuPatch.timeSinceLastChange = 0.0f;
        MenuPatch.myVarY1 -= MenuPatch.gainSpeed;
        MenuPatch.myVarY2 -= MenuPatch.gainSpeed;
        if ((double) MenuPatch.myVarY2 <= -6.0)
        {
          MenuPatch.myVarY1 = -5f;
          MenuPatch.myVarY2 = -5f;
        }
      }
      if (MenuPatch.reset)
      {
        MenuPatch.timeSinceLastChange = 0.0f;
        MenuPatch.myVarY1 = 0.0f;
        MenuPatch.myVarY2 = 0.0f;
      }
      if (!MenuPatch.fastr || (double) MenuPatch.myVarY1 != 5.0)
        return;
      MenuPatch.myVarY1 = 10f;
      MenuPatch.myVarY2 = 10f;
    }

    private static void ResetFreeze()
    {
      if (MenuPatch.canFreeze)
        return;
      ((Collider) Player.Instance.bodyCollider).attachedRigidbody.useGravity = true;
      ((Collider) Player.Instance.bodyCollider).attachedRigidbody.velocity = MenuPatch.lastVel;
      ((Collider) Player.Instance.bodyCollider).attachedRigidbody.angularVelocity = MenuPatch.lastAngVel;
      MenuPatch.canFreeze = true;
    }

    private static void ProcessTimeFreeze()
    {
      if ((double) MenuPatch.SpawnGrip > 0.100000001490116)
      {
        if (MenuPatch.canFreeze)
        {
          MenuPatch.lastVel = ((Collider) Player.Instance.bodyCollider).attachedRigidbody.velocity;
          MenuPatch.lastAngVel = ((Collider) Player.Instance.bodyCollider).attachedRigidbody.angularVelocity;
          MenuPatch.canFreeze = false;
        }
        ((Collider) Player.Instance.bodyCollider).attachedRigidbody.velocity = Vector3.zero;
        ((Collider) Player.Instance.bodyCollider).attachedRigidbody.angularVelocity = Vector3.zero;
        ((Collider) Player.Instance.bodyCollider).attachedRigidbody.useGravity = false;
      }
      else
        MenuPatch.ResetFreeze();
    }

    private static void AddButton2(float offset, string text)
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
      ((Collider) primitive.GetComponent<BoxCollider>()).isTrigger = true;
      primitive.transform.parent = MenuPatch.menu.transform;
      primitive.transform.rotation = Quaternion.identity;
      primitive.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
      primitive.transform.localPosition = new Vector3(0.56f, 0.0f, (float) (0.28999999165535 - (double) offset / 1.20000004768372));
      primitive.AddComponent<BtnCollider>().relatedText = text;
      int index1 = -1;
      for (int index2 = 0; index2 < MenuPatch.buttons2.Length; ++index2)
      {
        if (text == MenuPatch.buttons2[index2])
        {
          index1 = index2;
          break;
        }
      }
      bool? nullable = MenuPatch.buttonsActive2[index1];
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
      }
      else
      {
        nullable = MenuPatch.buttonsActive2[index1];
        bool flag2 = true;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
        else
          primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
      }
      Text text1 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text1.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text1.text = text;
      ((Graphic) text1).color = Color.white;
      text1.fontSize = 1;
      text1.alignment = (TextAnchor) 4;
      text1.resizeTextForBestFit = true;
      text1.resizeTextMinSize = 0;
      RectTransform component = ((Component) text1).GetComponent<RectTransform>();
      ((Transform) component).localPosition = Vector3.zero;
      component.sizeDelta = new Vector2(0.2f, 0.03f);
      ((Transform) component).localPosition = new Vector3(0.064f, 0.0f, (float) (0.111000001430511 - (double) offset / 3.04999995231628));
      ((Transform) component).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
    }

    private static void AddButton3(float offset, string text)
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
      ((Collider) primitive.GetComponent<BoxCollider>()).isTrigger = true;
      primitive.transform.parent = MenuPatch.menu.transform;
      primitive.transform.rotation = Quaternion.identity;
      primitive.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
      primitive.transform.localPosition = new Vector3(0.56f, 0.0f, (float) (0.28999999165535 - (double) offset / 1.20000004768372));
      primitive.AddComponent<BtnCollider>().relatedText = text;
      int index1 = -1;
      for (int index2 = 0; index2 < MenuPatch.buttons3.Length; ++index2)
      {
        if (text == MenuPatch.buttons3[index2])
        {
          index1 = index2;
          break;
        }
      }
      bool? nullable = MenuPatch.buttonsActive3[index1];
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
      }
      else
      {
        nullable = MenuPatch.buttonsActive3[index1];
        bool flag2 = true;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        else
          primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
      }
      Text text1 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text1.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text1.text = text;
      ((Graphic) text1).color = Color.white;
      text1.fontSize = 1;
      text1.alignment = (TextAnchor) 4;
      text1.resizeTextForBestFit = true;
      text1.resizeTextMinSize = 0;
      RectTransform component = ((Component) text1).GetComponent<RectTransform>();
      ((Transform) component).localPosition = Vector3.zero;
      component.sizeDelta = new Vector2(0.2f, 0.03f);
      ((Transform) component).localPosition = new Vector3(0.064f, 0.0f, (float) (0.111000001430511 - (double) offset / 3.04999995231628));
      ((Transform) component).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
    }

    public static void Draw2()
    {
      MenuPatch.menu = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) MenuPatch.menu.GetComponent<Rigidbody>());
      Object.Destroy((Object) MenuPatch.menu.GetComponent<BoxCollider>());
      Object.Destroy((Object) MenuPatch.menu.GetComponent<Renderer>());
      MenuPatch.menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
      Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
      primitive.transform.parent = MenuPatch.menu.transform;
      primitive.transform.rotation = Quaternion.identity;
      primitive.transform.localScale = new Vector3(0.1f, 1f, 2.1f);
      primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
      primitive.transform.position = new Vector3(0.05f, 0.0f, -0.05f);
      MenuPatch.canvasObj = new GameObject();
      MenuPatch.canvasObj.transform.parent = MenuPatch.menu.transform;
      Canvas canvas = MenuPatch.canvasObj.AddComponent<Canvas>();
      CanvasScaler canvasScaler = MenuPatch.canvasObj.AddComponent<CanvasScaler>();
      MenuPatch.canvasObj.AddComponent<GraphicRaycaster>();
      canvas.renderMode = (RenderMode) 2;
      canvasScaler.dynamicPixelsPerUnit = 1000f;
      Text text1 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text1.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text1.text = "Squishy's Menu";
      text1.fontSize = 1;
      ((Graphic) text1).color = Color.white;
      text1.alignment = (TextAnchor) 4;
      text1.resizeTextForBestFit = true;
      text1.resizeTextMinSize = 0;
      RectTransform component1 = ((Component) text1).GetComponent<RectTransform>();
      ((Transform) component1).localPosition = Vector3.zero;
      component1.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component1).position = new Vector3(0.06f, 0.0f, 0.175f);
      ((Transform) component1).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
      for (int index = 0; index < MenuPatch.buttons2.Length; ++index)
        MenuPatch.AddButton2((float) index * 0.13f, MenuPatch.buttons2[index]);
      Text text2 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text2.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text2.text = "By Squishy";
      text2.fontSize = 1;
      ((Graphic) text2).color = Color.white;
      text2.alignment = (TextAnchor) 0;
      text2.resizeTextForBestFit = true;
      text2.resizeTextMinSize = 0;
      RectTransform component2 = ((Component) text2).GetComponent<RectTransform>();
      ((Transform) component2).localPosition = Vector3.zero;
      component2.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component2).position = new Vector3(0.06f, -0.01f, -0.5f);
      ((Transform) component2).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
      Text text3 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text3.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text3.text = "discord.gg/exxo";
      text3.fontSize = 1;
      ((Graphic) text3).color = Color.white;
      text3.alignment = (TextAnchor) 0;
      text3.resizeTextForBestFit = true;
      text3.resizeTextMinSize = 0;
      RectTransform component3 = ((Component) text3).GetComponent<RectTransform>();
      ((Transform) component3).localPosition = Vector3.zero;
      component3.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component3).position = new Vector3(0.06f, -0.01f, -0.54f);
      ((Transform) component3).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
    }

    public static void Draw3()
    {
      MenuPatch.menu = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) MenuPatch.menu.GetComponent<Rigidbody>());
      Object.Destroy((Object) MenuPatch.menu.GetComponent<BoxCollider>());
      Object.Destroy((Object) MenuPatch.menu.GetComponent<Renderer>());
      MenuPatch.menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      Object.Destroy((Object) primitive.GetComponent<Rigidbody>());
      Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
      primitive.transform.parent = MenuPatch.menu.transform;
      primitive.transform.rotation = Quaternion.identity;
      primitive.transform.localScale = new Vector3(0.1f, 1f, 2.1f);
      primitive.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
      primitive.transform.position = new Vector3(0.05f, 0.0f, -0.05f);
      MenuPatch.canvasObj = new GameObject();
      MenuPatch.canvasObj.transform.parent = MenuPatch.menu.transform;
      Canvas canvas = MenuPatch.canvasObj.AddComponent<Canvas>();
      CanvasScaler canvasScaler = MenuPatch.canvasObj.AddComponent<CanvasScaler>();
      MenuPatch.canvasObj.AddComponent<GraphicRaycaster>();
      canvas.renderMode = (RenderMode) 2;
      canvasScaler.dynamicPixelsPerUnit = 1000f;
      Text text1 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text1.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text1.text = "Squishys Menu V3";
      text1.fontSize = 1;
      ((Graphic) text1).color = Color.white;
      text1.alignment = (TextAnchor) 4;
      text1.resizeTextForBestFit = true;
      text1.resizeTextMinSize = 0;
      RectTransform component1 = ((Component) text1).GetComponent<RectTransform>();
      ((Transform) component1).localPosition = Vector3.zero;
      component1.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component1).position = new Vector3(0.06f, 0.0f, 0.175f);
      ((Transform) component1).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
      for (int index = 0; index < MenuPatch.buttons3.Length; ++index)
        MenuPatch.AddButton3((float) index * 0.13f, MenuPatch.buttons3[index]);
      Text text2 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text2.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text2.text = "By Squishy";
      text2.fontSize = 1;
      ((Graphic) text2).color = Color.white;
      text2.alignment = (TextAnchor) 0;
      text2.resizeTextForBestFit = true;
      text2.resizeTextMinSize = 0;
      RectTransform component2 = ((Component) text2).GetComponent<RectTransform>();
      ((Transform) component2).localPosition = Vector3.zero;
      component2.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component2).position = new Vector3(0.06f, -0.01f, -0.5f);
      ((Transform) component2).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
      Text text3 = new GameObject()
      {
        transform = {
          parent = MenuPatch.canvasObj.transform
        }
      }.AddComponent<Text>();
      text3.font = Resources.GetBuiltinResource(typeof (Font), "Arial.ttf") as Font;
      text3.text = "discord.gg/exxo";
      text3.fontSize = 1;
      ((Graphic) text3).color = Color.white;
      text3.alignment = (TextAnchor) 0;
      text3.resizeTextForBestFit = true;
      text3.resizeTextMinSize = 0;
      RectTransform component3 = ((Component) text3).GetComponent<RectTransform>();
      ((Transform) component3).localPosition = Vector3.zero;
      component3.sizeDelta = new Vector2(0.28f, 0.05f);
      ((Transform) component3).position = new Vector3(0.06f, -0.01f, -0.54f);
      ((Transform) component3).rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
    }

    public static void Toggle2(string relatedText)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < MenuPatch.buttons2.Length; ++index2)
      {
        if (relatedText == MenuPatch.buttons2[index2])
        {
          index1 = index2;
          break;
        }
      }
      if (!MenuPatch.buttonsActive2[index1].HasValue)
        return;
      bool?[] buttonsActive2 = MenuPatch.buttonsActive2;
      int index3 = index1;
      bool? nullable1 = MenuPatch.buttonsActive2[index1];
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      buttonsActive2[index3] = nullable2;
      Object.Destroy((Object) MenuPatch.menu);
      MenuPatch.menu = (GameObject) null;
      MenuPatch.Draw2();
    }

    public static void Toggle3(string relatedText)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < MenuPatch.buttons3.Length; ++index2)
      {
        if (relatedText == MenuPatch.buttons3[index2])
        {
          index1 = index2;
          break;
        }
      }
      if (!MenuPatch.buttonsActive3[index1].HasValue)
        return;
      bool?[] buttonsActive3 = MenuPatch.buttonsActive3;
      int index3 = index1;
      bool? nullable1 = MenuPatch.buttonsActive3[index1];
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      buttonsActive3[index3] = nullable2;
      Object.Destroy((Object) MenuPatch.menu);
      MenuPatch.menu = (GameObject) null;
      MenuPatch.Draw3();
    }

    public static void resetGravity()
    {
      Physics.gravity = MenuPatch.gravityWas;
      Debug.Log((object) "Reset gravity");
    }

    public enum PhotonEventCodes
    {
      left_jump_photoncode = 69, // 0x00000045
      right_jump_photoncode = 70, // 0x00000046
      left_jump_deletion = 71, // 0x00000047
      right_jump_deletion = 72, // 0x00000048
    }

    public class TimedBehaviour : MonoBehaviour
    {
      public bool complete;
      public bool loop = true;
      public float progress;
      protected bool paused;
      protected float startTime;
      protected float duration = 2f;

      public virtual void Start() => this.startTime = Time.time;

      public virtual void Update()
      {
        if (this.complete)
          return;
        this.progress = Mathf.Clamp((Time.time - this.startTime) / this.duration, 0.0f, 1f);
        if ((double) Time.time - (double) this.startTime <= (double) this.duration)
          return;
        if (this.loop)
          this.OnLoop();
        else
          this.complete = true;
      }

      public virtual void OnLoop() => this.startTime = Time.time;

      [HarmonyPatch(typeof (Player), "GetSlidePercentage")]
      private class slidepatch
      {
        private static void Postfix(ref float __result)
        {
          bool? nullable = MenuPatch.buttonsActive[11];
          bool flag = true;
          if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
            return;
          __result = 0.0f;
        }
      }
    }

    public class ColorChanger : MenuPatch.TimedBehaviour
    {
      public Renderer gameObjectRenderer;
      public Gradient colors;
      public Color color;
      public bool timeBased = true;

      public override void Start()
      {
        base.Start();
        this.gameObjectRenderer = ((Component) this).GetComponent<Renderer>();
      }

      public override void Update()
      {
        base.Update();
        if (this.colors == null)
          return;
        if (this.timeBased)
          this.color = this.colors.Evaluate(this.progress);
        this.gameObjectRenderer.material.SetColor("_Color", this.color);
        this.gameObjectRenderer.material.SetColor("_EmissionColor", this.color);
      }
    }
  }
}
