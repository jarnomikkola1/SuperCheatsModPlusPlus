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
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            
                foreach (TacticalItemDef Item in Repo.GetAllDefs<TacticalItemDef>())
                {
                    Item.IsPermanentAugment = false;
                }                     
        }
        public static void TwoHands()
        {
            
                WeaponDef shooterTorso = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(ged => ged.name.Equals("AN_Berserker_Shooter_LeftArm_WeaponDef"));

                shooterTorso.Abilities = new AbilityDef[]
                {
                    shooterTorso.Abilities[0],
                };
                       
        }
        public static void FreeRepair()
        {
           
                foreach (TacticalItemDef item in Repo.GetAllDefs<TacticalItemDef>().Where(a => a.name.Contains("_BIO_")))
                {
                    item.PreserveDamageTaken = false;
                }           
        }
    }
}
