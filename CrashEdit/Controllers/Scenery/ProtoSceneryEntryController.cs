using Crash;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class ProtoSceneryEntryController : EntryController
    {
        public ProtoSceneryEntryController(EntryChunkController entrychunkcontroller,ProtoSceneryEntry protosceneryentry) : base(entrychunkcontroller,protosceneryentry)
        {
            ProtoSceneryEntry = protosceneryentry;
            if (protosceneryentry.ExtraData != null)
            {
                AddNode(new ItemController(null,protosceneryentry.ExtraData));
            }
            AddMenuSeparator();
            AddMenu("Export as OBJ",Menu_Export_OBJ);
            AddMenu("Export as COLLADA",Menu_Export_COLLADA);
            InvalidateNode();
        }

        public override void InvalidateNode()
        {
            Node.Text = string.Format("Prototype Scenery ({0})",ProtoSceneryEntry.EName);
            Node.ImageKey = "blueb";
            Node.SelectedImageKey = "blueb";
        }

        protected override Control CreateEditor()
        {
            return new UndockableControl(new ProtoSceneryEntryViewer(ProtoSceneryEntry));
        }

        public ProtoSceneryEntry ProtoSceneryEntry { get; }

        private void Menu_Export_OBJ()
        {
            if (MessageBox.Show("Exporting to OBJ is experimental.\nTexture information will not be exported.\n\nContinue anyway?","Export as OBJ",MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(ProtoSceneryEntry.ToOBJ(),FileFilters.OBJ,FileFilters.Any);
        }

        private void Menu_Export_COLLADA()
        {
            if (MessageBox.Show("Exporting to COLLADA is experimental.\nTexture information will not be exported.\n\nContinue anyway?","Export as COLLADA",MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(ProtoSceneryEntry.ToCOLLADA(),FileFilters.COLLADA,FileFilters.Any);
        }
    }
}
