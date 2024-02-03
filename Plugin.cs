using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace NoVomitingMask
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			Logger.LogInfo($"Patching...");
			Harmony.CreateAndPatchAll(typeof(Plugin));
			Logger.LogInfo($"Patch successful!");
		}
		
		[HarmonyPatch(typeof(MaskedPlayerEnemy), "Start")]
		[HarmonyPostfix]
		public static void MaskVomitPatch(MaskedPlayerEnemy __instance)
		{
			// Store a ref to the original particle's host object
			GameObject particleHost = __instance.gameObject;
			// Reset the flood particles
			Destroy(__instance.maskFloodParticle);
			// Create a dummy placeholder
			__instance.maskFloodParticle = particleHost.AddComponent<ParticleSystem>();
			ParticleSystem.MainModule test = __instance.maskFloodParticle.main;
			// Disable the placeholder
			test.duration = 0;
			test.maxParticles = 0;
			// Patch the vomiting sound effect
			__instance.enemyType.audioClips[2] = new AudioClip();
		}
	}
	
	
}
