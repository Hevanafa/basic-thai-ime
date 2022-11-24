using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Diagnostics;

// Basic Thai IME word list reader & browser
// By Hevanafa (24-11-2022)

namespace ThaiIMEBasic
{
    class Manager
    {
        const string
            wordlistFilename = "thai_wordlist.txt",
            frequencyFilename = "frequency_list.txt";


        private readonly static Dictionary<string, string>
            singlesDict,
            advancedSinglesDict,
            transcriptionDict,
            advancedTranscriptionDict;
        private readonly static List<(string, List<string>)> wordlist = new List<(string, List<string>)>(); // same as Tuple<string, List<string>>
        private readonly static Dictionary<string, int> frequencyDict = new Dictionary<string, int>();

        // must be called once during lifetime
        static Manager() { 
            singlesDict = new Dictionary<string, string>() {
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
            advancedSinglesDict = new Dictionary<string, string>() {
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

            transcriptionDict = new Dictionary<string, string>() {
                { "ก", "k" },
                { "ข", "kh" },
                { "ฃ", "kh" },
                { "ค", "kh" },
                { "ฅ", "kh" },
                { "ฆ", "kh" },
                { "ง", "ng" },
                { "จ", "c" },
                { "ฉ", "ch" },
                { "ช", "ch" },
                { "ซ", "s" },
                { "ฌ", "ch" },
                { "ญ", "y" },
                { "ฎ", "d" },
                { "ฏ", "t" },

                { "ฐ", "th" },
                { "ฑ", "th" },
                { "ฒ", "th" },
                { "ณ", "n" },
                { "ด", "d" },
                { "ต", "t" },
                { "ถ", "th" },
                { "ท", "th" },
                { "ธ", "th" },
                { "น", "n" },
                { "บ", "b" },
                { "ป", "p" },
                { "ผ", "ph" },
                { "ฝ", "f" },
                { "พ", "ph" },
                { "ฟ", "f" },

                { "ภ", "ph" },
                { "ม", "m" },
                { "ย", "y" },
                { "ร", "r" },
                { "ฤ", "r" },
                { "ล", "l" },
                { "ฦ", "l" },
                { "ว", "w" },
                { "ศ", "s" },
                { "ษ", "s" },
                { "ส", "s" },
                { "ห", "h" },
                { "ฬ", "l" },
                { "อ", "o" },
                { "ฮ", "h" },

                { "ะ", "a" },
                { "า", "a" },
                { "ำ", "am" },

                { "เ", "e" },
                { "แ", "ae" },
                { "โ", "o" },
                { "ใ", "ai" },
                { "ไ", "ai" },
                { "ๅ", "a" },
                { "ๆ", "q" },

                { "๐", "0" },
                { "๑", "1" },
                { "๒", "2" },
                { "๓", "3" },
                { "๔", "4" },
                { "๕", "5" },
                { "๖", "6" },
                { "๗", "7" },
                { "๘", "8" },
                { "๙", "9" },

                { " ", " " }
            };
            loadWordList();
            loadFrequencyDict();
        }

        public static string getTranscription(string thai) =>
            string.Join("",
                thai.ToCharArray()
                    .Where(c => transcriptionDict.ContainsKey("" + c))
                    .Select(c => transcriptionDict["" + c]));

        public async static void loadWordList()
        {
            Debug.WriteLine("load word list");

            StreamReader sr = null;

            try
            {
                sr = new StreamReader(wordlistFilename);
                string word, transcription;

                while (!sr.EndOfStream)
                {
                    word = await sr.ReadLineAsync();
                    transcription = getTranscription(word);

                    var result = wordlist.Where(pair => pair.Item1 == transcription);
                    if (result.Count() == 0)
                        wordlist.Add((
                            transcription,
                            new List<string>() { word }));
                    else
                        result.First().Item2.Add(word);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        public async static void loadFrequencyDict() {
            Debug.WriteLine("load frequency dict");

            StreamReader sr = null;

            try
            {
                sr = new StreamReader(frequencyFilename);

                string line;
                string[] temp;

                while (!sr.EndOfStream) {
                    line = await sr.ReadLineAsync();
                    temp = line.Split('\t');
                    frequencyDict.Add(temp[0], Convert.ToInt32(temp[1]));
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally {
                if (sr != null)
                    sr.Close();
            }
        }

        public static string[] filterWordList(string term, int limit = 0) {
            // filter singles dictionary
            //var d = isAdvanced ? Manager.adv : dict;

            var singles = singlesDict.ContainsKey(term)
                ? singlesDict[term].Select(x => "" + x)
                : Enumerable.Empty<string>();

            // exact matches
            var exact = wordlist.Where(pair => pair.Item1 == term)
                .SelectMany(pair => pair.Item2);

            // match only the start, flatten list of lists
            var startsWith = wordlist.Where(pair => pair.Item1.StartsWith(term)) // pair.Item1.Contains(term)
                .SelectMany(pair => pair.Item2);

            // concat.sort.uniq
            // show singular characters first, then words
            var ie = singles
                .Concat(exact.Concat(startsWith)
                    .OrderByDescending(word => getFrequency(word))
                ).Distinct();

            return (limit > 0 ? ie.Take(limit) : ie).ToArray();
        }

        public static int getFrequency(string thaiWord) =>
            frequencyDict.ContainsKey(thaiWord) ?
                frequencyDict[thaiWord] : 0;
    }
}
