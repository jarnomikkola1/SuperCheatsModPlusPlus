using System.Linq;
using Base.Defs;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Abilities;
using Base.Core;
using PhoenixPoint.Common.Entities.Addons;
using PhoenixPoint.Common.Entities.GameTagsTypes;

namespace SuperCheatsModPlus
{
    internal class MountedWeaponsMechArms
    {
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        public static void Change_Augmentations()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;

            TacticalItemDef jugg = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_Torso_BodyPartDef"));
            TacticalItemDef shin = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Torso_BodyPartDef"));
            TacticalItemDef neural = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_Torso_BodyPartDef"));
            TacticalItemDef tentacleTorso = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Torso_BodyPartDef"));
            TacticalItemDef regentorso = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Torso_BodyPartDef"));
            TacticalItemDef venom = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Torso_BodyPartDef"));
            ClassTagDef heavyclass = Repo.GetAllDefs<ClassTagDef>().FirstOrDefault(a => a.name.Equals("Heavy_ClassTagDef"));
            ClassTagDef techclass = Repo.GetAllDefs<ClassTagDef>().FirstOrDefault(a => a.name.Equals("Technician_ClassTagDef"));
            AddonSlotDef backpack = Repo.GetAllDefs<AddonSlotDef>().FirstOrDefault(a => a.name.Equals("Human_BackPack_SlotDef"));

            if(Config.UseMountedWeaponAndMechArmOnAugments == true)
            {
                tentacleTorso.Tags.Add(heavyclass);
                tentacleTorso.Tags.Add(techclass);
                regentorso.Tags.Add(heavyclass);
                regentorso.Tags.Add(techclass);
                venom.Tags.Add(heavyclass);
                venom.Tags.Add(techclass);
                jugg.Tags.Add(heavyclass);
                jugg.Tags.Add(techclass);
                shin.Tags.Add(heavyclass);
                shin.Tags.Add(techclass);

                jugg.ProvidedSlots = neural.ProvidedSlots;
                shin.ProvidedSlots = neural.ProvidedSlots;

                tentacleTorso.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    neural.ProvidedSlots[0],
                    neural.ProvidedSlots[1],
                    neural.ProvidedSlots[2],
                };

                regentorso.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    neural.ProvidedSlots[0],
                    neural.ProvidedSlots[1],
                    neural.ProvidedSlots[2],
                };

                venom.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    neural.ProvidedSlots[0],
                    neural.ProvidedSlots[1],
                    neural.ProvidedSlots[2],
                };
            }
            
            if(Config.MutationsCanEquipHeadItems == true)
            {
                TacticalItemDef pxAssHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Helmet_BodyPartDef"));
                TacticalItemDef regenhelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Helmet_BodyPartDef"));
                TacticalItemDef watcherHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Helmet_BodyPartDef"));
                TacticalItemDef resisthelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Helmet_BodyPartDef"));
                TacticalItemDef screaming = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Head03_BodyPartDef"));
                TacticalItemDef synod = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Head01_BodyPartDef"));
                TacticalItemDef judgement = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Head02_BodyPartDef"));

                regenhelmet.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    pxAssHelmet.ProvidedSlots[0],
                };

                watcherHelmet.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    pxAssHelmet.ProvidedSlots[0],
                };

                resisthelmet.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    pxAssHelmet.ProvidedSlots[0],
                };

                screaming.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    pxAssHelmet.ProvidedSlots[0],
                };

                synod.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    pxAssHelmet.ProvidedSlots[0],
                };

                judgement.ProvidedSlots = new AddonDef.ProvidedSlotBind[]
                {
                    pxAssHelmet.ProvidedSlots[0],
                };
            }
            
            if (Config.UseMountedWeaponsManyTimesPerTurn == true)
            {
                ShootAbilityDef Rocket = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("LaunchRocket_ShootAbilityDef"));
                ShootAbilityDef pxLaser = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("LaserArray_ShootAbilityDef"));

                Rocket.UsesPerTurn = 99;
                pxLaser.UsesPerTurn = 99;
            }
        }                          
    }   
}