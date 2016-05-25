using System;
using System.Text;
using System.Windows.Forms;

namespace TL_Importer {
    public partial class Main : Form {
        string Custom = AppDomain.CurrentDomain.BaseDirectory + "Custom.cs";
        bool Initialized = true;
        public Main() {
        again:
            ;
            string Code = System.IO.File.Exists(Custom) ? System.IO.File.ReadAllText(Custom, Encoding.UTF8) : TL_Importer.Properties.Resources.DefaultFunctions;
            try {
                ProcessatorOOS = new HighLevelCodeProcessator(Code);
                ProcessatorOUS = new HighLevelCodeProcessator(Code);
                ProcessatorOTS = new HighLevelCodeProcessator(Code);
                ProcessatorUTS = new HighLevelCodeProcessator(Code);
            }
            catch {
                if (MessageBox.Show("Error in the algorithm, you can't use the program.", "TL Importer Engine", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    goto again;
                Initialized = false;
            }
            InitializeComponent();
        }
        //Original Outdated Script
        HighLevelCodeProcessator ProcessatorOOS;
        public string[] OOS;

        //Original Updated Script
        HighLevelCodeProcessator ProcessatorOUS;
        public string[] OUS;

        //Outdated Translated Script
        HighLevelCodeProcessator ProcessatorOTS;
        public string[] OTS;

        //Update Translated Script
        HighLevelCodeProcessator ProcessatorUTS;
        public string[] UTS;
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            
        }

        private string[] OpenScript(string FileName, HighLevelCodeProcessator Processator) {
            try {
                return (string[])Processator.Call("ScriptManager", "Open", new object[] { FileName });
            }
            catch {
                HighLevelCodeProcessator.Crash();
                throw new Exception();
            }
        }
        private void SaveScript(string As, string[] Content, HighLevelCodeProcessator Processator) {
            bool rst = (bool)Processator.Call("ScriptManager", "Save", new object[] { As, Content });
            if (!rst)
                HighLevelCodeProcessator.Crash();
        }
        private void OpenOOS_Click(object sender, EventArgs e) {
            string File = OpenFile();
            PathOOS.Text = File != null ? File : "Select a script...";
        }

        private void OpenOUS_Click(object sender, EventArgs e) {
            string File = OpenFile();
            PathOUS.Text = File != null ? File : "Select a script...";
        }

        private void OpenOTS_Click(object sender, EventArgs e) {
            string File = OpenFile();
            PathOTS.Text = File != null ? File : "Select a script...";
        }
        private string OpenFile() {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "All Files|*.*";
            OFD.Title = "Select a File";
            DialogResult dr = OFD.ShowDialog();
            return dr == DialogResult.OK ? OFD.FileName : null;
        }
        private string SaveFile() {
            SaveFileDialog OFD = new SaveFileDialog();
            OFD.Filter = "All Files|*.*";
            OFD.Title = "Select a File";
            DialogResult dr = OFD.ShowDialog();
            return dr == DialogResult.OK ? OFD.FileName : null;
        }

        private void processTranslationToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!Initialized) {
                MessageBox.Show("Fix the algorithm problem before.", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((PathOOS.Text + PathOUS.Text + PathOTS.Text).Contains("Select a script...")) {
                MessageBox.Show("Open the script before.", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OOS = OpenScript(PathOOS.Text, ProcessatorOOS);
            OUS = OpenScript(PathOUS.Text, ProcessatorOUS);
            OTS = OpenScript(PathOTS.Text, ProcessatorOTS);
            UTS = OpenScript(PathOUS.Text, ProcessatorUTS);
            Compare();
        }

        IndexLink[] Links = new IndexLink[0];
        private bool IsLinked(int index) {
            foreach (IndexLink link in Links)
                if (link.Index2 == index)
                    return true;
            return false;
        }
        private void Compare() {
            //UI = UpdatedIndex
            //OI = OutdatedIndex
            for (int UI = 0; UI < OUS.Length; UI++) {
                string Line = OUS[UI];
                IndexLink Link = new IndexLink() {
                    Index1 = UI
                };
                for (int OI = 0; OI < OOS.Length; OI++)
                    if (Line == OOS[OI] && !IsLinked(OI)) {
                        Link.Index2 = OI;
                        break;
                    }
                if (Link.HaveLink) {
                    IndexLink[] tmp = new IndexLink[Links.Length + 1];
                    Links.CopyTo(tmp, 0);
                    tmp[Links.Length] = Link;
                    Links = tmp;
                }
            }

            OriginalString.Items.Clear();
            foreach (string item in OUS) {
                OriginalString.Items.Add(item);
            }
            TranslationString.Items.Clear();
            foreach (string item in OTS) {
                TranslationString.Items.Add(item);
            }
        }

        private void TranslationString_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                int index = TranslationString.SelectedIndex;
                string status = "Not Found in the Updated Script";
                foreach (IndexLink link in Links)
                    if (link.Index2 == index) {
                        OriginalString.SelectedIndex = link.Index1;
                        status = string.Format("The outdated string {0} is the {1} updated string", index, link.Index1);
                        break;
                    }
                Status.Text = status;
            }
            catch {
                return;
            }
        }

        private void OriginalString_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                int index = OriginalString.SelectedIndex;
                string status = "Not Found in the Outdated Script";
                foreach (IndexLink link in Links)
                    if (link.Index1 == index) {
                        TranslationString.SelectedIndex = link.Index2;
                        status = string.Format("The updated string {0} is the {1} outdated string", index, link.Index2);
                        break;
                    }
                Status.Text = status;
            }
            catch {
                return;
            }
        }

        private void exportScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            string file = SaveFile();
            if (file != null) {
                foreach (IndexLink link in Links) {
                    UTS[link.Index1] = OTS[link.Index2];
                }
                SaveScript(file, UTS, ProcessatorUTS);
                MessageBox.Show("Translation Exported to the Updated Script.", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editAlgoritmsToolStripMenuItem_Click(object sender, EventArgs e) {
            string[] FindProgram = new string[] {
                @":\Program Files (x86)\Notepad++\notepad++.exe",
                @":\Program Files\Notepad++\notepad++.exe",
                @":\Program Files (x86)\Sublime Text 2\sublime_text.exe",
                @":\Program Files\Sublime Text 2\sublime_text.exe",
                @":\Program Files (x86)\Sublime Text 3\sublime_text.exe",
                @":\Program Files\Sublime Text 3\sublime_text.exe",
                @":\Program Files (x86)\Hidemaru\Hidemaru.exe",
                @":\Program Files\Hidemaru\Hidemaru.exe",
                @":\Program Files (x86)\sakura\sakura.exe",
                @":\Program Files\sakura\sakura.exe",
                @":\Program Files (x86)\IDM Computer Solutions\UltraEdit\Uedit64.exe",
                @":\Program Files\IDM Computer Solutions\UltraEdit\Uedit64.exe",
                @":\Program Files (x86)\PSPad editor\PSPad.exe",
                @":\Program Files\PSPad editor\PSPad.exe",
                @":\Program Files (x86)\EditPlus 3\editplus.exe",
                @":\Program Files\EditPlus 3\editplus.exe",
                @":\Program Files (x86)\EmEditor\EmEditor.exe",
                @":\Program Files\EmEditor\EmEditor.exe",
                @":\Program Files (x86)\TextPad 7\TextPad.exe",
                @":\Program Files\TextPad 7\TextPad.exe",
                @":\Program Files (x86)\TextPad 8\TextPad.exe",
                @":\Program Files\TextPad 8\TextPad.exe",
                @":\Program Files (x86)\Vim\vim74\gvim.exe",
                @":\Program Files\Vim\vim74\gvim.exe"

            };
            string[] FindDisc = new string[] { "C", "E", "D", "F", "I" };
            string Editor = "%systemroot%\notepad.exe";
            foreach (string disc in FindDisc)
                foreach (string Program in FindProgram)
                    if (System.IO.File.Exists(disc + Program)) {
                        Editor = disc + Program;
                        break;
                    }
            if (!System.IO.File.Exists(Custom))
                System.IO.File.WriteAllText(Custom, TL_Importer.Properties.Resources.DefaultFunctions, Encoding.UTF8);
            System.Diagnostics.Process.Start(Editor, Custom);
        }
    }

    public class IndexLink {
        public int Index1;
        public int Index2 = -1;
        public bool HaveLink { get { return Index2 >= 0; } }
    }

}
