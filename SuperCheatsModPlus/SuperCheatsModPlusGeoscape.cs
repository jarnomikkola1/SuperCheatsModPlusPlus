using Base.Serialization.General;
using PhoenixPoint.Geoscape.Entities;
using PhoenixPoint.Geoscape.Levels;
using PhoenixPoint.Modding;
using System.Collections.Generic;

namespace SuperCheatsModPlus
{
	/// <summary>
	/// Mod's custom save data for geoscape.
	/// </summary>
	[SerializeType(SerializeMembersByDefault = SerializeMembersType.SerializeAll)]
	public class SuperCheatsModPlusGSInstanceData
	{
		//public bool OpArmorAbilitiesEnabled = true;		
		//public bool VenomTorsoCanUseBothHands = true;		
		//public bool RemovableMutationsAndAugmentations = true;	
		//public bool MutationsCanEquipHeadItems = true;		
		//public bool UseMountedWeaponsManyTimesPerTurn = true;		
		//public bool UseMountedWeaponAndMechArmOnAugments = true;		
		//public bool FreeBionicRepair = true;		
		//public bool OpSoldierSkills = true;
		//public bool RemoteControlBuff = true;
		//public bool OpLivingWeapons = true;
		//public bool ArchAngelRL1HasBlastRadius = true;		
		//public bool InstantResearch = true;
		//public bool InstantManufacturing = true;
		//public bool InstantFacilityConstruction = true;		
		//public bool FacilitiesDoNotRequirePower = true;
		//public bool UnlockAllFacilities = true;		
		//public bool EverythingHalfOff = true;		
		//public bool ManufactureEverything = true;		
		//public bool StartWithEliteSoldiers = true;		
		//public bool DisableCorruption = true;		
		//public bool IncreaseSoldierInventorySlots = true;		
		//public bool UnlockAllBionics = true;	
		//public bool UnlockAllMutations = true;		
		//public bool MaxLevelSoldiers = true;		
		//public bool Give350XPToAllSoldiersOnce = true;		
		//public bool InfiniteSpecialPoints = true;
		//public bool Get350SpecialPointsOnce = true;		
		//public bool UnlockAllSpecializations = true;		
		//public bool Get10ThousandOfAllResources = true;
		//public bool HealAllSoldiersAfterMissions = true;
		//public bool NeverGetTiredOrExhausted = true;
		//public bool OPKaosWeapons = true;
		//public bool GoldArmorSkinsHaveSpecialAbilities = true;
	}

	/// <summary>
	/// Represents a mod instance specific for Geoscape game.
	/// Each time Geoscape level is loaded, new mod's ModGeoscape is created.
	/// </summary>
	public class SuperCheatsModPlusGeoscape : ModGeoscape
	{
		/// <summary>
		/// Called when Geoscape starts.
		/// </summary>
		public override void OnGeoscapeStart() {
			/// Geoscape level controller is accessible at any time.
			GeoLevelController gsController = Controller;
			/// ModMain is accesible at any time
			SuperCheatsModPlusMain main = (SuperCheatsModPlusMain)Main;
		}
		/// <summary>
		/// Called when Geoscape ends.
		/// </summary>
		public override void OnGeoscapeEnd() {

		}

		/// <summary>
		/// Called when Geoscape save is going to be generated, giving mod option for custom save data.
		/// </summary>
		/// <returns>Object to serialize or null if not used.</returns>
		public override object RecordGeoscapeInstanceData() {
			return new SuperCheatsModPlusGSInstanceData() {};
		}
		/// <summary>
		/// Called when Geoscape save is being process. At this point level is already created, but GeoscapeStart is not called.
		/// </summary>
		/// <param name="instanceData">Instance data serialized for this mod. Cannot be null.</param>
		public override void ProcessGeoscapeInstanceData(object instanceData) {
			SuperCheatsModPlusGSInstanceData data = (SuperCheatsModPlusGSInstanceData)instanceData;
		}

		/// <summary>
		/// Called when new Geoscape world is generating. This only happens on new game.
		/// Useful to modify initial spawned sites.
		/// </summary>
		/// <param name="setup">Main geoscape setup object.</param>
		/// <param name="worldSites">Sites to spawn and start simulating.</param>
		public override void OnGeoscapeNewWorldInit(GeoInitialWorldSetup setup, IList<GeoSiteSceneDef.SiteInfo> worldSites) {
		}
		/// <summary>
		/// Called when generated Geoscape world will pass through simulation step. This only happens on new game.
		/// Useful to modify game startup setup before simulation.
		/// </summary>
		/// <param name="setup">Main geoscape setup object.</param>
		/// <param name="context">Context for game setup.</param>
		public override void OnGeoscapeNewWorldSimulationStart(GeoInitialWorldSetup setup, GeoInitialWorldSetup.SimContext context) {
		}
	}
}
