using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows.Forms;

namespace Key4
{
    class Keyboard
    {
        InputLanguageCollection installedLangs = InputLanguage.InstalledInputLanguages;
        public void SetJakutian()
        {
            foreach (InputLanguage lang in installedLangs)
            {
                if (lang.LayoutName == "Сахалыы - Custom" || lang.LayoutName == "Сахалыы" || lang.Culture.Name == "Якутский")
                    InputLanguage.CurrentInputLanguage = lang;
            }



        }
    }
}
