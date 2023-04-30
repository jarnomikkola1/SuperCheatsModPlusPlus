using System.Linq;
using Base.Defs;
using PhoenixPoint.Tactical.Entities.Weapons;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Entities;
using Base.Core;
using PhoenixPoint.Geoscape.Entities.Research;
using PhoenixPoint.Geoscape.Entities.Interception.Equipments;
using PhoenixPoint.Geoscape.Entities.Abilities;
using PhoenixPoint.Common.Core;

namespace SuperCheatsModPlus
{
    internal class InstantStuffAndDiscounts
    {
        public static void Change_Time()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;
            if (Config.InstantManufacturing == true)
            {
                foreach (TacticalItemDef item in Repo.GetAllDefs<TacticalItemDef>())
                {
                    item.ManufacturePointsCost = 0;
                }
                foreach (WeaponDef weapon in Repo.GetAllDefs<WeaponDef>())
                {
                    weapon.ManufacturePointsCost = 0;
                }
                foreach (GroundVehicleItemDef item in Repo.GetAllDefs<GroundVehicleItemDef>())
                {
                    item.ManufacturePointsCost = 0;
                }
                foreach (VehicleItemDef item in Repo.GetAllDefs<VehicleItemDef>())
                {
                    item.ManufacturePointsCost = 0;
                }
                foreach (GeoVehicleWeaponDef item1 in Repo.GetAllDefs<GeoVehicleWeaponDef>())
                {
                    item1.ManufacturePointsCost = 0;
                }
                foreach (GeoVehicleModuleDef item2 in Repo.GetAllDefs<GeoVehicleModuleDef>())
                {
                    item2.ManufacturePointsCost = 0;
                }
            }

            if (Config.InstantResearch == true)
            {
                foreach (ResearchDef rd in Repo.GetAllDefs<ResearchDef>().Where(rd => rd.name.StartsWith("PX_")))
                {
                    rd.ResearchCost = 0;
                }
            }

            if (Config.InstantFacilityConstruction == true)
            {
                foreach (PhoenixFacilityDef facility in Repo.GetAllDefs<PhoenixFacilityDef>().Where(rd => rd.name.Contains("PhoenixFacilityDef")))
                {
                    facility.ConstructionTimeDays = 0;
                }
            }

            if (Config.FacilitiesDoNotRequirePower == true)
            {
                foreach (PhoenixFacilityDef facility in Repo.GetAllDefs<PhoenixFacilityDef>().Where(rd => rd.name.Contains("PhoenixFacilityDef")))
                {
                    facility.PowerCost = 0;
                }
            }

            if (Config.EverythingHalfOff == true)
            {
                ActivateBaseAbilityDef baseCost = Repo.GetAllDefs<ActivateBaseAbilityDef>().FirstOrDefault(rd => rd.name.Contains("ActivateBaseAbilityDef"));
                TheMarketplaceSettingsDef MarketPlaceCost = Repo.GetAllDefs<TheMarketplaceSettingsDef>().FirstOrDefault(rd => rd.name.Contains("TheMarketplaceSettingsDef"));

                baseCost.Cost *= 0.5f;
                MarketPlaceCost.PriceModulus *= 0.5f;

                for (int i = 0; i < MarketPlaceCost.TheMarketplaceItemPriceMultipliers.Length; i++)
                {
                    MarketPlaceCost.TheMarketplaceItemPriceMultipliers[i].PriceMultiplier *= 0.5f;
                }


                foreach (PhoenixFacilityDef facility in Repo.GetAllDefs<PhoenixFacilityDef>().Where(rd => rd.name.Contains("PhoenixFacilityDef")))
                {
                    facility.ResourceCost /= 2;
                }
                foreach (TacticalItemDef item in Repo.GetAllDefs<TacticalItemDef>())
                {
                    item.ManufactureMaterials *= 0.5f;
                    item.ManufactureTech *= 0.5f;
                    item.ManufactureMutagen *= 0.5f;
                    item.ManufactureLivingCrystals *= 0.5f;
                    item.ManufactureProteanMutane *= 0.5f;
                    item.ManufactureOricalcum *= 0.5f;
                }
                //foreach (WeaponDef weapon in Repo.GetAllDefs<WeaponDef>())
                //{
                //    weapon.ManufactureMaterials *= 0.5f;
                //    weapon.ManufactureTech *= 0.5f;
                //    weapon.ManufactureMutagen *= 0.5f;
                //    weapon.ManufactureLivingCrystals *= 0.5f;
                //    weapon.ManufactureProteanMutane *= 0.5f;
                //    weapon.ManufactureOricalcum *= 0.5f;
                //}
                foreach (GroundVehicleItemDef item in Repo.GetAllDefs<GroundVehicleItemDef>())
                {
                    item.ManufactureMaterials *= 0.5f;
                    item.ManufactureTech *= 0.5f;
                    item.ManufactureMutagen *= 0.5f;
                    item.ManufactureLivingCrystals *= 0.5f;
                    item.ManufactureProteanMutane *= 0.5f;
                    item.ManufactureOricalcum *= 0.5f;
                }
                //foreach (GroundVehicleModuleDef item in Repo.GetAllDefs<GroundVehicleModuleDef>())
                //{
                //    item.ManufactureMaterials *= 0.5f;
                //    item.ManufactureTech *= 0.5f;
                //    item.ManufactureMutagen *= 0.5f;
                //    item.ManufactureLivingCrystals *= 0.5f;
                //    item.ManufactureProteanMutane *= 0.5f;
                //    item.ManufactureOricalcum *= 0.5f;
                //}
                //foreach (GroundVehicleWeaponDef item in Repo.GetAllDefs<GroundVehicleWeaponDef>())
                //{
                //    item.ManufactureMaterials *= 0.5f;
                //    item.ManufactureTech *= 0.5f;
                //    item.ManufactureMutagen *= 0.5f;
                //    item.ManufactureLivingCrystals *= 0.5f;
                //    item.ManufactureProteanMutane *= 0.5f;
                //    item.ManufactureOricalcum *= 0.5f;
                //}

                foreach (GeoVehicleWeaponDef item2 in Repo.GetAllDefs<GeoVehicleWeaponDef>())
                {
                    item2.ManufactureMaterials *= 0.5f;
                    item2.ManufactureTech *= 0.5f;
                    item2.ManufactureMutagen *= 0.5f;
                    item2.ManufactureLivingCrystals *= 0.5f;
                    item2.ManufactureProteanMutane *= 0.5f;
                    item2.ManufactureOricalcum *= 0.5f;
                }
                foreach (GeoVehicleModuleDef item3 in Repo.GetAllDefs<GeoVehicleModuleDef>())
                {
                    item3.ManufactureMaterials *= 0.5f;
                    item3.ManufactureTech *= 0.5f;
                    item3.ManufactureMutagen *= 0.5f;
                    item3.ManufactureLivingCrystals *= 0.5f;
                    item3.ManufactureProteanMutane *= 0.5f;
                    item3.ManufactureOricalcum *= 0.5f;
                }
                foreach (VehicleItemDef item in Repo.GetAllDefs<VehicleItemDef>())
                {
                    item.ManufactureMaterials *= 0.5f;
                    item.ManufactureTech *= 0.5f;
                    item.ManufactureMutagen *= 0.5f;
                    item.ManufactureLivingCrystals *= 0.5f;
                    item.ManufactureProteanMutane *= 0.5f;
                    item.ManufactureOricalcum *= 0.5f;
                }
            }
        }
    }
}
