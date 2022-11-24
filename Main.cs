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
        List<string> resultList = new List<string>();
        Stopwatch stopwatch = new Stopwatch();

        public Main()
        {
            InitializeComponent();
        }

        bool isAdvanced {
            get { return cbAdvanced?.Checked ?? false; }
        }

        bool showFrequency {
            get { return cbFrequency?.Checked ?? false; }
        }

        bool isLimited {
            get { return cbLimit?.Checked ?? false; }
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

            // add results from the word list
            var result = Manager.filterWordList(term, isLimited ? 100 : 0);

            //Debug.WriteLine("Found " + result.Length);
            resultList.AddRange(result);

            lbCandidates.Items.AddRange(
                resultList.Select((word, idx) =>
                    string.Join(" ", new string[] {
                        idx < 9 ? idx + 1 + " - " : "",
                        word,
                        showFrequency && word.Length > 1 ? $"({Manager.getFrequency(word)})" : ""
                    })
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

        void transferInput() {
            // transfer input
            var idx = lbCandidates.SelectedIndex;
            if (idx >= 0)
            {
                appendOutput(resultList[idx]);
                clearInput();
            }
        }

        private void lbCandidates_DoubleClick(object sender, EventArgs e)
        {
            transferInput();
        }

        private void lbCandidates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                transferInput();
        }

        private void txbInput_KeyDown(object sender, KeyEventArgs e)
        {
            int idx;

            switch (e.KeyCode) {
                case Keys.Space:
                    //appendOutput(' ');
                    //break;
                    //e.Handled = true;
                    transferInput();
                    skipNext = true;
                    break;

                case Keys.Enter:
                    transferInput();
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
