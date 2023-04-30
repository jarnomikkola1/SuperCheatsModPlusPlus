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
		[ConfigField(text: "Max Augmentations",
		description: "Max Augmentations")]
		public int MaxAugmentations = 2;

		[ConfigField(text: "Op Armor Abilities",
		description: "Give OP Abilities To Mutations/Augmentations")]
		public  bool OpArmorAbilitiesEnabled = false;

		[ConfigField(text: "Venom Torso Can Use Two Handed Weapons",
		description: "Venom Torso Can Now Use Two Handed Weapons")]
		public  bool VenomTorsoCanUseBothHands = false;
		
		[ConfigField(text: "Removable Mutations And Augmentations",
		description: "Mutations/Augmentations Can Now Be Equipped And Unequipped like Normal Armor")]	
		public  bool RemovableMutationsAndAugmentations = false;

		[ConfigField(text: "Allow Head Mutations To Equip Items",
		description: "Mutation Heads Can Equip Modules")]
		public  bool MutationsCanEquipHeadItems = false;

		[ConfigField(text: "Use Mounted Weapons Unlimited Times Per Turn",
		description: "Mounted Weapons Can Be Used As many Times Per Turn As You Want")]
		public  bool UseMountedWeaponsManyTimesPerTurn = false;

		[ConfigField(text: "All Mutations/Augmentations Can Equip Mounted Weapons/Mech Arms",
		description: "All Mutations/Augmentations Can Equip Mounted Weapons/Mech Arms")]
		public  bool UseMountedWeaponAndMechArmOnAugments = false;

		[ConfigField(text: "Free Bionic Repairs",
		description: "Bionics Get Repaired After Every Mission")]
		public  bool FreeBionicRepair = false;

		[ConfigField(text: "Op Soldier Skills",
		description: "Assault Has Rally, Sniper Has Gunslinger, Heavy Has Original OP Rage Burst")]
		public  bool OpSoldierSkills = false;

		[ConfigField(text: "Remote Control Buff",
		description: "Remote Controle Costs 1 Ap And 1 Wp To Use")]
		public  bool RemoteControlBuff = false;

		[ConfigField(text: "Op Living Weapons",
		description: "Gives A Big Buff To All Living Weapons")]
		public  bool OpLivingWeapons = false;

		[ConfigField(text: "ArchAngel RL1 Has A Blast Radius",
		description: "Gives The RL1 a Blast Radius Like All other Rocket/Grenade Launchers")]
		public  bool ArchAngelRL1HasBlastRadius = false;

		[ConfigField(text: "Instant Research",
		description: "All Research Is Instant ")]
		public  bool InstantResearch = false;

		[ConfigField(text: "Instant Manufacturing",
		description: "All Manufacturing Is Instant")]
		public  bool InstantManufacturing = false;

		[ConfigField(text: "Instant Facility Construction",
		description: "Instant Facility Constructio")]
		public  bool InstantFacilityConstruction = false;

		[ConfigField(text: "Facilities Do Not RequirePower",
		description: "Facilities Do Not RequirePower")]
		public  bool FacilitiesDoNotRequirePower = false;

		[ConfigField(text: "Unlock All Facilities",
		description: "Unlock All Facilities")]
		public  bool UnlockAllFacilities = false;

		[ConfigField(text: "Everything Half Off",
		description: "Buy All Weapons, Armor, Vehicles, Modules, Items, etc... At A 50% Discount")]
		public  bool EverythingHalfOff = false;

		[ConfigField(text: "Manufacture Everything",
		description: "Unlock Everything For Manufacturing, Even Promo Skins And Hidden Weapons Like Biogas Launcher")]
		public  bool ManufactureEverything = false;

		[ConfigField(text: "Start With Elite Soldiers",
		description: "Start The Game With Tobias West, Pirate King, The Exalted, Synedrion Leader, And Godly")]
		public  bool StartWithEliteSoldiers = false;

		[ConfigField(text: "Disable Corruption",
		description: "Turn Off Corruption")]
		public  bool DisableCorruption = false;

		[ConfigField(text: "Increase Backpack Slots From 6 To 9",
		description: "Allows Backpack To Hold 9 Items")]
		public  bool IncreaseSoldierInventorySlots = false;

		[ConfigField(text: "Unlock All Bionics",
		description: "Unlock All Bionics With Only The First Bionics Research")]
		public  bool UnlockAllBionics = false;

		[ConfigField(text: "Unlock All Mutations",
		description: "Unlock All Mutations With Only The First Mutation Research")]
		public  bool UnlockAllMutations = false;

		[ConfigField(text: "Max Level Soldiers",
		description: "Give 9K XP To All Soldiers Everytime You Enter Geoscape")]
		public  bool MaxLevelSoldiers = false;

		[ConfigField(text: "Give 350 XP To All Soldiers Once",
		description: "Gives 350 XP To All Soldiers Once When You Enter Geoscape")]
		public  bool Give350XPToAllSoldiersOnce = false;

		[ConfigField(text: "Infinite Special Points",
		description: "Gives 9k SP Everytime You Enter Geoscape")]
		public  bool InfiniteSpecialPoints = false;

		[ConfigField(text: "Get 350 Special Points Once",
		description: "Gives 350 SP To All Soldiers Once When You Enter Geoscape")]
		public  bool Get350SpecialPointsOnce = false;

		[ConfigField(text: "Unlock All Specializations",
		description: "Unlock All Specializations From The Beginning Of The Game")]
		public  bool UnlockAllSpecializations = false;

		[ConfigField(text: "Get 10K Of Each Resource",
		description: "Get 10K Of Each Resource")]
		public  bool Get10ThousandOfAllResources = false;

		[ConfigField(text: "Heal All Soldiers After Missions",
		description: "Automatically Heal All Soldiers After Missions")]
		public  bool HealAllSoldiersAfterMissions = false;

		[ConfigField(text: "Never Get Tired Or Exhausted",
		description: "Never Get Tired Or Exhausted")]
		public  bool NeverGetTiredOrExhausted = false;

		[ConfigField(text: "OP Kaos Weapons",
		description: "Gives A Big Buffs To Kaos Weapons")]
		public  bool OPKaosWeapons = false;

		[ConfigField(text: "Gold Armor Skins Have Special Abilities",
		description: "Gold Promo Armor Set Have Special Abilities")]
		public  bool GoldArmorSkinsHaveSpecialAbilities = false;
		
		[ConfigField(text: "Adjust Personal Perks Amount",
		description: "Adjust Amount Of Personal Perks Soldiers Get, Max 7")]	
		public int PersonalAbilitiesCount = 3;
		
		[ConfigField(text: "Adjust Maximum Strength",
		description: "vanilla default is 35")]		
		public int MaxStrength = 35;
		
		[ConfigField(text: "Adjust Maximum Willpower",
		description: "vanilla default is 20")]		
		public int MaxWill = 20;
		
		[ConfigField(text: "Adjust Maximum Speed",
		description: "vanilla default is 20")]
		public int MaxSpeed = 20;
		
		[ConfigField(text: "Adjust Maximum Stamina",
		description: "vanilla default is 40")]
		public int Stamina = 40;
		
		[ConfigField(text: "Adjust Tired Status Stamina Below",
		description: "Soldiers will get the status 'Tired' when their stamina falls be below this value (percentage), vanilla default is 25%, 30")]		
		public int TiredStatusStaminaBelow = 30;
		
		[ConfigField(text: "Adjust Exhausted Status Stamina Below",
		description: "Soldiers will get the status 'Exhausted' when their stamina falls be below this value (percentage), vanilla default is 0%, 10")]
		public int ExhaustedStatusStaminaBelow = 10;		

		[ConfigField(text: "Recruit Generation Count",
		description: "Amount Of Recruits Generated By Phoenix Faction")]
		public int RecruitGenerationCount = 3;

		[ConfigField(text: "Recruit Interval Check Days",
		description: "Number Of Days It Takes For New Recruits To Be Generated, Vanilla is 3")]
		public float RecruitIntervalCheckDays = 3f;

		[ConfigField(text: "Recruit Generation Has Armor",
		description: "Recruits Comes With Armor")]
		public bool RecruitGenerationHasArmor = true;

		[ConfigField(text: "Recruit Generation Has Weapons",
		description: "Recruits Comes With A Weapon")]
		public bool RecruitGenerationHasWeapons = true;

		[ConfigField(text: "Recruit Generation Has Medkit",
		description: "Recruits Comes With Medkit")]
		public bool RecruitGenerationHasConsumableItems = true;

		[ConfigField(text: "Recruit Generation Has Inventory Items",
		description: "Allows For Recruits To Come With Inventory Items")]
		public bool RecruitGenerationHasInventoryItems = true;

		[ConfigField(text: "Recruit Generation Has Augmentations",
		description: "Allows For Recruits To Come With Augmentations")]
		public bool RecruitGenerationCanHaveAugmentations = false;

		[ConfigField(text: "Return Fire Angle",
		description: "Maximum angle in which return fire is possible at all. Vanilla didn't check at all, returned fire for 360 degrees.")]
		public int ReturnFireAngle = 360;

		[ConfigField(text: "Return Fire Limit",
		description: "Limit the ability to return fire to X times per round, vanilla default is unlimited.")]
		public int ReturnFireLimit = 99;

		[ConfigField(text: "No Return Fire When Stepping Out Of Cover",
		description: "Disables return fire when the attacker side-steps from full cover.")]
		public bool NoReturnFireWhenSteppingOut = false;

		[ConfigField(text: "Emphasize Return Fire Hint",
		description: "Enlarges and animates the small return fire icon indicating the target's ability to return fire.")]
		public bool EmphasizeReturnFireHint = true;

		[ConfigField(text: "Max Player Units Add",
		description: "Adds to the maximum squad size for tactical missions. With a current vanilla default of 8, a value of 2 means you can bring up to 10 soldiers")]
		public int MaxPlayerUnitsAdd = 0;

		[ConfigField(text: "Always Recover All Items From Tactical Missions",
		description: "Recover All Dropped Items, Makes scavenging missions somewhat pointless")]		
		public bool AlwaysRecoverAllItemsFromTacticalMissions = false;

		[ConfigField(text: "Item Destruction Chance",
		description: "Base chance for items to be destroyed when dropped by a dying enemy.")]
		public int ItemDestructionChance = 80;

		[ConfigField(text: "Allow Weapon Drops",
		description: "Allows for weapons to drop too.")]
		public bool AllowWeaponDrops = true;

		[ConfigField(text: "Flat Weapon Destruction Chance",
		description: "If weapon drops are allowed, this is the chance for them to be destroyed")]		
		public int FlatWeaponDestructionChance = 60;
		
		[ConfigField(text: "Allow Armor Drops",
		description: "Allows for armor to drop too")]
		public bool AllowArmorDrops = true;

		[ConfigField(text: "Flat Armor Destruction Chance",
		description: "If armor drops are allowed, this is the chance for them to be destroyed")]		
		public int FlatArmorDestructionChance = 70;


		//[ConfigField(text: "EnableScrapAircraft",
		//description: "Allows Aircraft To Be Scraped For Resources")]
		//public bool EnableScrapAircraft = false;

		[ConfigField(text: "Skip Intro Logos",
		description: "Skips logos when loading up the game")]
		public bool SkipIntroLogos = true;
		
		[ConfigField(text: "Skip Intro Movie",
		description: "Skips intro movie")]
		public bool SkipIntroMovie = true;
		
		[ConfigField(text: "Skip Landing Sequences",
		description: "Skips landing sequences before tactical missions")]
		public bool SkipLandingSequences = true;

		[ConfigField(text: "Aircraft edits",
		description: "Applies the following edits to the game in enabled")]
		public bool AircraftEdits = false;

		[ConfigField(text: "Aircraft Tiamat Speed",
		description: "Maximum speed for the Tiamat")]
		public float AircraftBlimpSpeed = 250f;

		[ConfigField(text: "Aircraft Thunderbird Speed",
		description: "Maximum speed for the Thunderbird")]
		public float AircraftThunderbirdSpeed = 380f;

		[ConfigField(text: "Aircraft Manticore Speed",
		description: "Maximum speed for the Manticore")]
		public float AircraftManticoreSpeed = 500f;
				     
		[ConfigField(text: "Aircraft Helios Speed",
		description: "Maximum speed for the Helios")]
		public float AircraftHeliosSpeed = 650f;

		[ConfigField(text: "Aircraft Tiamat Space",
		description: "Maximum soldier capacity for the Tiamat")]
		public int AircraftBlimpSpace = 8;

		[ConfigField(text: "Aircraft Thunderbird Space",
		description: "Maximum soldier capacity for the Thunderbird")]
		public int AircraftThunderbirdSpace = 7;

		[ConfigField(text: "Aircraft Manticore Space",
		description: "Maximum soldier capacity for the Manticore")]
		public int AircraftManticoreSpace = 6;

		[ConfigField(text: "Aircraft Helios Space",
		description: "Maximum soldier capacity for the Helios")]
		public int AircraftHeliosSpace = 5;

		[ConfigField(text: "Aircraft Tiamat Range",
		description: "Maximum range for the Tiamat")]
		public float AircraftBlimpRange = 4000f;

		[ConfigField(text: "Aircraft Thunderbird Range",
		description: "Maximum range for the Thunderbird")]		
		public float AircraftThunderbirdRange = 3000f;

		[ConfigField(text: "Aircraft Manticore Range",
		description: "Maximum range for the Manticore")]
		public float AircraftManticoreRange = 2500f;

		[ConfigField(text: "Aircraft Helios Range",
		description: "Maximum range for the Helios")]
		public float AircraftHeliosRange = 3500f;

		[ConfigField(text: "Occupying Space Mutog",
		description: "Size of Mutogs for squad/space calculations")]
		public int OccupyingSpaceMutog = 2;

		[ConfigField(text: "Occupying Space Armadillo",
		description: "Size of Armadillos for squad/space calculations")]
		public int OccupyingSpaceArmadillo = 3;

		[ConfigField(text: "Occupying Space Scarab",
		description: "Size of Scarabs for squad/space calculations")]
		public int OccupyingSpaceScarab = 3;

		[ConfigField(text: "Occupying Space Aspida",
		description: "Size of Aspida for squad/space calculations")]
		public int OccupyingSpaceAspida = 3;

		[ConfigField(text: "Occupying Space Kaos Buggy",
		description: "Size of Kaos Buggy for squad/space calculations")]
		public int OccupyingSpaceKaosBuggy = 3;

		[ConfigField(text: "Deployment to Food Harvest Ratio",
		description: "This is the percentage amount of mutagen harvested from pandoran creatures from what the game describes as it's deployment cost.")]
		public float DepFoodRatio = 30f;

		[ConfigField(text: "Deployment to Mutagen Harvest Ratio",
		description: "This is the percentage amount of mutagen harvested from pandoran creatures from what the game describes as it's deployment cost.")]
		public float DepMutagenRatio = 50f;

		[ConfigField(text: "Shield deployment cost",
		description: "This is the percentage amount of action points shilds deployment costs for the player.")]
		public float DepSiheldCost = 25f;

		[ConfigField(text: "Heavy Armors Jumpjet edits",
		description: "Gives heavy armor rocket leap ability and removes the fumble chance")]
		public bool HeavyEdits = false;

		[ConfigField(text: "Enables the skill point edits",
		description: "Just a switch to the following edits")]
		public bool skilloverride = false;

		[ConfigField(text: "Default skill points per mission",
		description: "This setting overwrites all the difficulty settings.")]
		public int SkillPointsPerMission = 5;

		[ConfigField(text: "Default skill points per level",
		description: "...")]
		public int Skillpointamount = 20;

		[ConfigField(text: "Disable Ambushes",
		description: "Disables ambushes when exploring sites.")]
		public bool DisableAmbushes = false;

		[ConfigField(text: "Retain Ambushes Inside Mist",
		description: "When sites are inside the mist ambushes are still a possibility.")]
		public bool RetainAmbushesInsideMist = true;
		//
		//[ConfigField(text: "Recover From Paralysis And Virus Faster",
		//description: "Recover From Paralysis And Virus Faster0")]			
		//public bool FastMetabolism = false;

		//[ConfigField(text: "Turn On Other Adjustments",
		//description: "Turn On Other Adjustments")]
		//public  bool TurnOnOtherAdjustments = false;
		//
		//[ConfigField(text: "Other Adjustments Vehicle Bay Aircraft Slots",
		//description: "Modify The Amount Of Aircraft Slots Vehicle Bay Can Hold")]
		//public  int VehicleBayAircraftSlots = 2;
		//
		//[ConfigField(text: "Other Adjustments Ground Vehicle Slots",
		//description: "Modify The Amount Of Ground Vehicle Slots Vehicle Bay Can Hold")]
		//public  int VehicleBayGroundVehicleSlots = 2;
		//
		//[ConfigField(text: "Vehicle Bay Aircraft Heal Amount",
		//description: "Modify The Amount Of Healing Vehicle Bay gives Per Hour")]
		//public  int VehicleBayAircraftHealAmount = 48;
		//
		//[ConfigField(text: "Vehicle Bay Ground Vehicle Heal Amount",
		//description: "Modify The Amount Of Healing Vehicle Bay gives Per Hour")]
		//public  int VehicleBayGroundVehicleHealAmount = 20;
	}
}
