using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            About a = new About();
            a.Show();
        }

        private string[] OpenScript(string FileName, HighLevelCodeProcessator Processator) {
            try {
                return (string[])Processator.Call("ScriptManager", "Open", FileName);
            }
            catch {
                HighLevelCodeProcessator.Crash();
                throw new Exception();
            }
        }
        private void SaveScript(string As, string[] Content, HighLevelCodeProcessator Processator) {
            bool rst = (bool)Processator.Call("ScriptManager", "Save", As, Content);
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
        private void Compare() {
            //UI = UpdatedIndex
            //OI = OutdatedIndex
            for (int UI = 0; UI < OUS.Length; UI++) {
                string Line = OUS[UI];
                for (int OI = 0; OI < OOS.Length; OI++)
                    if (Line == OOS[OI]) {
                        if (!TranslationDataBase.ContainsKey(Line))
                            TranslationDataBase.Add(Line, OTS[OI]);
                        break;
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
        }

        private void OriginalString_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                int index = OriginalString.SelectedIndex;
                string status = "Not Found in the Updated Script";
                if (TranslationDataBase.ContainsKey(UTS[index]))
                    status = string.Format("Translation Found.");
                Status.Text = status;
            }
            catch {
                return;
            }
        }

        Dictionary<string, string> TranslationDataBase = new Dictionary<string, string>();
        private void exportScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            string file = SaveFile();
            if (file != null) {
                for (int i = 0; i < UTS.Length; i++)
                    if (TranslationDataBase.ContainsKey(UTS[i]) && !OriginalString.GetItemChecked(i)) {
                        string TL = TranslationDataBase[UTS[i]];
                        if (TL == IGNORE)
                            continue;
                        UTS[i] = TL;
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
            string Editor = Environment.SystemDirectory + "\\notepad.exe";
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
            ExportDataBase();
            MessageBox.Show("Saved As: \"" + TLDicPath + "\"", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string TLDicPath { get { return AppDomain.CurrentDomain.BaseDirectory + "TL.DB"; } }
        public void ImportDataBase() {
            if (File.Exists(TLDicPath))
                using (StreamReader SR = new StreamReader(TLDicPath, Encoding.UTF8)) {
                    while (SR.Peek() != -1) {
                        string TL = SR.ReadLine();
                        if (!TL.Contains("\x0"))
                            continue;
                        string[] WTL = TL.Split('\x0');
                        if (!TranslationDataBase.ContainsKey(WTL[0]))
                            TranslationDataBase.Add(WTL[0], WTL[1]);
                    }
                    SR.Close();
                }
        }

        public void ExportDataBase() {
            using (StreamWriter SW = new StreamWriter(TLDicPath, false, Encoding.UTF8)) {
                for (int i = 0; i < TranslationDataBase.Count; i++) {
                    bool NotIsLast = (i + 1 < TranslationDataBase.Count);
                    string WTL = string.Format("{0}\x0{1}", TranslationDataBase.Keys.ElementAt(i), TranslationDataBase.Values.ElementAt(i));
                    SW.WriteLine(WTL);
                }
                SW.Close();
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {
            TranslationDataBase.Clear();
            ImportDataBase();
            MessageBox.Show("Imported From: \"" + TLDicPath + "\"", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e) {
            TranslationDataBase.Clear();
            MessageBox.Show("Database Cleared.", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            Form2 frm = new Form2();
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            for (int i = 0; i < UTS.Length; i++)
                if (UTS[i].Contains(frm.result))
                        OriginalString.SetItemChecked(i, true);
        }

        private void changeFilesNameToolStripMenuItem_Click(object sender, EventArgs e) {
            Form2 frm = new Form2();
            frm.Text = "Next File Name";
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            PathOOS.Text = System.IO.Path.GetDirectoryName(PathOOS.Text) + "\\" + frm.result;
            PathOUS.Text = System.IO.Path.GetDirectoryName(PathOUS.Text) + "\\" + frm.result;
            PathOTS.Text = System.IO.Path.GetDirectoryName(PathOTS.Text) + "\\" + frm.result;
        }

        private void addBlackListToolStripMenuItem_Click(object sender, EventArgs e) {
            Form2 frm = new Form2();
            frm.Text = "Line to add in the black list:";
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            if (TranslationDataBase.ContainsKey(frm.result))
                TranslationDataBase[frm.result] = IGNORE;
            else
                TranslationDataBase.Add(frm.result, IGNORE);
        }
        const string IGNORE = "%$:IGNORETRANSLATION:$%";

        private void delBlackListToolStripMenuItem_Click(object sender, EventArgs e) {
            Form2 frm = new Form2();
            frm.Text = "Line to remove from black list:";
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            if (TranslationDataBase.ContainsKey(frm.result))
                TranslationDataBase.Remove(frm.result);
        }
    }
}
