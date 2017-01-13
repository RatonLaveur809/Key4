using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using Microsoft.Win32;

namespace Key4
{
    class Voiceseeker
    {
        public List<string> VoiceName = new List<string>();
        public Voiceseeker()
        {
            RegistryKey readKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Speech\Voices\Tokens\");
            int i = 0;
            foreach (string name in readKey.GetSubKeyNames())
            {
                readKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Speech\Voices\Tokens\" + name + "\\Attributes");
                VoiceName.Add(readKey.GetValue("Name").ToString());
                i++;
            }
        }
    }
}
