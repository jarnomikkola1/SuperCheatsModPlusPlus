using Base.Defs;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Weapons;
using System.Linq;
using PhoenixPoint.Geoscape.Entities.Research.Reward;
using PhoenixPoint.Geoscape.Entities.Research;
using PhoenixPoint.Geoscape.Entities.PhoenixBases.FacilityComponents;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using System.Collections.Generic;

namespace SuperCheatsModPlus
{
    internal class OtherChanges
    {
        private static readonly SharedData Shared = SuperCheatsModPlusMain.Shared;
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        public static void Change_Others()
        {
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;
            ResearchDef atmosphiricAnalysis = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(ged => ged.name.Equals("PX_AtmosphericAnalysis_ResearchDef"));
            PhoenixFacilityDef VehicleBay = Repo.GetAllDefs<PhoenixFacilityDef>().FirstOrDefault(ged => ged.name.Equals("VehicleBay_PhoenixFacilityDef"));
            VehicleSlotFacilityComponentDef VehicleBaySlotComponent = Repo.GetAllDefs<VehicleSlotFacilityComponentDef>().FirstOrDefault(ged => ged.name.Equals("E_Element0 [VehicleBay_PhoenixFacilityDef]"));

            if (Config.OpLivingWeapons == true)
            {
                WeaponDef assRifle = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_AcidAssaultRifle_WeaponDef"));
                WeaponDef Machinegun = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_PoisonMachineGun_WeaponDef"));

                assRifle.DamagePayload.DamageKeywords[0].Value = 40;
                assRifle.DamagePayload.DamageKeywords[1].Value = 10;
                Machinegun.DamagePayload.DamageKeywords[0].Value = 35;
                Machinegun.DamagePayload.DamageKeywords[1].Value = 10;
                assRifle.DamagePayload.StopOnFirstHit = false;
                assRifle.DamagePayload.AutoFireShotCount = 7;
                assRifle.SpreadDegrees = 1.1f;
                Machinegun.DamagePayload.StopOnFirstHit = false;
                Machinegun.DamagePayload.AutoFireShotCount = 20;
                Machinegun.SpreadDegrees = 2.22704697f;
            }
            if (Config.OPKaosWeapons == true)
            {
                WeaponDef AR = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Obliterator_WeaponDef"));
                WeaponDef SR = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Subjector_WeaponDef"));
                WeaponDef HC = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Devastator_WeaponDef"));
                WeaponDef SG = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Redemptor_WeaponDef"));
                WeaponDef Pistol = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Tormentor_WeaponDef"));

                AR.SpreadDegrees = 1f;
                AR.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.DamageKeyword,
                        Value = 40,
                    },
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword,
                        Value = 2,
                    },
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.ParalysingKeyword,
                        Value = 2,
                    },
                };

                SR.SpreadDegrees = 0.4f;
                SR.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef= Shared.SharedDamageKeywords.DamageKeyword,
                        Value = 130,
                    },
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.PoisonousKeyword,
                        Value = 30,
                    },
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.SyphonKeyword,
                        Value = 30
                    }
                };

                HC.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef= Shared.SharedDamageKeywords.DamageKeyword,
                        Value = 200,
                    },
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.ShockKeyword,
                        Value = 200,
                    },
                    new DamageKeywordPair()
                    {
                        DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword,
                        Value = 20,
                    },
                };

                HC.SpreadDegrees = 1.6f;

                SG.DamagePayload.ProjectilesPerShot = 10;
                SG.DamagePayload.DamageKeywords[0].Value = 50;

                Pistol.DamagePayload.DamageKeywords[0].Value = 60;
                Pistol.DamagePayload.DamageKeywords[1].Value = 20;
                Pistol.SpreadDegrees = 1.4f;
            }

            if (Config.IncreaseSoldierInventorySlots == true)
            {
                BackpackFilterDef backpack = Repo.GetAllDefs<BackpackFilterDef>().FirstOrDefault(a => a.name.Equals("BackpackFilterDef"));
                backpack.MaxItems = 9;
            }

            if (Config.RemoteControlBuff == true)
            {
                ApplyStatusAbilityDef remoteControl = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(a => a.name.Equals("ManualControl_AbilityDef"));
                remoteControl.ActionPointCost = 0.25f;
                remoteControl.WillPointCost = 1;
            }

            if (Config.ArchAngelRL1HasBlastRadius == true)
            {
                WeaponDef rl1 = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_HeavyRocketLauncher_WeaponDef"));
                WeaponDef rebuke = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_Rebuke_WeaponDef"));
                ShootAbilityDef ShootRocket = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("LaunchRocket_ShootAbilityDef"));

                rl1.DamagePayload.DamageDeliveryType = DamageDeliveryType.Sphere;
                rl1.SpreadRadius = rebuke.SpreadRadius;
                rl1.SpreadDegrees = rebuke.SpreadDegrees;
                rl1.UseAimIK = false;
                rl1.AimTransform = rebuke.AimTransform;
                rl1.AimPoint = rebuke.AimPoint;
                rl1.DamagePayload.ParabolaHeightToLengthRatio = 0.3f;
                rl1.DamagePayload.ProjectileVisuals.TimeToLiveAfterStop = 1.5f;
                rl1.DamagePayload.ProjectileVisuals.ImpactNormalDisplacement = 0.5f;
                rl1.DamagePayload.ProjectileVisuals.HitEffect.Offset = 0.02f;
                rl1.DamagePayload.ProjectileVisuals.HitEffect.Alignment = PhoenixPoint.Common.Core.HitEffect.HitAlignment.SurfaceNormal;
                rl1.DamagePayload.StopOnFirstHit = false;
                rl1.DamagePayload.Range = 25;
                rl1.DamagePayload.AoeRadius = 3.5f;
                rl1.DamagePayload.ConeRadius = 1;
                rl1.DamagePayload.ProjectileOrigin = rebuke.DamagePayload.ProjectileOrigin;
                rl1.Abilities[0] = rebuke.Abilities[0];

            }

            if (Config.UnlockAllFacilities == true)
            {
                atmosphiricAnalysis.Unlocks = new ResearchRewardDef[]
                {
                    atmosphiricAnalysis.Unlocks[0],
                    Repo.GetAllDefs<FacilityResearchRewardDef>().FirstOrDefault(a => a.name.Equals("SYN_MistRepellers_ResearchDef_FacilityResearchRewardDef_0")),
                    Repo.GetAllDefs<FacilityResearchRewardDef>().FirstOrDefault(a => a.name.Equals("NJ_Bionics1_ResearchDef_FacilityResearchRewardDef_0")),
                    Repo.GetAllDefs<FacilityResearchRewardDef>().FirstOrDefault(a => a.name.Equals("ANU_AnuFungusFood_ResearchDef_FacilityResearchRewardDef_0")),
                    Repo.GetAllDefs<FacilityResearchRewardDef>().FirstOrDefault(a => a.name.Equals("PX_CaptureTech_ResearchDef_FacilityResearchRewardDef_0")),
                    Repo.GetAllDefs<FacilityResearchRewardDef>().FirstOrDefault(a => a.name.Equals("ANU_MutationTech_ResearchDef_FacilityResearchRewardDef_0")),
                    Repo.GetAllDefs<FacilityResearchRewardDef>().FirstOrDefault(a => a.name.Equals("PX_AntediluvianArchaeology_ResearchDef_FacilityResearchRewardDef_0")),
                };
            }            

            //if (Config.TurnOnOtherAdjustments == true)
            //{
            //    VehicleBaySlotComponent.AircraftSlots = Config.VehicleBayAircraftSlots;
            //    VehicleBaySlotComponent.GroundVehicleSlots = Config.VehicleBayGroundVehicleSlots;
            //    VehicleBaySlotComponent.AircraftHealAmount = Config.VehicleBayAircraftHealAmount;
            //    VehicleBaySlotComponent.VehicleHealAmount = Config.VehicleBayGroundVehicleHealAmount;
            //}
        }
    }
}

