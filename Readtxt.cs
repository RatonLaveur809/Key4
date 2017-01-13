using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Key4
{
    class Readtxt
    {
        public List<string> strings = new List<string>();
        public string Greetings;
        public string EndOfText;
        public string Saved;
        public string Help;
        public string teacheron;
        public string teacheroff;
        public string success;
        public string fail;
        public string close;
        public List<string> offer = new List<string>();
        public List<string> phrase = new List<string>();
        public int CurrentOffer = 0;
        public int CurrentPhrase = 0;
        // Constructor, loads Replics from the Replics.txt
        public Readtxt()
        {
            readlines("KeysText\\Replics.txt");
            // Loading of the Greetings phrase
            #region greetings
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<greetings>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</greetings>")) { break; }
                        Greetings += strings[j];
                    }

                }
            }
            #endregion greetings
            //Loading of the End of text phrase
            #region End of Text
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<end of text>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</end of text>")) { break; }
                        EndOfText += strings[j];
                    }
                }
            }
            
            #endregion End of Text
            #region saved
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<saved>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</saved>")) { break; }
                        Saved += strings[j];
                    }
                }
            }
            #endregion saved
            #region help
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<help>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</help>")) { break; }
                        Help += strings[j];
                    }
                }
            }
            #endregion help
            #region teacher on
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<teacher on>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</teacher on>")) { break; }
                        teacheron += strings[j];
                    }
                }
            }
            #endregion teacher on
            #region teacher off
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<teacher off>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</teacher off>")) { break; }
                        teacheroff += strings[j];
                    }
                }
            }
            #endregion teacher off
            #region success
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<success>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</success>")) { break; }
                        success += strings[j];
                    }
                }
            }
            #endregion success
            #region fail
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<fail>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</fail>")) { break; }
                        fail += strings[j];
                    }
                }
            }
            #endregion fail
            #region close
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i].Contains("<close>"))
                {
                    for (int j = i + 1; j < strings.Count; j++)
                    {
                        if (strings[j].Contains("</close>")) { break; }
                        close += strings[j];
                    }
                }
            }
            #endregion close
            strings.Clear();
        }
        // Reading lines from path
        public void readlines(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(file, Encoding.Default);
            while (reader.Peek() >= 0)
                strings.Add(reader.ReadLine());
        }

        //Read offers from control
        public void readoffers(TextBox ctrl)
        {

            int startpos = 0;
            int endpos = 0;
            for (int i = 0; i < ctrl.TextLength; i++)
            {
                if (ctrl.Text[i] == '.' 
                    || ctrl.Text[i] == '!' 
                    || ctrl.Text[i] == '?')

                    if (char.IsWhiteSpace(ctrl.Text[i + 1]) 
                        || ctrl.Text[i + 1] == '\n')
                    {
                        endpos = i + 1;
                        offer.Add(ctrl.Text.Substring(startpos, endpos - startpos));
                        startpos = endpos;
                    }
            }
        }
        //reading phrases from current offer
        public void readphrases()
        {
            phrase.Clear();
            CurrentPhrase = 0;
            int startpos = 0;
            int endpos = 0;
            for (int i = 0; i < offer[CurrentOffer].Length; i++)
            {
                
                if (offer[CurrentOffer][i] == ','
                    || offer[CurrentOffer][i] == '.'
                    || offer[CurrentOffer][i] == '!'
                    || offer[CurrentOffer][i] == ';'
                    || offer[CurrentOffer][i] == '?'
                    || offer[CurrentOffer][i] == '-'
                    || offer[CurrentOffer][i] == ':'
                    || offer[CurrentOffer][i] == '=') 
                    {
                        endpos = i + 1;
                        phrase.Add(offer[CurrentOffer].Substring(startpos, endpos - startpos));
                        startpos = endpos;
                    }
            }
        }
    }
}
