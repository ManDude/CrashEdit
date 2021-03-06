using Crash;
using System.Collections.Generic;
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
            AddMenu("Add Camera", Menu_AddCamera);
            AddMenu("Add Entity", Menu_AddEntity);
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

        void Menu_AddCamera()
        {
            OldCamera newcam = OldCamera.Load(new OldCamera(Entry.ENameToEID("NONE!"),0,0,new OldCameraNeighbor[4],0,0,0,0,1600,0,0,0,0,0,0,new List<OldCameraPosition>(),0).Save());
            OldZoneEntry.Cameras.Add(newcam);
            InsertNode(2 + OldZoneEntry.Cameras.Count - 1,new OldCameraController(this,newcam));
            OldZoneEntry.CameraCount = OldZoneEntry.Cameras.Count;
        }

        void Menu_AddEntity()
        {
            short maxid = 1;
            foreach (Chunk chunk in EntryChunkController.NSFController.NSF.Chunks)
            {
                if (chunk is EntryChunk)
                {
                    foreach (Entry entry in ((EntryChunk)chunk).Entries)
                    {
                        if (entry is OldZoneEntry)
                        {
                            foreach (OldEntity otherentity in ((OldZoneEntry)entry).Entities)
                            {
                                if (otherentity.ID > maxid)
                                {
                                    maxid = otherentity.ID;
                                }
                            }
                        }
                    }
                }
            }
            ++maxid;
            OldEntity newentity = OldEntity.Load(new OldEntity(0,0,0,maxid,0,0,0,0,0,new List<EntityPosition>() { new EntityPosition(0,0,0) },0).Save());
            OldZoneEntry.Entities.Add(newentity);
            AddNode(new OldEntityController(this,newentity));
            OldZoneEntry.EntityCount = OldZoneEntry.Entities.Count;
        }
    }
}
