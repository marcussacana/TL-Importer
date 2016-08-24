using SiglusSceneManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RewritePlusWordWrap {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        public SSManager Script;
        public string[] Open(string FileName) {
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
        private void button1_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            textBox1.Text = folderBrowserDialog1.SelectedPath;
            string[] Scripts = System.IO.Directory.GetFiles(textBox1.Text, "*.ss");
            for (int j = 0; j < Scripts.Length; j++) {
                Text = "Working... " + j + "/" + Scripts.Length;
                string[] Strings = Open(Scripts[j]);
                for (int i = 0; i < Strings.Length; i++) {
                    if (isString(Strings[i])) {
                        Strings[i] = WordWrap(Strings[i]);
                    }
                }
                Save(Scripts[j], Strings);
            }
            Text = "Rewrite+ WordWrapper";
        }

        private string WordWrap(string line) {
            if (line.Contains("para mostrar o quanto nos damos bem,"))
                System.Diagnostics.Debugger.Break();
            const char Separator = '\n';
            //const int MaxLen = 52; //Rewrite+ Terra Dialogue
            const int MaxLen = 50; //Rewrite+ Default Dialogue
            string outLine = string.Empty;
            string[] Words = line.Split(' ');
            string[] Lines = new string[0];
            AdvLine(ref Lines);
            for (int i = 0, l = 0; i < Words.Length; i++) {
                if (Lines[l].Length + Words[i].Length > MaxLen) {
                    AdvLine(ref Lines);
                    l++;
                }

                if (Lines[l].Length + Words[i].Length == MaxLen) {
                    Lines[l] += Words[i];
                    AdvLine(ref Lines);
                    l++;
                    continue;
                }
                if (Lines[l].Length + Words[i].Length + 1 == MaxLen) {
                    Lines[l] += Words[i] + ' ';
                    AdvLine(ref Lines);
                    l++;
                    continue;
                }
                Lines[l] += Words[i] + ' ';
            }
            foreach (string L in Lines) {
                string Line = L;
                if (Line.StartsWith(@" "))
                    Line = Line.Substring(1, Line.Length - 1);
                if (Line.EndsWith(@" "))
                    Line = Line.Substring(0, Line.Length - 1);
                outLine += Line + Separator;
            }
            if (outLine.EndsWith(Separator.ToString()))
                outLine = outLine.Substring(0, outLine.Length - 1);
            return outLine;
        }

        private void AdvLine(ref string[] Arr) {
            if (Arr.Length != 0 && Arr[Arr.Length - 1].Length != 52 && Arr[Arr.Length - 1].EndsWith(@" "))
                Arr[Arr.Length - 1] = Arr[Arr.Length - 1].Substring(0, Arr[Arr.Length - 1].Length - 1);
            Array.Resize(ref Arr, Arr.Length + 1);
            Arr[Arr.Length - 1] = string.Empty;
        }

        private bool isString(string text) {
            bool Status = true;
            int Process = 0;
            while (Status) {
                switch (Process) {
                    default:
                        goto ExitWhile;
                    case 0:
                        Status = !ContainsOR(text, "@,§,$,|,_,<,>");
                        break;
                    case 1:
                        Status = NumberLimiter(text, text.Length / 4);
                        break;
                    case 2:
                        Status = text.Length >= 3;
                        break;
                    case 3:
                        Status = text.Contains(((char)32).ToString()) || ContainsOR(text.Substring(text.Length - 2, 2), "!,?,.");
                        break;
                }
                Process++;
            }
        ExitWhile:
            ;
            if (!Status)
                return false;
            return Status;
        }
        private bool NumberLimiter(string text, int val) {
            int min = '0', max = '9', total = 0;
            int asmin = '０', asmax = '９';
            foreach (char chr in text)
                if (chr >= min && chr <= max)
                    total++;
                else if (chr >= asmin && chr <= asmax)
                    total++;
            return total < val;
        }
        private bool ContainsOR(string text, string MASK) {
            string[] entries = MASK.Split(',');
            foreach (string entry in entries)
                if (text.Contains(entry))
                    return true;
            return false;
        }
    }
}
