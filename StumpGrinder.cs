using BepInEx;
using HarmonyLib;
using BepInEx.Logging;

namespace StumpGrinder
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    [HarmonyPatch]
    class StumpGrinder : BaseUnityPlugin
    {
        const string pluginGUID = "com.zenvent.stumpgrinder";
        const string pluginName = "StumpGrinder";
        const string pluginVersion = "1.0.0";
        public static ManualLogSource logger;
        private readonly Harmony harmony = new Harmony(pluginGUID);

        public void Awake()
        {
            logger = Logger;
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Destructible), "Damage")]
        [HarmonyPrefix]
        static void PrefixDestructible(ref HitData hit, ref DestructibleType ___m_destructibleType)
        {
            if (___m_destructibleType == DestructibleType.Tree)
            {
                logger.LogInfo("Destroying Shub / Stump");
                hit.m_damage.Modify(100);
            }
        }

    }
}