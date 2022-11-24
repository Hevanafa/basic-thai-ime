using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThaiIMEBasic
{
    public partial class Main : Form
    {
        Dictionary<string, string> dict, advancedDict;
        string result;

        public Main()
        {
            InitializeComponent();

            dict = new Dictionary<string, string>() {
                { "b", "บ" },
                { "c", "จ " },
                { "ch", "ฉชฌ" },
                { "d", "ฎด" },
                { "f", "ฝฟ" },
                { "k", "ก " },
                { "kh", "ขฃคฅฆ"},
                { "h", "หฮ"},
                { "l", "ลฦฬ"},
                { "m", "ม"},
                { "n", "ณน" },
                { "ng", "ง" },
                { "ny", "ญญ"},
                { "o", "อโ" },
                { "p", "ป " },
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
        }

        string trimmedResult {
            get { return result?.Trim(); }
        }

        bool isAdvanced {
            get { return cbAdvanced?.Checked ?? false; }
        }

        void updateList() {
            lbCandidates.Items.Clear();
            lbCandidates.SelectedIndex = -1;
            var search = txbInput.Text.ToLower();

            var d = isAdvanced ? advancedDict : dict;
            result = d.ContainsKey(search)
                ? d[search] : null;

            if (result != null)
            {
                if (result.Length == 1) {
                    appendOutput(result[0]);
                    clearInput();
                    return;
                }

                char c;
                for (var a = 0; a < trimmedResult.Length; a++)
                {
                    c = result[a];
                    lbCandidates.Items.Add($"{a + 1} - { c }");
                }

                lbCandidates.SelectedIndex = 0;
            }
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

        void insertOutput(char c) {
            txbOutput.Text = txbOutput.Text.Insert(txbOutput.SelectionStart, $"{ c }");
            txbOutput.SelectionStart++;
        }

        private void txbInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Space:
                    e.Handled = true;
                    appendOutput(' ');
                    break;
                case Keys.Enter:
                    // transfer input
                    var idx = lbCandidates.SelectedIndex;
                    if (idx >= 0)
                    {
                        appendOutput(result[idx]);
                        clearInput();
                    }

                    break;
                case Keys.Delete:
                    clearInput();
                    break;

                case Keys.Up:
                    lbCandidates.SelectedIndex--;
                    break;
                case Keys.Down:
                    lbCandidates.SelectedIndex++;
                    break;

                case Keys k when k >= Keys.D0 && k <= Keys.D9:
                    if (result?.Length > 0 && char.IsLetter(txbInput.Text[0]))
                    {
                        appendOutput(result[k - Keys.D1]);
                        clearInput();
                        skipNext = true;
                    }

                    break;
            }
        }
    }
}
