using Base.AI.Defs;
using Base.Core;
using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects;
using Base.Entities.Statuses;
using Base.Levels;
using Base.UI;
using Base.Utils.Maths;
using Base.Utils.GameConsole;
using Code.PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Common.Entities.Equipments;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsSharedData;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Entities.RedeemableCodes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Geoscape.Entities.DifficultySystem;
using PhoenixPoint.Geoscape.Entities.Interception.Equipments;
using PhoenixPoint.Geoscape.Events.Eventus;
using PhoenixPoint.Geoscape.Levels;
using PhoenixPoint.Geoscape.Levels.Factions;
using PhoenixPoint.Tactical;
using PhoenixPoint.Tactical.AI;
using PhoenixPoint.Tactical.AI.Actions;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Animations;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Effects;
using PhoenixPoint.Tactical.Entities.Effects.DamageTypes;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using PhoenixPoint.Tactical.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using HarmonyLib;

namespace SuperCheatsModPlus
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class SuperCheatsModPlusMain : ModMain
	{
		internal static readonly DefRepository Repo = GameUtl.GameComponent<DefRepository>();
		internal static readonly SharedData Shared = GameUtl.GameComponent<SharedData>();
		/// Config is accessible at any time, if any is declared.		
		public new SuperCheatsModPlusConfig Config
		{
			get
			{
				return (SuperCheatsModPlusConfig)base.Config;
			}
		}


		/// This property indicates if mod can be Safely Disabled from the game.
		/// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
		/// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
		public override bool CanSafelyDisable => true;

		/// <summary>
		/// Callback for when mod is enabled. Called even on game starup.
		/// </summary>
		public override void OnModEnabled()
		{

			/// All mod dependencies are accessible and always loaded.
			int c = base.Dependencies.Count<ModEntry>();
			/// Mods have their own logger. Message through this logger will appear in game console and Unity log file.
			Logger.LogInfo($"Say anything crab people-related.");
			/// Metadata is whatever is written in meta.json
			string v = MetaData.Version.ToString();
			/// Game creates Harmony object for each mod. Accessible if needed.
			Harmony harmony = (Harmony)base.HarmonyInstance;
			/// Mod instance is mod's runtime representation in game.
			string id = Instance.ID;
			/// Game creates Game Object for each mod. 
			GameObject go = ModGO;
			/// PhoenixGame is accessible at any time.
			PhoenixGame game = GetGame();
			/// Apply any general game modifications.
			try
			{
				((Harmony)base.HarmonyInstance).PatchAll(base.GetType().Assembly);
			}
			catch (Exception e)
			{
				base.Logger.LogInfo(e.ToString() ?? "");
			}
			MountedWeaponsMechArms.Change_Augmentations();
			InstantStuffAndDiscounts.Change_Time();
			EliteSoldiers.EliteSquad();
			Corruption.Change_Corruption();
			Patches.Change_Patches();
			MutationsAndAugmentations.Change_PermanentAug();
			OtherChanges.Change_Others();
			PromoSkinArmor.Create_PromoSkinArmor();
			SoldierSkills.Skills();
			OpArmor.Change_Armor();		
		}

		/// <summary>
		/// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
		/// Guaranteed to have OnModEnabled before.
		/// </summary>
		public override void OnModDisabled()
		{
			/// Undo any game modifications if possible. Else "CanSafelyDisable" must be set to false.
			/// ModGO will be destroyed after OnModDisabled.
		}

		/// <summary>
		/// Callback for when any property from mod's config is changed.
		/// </summary>
		public override void OnConfigChanged()
		{
			/// Config is accessible at any time.
		}


		/// <summary>
		/// In Phoenix Point there can be only one active level at a time. 
		/// Levels go through different states (loading, unloaded, start, etc.).
		/// General puprose level state change callback.
		/// </summary>
		/// <param name="level">Level being changed.</param>
		/// <param name="prevState">Old state of the level.</param>
		/// <param name="state">New state of the level.</param>
		public override void OnLevelStateChanged(Level level, Level.State prevState, Level.State state)
		{
			/// Alternative way to access current level at any time.
			Level l = GetLevel();
		}

		/// <summary>
		/// Useful callback for when level is loaded, ready, and starts.
		/// Usually game setup is executed.
		/// </summary>
		/// <param name="level">Level that starts.</param>
		public override void OnLevelStart(Level level)
		{
		}

		/// <summary>
		/// Useful callback for when level is ending, before unloading.
		/// Usually game cleanup is executed.
		/// </summary>
		/// <param name="level">Level that ends.</param>
		public override void OnLevelEnd(Level level)
		{
		}
	}		
}
