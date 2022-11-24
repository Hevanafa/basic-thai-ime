using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ThaiIMEBasic
{
    public partial class Main : Form
    {
        Dictionary<string, string> dict, advancedDict;
        //string result;
        List<string> resultList = new List<string>();
        Stopwatch stopwatch = new Stopwatch();

        public Main()
        {
            InitializeComponent();

            dict = new Dictionary<string, string>() {
                { "b", "บ" },
                { "c", "จ" },
                { "ch", "ฉชฌ" },
                { "d", "ฎด" },
                { "f", "ฝฟ" },
                { "k", "ก" },
                { "kh", "ขฃคฅฆ"},
                { "h", "หฮ"},
                { "l", "ลฦฬ"},
                { "m", "ม"},
                { "n", "ณน" },
                { "ng", "ง" },
                { "ny", "ญญ"},
                { "o", "อโ" },
                { "p", "ป" },
                { "ph", "ผพภ"},
                { "r", "รฤ"},
                //{ "rr", "รร" },
                { "s", "ซศษส" },
                { "t", "ฏต" },
                { "th", "ฐฑฒถทธ"},
                { "w", "ว"},
                { "y", "ญย"},

                // special case
                { "q", "ๆ" },
                { "-", "\u0e47" },

                // vowels
                { "a", "ะ\u0e31า" },
                { "ae", "แ" },
                { "am", "ำ" },
                { "ai", "ใไ" },
                { "e", "เ" },
                { "i", "\u0e34\u0e35"},
                { "ue", "\u0e36\u0e37ๅ"},
                { "u", "\u0e38\u0e39"},

                // tone marks
                { "1", "\u0e48" },
                { "2", "\u0e49" },
                { "3", "\u0e4a" },
                { "4", "\u0e4b" },
                { "8", "\u0e47" },
                { "9", "\u0e4c" },
            };
            advancedDict = new Dictionary<string, string>() {
                { "b", "บพ" },
                { "bh", "ภ" },
                { "c", "จ " },
                { "ch", "ฉ" },
                { "d", "ฎฑดท" },
                { "dh", "ฒธ" },
                { "f", "ฟ" },
                { "g", "ค " },
                { "gg", "ฅ"}, // obsolete
                { "gh", "ฆ" },
                { "k", "ก " },
                { "kh", "ข"},
                { "h", "หฮ"},
                { "l", "ลฦฬ"},
                { "j", "ช "},
                { "jh", "ฌ"},
                { "m", "ม"},
                { "n", "ณน" },
                { "ng", "ง" },
                { "ny", "ญ"},
                { "o", "อโ" },
                { "p", "ป " },
                { "ph", "ผ"},
                { "r", "รฤ"},
                //{ "rr", "รร" },
                { "s", "ศษส" },
                { "t", "ฏต" },
                { "th", "ฐถ"},
                { "v", "ฝ" },
                { "w", "ว"},
                { "x", "ฃ"}, // obsolete
                { "y", "ย"},
                { "z", "ซ"},

                // special case
                { "q", "ๆ" },
                { "-", "\u0e47" }, // mai-taikhu

                // vowels
                { "a", "ะ\u0e31า" },
                { "ae", "แ" },
                { "am", "ำ" },
                { "ai", "ใไ" },
                { "e", "เ" },
                { "i", "\u0e34\u0e35"},
                { "ue", "\u0e36\u0e37ๅ"},
                { "u", "\u0e38\u0e39"},

                // tone marks
                { "1", "\u0e48" },
                { "2", "\u0e49" },
                { "3", "\u0e4a" },
                { "4", "\u0e4b" },
                { "8", "\u0e47" },
                { "9", "\u0e4c" },
            };

            Manager.init();
        }

        //string trimmedResult {
        //    get { return result?.Trim(); }
        //}

        bool isAdvanced {
            get { return cbAdvanced?.Checked ?? false; }
        }

        void updateFoundCounter() {
            var secondsStr = decimal.Round((decimal) stopwatch.Elapsed.TotalSeconds, 2, MidpointRounding.AwayFromZero); // $"{stopwatch.Elapsed.TotalSeconds:#.##}";
            lblFoundCount.Text = $"Found {resultList.Count} item(s) in {secondsStr} s";
        }

        void updateList() {
            stopwatch.Restart();

            lbCandidates.Items.Clear();
            lbCandidates.SelectedIndex = -1;
            resultList.Clear();

            var term = txbInput.Text.ToLower();

            if (term.Length == 0)
            {
                stopwatch.Stop();
                updateFoundCounter();
                return;
            }

            var d = isAdvanced ? advancedDict : dict;
            //result = d.ContainsKey(term)
            //    ? d[term] : null;

            if (d.ContainsKey(term))
                resultList.AddRange(d[term].Select(x => "" + x));

            //if (result != null)
            //{
            //    if (result.Length == 1) {
            //        appendOutput(result[0]);
            //        clearInput();
            //        return;
            //    }

            //    char c;
            //    for (var a = 0; a < trimmedResult.Length; a++)
            //    {
            //        c = result[a];
            //        lbCandidates.Items.Add($"{a + 1} - { c }");
            //    }

            //    lbCandidates.SelectedIndex = 0;
            //}

            // add results from the word list
            var result = Manager.search(term);
            //Debug.WriteLine("Found " + result.Length);
            resultList.AddRange(result);

            lbCandidates.Items.AddRange(
                resultList.Select((word, idx) =>
                    (idx < 9 ? idx + 1 + " - " : "") + word
                ).ToArray()
            );

            if (resultList.Count > 0)
                lbCandidates.SelectedIndex = 0;

            stopwatch.Stop();
            updateFoundCounter();
        }

        bool skipNext = false;
        private void txbInput_TextChanged(object sender, EventArgs e)
        {
            if (skipNext) {
                skipNext = false;
                clearInput();
                return;
            }

            updateList();
        }

        private void txbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        void clearInput() {
            txbInput.Text = "";
        }

        void appendOutput(char c) {
            txbOutput.Text += c;
        }

        void appendOutput(string s) {
            txbOutput.Text += s;
        }

        void insertOutput(char c) {
            txbOutput.Text = txbOutput.Text.Insert(txbOutput.SelectionStart, $"{ c }");
            txbOutput.SelectionStart++;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void txbInput_KeyDown(object sender, KeyEventArgs e)
        {
            int idx;

            switch (e.KeyCode) {
                case Keys.Space:
                    e.Handled = true;
                    appendOutput(' ');
                    break;
                case Keys.Enter:
                    // transfer input
                    idx = lbCandidates.SelectedIndex;
                    if (idx >= 0)
                    {
                        appendOutput(resultList[idx]);
                        clearInput();
                    }

                    break;
                case Keys.Delete:
                    clearInput();
                    break;

                case Keys.Up:
                    if (lbCandidates.SelectedIndex > 0)
                        lbCandidates.SelectedIndex--;
                    break;
                case Keys.Down:
                    if (lbCandidates.SelectedIndex < lbCandidates.Items.Count - 1)
                        lbCandidates.SelectedIndex++;
                    break;

                case Keys k when k >= Keys.D1 && k <= Keys.D9:
                    //if (result?.Length > 0 && char.IsLetter(txbInput.Text[0]))
                    idx = k - Keys.D1;

                    if (idx < resultList.Count)
                    {
                        //appendOutput(result[k - Keys.D1]);
                        appendOutput(resultList[idx]);
                        clearInput();
                        skipNext = true;
                    }

                    break;
            }
        }
    }
}
