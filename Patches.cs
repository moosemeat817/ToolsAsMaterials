using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using HarmonyLib;
using MelonLoader;
using Il2CppTLD.Gear;

namespace ToolsAsMaterials
{
    internal class Patches
    {
        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
        public class AddToolsToMaterialsFilter
        {
            public static void Postfix(GearItem __instance)
            {
                // Normalize the name to strip "(Clone)" and whitespace
                string cleanName = __instance.name.Replace("(Clone)", "").Trim();

                HashSet<string> toolNames = new()
            {
                "GEAR_Line",
                "GEAR_Hook",
                "GEAR_ArrowHead",
                "GEAR_ArrowShaft",
                "GEAR_RevolverAmmoCasing",
                "GEAR_RifleAmmoCasing"
            };

                //MelonLogger.Msg($"[ToolsAsMaterials] Checking gear name: {cleanName}");

                if (toolNames.Contains(cleanName))
                {
                    __instance.GearItemData.m_Type = GearType.Material;
                    //MelonLogger.Msg($"[ToolsAsMaterials] Converted {cleanName} to Material");
                }
            }
        }
    }

}
