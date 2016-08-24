#define NOPORTABLE

using SiglusSceneManager;
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
#if NOPORTABLE
#else
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
#endif
            InitializeComponent();
        }

        //Original Outdated Script
#if NOPROTABLE
        HighLevelCodeProcessator ProcessatorOOS;
        HighLevelCodeProcessator ProcessatorOUS;
        HighLevelCodeProcessator ProcessatorOTS;
        HighLevelCodeProcessator ProcessatorUTS;
#else
        ScriptManager ProcessatorOOS;
        ScriptManager ProcessatorOUS;
        ScriptManager ProcessatorOTS;
        ScriptManager ProcessatorUTS;
#endif    
        public string[] OOS;

        //Original Updated Script
        public string[] OUS;

        //Outdated Translated Script
        public string[] OTS;

        //Update Translated Script
        public string[] UTS;
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About a = new About();
            a.Show();
        }

#if NOPORTABLE
        class ScriptManager {
            public SSManager Script;
            public string[] Open(string FileName) {
                if (!File.Exists(FileName))
                    return new string[0];
                byte[] data = System.IO.File.ReadAllBytes(FileName);
                Script = new SSManager(data);
                Script.Import();
                return Script.Strings;
            }
            public bool Save(string SavePath, string[] NewStrings) {
                Script.Strings = NewStrings;
                try {
                    byte[] data = Script.Export();
                    System.IO.File.WriteAllBytes(SavePath, data);
                }
                catch {
                    return false;
                }
                return true;
            }
        }

#endif
#if NOPORTABLE
        private string[] OpenScript(string FileName, ref ScriptManager Processator) {
            Processator = new ScriptManager();
            return Processator.Open(FileName);
        }

        private void SaveScript(string As, string[] Content, ScriptManager Processator) {
            Processator.Save(As, Content);
        }
#else
       private string[] OpenScript(string FileName, HighLevelCodeProcessator Processator, bool IgnoreError, out bool Result) {
            try {
                Result = true;
                return (string[])Processator.Call("ScriptManager", "Open", FileName);
            }
            catch {
                if (IgnoreError) {
                    Result = false;
                    return new string[0];
                }
                HighLevelCodeProcessator.Crash();
                throw new Exception();
            }
    }
        private void SaveScript(string As, string[] Content, HighLevelCodeProcessator Processator) {
            bool rst = (bool)Processator.Call("ScriptManager", "Save", As, Content);
            if (!rst)
                HighLevelCodeProcessator.Crash();
        }
#endif

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
#if NOPORTABLE
            OOS = OpenScript(PathOOS.Text, ref ProcessatorOOS);
            OUS = OpenScript(PathOUS.Text, ref ProcessatorOUS);
            OTS = OpenScript(PathOTS.Text, ref ProcessatorOTS);
            UTS = OpenScript(PathOUS.Text, ref ProcessatorUTS);
#else
            bool ig;
            OOS = OpenScript(PathOOS.Text, ProcessatorOOS, false, out ig);
            OUS = OpenScript(PathOUS.Text, ProcessatorOUS, false, out ig);
            OTS = OpenScript(PathOTS.Text, ProcessatorOTS, false, out ig);
            UTS = OpenScript(PathOUS.Text, ProcessatorUTS, false, out ig);
#endif
            Compare();
        }
        private void Compare(bool DBOnly = false) {
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
            if (DBOnly)
                return;

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
                        if (!TL.Contains("\x0") || TL.StartsWith("//"))
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
            frm.Text = "Line to remove from DataBase:";
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            if (TranslationDataBase.ContainsKey(frm.result))
                TranslationDataBase.Remove(frm.result);
        }

        private void bathOperationToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!Initialized) {
                MessageBox.Show("Fix the algorithm problem before.", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((PathOOS.Text + PathOUS.Text + PathOTS.Text).Contains("Select a script...")) {
                MessageBox.Show("Open the script before.", "TL Importer Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FolderBrowserDialog BD = new FolderBrowserDialog();
            if (BD.ShowDialog() != DialogResult.OK)
                return;

            //Prepare Variables
            TextWriter LOG = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "Bath Import.log");
            TextWriter Untranslated = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "Bath Import Failed.log");
            PathOOS.Text = System.IO.Path.GetDirectoryName(PathOOS.Text) + "\\";
            PathOUS.Text = System.IO.Path.GetDirectoryName(PathOUS.Text) + "\\";
            PathOTS.Text = System.IO.Path.GetDirectoryName(PathOTS.Text) + "\\";
            string OutDir = BD.SelectedPath;
            List<string> NotFounds = new List<string>();
            List<string> Positions = new List<string>();

            if (!OutDir.EndsWith("\\"))
                OutDir += "\\";

            //Step 1 - Create a Fully Database
            string[] Scripts = System.IO.Directory.GetFiles(PathOOS.Text, "*.ss");
            bool[] Okay = new bool[Scripts.Length];
            for (int i = 0; i < Okay.Length; i++)
                Okay[i] = true;
            for (int i = 0; i < Scripts.Length; i++) {
                string ScriptName = Path.GetFileName(Scripts[i]);
                if (!File.Exists(PathOUS.Text + ScriptName) || !File.Exists(PathOTS.Text + ScriptName))
                    Okay[i] = false;
            }
            for (int i = 0; i < Scripts.Length; i++) {
                string ScriptName = Path.GetFileName(Scripts[i]);
                if (!Okay[i])
                    LOG.WriteLine("Unable to Append the \"" + ScriptName + "\" in the database.");

                Text = "Importing: " + i + "/" + Scripts.Length;
#if NOPORTABLE
                OOS = OpenScript(PathOOS.Text + ScriptName, ref ProcessatorOOS);
                OUS = OpenScript(PathOUS.Text + ScriptName, ref ProcessatorOUS);
                OTS = OpenScript(PathOTS.Text + ScriptName, ref ProcessatorOTS);
#else
                bool Sucess;
                OOS = OpenScript(PathOOS.Text + ScriptName, ProcessatorOOS, true, out Sucess);
                if (!Sucess) {
                    LOG.WriteLine("Unable to Compare \"" + ScriptName + "\".");
                    continue;
                }
                OUS = OpenScript(PathOUS.Text + ScriptName, ProcessatorOUS, true, out Sucess);
                if (!Sucess) {
                    LOG.WriteLine("Unable to Compare \"" + ScriptName + "\".");
                    continue;
                }
                OTS = OpenScript(PathOTS.Text + ScriptName, ProcessatorOTS, true, out Sucess);
                if (!Sucess) {
                    LOG.WriteLine("Unable to Compare \"" + ScriptName + "\".");
                    continue;
                }
#endif
                Compare(true);
            }

            //Step 2 - Copy Translations
            Scripts = System.IO.Directory.GetFiles(PathOUS.Text, "*.ss");
            for (int ind = 0; ind < Scripts.Length; ind++){
                string Script = Scripts[ind];
                string ScriptName = Path.GetFileName(Script);
#if NOPORTABLE
                UTS = OpenScript(PathOUS.Text + ScriptName, ref ProcessatorUTS);
#else
                bool Sucess;
                UTS = OpenScript(PathOUS.Text + ScriptName, ProcessatorUTS, true, out Sucess);
                if (!Sucess) {
                    LOG.WriteLine("Failed to Open the Script: \"" + ScriptName +"\".");
                    continue;
                }
#endif
                Text = "Translating: " + ind + "/" + Scripts.Length;
                bool Fully = true;
                for (int i = 0; i < UTS.Length; i++) {
                    if (TranslationDataBase.ContainsKey(UTS[i])) {
                        string Line = TranslationDataBase[UTS[i]];
                        if (Line == IGNORE) {
                            continue;
                        }
                        else
                            UTS[i] = Line;
                    } else {
                        if (!NotFounds.Contains(UTS[i])) {
                            Fully = false;
                            NotFounds.Add(UTS[i]);
                            Positions.Add(string.Format("//Found At \"{0}\" with ID: {1} Line: {2}", ScriptName, i, ((i * 3) + 5)));
                        }
                    }
                }
                SaveScript(OutDir + ScriptName, UTS, ProcessatorUTS);
                LOG.WriteLine("Sucess with \"" + ScriptName + "\" Import. " + (Fully ? "(Complete)" : "(Partial)"));
            }
            for (int i = 0; i < NotFounds.Count; i++) {
                Untranslated.WriteLine(Positions.ElementAt(i));
                Untranslated.WriteLine(NotFounds.ElementAt(i) + "==" + IGNORE);
                Untranslated.WriteLine();
            }
            //End Algorithm
            Text = "TL Importer Engine";
            Untranslated.Close();
            LOG.Close();
            MessageBox.Show("Bath Operation Completed!", "TL Impotert Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addInDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            Form2 frm = new Form2();
            frm.Text = "Line to add in the Database:";
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            if (TranslationDataBase.ContainsKey(frm.result)) {
                MessageBox.Show("This entry is actually in the database with the value:\n" + TranslationDataBase[frm.result], "TL Impotert Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string Entry = frm.result;
            frm.Text = "Value of this new entry to add:";
            frm.ShowDialog();
            if (string.IsNullOrEmpty(frm.result))
                return;
            TranslationDataBase.Add(Entry, frm.result);
            MessageBox.Show("Entry Added.", "TL Impotert Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
