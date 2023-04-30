using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Tactical.Entities.Abilities;
using System.Linq;

namespace SuperCheatsModPlus
{
    internal class SoldierSkills
    {
        private static readonly DefRepository Repo = SuperCheatsModPlusMain.Repo;
        private static readonly SharedData Shared = SuperCheatsModPlusMain.Shared;
        public static void Skills()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;
            ApplyStatusAbilityDef Rally = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(a => a.name.Equals("Rally_AbilityDef"));

            if (Config.OpSoldierSkills == true)
            {
                SpecializationDef assault = Repo.GetAllDefs<SpecializationDef>().FirstOrDefault(a => a.name.Equals("AssaultSpecializationDef"));
                assault.AbilityTrack.AbilitiesByLevel[5] = new AbilityTrackSlot
                {
                    Ability = Rally,
                    RequiresPrevAbility = false,
                };
                SpecializationDef sniper = Repo.GetAllDefs<SpecializationDef>().FirstOrDefault(a => a.name.Equals("SniperSpecializationDef"));
                sniper.AbilityTrack.AbilitiesByLevel[2] = new AbilityTrackSlot
                {
                    Ability = Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault((TacticalAbilityDef a) => a.name.Equals("Gunslinger_AbilityDef")),
                    RequiresPrevAbility = false,
                };
                SpecializationDef heavy = Repo.GetAllDefs<SpecializationDef>().FirstOrDefault(a => a.name.Equals("HeavySpecializationDef"));
                heavy.AbilityTrack.AbilitiesByLevel[6] = new AbilityTrackSlot
                {
                    Ability = Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("RageBurst_ShootAbilityDef")),
                    RequiresPrevAbility = false,
                };
            }
        }
    }
}