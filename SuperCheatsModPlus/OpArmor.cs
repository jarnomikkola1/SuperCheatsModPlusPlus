using Base.Defs;
using Base.Entities.Abilities;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Linq;


namespace SuperCheatsModPlus
{
    internal class OpArmor
    {
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        public static void Change_Armor()
        {          
            ShootAbilityDef DD = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("DeadlyDuo_ShootAbilityDef"));
            ShootAbilityDef RB = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("RageBurst_ShootAbilityDef"));
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;

            DD.ExecutionsCount = 2;
            DD.AddFollowupAbilityStatusDef = null;
            DD.CharacterProgressionData = null;
            DD.OverrideEquipmentTrait = null;
            DD.TraitsToApply = new string[]
            {
                DD.TraitsToApply[0],
                DD.TraitsToApply[1],
                DD.TraitsToApply[2],
            };


            if (Config.OpArmorAbilitiesEnabled == true)
            {
                TacticalItemDef tentacleTorso = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Torso_BodyPartDef"));
                tentacleTorso.Abilities = new AbilityDef[]
                {
                            tentacleTorso.Abilities[0],
                            tentacleTorso.Abilities[1],
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Arm_ParalyzeLimb_AbilityDef")),
                };

                TacticalItemDef regentorso = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Torso_BodyPartDef"));
                regentorso.Abilities = new AbilityDef[]
                {
                            regentorso.Abilities[0],
                            regentorso.Abilities[1],
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_RightArm_Syphon_AdaptiveWeaponStatusDef")),
                };

                TacticalItemDef regenhelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Helmet_BodyPartDef"));
                regenhelmet.Abilities = new AbilityDef[]
                {
                            regenhelmet.Abilities[0],
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Head_Sonic_AbilityDef")),
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Head_GooStream_AbilityDef")),
                };

                TacticalItemDef venom = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Torso_BodyPartDef"));
                venom.Abilities = new AbilityDef[]
                {
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Arm_PoisonWorm_AbilityDef")),
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Arm_FireWorm_AbilityDef")),
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Arm_AcidWorm_AbilityDef")),
                            Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Adapt_Arm_GooGrenade_AbilityDef")),
                };

                ItemDef shadowLegs = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Legs_ItemDef"));
                shadowLegs.Abilities = new AbilityDef[]
                {
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("PainChameleon_AbilityDef")),
                };

                TacticalItemDef watcherHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Helmet_BodyPartDef"));
                watcherHelmet.Abilities = new AbilityDef[]
                {
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("SenseLocate_AbilityDef")),
                };



                TacticalItemDef resisthelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Helmet_BodyPartDef"));
                resisthelmet.Abilities = new AbilityDef[]
                {
                    resisthelmet.Abilities[0],
                    resisthelmet.Abilities[1],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("FireResistant_DamageMultiplierAbilityDef")),
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("ExpertLightWeapons_AbilityDef")),
                };

                TacticalItemDef juggTorso = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_Torso_BodyPartDef"));
                juggTorso.Abilities = new AbilityDef[]
                {
                    juggTorso.Abilities[0],
                    juggTorso.Abilities[1],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("GrenadeSpam_ShootAbilityDef")),
                };

                TacticalItemDef distruptorHead = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_Helmet_BodyPartDef"));
                distruptorHead.Abilities = new AbilityDef[]
                {
                    distruptorHead.Abilities[0],
                    distruptorHead.Abilities[1],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("DeadlyDuo_ShootAbilityDef")),
                    //Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("DeadlyDuo_FollowUp_ShootAbilityDef")),
                };

                TacticalItemDef stealthHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Helmet_BodyPartDef"));
                TacticalItemDef stealthTorso = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Torso_BodyPartDef"));
                TacticalItemDef stealthLegs = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Legs_ItemDef"));

                stealthHelmet.Abilities = new AbilityDef[]
                {
                    stealthHelmet.Abilities[0],
                    stealthHelmet.Abilities[1],
                    stealthHelmet.Abilities[2],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("MindControlImmunity_AbilityDef")),
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("NeuralDisruption_AbilityDef")),
                };
                stealthTorso.Abilities = new AbilityDef[]
                {
                    stealthTorso.Abilities[0],
                    stealthTorso.Abilities[1],
                    stealthTorso.Abilities[2],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("DemolitionMan_AbilityDef")),
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("HeavyLifter_AbilityDef")),
                };
                stealthLegs.Abilities = new AbilityDef[]
                {
                    stealthLegs.Abilities[0],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("GooImmunity_AbilityDef")),
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("SafeLanding_AbilityDef")),
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Exo_Leap_AbilityDef")),
                };

                ItemDef agileLegs = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Legs_ItemDef"));
                agileLegs.Abilities = new AbilityDef[] {
                    agileLegs.Abilities[0],
                    agileLegs.Abilities[1],
                    Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Dash_AbilityDef")),
                };
            }
        }
    }
}