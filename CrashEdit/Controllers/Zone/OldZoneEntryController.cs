using Crash;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class OldZoneEntryController : EntryController
    {
        public OldZoneEntryController(EntryChunkController entrychunkcontroller,OldZoneEntry zoneentry)
            : base(entrychunkcontroller,zoneentry)
        {
            OldZoneEntry = zoneentry;
            AddNode(new ItemController(null,zoneentry.Header));
            AddNode(new ItemController(null,zoneentry.Layout));
            foreach (OldCamera camera in zoneentry.Cameras)
            {
                AddNode(new OldCameraController(this,camera));
            }
            foreach (OldEntity entity in zoneentry.Entities)
            {
                AddNode(new OldEntityController(this,entity));
            }
            InvalidateNode();
        }

        public override void InvalidateNode()
        {
            Node.Text = string.Format("Old Zone ({0})",OldZoneEntry.EName);
            Node.ImageKey = "violetb";
            Node.SelectedImageKey = "violetb";
        }

        protected override Control CreateEditor()
        {
            int linkedsceneryentrycount = BitConv.FromInt32(OldZoneEntry.Header,0);
            OldSceneryEntry[] linkedsceneryentries = new OldSceneryEntry[linkedsceneryentrycount];
            for (int i = 0; i < linkedsceneryentrycount; i++)
            {
                linkedsceneryentries[i] = FindEID<OldSceneryEntry>(BitConv.FromInt32(OldZoneEntry.Header,4 + i * 64));
            }
            int linkedzoneentrycount = BitConv.FromInt32(OldZoneEntry.Header,528);
            OldZoneEntry[] linkedzoneentries = new OldZoneEntry[linkedzoneentrycount];
            for (int i = 0; i < linkedzoneentrycount; i++)
            {
                linkedzoneentries[i] = FindEID<OldZoneEntry>(BitConv.FromInt32(OldZoneEntry.Header,532 + i * 4));
            }
            return new UndockableControl(new OldZoneEntryViewer(OldZoneEntry,linkedsceneryentries,linkedzoneentries));
        }

        public OldZoneEntry OldZoneEntry { get; }
    }
}
