namespace Crash
{
    [EntryType(7,GameVersion.Crash1BetaMAR08)]
    [EntryType(7,GameVersion.Crash1BetaMAY11)]
    [EntryType(7,GameVersion.Crash1)]
    public sealed class OldZoneEntryLoader : EntryLoader
    {
        public override Entry Load(byte[][] items,int eid,int size)
        {
            if (items.Length < 2)
            {
                ErrorManager.SignalError("OldZoneEntry: Wrong number of items");
            }
            byte[] unknown1 = items[0];
            byte[] unknown2 = items[1];
            int camcount = BitConv.FromInt32(unknown1,0x208);
            OldCamera[] cameras = new OldCamera[camcount];
            for (int i = 2; i < 2 + camcount; i++)
            {
                cameras[i - 2] = OldCamera.Load(items[i]);
            }
            int entitycount = BitConv.FromInt32(unknown1,0x20C);
            OldEntity[] entities = new OldEntity[entitycount];
            for (int i = 2 + camcount; i < items.Length; i++)
            {
                entities[i - 2 - camcount] = OldEntity.Load(items[i]);
            }
            return new OldZoneEntry(unknown1,unknown2,cameras,entities,eid,size);
        }
    }
}