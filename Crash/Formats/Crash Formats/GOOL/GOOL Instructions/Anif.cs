﻿namespace Crash.GOOLIns
{
    [GOOLInstruction(132,GameVersion.Crash1)]
    [GOOLInstruction(132,GameVersion.Crash1Beta1995)]
    [GOOLInstruction(132,GameVersion.Crash1BetaMAR08)]
    [GOOLInstruction(132,GameVersion.Crash1BetaMAY11)]
    [GOOLInstruction(57,GameVersion.Crash2)]
    [GOOLInstruction(57,GameVersion.Crash3)]
    public sealed class Anif : GOOLInstruction
    {
        public Anif(int value,GOOLEntry gool) : base(value,gool) { }

        public override string Name => "ANIF";
        public override string Format => "[FFFFFFFFFFFF] ---- TTTTTT HH";
        public override string Comment => $"play frame {GetArg('F')} @{GetArg('T')}fps (flip {GetArg('H')})";
    }
}