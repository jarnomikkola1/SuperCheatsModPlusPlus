using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;
using Base.Core;
using Base.Defs;
using Base.UI;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsSharedData;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Entities.RedeemableCodes;
using PhoenixPoint.Common.View.ViewControllers;
using PhoenixPoint.Geoscape.Entities;
using PhoenixPoint.Geoscape.Entities.Interception.Equipments;
using PhoenixPoint.Geoscape.Levels;
using PhoenixPoint.Geoscape.Levels.Factions;
using PhoenixPoint.Geoscape.View.ViewModules;
using PhoenixPoint.Tactical.Entities.Equipments;
using UnityEngine.Events;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using SuperCheatsModPlus;
using UnityEngine;

namespace SuperCheatsModPlus
{
    internal class Patches
    {        
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        public static void Change_Patches()
        {

        }
    }
    
        [HarmonyPatch(typeof(PhoenixStatisticsManager), "OnGeoscapeLevelStart")]
        internal static class PhoenixStatisticsManager_OnGeoscapeLevelStart2
        {
           
            public static bool flag = true;
            public static bool flag2 = true;
            public static bool flag3 = true;
            
            private static void Postfix()
            {
                SuperCheatsModPlusConfig Config = new SuperCheatsModPlusConfig();
                if (Config.UnlockAllBionics == true)
                {
                    UnlockBionics();
                }
                if (Config.UnlockAllMutations == true)
                {
                    UnlockMutations();
                }
                if (Config.MaxLevelSoldiers == true)
                {
                    MaxLvLSoldiers();
                }
                if (Config.Give350XPToAllSoldiersOnce == true && flag == true)
                {
                    Give350XP();
                }
                if (Config.Get350SpecialPointsOnce == true && flag2 == true)
                {
                    Give350SP();
                }
                if (Config.InfiniteSpecialPoints == true)
                {
                    InfiniteSp();
                }
                if (Config.ManufactureEverything == true)
                {
                    ManufactureAll();
                }
                if (Config.UnlockAllSpecializations == true)
                {
                    UnlockSpecializations();
                }
                if (Config.Get10ThousandOfAllResources == true && flag3 == true)
                {
                    GetResources();
                }
                if (Config.HealAllSoldiersAfterMissions == true)
                {
                    SetHp();
                }
                if (Config.NeverGetTiredOrExhausted == true)
                {
                    SetStamina();
                }
            }

            private static void Give350XP()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        int exp = 350;
                        (from p in GeoLevelController._ConsoleGetLevelController().ViewerFaction.Characters
                         where p.Progression != null
                         select p).ForEach(delegate (GeoCharacter c)
                         {
                             c.Progression.LevelProgression.AddExperience(exp);
                         });
                    }
                }
                flag = false;
            }

            private static void Give350SP()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        int skillPoints = 350;
                        GeoLevelController._ConsoleGetLevelController().PhoenixFaction.Skillpoints += skillPoints;
                    }
                }
                flag2 = false;
            }

            private static void SetStamina()
            {
                int stamina = 100;

                (from c in GeoLevelController._ConsoleGetLevelController().ViewerFaction.Characters
                 where c.Fatigue != null
                 select c).ForEach(delegate (GeoCharacter c)
                 {
                     c.Fatigue.Stamina.Set((float)stamina, true);
                 });
            }

            private static void SetHp()
            {
                int hp = 500;
                GeoLevelController geoLevelController = GeoLevelController._ConsoleGetLevelController();
                geoLevelController.ViewerFaction.Characters.ForEach(delegate (GeoCharacter c)
                {
                    c.Health.Set((float)hp, true);
                });
                foreach (GeoCharacter geoCharacter in geoLevelController.ViewerFaction.Characters.ToArray<GeoCharacter>())
                {
                    if (!geoCharacter.IsAlive)
                    {
                        geoLevelController.ViewerFaction.KillCharacter(geoCharacter, CharacterDeathReason.Cheat);
                    }
                }
            }

            private static void GetResources()
            {
                GeoLevelController geoLevelController = GeoLevelController._ConsoleGetLevelController();
                ResourcePack resourcePack = new ResourcePack();
                foreach (object obj in Enum.GetValues(typeof(ResourceType)))
                {
                    resourcePack.Add(new ResourceUnit((ResourceType)obj, 10000f));
                }
                geoLevelController.ViewerFaction.Wallet.Give(resourcePack, OperationReason.Cheat);
                flag3 = false;
            }

            private static void UnlockSpecializations()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelControllers = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelControllers != null)
                    {
                        string spec1 = "InfiltratorSpecializationDef";
                        string spec2 = "TechnicianSpecializationDef";
                        string spec3 = "BerserkerSpecializationDef";
                        string spec4 = "PriestSpecializationDef";
                        GeoLevelController geoLevelController = GeoLevelController._ConsoleGetLevelController();
                        DefRepository defRepository = GameUtl.GameComponent<DefRepository>();
                        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
                        SpecializationDef specializationDef1 = defRepository.GetAllDefs<SpecializationDef>().FirstOrDefault((SpecializationDef i) => i.name.IndexOf(spec1, stringComparison) >= 0);
                        if (specializationDef1 != null)
                        {
                            geoLevelController.PhoenixFaction.AddSpecialization(specializationDef1);
                        }
                        SpecializationDef specializationDef2 = defRepository.GetAllDefs<SpecializationDef>().FirstOrDefault((SpecializationDef i) => i.name.IndexOf(spec2, stringComparison) >= 0);
                        if (specializationDef2 != null)
                        {
                            geoLevelController.PhoenixFaction.AddSpecialization(specializationDef2);
                        }
                        SpecializationDef specializationDef3 = defRepository.GetAllDefs<SpecializationDef>().FirstOrDefault((SpecializationDef i) => i.name.IndexOf(spec3, stringComparison) >= 0);
                        if (specializationDef3 != null)
                        {
                            geoLevelController.PhoenixFaction.AddSpecialization(specializationDef3);
                        }
                        SpecializationDef specializationDef4 = defRepository.GetAllDefs<SpecializationDef>().FirstOrDefault((SpecializationDef i) => i.name.IndexOf(spec4, stringComparison) >= 0);
                        if (specializationDef4 != null)
                        {
                            geoLevelController.PhoenixFaction.AddSpecialization(specializationDef4);
                        }
                    }
                }
            }

            private static void InfiniteSp()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        int skillPoints = 9999;
                        GeoLevelController._ConsoleGetLevelController().PhoenixFaction.Skillpoints += skillPoints;
                    }
                }
            }

            private static void MaxLvLSoldiers()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        int exp = 9999;
                        (from p in GeoLevelController._ConsoleGetLevelController().ViewerFaction.Characters
                         where p.Progression != null
                         select p).ForEach(delegate (GeoCharacter c)
                         {
                             c.Progression.LevelProgression.AddExperience(exp);
                         });
                    }
                }
            }

            private static void UnlockBionics()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        GameTagDef bionicsTag = GameUtl.GameComponent<SharedData>().SharedGameTags.BionicalTag;
                        List<ItemDef> enumerable = (from p in GameUtl.GameComponent<DefRepository>().GetAllDefs<ItemDef>()
                                                    where p.Tags.Contains(bionicsTag) && p.ViewElementDef != null
                                                    select p).ToList<ItemDef>();
                        GeoLevelController._ConsoleGetLevelController().ViewerFaction.UnlockedAugmentations.AddRange(enumerable);
                    }
                }
            }

            private static void UnlockMutations()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        GameTagDef mutationTag = GameUtl.GameComponent<SharedData>().SharedGameTags.AnuMutationTag;
                        List<ItemDef> enumerable = (from p in GameUtl.GameComponent<DefRepository>().GetAllDefs<ItemDef>()
                                                    where p.Tags.Contains(mutationTag) && p.ViewElementDef != null
                                                    select p).ToList<ItemDef>();
                        GeoLevelController._ConsoleGetLevelController().ViewerFaction.UnlockedAugmentations.AddRange(enumerable);
                    }
                }
            }

            private static void ManufactureAll()
            {
                OptionsManager optionsManager = GameUtl.GameComponent<OptionsManager>();
                if (optionsManager != null)
                {
                    GeoLevelController geoLevelController = GameUtl.CurrentLevel().GetComponent<GeoLevelController>();
                    if (geoLevelController != null)
                    {
                        GeoPhoenixFaction phoenixFaction = geoLevelController.PhoenixFaction;
                        SharedGameTagsDataDef sharedGameTags = GameUtl.GameComponent<SharedData>().SharedGameTags;
                        GameTagDef manufacturableTag = sharedGameTags.ManufacturableTag;
                        UnlockWeaponsArmorAndSkins(optionsManager, geoLevelController, phoenixFaction, manufacturableTag);
                        UnlockAircraftEquipment(optionsManager, geoLevelController, phoenixFaction, manufacturableTag);
                        UnlockGroundVehicles(optionsManager, geoLevelController, phoenixFaction, manufacturableTag);
                        UnlockAircraft(optionsManager, geoLevelController, phoenixFaction, manufacturableTag);
                    }
                }
            }

            private static void UnlockAircraft(OptionsManager optionsManager, GeoLevelController geoLevelController, GeoPhoenixFaction phoenixFaction, GameTagDef manufacturableTag)
            {
                List<VehicleItemDef> list4 = new List<VehicleItemDef>();
                list4.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<VehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.PhoenixFactionDef.Tag)
                               select x);
                list4.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<VehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.SynedrionFaction.Def.Tag)
                               select x);
                list4.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<VehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.AnuFaction.Def.Tag)
                               select x);
                list4.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<VehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.NewJerichoFaction.Def.Tag)
                               select x);
                using (List<VehicleItemDef>.Enumerator enumerator = list4.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        VehicleItemDef item = enumerator.Current;
                        if (!phoenixFaction.Manufacture.ManufacturableItems.Any((ManufacturableItem x) => x.RelatedItemDef.name == item.name))
                        {
                            if (!item.Tags.Contains(manufacturableTag))
                            {
                                item.Tags.Add(manufacturableTag);
                            }
                            ManufacturableItem item2 = new ManufacturableItem(item);
                            phoenixFaction.Manufacture.ManufacturableItems.Add(item2);
                        }
                    }
                }
            }

            private static void UnlockGroundVehicles(OptionsManager optionsManager, GeoLevelController geoLevelController, GeoPhoenixFaction phoenixFaction, GameTagDef manufacturableTag)
            {
                List<GroundVehicleItemDef> list3 = new List<GroundVehicleItemDef>();
                list3.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GroundVehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.PhoenixFactionDef.Tag)
                               select x);
                list3.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GroundVehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.SynedrionFaction.Def.Tag)
                               select x);
                list3.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GroundVehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.AnuFaction.Def.Tag)
                               select x);
                list3.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GroundVehicleItemDef>()
                               where x.Tags.Contains(geoLevelController.NewJerichoFaction.Def.Tag)
                               select x);
                using (List<GroundVehicleItemDef>.Enumerator enumerator = list3.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        GroundVehicleItemDef item = enumerator.Current;
                        if (!phoenixFaction.Manufacture.ManufacturableItems.Any((ManufacturableItem x) => x.RelatedItemDef.name == item.name))
                        {
                            if (!item.Tags.Contains(manufacturableTag))
                            {
                                item.Tags.Add(manufacturableTag);
                            }
                            ManufacturableItem item2 = new ManufacturableItem(item);
                            phoenixFaction.Manufacture.ManufacturableItems.Add(item2);
                        }
                    }
                }
            }

            private static void UnlockAircraftEquipment(OptionsManager optionsManager, GeoLevelController geoLevelController, GeoPhoenixFaction phoenixFaction, GameTagDef manufacturableTag)
            {
                DefRepository Repo = GameUtl.GameComponent<DefRepository>();

                List<GeoVehicleEquipmentDef> list2 = new List<GeoVehicleEquipmentDef>();
                list2.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GeoVehicleEquipmentDef>()
                               where x.Tags.Contains(geoLevelController.PhoenixFactionDef.Tag)
                               select x);
                list2.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GeoVehicleEquipmentDef>()
                               where x.Tags.Contains(geoLevelController.SynedrionFaction.Def.Tag)
                               select x);
                list2.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GeoVehicleEquipmentDef>()
                               where x.Tags.Contains(geoLevelController.AnuFaction.Def.Tag)
                               select x);
                list2.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<GeoVehicleEquipmentDef>()
                               where x.Tags.Contains(geoLevelController.NewJerichoFaction.Def.Tag)
                               select x);
                using (List<GeoVehicleEquipmentDef>.Enumerator enumerator = list2.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        GeoVehicleEquipmentDef item = enumerator.Current;
                        if (!phoenixFaction.Manufacture.ManufacturableItems.Any((ManufacturableItem x) => x.RelatedItemDef.name == item.name))
                        {
                            if (!item.Tags.Contains(manufacturableTag))
                            {
                                item.Tags.Add(manufacturableTag);
                            }
                            ManufacturableItem item2 = new ManufacturableItem(item);
                            phoenixFaction.Manufacture.ManufacturableItems.Add(item2);
                        }
                    }
                }

            }

            private static void UnlockWeaponsArmorAndSkins(OptionsManager optionsManager, GeoLevelController geoLevelController, GeoPhoenixFaction phoenixFaction, GameTagDef manufacturableTag)
            {
                List<TacticalItemDef> list = new List<TacticalItemDef>();
                list.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<TacticalItemDef>()
                              where x.Tags.Contains(geoLevelController.PhoenixFactionDef.Tag)
                              select x);
                list.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<TacticalItemDef>()
                              where x.Tags.Contains(geoLevelController.SynedrionFaction.Def.Tag)
                              select x);
                list.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<TacticalItemDef>()
                              where x.Tags.Contains(geoLevelController.AnuFaction.Def.Tag)
                              select x);
                list.AddRange(from x in optionsManager.DefsRepo.GetAllDefs<TacticalItemDef>()
                              where x.Tags.Contains(geoLevelController.NewJerichoFaction.Def.Tag)
                              select x);
                list.AddRange(optionsManager.DefsRepo.GetAllDefs<RedeemableCodeDef>().SelectMany((RedeemableCodeDef x) => x.GiftedItems));
                using (List<TacticalItemDef>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        TacticalItemDef item = enumerator.Current;
                        if (!phoenixFaction.Manufacture.ManufacturableItems.Any((ManufacturableItem x) => x.RelatedItemDef.name == item.name))
                        {
                            if (!item.Tags.Contains(manufacturableTag))
                            {
                                item.Tags.Add(manufacturableTag);
                            }
                            ManufacturableItem item2 = new ManufacturableItem(item);
                            phoenixFaction.Manufacture.ManufacturableItems.Add(item2);
                        
                    }                  
                }               
            }            
        }     
    }
    
}