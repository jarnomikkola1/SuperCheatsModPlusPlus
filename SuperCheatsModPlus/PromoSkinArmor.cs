using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects;
using Base.Entities.Statuses;
using Base.UI;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Animations;
using PhoenixPoint.Tactical.Entities.Effects.ApplicationConditions;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using System.Linq;


namespace SuperCheatsModPlus
{
    internal class PromoSkinArmor
    {
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        private static readonly SharedData Shared = SuperCheatsModPlusMain.Shared;
        public static void Create_PromoSkinArmor()
        {
            Create_ArmorBuff();
            Clone_OilCrab();
            Create_Reinforcements();
            AddAbilities();
        }

        public static void Create_ArmorBuff()
        {
            string skillName = "BonusArmor2_AbilityDef";
            ApplyEffectAbilityDef source = Repo.GetAllDefs<ApplyEffectAbilityDef>().FirstOrDefault(p => p.name.Equals("Acheron_RestorePandoranArmor_AbilityDef"));
            ItemSlotStatsModifyStatusDef sourceStatus = Repo.GetAllDefs<ItemSlotStatsModifyStatusDef>().FirstOrDefault(p => p.name.Equals("E_Status [Acheron_RestorePandoranArmor_AbilityDef]"));
            ApplyEffectAbilityDef addarmour = Helper.CreateDefFromClone(
                source,
                "251E62C2-F652-481E-B043-A2B1D6525B75",
                skillName);
            addarmour.ViewElementDef = Helper.CreateDefFromClone(
                source.ViewElementDef,
               "8E49AFB8-E450-49A2-A732-9231EE8CDBA2",
               skillName);
            addarmour.CharacterProgressionData = Helper.CreateDefFromClone(
                source.CharacterProgressionData,
               "3DE5F496-7515-4975-AE3C-8E68AE35DF0C",
               skillName);
            addarmour.EffectDef = Helper.CreateDefFromClone(
                source.EffectDef,
               "E31F7344-8F19-4AAE-8FE7-141865E34760",
               "E_Effect [BonusArmor2_AbilityDef]");
            //StatusEffectDef addarmourEffect = Helper.CreateDefFromClone(
            //    addarmour.EffectDef as StatusEffectDef,
            //   "E31F7344-8F19-4AAE-8FE7-141865E34760",
            //   "E_Effect [BonusArmor_AbilityDef]");
            //ItemSlotStatsModifyStatusDef addarmourStatus = Helper.CreateDefFromClone(
            //    addarmourEffect.StatusDef as ItemSlotStatsModifyStatusDef,
            //   "5262FA8D-5F25-44C2-A50F-3B32F39CC978",
            //   "E_Effect [BonusArmor_AbilityDef]");
            StatusEffectDef addarmourEffect = (StatusEffectDef)addarmour.EffectDef;

            addarmourEffect.StatusDef = Helper.CreateDefFromClone(
                sourceStatus,
               "5262FA8D-5F25-44C2-A50F-3B32F39CC978",
               "E_Status [BonusArmor2_AbilityDef]");

            addarmour.CharacterProgressionData = null;
            ItemSlotStatsModifyStatusDef addarmourStatus = (ItemSlotStatsModifyStatusDef)addarmourEffect.StatusDef;

            //addarmour.EffectDef = addarmourEffect;
            //addarmourEffect.StatusDef = addarmourStatus;

            addarmour.TargetingDataDef.Origin.TargetTags = new GameTagsList
            {
                Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("Human_TagDef"))
            };

            addarmourStatus.StatsModifications[0].Type = ItemSlotStatsModifyStatusDef.StatType.Armour;
            addarmourStatus.StatsModifications[0].ModificationType = StatModificationType.Add;
            addarmourStatus.StatsModifications[0].Value = 10;
            //addarmourStatus.StatsModifications = new ItemSlotStatsModifyStatusDef.ItemSlotModification[]
            //{
            //    new ItemSlotStatsModifyStatusDef.ItemSlotModification()
            //    {
            //        Type = ItemSlotStatsModifyStatusDef.StatType.Armour,
            //        ModificationType = StatModificationType.Add,
            //        Value = 10,
            //    },
            //
            //    new ItemSlotStatsModifyStatusDef.ItemSlotModification()
            //    {
            //        Type = ItemSlotStatsModifyStatusDef.StatType.Armour,
            //        ModificationType = StatModificationType.AddMax,
            //        Value = 10,
            //    },
            //};

            addarmour.ViewElementDef.DisplayName1 = new LocalizedTextBind("Armor Buff", true);
            addarmour.ViewElementDef.Description = new LocalizedTextBind("Add +10 armor to allies for the duration of the mission", true);
            addarmour.UsesPerTurn = 1;
            addarmour.WillPointCost = 4;

            foreach (TacActorSimpleAbilityAnimActionDef animActionDef in Repo.GetAllDefs<TacActorSimpleAbilityAnimActionDef>().Where(aad => aad.name.Contains("Soldier_Utka_AnimActionsDef")))
            {
                if (animActionDef.AbilityDefs != null && !animActionDef.AbilityDefs.Contains(addarmour))
                {
                    animActionDef.AbilityDefs = animActionDef.AbilityDefs.Append(addarmour).ToArray();
                }
            }
        }
        public static void Clone_OilCrab()
        {
            string skillName3 = "GoldTorsoOilCrab_AbilityDef";
            DeathBelcherAbilityDef source3 = Repo.GetAllDefs<DeathBelcherAbilityDef>().FirstOrDefault(p => p.name.Equals("Oilcrab_Die_DeathBelcher_AbilityDef"));
            DeathBelcherAbilityDef oilCrab = Helper.CreateDefFromClone(
                source3,
                "54CD9E74-7F1D-4D84-8316-FCBF56C0D38D",
                skillName3);

            AddAbilityStatusDef oilCrabStatus = Repo.GetAllDefs<AddAbilityStatusDef>().FirstOrDefault(a => a.name.Equals("OilCrab_AddAbilityStatusDef"));
            ActorHasTagEffectConditionDef oilCrabCondition = (ActorHasTagEffectConditionDef)oilCrabStatus.ApplicationConditions[0];
            oilCrabCondition.GameTag = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(a => a.name.Equals("SmallGeyser_GameTagDef"));
            oilCrabCondition.HasTag = false;
        }
        public static void Create_Reinforcements()
        {
            TacCharacterDef Exalted = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(p => p.name.Equals("AN_Exalted_TacCharacterDef"));
            ClassTagDef ExaltedTag = Repo.GetAllDefs<ClassTagDef>().FirstOrDefault(p => p.name.Equals("Heavy_ClassTagDef"));

            string skillName4 = "HumanReinforcements2_AbilityDef";
            SpawnActorAbilityDef source4 = Repo.GetAllDefs<SpawnActorAbilityDef>().FirstOrDefault(p => p.name.Equals("Decoy_AbilityDef"));
            SpawnActorAbilityDef SpawnActor = Helper.CreateDefFromClone(
                source4,
                "F128D091-E92B-4765-82BA-6F46E654E125",
                skillName4);
            SpawnActor.ViewElementDef = Helper.CreateDefFromClone(
                source4.ViewElementDef,
               "AFF08D93-FD49-47F7-A05A-D9A246684248",
               skillName4);
            SpawnActor.CharacterProgressionData = Helper.CreateDefFromClone(
                source4.CharacterProgressionData,
               "4D9DA012-78BA-4A6B-B84D-6288161986D0",
               skillName4);

            SpawnActor.CharacterProgressionData = null;
            SpawnActor.TacCharacterDef = Exalted;
            SpawnActor.ActorComponentSetDef = Exalted.ComponentSetDef;

            SpawnActor.ActorTags = new GameTagDef[]
            {
                ExaltedTag,
            };

            SpawnActor.UseSelfAsTemplate = false;
            SpawnActor.TacCharacterDef = Exalted;
            SpawnActor.ActorComponentSetDef = Exalted.ComponentSetDef;
            SpawnActor.ViewElementDef.DisplayName1 = new LocalizedTextBind("Reinforcements", true);
            SpawnActor.ViewElementDef.Description = new LocalizedTextBind("Call on the exalted to reinforce your squad", true);
            SpawnActor.ActionPointCost = 1;
            SpawnActor.WillPointCost = 8;
            SpawnActor.UsesPerTurn = 1;

            //SpawnActor.ReinforcementsSettings[0].CharacterTagDef = assaultTag;
            //SpawnActor.ReinforcementsSettings[1].CharacterTagDef = ExaltedTag;
            //SpawnActor.ViewElementDef.DisplayName1 = new LocalizedTextBind("Reinforcements", true);
            //SpawnActor.ViewElementDef.Description = new LocalizedTextBind("Reinforce your squad", true);
            //SpawnActor.EventOnActivate.CullFilters = new Base.Eventus.BaseEventFilterDef[]
            //{
            //    SpawnActor.EventOnActivate.CullFilters[0],
            //};
            //reinforcementsCall.EventOnActivate = new TacticalEventDef();
            foreach (TacActorSimpleAbilityAnimActionDef animActionDef in Repo.GetAllDefs<TacActorSimpleAbilityAnimActionDef>().Where(aad => aad.name.Contains("Soldier_Utka_AnimActionsDef")))
            {
                if (animActionDef.AbilityDefs != null && !animActionDef.AbilityDefs.Contains(SpawnActor))
                {
                    animActionDef.AbilityDefs = animActionDef.AbilityDefs.Append(SpawnActor).ToArray();
                }
            }
        }
        public static void AddAbilities()
        {
            TacticalItemDef assaultTorsoGold = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Assault_Torso_Gold_BodyPartDef"));
            TacticalItemDef assaultHelmetGold = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Assault_Helmet_Gold_BodyPartDef"));
            TacticalItemDef SniperHelmetGold = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Sniper_Helmet_Gold_BodyPartDef"));
            TacticalItemDef HeavyTorsoGoldGold = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Heavy_Torso_Gold_BodyPartDef"));
            ApplyDamageEffectAbilityDef viralAreaAttack = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(p => p.name.Equals("ViralAreaAttack_ApplyDamageEffect_AbilityDef"));
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;

            foreach (TacActorSimpleAbilityAnimActionDef animActionDef in Repo.GetAllDefs<TacActorSimpleAbilityAnimActionDef>().Where(aad => aad.name.Contains("Soldier_Utka_AnimActionsDef")))
            {
                if (animActionDef.AbilityDefs != null && !animActionDef.AbilityDefs.Contains(viralAreaAttack))
                {
                    animActionDef.AbilityDefs = animActionDef.AbilityDefs.Append(viralAreaAttack).ToArray();
                }
            }


            if (Config.GoldArmorSkinsHaveSpecialAbilities == true)
            {
                assaultTorsoGold.Abilities = new AbilityDef[]
                {
                    Repo.GetAllDefs<DeathBelcherAbilityDef>().FirstOrDefault(p => p.name.Equals("GoldTorsoOilCrab_AbilityDef")),
                };
                assaultHelmetGold.Abilities = new AbilityDef[]
                {
                    Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(p => p.name.Equals("SenseLocate_AbilityDef")),
                };
                SniperHelmetGold.Abilities = new AbilityDef[]
                {
                    Repo.GetAllDefs<ApplyEffectAbilityDef>().FirstOrDefault(p => p.name.Equals("BonusArmor2_AbilityDef")),
                    Repo.GetAllDefs<SpawnActorAbilityDef>().FirstOrDefault(p => p.name.Equals("HumanReinforcements2_AbilityDef")),
                };
                HeavyTorsoGoldGold.Abilities = new AbilityDef[]
                {
                    Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(p => p.name.Equals("CrystalStacks_DamageAmplification_AbilityDef")),
                    viralAreaAttack,
                };
            }
        }
    }
}
