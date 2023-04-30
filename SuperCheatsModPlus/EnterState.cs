using Base.UI.VideoPlayback;
using PhoenixPoint.Home.View.ViewStates;
using System;
using System.Reflection;

namespace SuperCheatsModPlus
{
    internal class EnterState
    {

        public static void Postfix_UIStateHomeScreenCutscene_EnterState(UIStateHomeScreenCutscene __instance, VideoPlaybackSourceDef ____sourcePlaybackDef)
        {
            try
            {
                if (____sourcePlaybackDef == null)
                {
                    return;
                }

                if (____sourcePlaybackDef.ResourcePath.Contains("Game_Intro_Cutscene"))
                {
                    typeof(UIStateHomeScreenCutscene).GetMethod("OnCancel", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, null);
                }
            }
            catch (Exception e)
            {
                SuperCheatsModPlusLogger.Error(e);
            }
        }
    }
}