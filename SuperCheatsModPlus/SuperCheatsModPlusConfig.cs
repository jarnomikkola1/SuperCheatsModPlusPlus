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
using PhoenixPoint.Modding;

namespace SuperCheatsModPlus
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class SuperCheatsModPlusConfig : ModConfig
	{
		/// Only public fields are serialized.
		/// Supported types for in-game UI are:
		 [ConfigField(text: "Mutations/Augmentations Abilities",
		description: "Give OP Abilities To Mutations/Augmentations")]
		public static bool OpArmorAbilitiesEnabled = true;

		[ConfigField(text: "Venom Torso Tweak",
		description: "Venom Torso Can Now Use Two Handed Weapons")]
		public static bool VenomTorsoCanUseBothHands = true;
		
		[ConfigField(text: "Removable Mutations And Augmentations",
		description: "Mutations/Augmentations Can Now Be Equipped And Unequipped like Normal Armor")]	
		public static bool RemovableMutationsAndAugmentations = true;

		[ConfigField(text: "Alllow Mutation Heads Modules",
		description: "Mutation Heads Can Equip Modules")]
		public static bool MutationsCanEquipHeadItems = true;

		[ConfigField(text: "Remove Mounted Weapon Limit",
		description: "Mounted Weapons Can Be Used As many Times Per Turn As You Want")]
		public static bool UseMountedWeaponsManyTimesPerTurn = true;

		[ConfigField(text: "All Mutations/Augmentations Can Equip Mounted Weapons/Mech Arms",
		description: "All Mutations/Augmentations Can Equip Mounted Weapons/Mech Arms")]
		public static bool UseMountedWeaponAndMechArmOnAugments = true;

		[ConfigField(text: "Free Bionic Repair",
		description: "Bionics Get Repaired After Every Mission")]
		public static bool FreeBionicRepair = true;

		[ConfigField(text: "Op Soldier Skills",
		description: "Assault Has Rally, Sniper Has Gunslinger, Heavy Has Original OP Rage Burst")]
		public static bool OpSoldierSkills = true;

		[ConfigField(text: "Remote Control Buff",
		description: "Remote Controle Costs 1 Ap And 1 Wp To Use")]
		public static bool RemoteControlBuff = true;

		[ConfigField(text: "Op Living Weapons",
		description: "Gives A Big Buff To All Living Weapons")]
		public static bool OpLivingWeapons = true;

		[ConfigField(text: "ArchAngel RL1 Blast Radius",
		description: "Gives The RL1 a Blast Radius Like All other Rocket/Grenade Launchers")]
		public static bool ArchAngelRL1HasBlastRadius = true;

		[ConfigField(text: "Instant Research",
		description: "All Research Is Instant ")]
		public static bool InstantResearch = true;

		[ConfigField(text: "Instant Manufacturing",
		description: "All Manufacturing Is Instant")]
		public static bool InstantManufacturing = true;

		[ConfigField(text: "Instant Facility Construction",
		description: "Instant Facility Constructio")]
		public static bool InstantFacilityConstruction = true;

		[ConfigField(text: "Facilities Do Not RequirePower",
		description: "Facilities Do Not RequirePower")]
		public static bool FacilitiesDoNotRequirePower = true;

		[ConfigField(text: "Unlock All Facilities",
		description: "Unlock All Facilities")]
		public static bool UnlockAllFacilities = true;

		[ConfigField(text: "Everything Half Off",
		description: "Buy All Weapons, Armor, Vehicles, Modules, Items, etc... At A 50% Discount")]
		public static bool EverythingHalfOff = true;

		[ConfigField(text: "Manufacture Everything",
		description: "Unlock Everything For Manufacturing, Even Promo Skins And Hidden Weapons Like Biogas Launcher")]
		public static bool ManufactureEverything = true;

		[ConfigField(text: "Start With Elite Soldiers",
		description: "Start The Game With Tobias West, Pirate King, The Exalted, Synedrion Leader, And Godly")]
		public static bool StartWithEliteSoldiers = true;

		[ConfigField(text: "Disable Corruption",
		description: "Turn Off Corruption")]
		public static bool DisableCorruption = true;

		[ConfigField(text: "Increase Soldier Inventory Slots",
		description: "Allows Backpack To Hold 9 Items")]
		public static bool IncreaseSoldierInventorySlots = true;

		[ConfigField(text: "Unlock All Bionics",
		description: "Unlock All Bionics With Only The First Bionics Research")]
		public static bool UnlockAllBionics = true;

		[ConfigField(text: "Unlock All Mutations",
		description: "Unlock All Mutations With Only The First Mutation Research")]
		public static bool UnlockAllMutations = true;

		[ConfigField(text: "Max Level Soldiers",
		description: "Give 9K XP To All Soldiers Everytime You Enter Geoscape")]
		public static bool MaxLevelSoldiers = true;

		[ConfigField(text: "Give 350 XP To All Soldiers Once",
		description: "Gives 350 XP To All Soldiers Once When You Enter Geoscape")]
		public static bool Give350XPToAllSoldiersOnce = true;

		[ConfigField(text: "Infinite Special Points",
		description: "Gives 9k SP Everytime You Enter Geoscape")]
		public static bool InfiniteSpecialPoints = true;

		[ConfigField(text: "Get 350 Special Points Once",
		description: "Gives 350 SP To All Soldiers Once When You Enter Geoscape")]
		public static bool Get350SpecialPointsOnce = true;

		[ConfigField(text: "Unlock All Specializations",
		description: "Unlock All Specializations From The Beginning Of The Game")]
		public static bool UnlockAllSpecializations = true;

		[ConfigField(text: "Show Me The Money",
		description: "Get 10K Of Each Resource")]
		public static bool Get10ThousandOfAllResources = true;

		[ConfigField(text: "Heal All Soldiers After Missions",
		description: "Automatically Heal All Soldiers After Missions")]
		public static bool HealAllSoldiersAfterMissions = true;

		[ConfigField(text: "Never Get Tired Or Exhausted",
		description: "Never Get Tired Or Exhausted")]
		public static bool NeverGetTiredOrExhausted = true;

		[ConfigField(text: "OP Kaos Weapons",
		description: "Gives A Big Buffs To Kaos Weapons")]
		public static bool OPKaosWeapons = true;

		[ConfigField(text: "Gold Armor Skins Have Special Abilities",
		description: "Gold Promo Armor Set Have Special Abilities")]
		public static bool GoldArmorSkinsHaveSpecialAbilities = true;

		[ConfigField(text: "Turn On Other Adjustments",
		description: "Turn On Other Adjustments")]
		public static bool TurnOnOtherAdjustments = true;

		[ConfigField(text: "Other Adjustments Vehicle Bay Aircraft Slots",
		description: "Modify The Amount Of Aircraft Slots Vehicle Bay Can Hold")]
		public static int VehicleBayAircraftSlots = 2;

		[ConfigField(text: "Other Adjustments Ground Vehicle Slots",
		description: "Modify The Amount Of Ground Vehicle Slots Vehicle Bay Can Hold")]
		public static int VehicleBayGroundVehicleSlots = 2;

		[ConfigField(text: "Vehicle Bay Aircraft Heal Amount",
		description: "Modify The Amount Of Healing Vehicle Bay gives Per Hour")]
		public static int VehicleBayAircraftHealAmount = 48;

		[ConfigField(text: "Vehicle Bay Ground Vehicle Heal Amount",
		description: "Modify The Amount Of Healing Vehicle Bay gives Per Hour")]
		public static int VehicleBayGroundVehicleHealAmount = 20;
	}
}
