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
using PhoenixPoint.Geoscape.Core;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Geoscape.View.ViewControllers.BaseRecruits;
using PhoenixPoint.Geoscape.View.DataObjects;
using PhoenixPoint.Geoscape.View.ViewControllers.AugmentationScreen;
using PhoenixPoint.Common.Entities.Addons;
using PhoenixPoint.Common.UI;

namespace SuperCheatsModPlus
{
    internal class Patches
    {        
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        // Token: 0x040000DC RID: 220
        internal static int VanillaAbilityLimit = 3;

        // Token: 0x040000DD RID: 221
        internal static int MinAbilityLimit = 1;

        // Token: 0x040000DE RID: 222
        internal static int MaxAbilityLimit = 7;


        //public bool UnlockAllBionics = false;
        //public bool UnlockAllMutations = false;
        //public bool MaxLevelSoldiers = false;
        //public bool Give350XPToAllSoldiersOnce = false;
        //public bool Get350SpecialPointsOnce = false;
        //public bool InfiniteSpecialPoints = false;
        //public bool ManufactureEverything = false;
        //public bool UnlockAllSpecializations = false;
        //public bool Get10ThousandOfAllResources = false;
        //public bool HealAllSoldiersAfterMissions = false;
        //public bool NeverGetTiredOrExhausted = false;

        public static void Change_Patches()
        {                   
        }
        [HarmonyPatch(typeof(PhoenixStatisticsManager), "OnGeoscapeLevelStart")]
        internal static class PhoenixStatisticsManager_OnGeoscapeLevelStart2
        {

            public static bool flag = true;
            public static bool flag2 = true;
            public static bool flag3 = true;

            private static void Postfix()
            {
                SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;
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
        //[HarmonyPatch(typeof(FactionCharacterGenerator), "GeneratePersonalAbilities")]
        //public static class GeneratePersonalAbilities_Patch
        //{
        //
        //
        //    // Fix ability generation if vanilla generated less than the expected number of personal abilities
        //    public static void Postfix(FactionCharacterGenerator __instance, ref Dictionary<int, TacticalAbilityDef> __result, int abilitiesCount, LevelProgressionDef levelDef, List<TacticalAbilityDef> ____personalAbilityPool)
        //    {
        //        try
        //        {
        //            if (__result.Count < abilitiesCount)
        //            {
        //                Dictionary<int, TacticalAbilityDef> dictionary = new Dictionary<int, TacticalAbilityDef>();
        //                List<TacticalAbilityDef> tmpList = new List<TacticalAbilityDef>();
        //                List<TacticalAbilityDef> personalAbilityPool = ____personalAbilityPool;
        //                int maxLevel = levelDef.MaxLevel;
        //                List<int> availableSlots = new List<int>();
        //                for (int i = 0; i < maxLevel; i++)
        //                {
        //                    availableSlots.Add(i);
        //                }
        //
        //                int num = 0;
        //                while (num < abilitiesCount && personalAbilityPool.Count != 0)
        //                {
        //                    TacticalAbilityDef randomElement = personalAbilityPool.GetRandomElement();
        //                    if (randomElement != null)
        //                    {
        //                        personalAbilityPool.Remove(randomElement);
        //                        tmpList.Add(randomElement);
        //                        int slot = availableSlots.GetRandomElement();
        //                        //Logger.Info($"[FactionCharacterGenerator_GeneratePersonalAbilities_POSTFIX] slot: {slot}");
        //                        //Logger.Info($"[FactionCharacterGenerator_GeneratePersonalAbilities_POSTFIX] ability: {randomElement.ViewElementDef.DisplayName1.Localize()}");
        //
        //                        availableSlots.Remove(slot);
        //                        //Logger.Info($"[FactionCharacterGenerator_GeneratePersonalAbilities_POSTFIX] availableSlots: {availableSlots.Count}");
        //
        //                        dictionary.Add(slot, randomElement);
        //                        num++;
        //                    }
        //                    else
        //                    {
        //                        throw new NullReferenceException("Personal ability pool returned no TacticalAbilityDef");
        //                    }
        //                }
        //                personalAbilityPool.AddRange(tmpList);
        //
        //
        //
        //                __result = dictionary;
        //            }
        //
        //        }
        //        catch (Exception e)
        //        {
        //        }
        //    }
        //}
        //[HarmonyPatch(typeof(RecruitsListElementController), "SetRecruitElement")]
        //public static class RecruitsListElementController_SetRecruitElement_Patch
        //{
        //    internal static int VanillaAbilityLimit = 3;
        //    internal static int MinAbilityLimit = 1;
        //    internal static int MaxAbilityLimit = 7;
        //    // Prepare UI for more than 3 personal abilities
        //    public static void Prefix(RecruitsListElementController __instance, RecruitsListEntryData entryData, List<RowIconTextController> ____abilityIcons)
        //    {
        //        try
        //        {
        //            RowIconTextController[] rowItems = __instance.PersonalTrackRoot.transform.GetComponentsInChildren<RowIconTextController>(true);
        //
        //            //Logger.Debug($"[RecruitsListElementController_SetRecruitElement_PREFIX] rowItems: {rowItems.Length}");
        //            //Logger.Debug($"[RecruitsListElementController_SetRecruitElement_PREFIX] abilities: {entryData.PersonalTrackAbilities.Count()}");
        //
        //            // Only add the icon containers if we need them
        //            if (rowItems.Length < MaxAbilityLimit)
        //            {
        //                // Clone the first item as often as needed, it's filled with content in the original method by calling RecruitsListElementController.SetAbilityIcons()
        //                RowIconTextController cloneBase = rowItems.FirstOrDefault();
        //                int clonesNeeded = MaxAbilityLimit - VanillaAbilityLimit;
        //
        //                if (cloneBase == null)
        //                {
        //                    throw new NullReferenceException("Object to clone is null");
        //                }
        //
        //                for (int i = 0; i < clonesNeeded; i++)
        //                {
        //                    UnityEngine.Object.Instantiate<RowIconTextController>(rowItems.FirstOrDefault(), __instance.PersonalTrackRoot.transform, true);
        //                }
        //            }
        //
        //
        //
        //            rowItems = __instance.PersonalTrackRoot.transform.GetComponentsInChildren<RowIconTextController>(true);
        //            //Logger.Debug($"[RecruitsListElementController_SetRecruitElement_PREFIX] rowItems: {rowItems.Length}");
        //
        //            // Disable textfield and rescale item for more than 3 abilities
        //            if (entryData.PersonalTrackAbilities.Count() > VanillaAbilityLimit)
        //            {
        //                foreach (RowIconTextController rowItem in rowItems)
        //                {
        //                    rowItem.DisplayText.gameObject.SetActive(false);
        //                    RectTransform rtRowItem = rowItem.GetComponent<RectTransform>();
        //                    RectTransform rtText = rowItem.DisplayText.GetComponent<RectTransform>();
        //                    rtText.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
        //                    rtRowItem.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //        }
        //    }
        //}
        //[HarmonyPatch(typeof(RecruitsListElementController), "SetAbilityIcons")]
        //public static class RecruitsListElementController_SetAbilityIcons_Patch
        //{
        //    // Token: 0x06000126 RID: 294 RVA: 0x0000C924 File Offset: 0x0000AB24
        //
        //    // Token: 0x06000127 RID: 295 RVA: 0x0000CB40 File Offset: 0x0000AD40
        //    public static void Postfix(RecruitsListElementController __instance, List<TacticalAbilityViewElementDef> abilities, List<RowIconTextController> ____abilityIcons)
        //    {
        //        try
        //        {
        //            for (int i = 0; i < abilities.Count; i++)
        //            {
        //                TacticalAbilityViewElementDef tacticalAbilityViewElementDef = abilities[i];
        //                ____abilityIcons[i].Tooltip.TipKey = null;
        //                ____abilityIcons[i].Tooltip.TipText = tacticalAbilityViewElementDef.DisplayName1.Localize(null) + " - " + tacticalAbilityViewElementDef.Description.Localize(null);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //}

        [HarmonyPatch(typeof(UIModuleBionics), "InitCharacterInfo")]
        internal class Init_Bionics
        {

            // Token: 0x0600000A RID: 10 RVA: 0x000021B8 File Offset: 0x000003B8
            [HarmonyPrefix]
            private static bool Prefix(UIModuleBionics __instance, ref int ____currentCharacterAugmentsAmount, Dictionary<AddonSlotDef, UIModuleMutationSection> ____augmentSections, GameTagDef ____bionicsTag, GameTagDef ____mutationTag)
            {
                SuperCheatsModPlusConfig Config = new SuperCheatsModPlusConfig();
                ____currentCharacterAugmentsAmount = 0;
                ____currentCharacterAugmentsAmount = AugmentScreenUtilities.GetNumberOfAugments(__instance.CurrentCharacter);
                bool flag = ____currentCharacterAugmentsAmount < Config.MaxAugmentations;
                foreach (KeyValuePair<AddonSlotDef, UIModuleMutationSection> keyValuePair in ____augmentSections)
                {
                    AugumentSlotState slotState = AugumentSlotState.Available;
                    string lockedReasonKey = null;
                    ItemDef augmentAtSlot = AugmentScreenUtilities.GetAugmentAtSlot(__instance.CurrentCharacter, keyValuePair.Key);
                    bool flag2 = augmentAtSlot != null && augmentAtSlot.Tags.Contains(____mutationTag);
                    bool flag3 = augmentAtSlot != null && augmentAtSlot.Tags.Contains(____bionicsTag);
                    bool flag4 = flag2;
                    if (flag4)
                    {
                        lockedReasonKey = __instance.LockedDueToMutationKey.LocalizationKey;
                        slotState = AugumentSlotState.BlockedByPermenantAugument;
                    }
                    else
                    {
                        bool flag5 = !flag && !flag3;
                        if (flag5)
                        {
                            lockedReasonKey = __instance.LockedDueToLimitKey.LocalizationKey;
                            slotState = AugumentSlotState.AugumentationLimitReached;
                        }
                    }
                    keyValuePair.Value.ResetContainer(slotState, lockedReasonKey);
                }
                foreach (GeoItem geoItem in __instance.CurrentCharacter.ArmourItems)
                {
                    bool flag6 = geoItem.ItemDef.Tags.Contains(____bionicsTag);
                    if (flag6)
                    {
                        foreach (AddonDef.RequiredSlotBind requiredSlotBind in geoItem.ItemDef.RequiredSlotBinds)
                        {
                            bool flag7 = ____augmentSections.ContainsKey(requiredSlotBind.RequiredSlot);
                            if (flag7)
                            {
                                ____augmentSections[requiredSlotBind.RequiredSlot].SetMutationUsed(geoItem.ItemDef);
                            }
                        }
                    }
                }
                string text = __instance.XoutOfY.Localize(null);
                text = text.Replace("{0}", ____currentCharacterAugmentsAmount.ToString());
                text = text.Replace("{1}", Config.MaxAugmentations.ToString());
                __instance.AugmentsAvailableValue.text = text;
                __instance.AugmentsAvailableValue.GetComponent<UIColorController>().SetWarningActive(Config.MaxAugmentations <= ____currentCharacterAugmentsAmount, false);
                return false;
            }
        }
        [HarmonyPatch(typeof(UIModuleMutate), "InitCharacterInfo")]
        internal class Init_Mutate
        {
            // Token: 0x0600000C RID: 12 RVA: 0x000023FC File Offset: 0x000005FC
            [HarmonyPrefix]
            private static bool Prefix(UIModuleMutate __instance, ref int ____currentCharacterAugmentsAmount, Dictionary<AddonSlotDef, UIModuleMutationSection> ____augmentSections, GameTagDef ____bionicsTag, GameTagDef ____mutationTag)
            {
                SuperCheatsModPlusConfig Config = new SuperCheatsModPlusConfig();
                ____currentCharacterAugmentsAmount = 0;
                ____currentCharacterAugmentsAmount = AugmentScreenUtilities.GetNumberOfAugments(__instance.CurrentCharacter);
                bool flag = ____currentCharacterAugmentsAmount < Config.MaxAugmentations;
                foreach (KeyValuePair<AddonSlotDef, UIModuleMutationSection> keyValuePair in ____augmentSections)
                {
                    AugumentSlotState slotState = AugumentSlotState.Available;
                    string lockedReasonKey = null;
                    ItemDef augmentAtSlot = AugmentScreenUtilities.GetAugmentAtSlot(__instance.CurrentCharacter, keyValuePair.Key);
                    bool flag2 = augmentAtSlot != null && augmentAtSlot.Tags.Contains(____bionicsTag);
                    bool flag3 = augmentAtSlot != null && augmentAtSlot.Tags.Contains(____mutationTag);
                    bool flag4 = flag2;
                    if (flag4)
                    {
                        lockedReasonKey = __instance.LockedDueToBionicsKey.LocalizationKey;
                        slotState = AugumentSlotState.BlockedByPermenantAugument;
                    }
                    else
                    {
                        bool flag5 = !flag && !flag3;
                        if (flag5)
                        {
                            lockedReasonKey = __instance.LockedDueToLimitKey.LocalizationKey;
                            slotState = AugumentSlotState.AugumentationLimitReached;
                        }
                    }
                    keyValuePair.Value.ResetContainer(slotState, lockedReasonKey);
                }
                foreach (GeoItem geoItem in __instance.CurrentCharacter.ArmourItems)
                {
                    bool flag6 = geoItem.ItemDef.Tags.Contains(____mutationTag);
                    if (flag6)
                    {
                        foreach (AddonDef.RequiredSlotBind requiredSlotBind in geoItem.ItemDef.RequiredSlotBinds)
                        {
                            bool flag7 = ____augmentSections.ContainsKey(requiredSlotBind.RequiredSlot);
                            if (flag7)
                            {
                                ____augmentSections[requiredSlotBind.RequiredSlot].SetMutationUsed(geoItem.ItemDef);
                            }
                        }
                    }
                }
                string text = __instance.XoutOfY.Localize(null);
                text = text.Replace("{0}", ____currentCharacterAugmentsAmount.ToString());
                text = text.Replace("{1}", Config.MaxAugmentations.ToString());
                __instance.MutationsAvailableValue.text = text;
                __instance.MutationsAvailableValue.GetComponent<UIColorController>().SetWarningActive(Config.MaxAugmentations <= ____currentCharacterAugmentsAmount, false);
                return false;
            }
        }
    }       
}
