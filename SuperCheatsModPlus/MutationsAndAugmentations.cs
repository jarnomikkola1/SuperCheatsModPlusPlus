using Base.Defs;
using PhoenixPoint.Tactical.Entities.Equipments;
using Base.Core;
using PhoenixPoint.Tactical.Entities.Weapons;
using System.Linq;
using Base.Entities.Abilities;

namespace SuperCheatsModPlus
{
    internal class MutationsAndAugmentations
    {
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        public static void Change_PermanentAug()
        {
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();

            if (Config.RemovableMutationsAndAugmentations == true)
            {
                foreach (TacticalItemDef Item in Repo.GetAllDefs<TacticalItemDef>())
                {
                    Item.IsPermanentAugment = false;
                }
            }

            if (Config.VenomTorsoCanUseBothHands == true)
            {
                WeaponDef shooterTorso = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(ged => ged.name.Equals("AN_Berserker_Shooter_LeftArm_WeaponDef"));

                shooterTorso.Abilities = new AbilityDef[]
                {
                    shooterTorso.Abilities[0],
                };
            }

            if (Config.FreeBionicRepair == true)
            {
                foreach (TacticalItemDef item in Repo.GetAllDefs<TacticalItemDef>().Where(a => a.name.Contains("_BIO_")))
                {
                    item.PreserveDamageTaken = false;
                }
            }
        }        
    }
}
