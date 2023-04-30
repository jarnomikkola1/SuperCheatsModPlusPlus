using System.Linq;
using System.Collections.Generic;
using Base.Defs;
using Base.Core;
using PhoenixPoint.Geoscape.Events.Eventus;
using PhoenixPoint.Geoscape.Events;

namespace SuperCheatsModPlus
{
    internal class Corruption
    {
        public static void Change_Corruption()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            SuperCheatsModPlusConfig Config = (SuperCheatsModPlusConfig)SuperCheatsModPlusMain.Main.Config;
            if (Config.DisableCorruption == true)
            {

                GeoscapeEventDef geoEventCH0WIN2 = Repo.GetAllDefs<GeoscapeEventDef>().FirstOrDefault(ged => ged.name.Equals("PROG_CH2_WIN_GeoscapeEventDef"));
                GeoscapeEventDef geoEventCH0WIN = Repo.GetAllDefs<GeoscapeEventDef>().FirstOrDefault(ged => ged.name.Equals("PROG_CH0_WIN_GeoscapeEventDef"));

                /*
                var corruption = geoEventCH0WIN.GeoscapeEventData.Choices[0].Outcome.VariablesChange[1];
                var corruption2 = geoEventCH0WIN2.GeoscapeEventData.Choices[0].Outcome.VariablesChange[1];
                geoEventCH0WIN.GeoscapeEventData.Choices[0].Outcome.VariablesChange.Remove(corruption);
                geoEventCH0WIN2.GeoscapeEventData.Choices[0].Outcome.VariablesChange.Remove(corruption2);
                */

                geoEventCH0WIN.GeoscapeEventData.Choices[0].Outcome.VariablesChange = new List<OutcomeVariableChange>()
                        {
                            geoEventCH0WIN.GeoscapeEventData.Choices[0].Outcome.VariablesChange[0],
                        };

                geoEventCH0WIN2.GeoscapeEventData.Choices[0].Outcome.VariablesChange = null;
            }

        }
    }
}
