using System;
using System.Collections.Generic;
using AnimalGenetics;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AGSGACS
{
    public class AGSGACS : Mod
    {
        public AGSGACS(ModContentPack content) : base(content)
        {
        }
    }
    [StaticConstructorOnStartup]
	public static class AGSGACSAssemblyLoader
    {
        static AGSGACSAssemblyLoader()
        {
            try
            {
                List<RimWorld.StatPart> parts = StatDefOf.CaravanRidingSpeedFactor.parts;
                if (parts != null)
                {
                    parts.Insert(0, new StatPartSpeed());
                } else
                {
                    parts = new List<RimWorld.StatPart>
                    {
                        new StatPartSpeed()
                    };
                    StatDefOf.CaravanRidingSpeedFactor.parts = parts;
                }
            }
            catch
            {
                Log.Error(string.Format("[AGSGACS]: {0} is broken", StatDefOf.CaravanRidingSpeedFactor));
            }
        }
	}
}