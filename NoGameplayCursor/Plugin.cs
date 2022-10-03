using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoGameplayCursor
{
    [HarmonyPatch]
    [BepInPlugin("com.steven.trombone.nogameplaycursor", "No Gameplay Cursor", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        const string GAMEPLAY_SCENE_NAME = "gameplay";

        void Awake()
        {
            var harmony = new Harmony("com.steven.trombone.nogameplaycursor");
            harmony.PatchAll();

            SceneManager.activeSceneChanged += ActiveSceneChanged;
        }

        void OnDestroy() => SceneManager.activeSceneChanged -= ActiveSceneChanged;

        void ActiveSceneChanged(Scene _, Scene nextScene)
        {
            if (nextScene.name == GAMEPLAY_SCENE_NAME)
                Cursor.visible = false;
            else
                Cursor.visible = true;
        }

        [HarmonyPatch(typeof(PauseCanvasController), nameof(PauseCanvasController.showPausePanel))]
        static void Prefix() => Cursor.visible = true;
    }
}
