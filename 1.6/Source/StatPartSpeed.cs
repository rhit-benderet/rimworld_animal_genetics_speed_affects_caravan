using System;
using AnimalGenetics;
using RimWorld;
using Verse;

namespace AGSGACS
{
	// Token: 0x02000026 RID: 38
	public class StatPartSpeed : RimWorld.StatPart
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00005949 File Offset: 0x00003B49
		public StatPartSpeed()
		{
			this.priority = 1.1f;
		}
		public override void TransformValue(StatRequest req, ref float val)
		{
			float? factor = this.getFactor(req);
			if (factor != null)
			{
				val *= factor.Value;
			}
		}
		public override string ExplanationPart(StatRequest req)
		{
			if (!Genes.EffectsThing(req.Thing))
			{
				return null;
			}
			Pawn pawn = req.Thing as Pawn;
			if (pawn == null)
			{
				return "";
			}
			if (!CoreMod.Instance.Settings.omniscientMode && pawn.Faction != Faction.OfPlayer)
			{
				return null;
			}
			GeneRecord geneRecord = pawn.GetGeneRecord(StatDefOf.MoveSpeed);
			if (geneRecord == null)
			{
				return null;
			}
			string t = "";
			if (geneRecord.Parent == GeneRecord.Source.None)
			{
				return "AG.Genetics".Translate() + ": x" + geneRecord.Value.ToStringPercent() + t;
			}
			string str = (geneRecord.Parent == GeneRecord.Source.Mother) ? "♀" : "♂";
			GeneticInformation geneticInformation2;
			if (geneRecord.Parent != GeneRecord.Source.Mother)
			{
				GeneticInformation geneticInformation = pawn.AnimalGenetics();
				geneticInformation2 = ((geneticInformation != null) ? geneticInformation.Father : null);
			}
			else
			{
				GeneticInformation geneticInformation3 = pawn.AnimalGenetics();
				geneticInformation2 = ((geneticInformation3 != null) ? geneticInformation3.Mother : null);
			}
			GeneticInformation geneticInformation4 = geneticInformation2;
			if (geneticInformation4 == null)
			{
				return "AG.Genetics".Translate() + ": x" + geneRecord.Value.ToStringPercent() + t;
			}
			float value = geneticInformation4.GeneRecords[StatDefOf.MoveSpeed].Value;
			t = " (x" + value.ToStringPercent() + str + ")";
			return "AG.Genetics".Translate() + ": x" + geneRecord.Value.ToStringPercent() + t;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005B10 File Offset: 0x00003D10
		private float? getFactor(StatRequest req)
		{
			if (!req.HasThing)
			{
				return null;
			}
			if (!Genes.EffectsThing(req.Thing))
			{
				return null;
			}
			Pawn pawn = req.Thing as Pawn;
			if (pawn == null)
			{
				Log.Error("[AnimalGenetics]: " + req.Thing.ToStringSafe<Thing>() + " is not a Pawn");
			}
			GeneRecord geneRecord = pawn.GetGeneRecord(StatDefOf.MoveSpeed);
			return new float?((geneRecord != null) ? geneRecord.Value : 1f);
		}
	}
}
