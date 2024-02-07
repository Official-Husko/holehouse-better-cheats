using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace CombinedPlugin
{
    [BepInPlugin("husko.holehouse.better_cheating", "Better Cheating", "1.0.0")]
    [BepInProcess("HoleHouse v0.1.exe")]
    [BepInProcess("HoleHouse.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private bool menuOpen = false;
        private int buttonCount = 5; // Updated button count
        private float buttonHeight = 30; // Button height
        private float buttonPadding = 5; // Vertical padding between buttons
        private float extraSpace = 20; // Extra space at the bottom of the box

        // Assuming EnterCode is in the same namespace
        private EnterCode enterCodeInstance = new EnterCode();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                menuOpen = !menuOpen;
            }
        }

        private void OnGUI()
        {
            if (menuOpen)
            {
                // Main Menu (Moved further to the left)
                GUIStyle menuStyle = new GUIStyle(GUI.skin.box);
                menuStyle.fontSize = 20;
                menuStyle.normal.textColor = RainbowColorCycle();

                // Calculate the total content height based on the number of buttons
                float totalContentHeight = 70 + buttonCount * buttonHeight + (buttonCount - 1) * buttonPadding + extraSpace; // Initial height 70, adjust as needed

                Rect menuRect = new Rect(435, 10, 240, totalContentHeight);
                GUI.Box(menuRect, "Mod Menu", menuStyle);

                float currentY = 70; // Initial Y position

                // Potion button
                if (GUI.Button(new Rect(445, currentY, 220, buttonHeight), "Add Potions"))
                {
                    Debug.Log("Added Potions!");
                    IntManager.FileHolderer += 999999;
                }

                currentY += buttonHeight + buttonPadding; // Adjust Y position for the next button

                // Coin button
                if (GUI.Button(new Rect(445, currentY, 220, buttonHeight), "Add Coins"))
                {
                    Debug.Log("Added Coins!");
                    IntManager.Coins += 999999;
                }

                currentY += buttonHeight + buttonPadding; // Adjust Y position for the next button

                // Diamond button
                if (GUI.Button(new Rect(445, currentY, 220, buttonHeight), "Add Diamonds"))
                {
                    Debug.Log("Added Diamonds!");
                    IntManager.Diamonds += 999999;
                }

                currentY += buttonHeight + buttonPadding; // Adjust Y position for the next button

                // Level button
                if (GUI.Button(new Rect(445, currentY, 220, buttonHeight), "Add Levels"))
                {
                    Debug.Log("Added Levels!");
                    IntManager.CurrentLevel += 999;
                }

                currentY += buttonHeight + buttonPadding; // Adjust Y position for the next button

                // Experience button
                if (GUI.Button(new Rect(445, currentY, 220, buttonHeight), "Add XP"))
                {
                    Debug.Log("Added XP!");
                    IntManager.XP += 999999;
                }

                currentY += buttonHeight + buttonPadding; // Adjust Y position for the next button

                GUIStyle labelStyle = new GUIStyle();
                labelStyle.richText = true;
                labelStyle.normal.textColor = RainbowColorCycle();

                string labelContent = "<color=#808080>by</color> <color=#e9950c>paw_beans on F95</color>";
                float labelWidth = 220;
                float labelHeight = 20;

                // Calculate center position under the RGB Mod Menu text
                float centerX = menuRect.x + menuRect.width / 2 - labelWidth / 2;
                float centerY = menuRect.y + totalContentHeight - extraSpace; // Position label at the bottom of the Mod Menu box

                GUI.Label(new Rect(centerX, centerY, labelWidth, labelHeight), labelContent, labelStyle);

                // Handle the link click action (e.g., open a web page) when the label is clicked
                if (Event.current.type == EventType.MouseDown && new Rect(centerX, centerY, labelWidth, labelHeight).Contains(Event.current.mousePosition))
                {
                    Application.OpenURL("https://f95zone.to/members/paw_beans.4946040/");
                }

                // Display version number next to the "paw_beans on F95" text
                GUIStyle versionStyle = new GUIStyle(GUI.skin.label);
                versionStyle.normal.textColor = Color.gray; // Change text color to gray
                string versionContent = "v1.0.0"; // Replace with your actual version number
                float versionWidth = 60; // Adjust width as needed
                float versionHeight = 20;
                // Calculate the position for the version number
                float versionX = centerX + labelWidth / 2 + 75; // Adjust the offset as needed
                float versionY = menuRect.y + totalContentHeight - extraSpace - 3; // Position label at the bottom of the Mod Menu box

                // Display version number
                GUI.Label(new Rect(versionX, versionY, versionWidth, versionHeight), versionContent, versionStyle);

                // Handle the link click action for the version number
                if (Event.current.type == EventType.MouseDown && new Rect(versionX, versionY, versionWidth, versionHeight).Contains(Event.current.mousePosition))
                {
                    // Open GitHub when the version number is clicked
                    Application.OpenURL("https://github.com/your-github-repository");
                }
            }

            // Small clickable button/box on the upper left (Moved further to the left)
            if (GUI.Button(new Rect(350, 10, 80, 30), "Menu"))
            {
                menuOpen = !menuOpen;
            }
        }

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {Info.Metadata.Name} v{Info.Metadata.Version} is loaded!");

            // Apply the Harmony patch
            Harmony harmony = new Harmony("husko.holehouse.bettercheats");
            harmony.PatchAll();
        }

        private Color RainbowColorCycle()
        {
            // Simple RGB color cycle logic
            float time = Time.time * 0.5f; // Adjust the multiplier for the speed of the color change
            float r = Mathf.Sin(time) * 0.5f + 0.5f;
            float g = Mathf.Sin(time + 2f) * 0.5f + 0.5f;
            float b = Mathf.Sin(time + 4f) * 0.5f + 0.5f;

            return new Color(r, g, b);
        }
    }

    [HarmonyPatch(typeof(EnterCode))]
    [HarmonyPatch("CodeEntered")]
    class EnterCodePatch
    {
        static bool Prefix(EnterCode __instance)
        {
            string CoinsCheat = "fuzzy doubloons";
            string DiamondsCheat = "shiny stones";
            string LevelCheat = "level 1 crook";
            string ExperienceCheat = "i had sex yes";
            string PregnanCheat = "gotta buy milk";
            string GodCheat = "fill me up daddy paw_beans uwu";
            string CopyCodes = "copy codes";

            if (__instance.RedeemInput.text == CoinsCheat)
            {
                IntManager.Coins += 999999;
                ToolTipSystem.Show($"{CoinsCheat} activated!");
                return false;
            }
            if (__instance.RedeemInput.text == DiamondsCheat)
            {
                IntManager.Diamonds += 999999;
                ToolTipSystem.Show($"{DiamondsCheat} activated!");
                return false;
            }
            if (__instance.RedeemInput.text == LevelCheat)
            {
                IntManager.CurrentLevel += 999;
                ToolTipSystem.Show($"{LevelCheat} activated!");
                return false;
            }
            if (__instance.RedeemInput.text == ExperienceCheat)
            {
                IntManager.XP += 999999;
                ToolTipSystem.Show($"{ExperienceCheat} activated!");
                return false;
            }
            if (__instance.RedeemInput.text == PregnanCheat)
            {
                IntManager.Android18Pregnant = 1;
                IntManager.GwenStacyPregnant = 1;
                IntManager.HarleyQuinnPregnant = 1;
                IntManager.JinxPregnant = 1;
                IntManager.KimPossiblePregnant = 1;
                IntManager.MargePregnant = 1;
                IntManager.MirkoPregnant = 1;
                IntManager.PeachPregnant = 1;
                IntManager.RavenPregnant = 1;
                IntManager.SamsungSamPregnant = 1;
                IntManager.SamusPregnant = 1;
                IntManager.VelmaPregnant = 1;
                IntManager.ZeldaPregnant = 1;

                ToolTipSystem.Show($"{PregnanCheat} activated!"); ;
                return false;
            }
            if (__instance.RedeemInput.text == GodCheat)
            {
                IntManager.FileHolderer += 999999;
                IntManager.Coins += 999999;
                IntManager.Diamonds += 999999;
                IntManager.CurrentLevel += 999;
                IntManager.XP += 999999;

                ToolTipSystem.Show($"{GodCheat} activated!");
                return false;
            }
            if (__instance.RedeemInput.text == CopyCodes)
            {
                GUIUtility.systemCopyBuffer = string.Concat(new string[]
                {
                    __instance.ThreeCode + " = 2 Potions",
                    "\n",
                    __instance.FiveCode + " = 4 Potions",
                    "\n",
                    __instance.TenCode + " = 8 Potions",
                    "\n",
                    __instance.ThirtyCode + " = 20 Potions",
                    "\n",
                    __instance.TenGleamCode + " = 10 Potions",
                    "\n",
                    __instance.FiveGleamCode + " = 5 Potions",
                    "\n",
                    __instance.ThreeGleamCode + " = 3 Potions"
                });

                ToolTipSystem.Show("Codes copied to clipboard!");
                return false;
            }
            ToolTipSystem.Show("Invalid Code. Check F95 or use 'fill me up daddy paw_beans uwu'");
            LeanTween.delayedCall(20.5f, delegate ()
            {
                ToolTipSystem.Hide();
                LeanTween.cancelAll();
            });
            __instance.RedeemInput.text = "Enter Code";

            return false; // Skip executing the original method
        }
    }
}
