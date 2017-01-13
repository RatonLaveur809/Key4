using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace Key4
{
    class KeyHook
    {
        public string Hkey="";
        public bool hook = false;
        public void Keyhook(char key)
        {
            byte b = Encoding.GetEncoding(1251).GetBytes(new char[] { key })[0];
            if (b >= 224 && b <= 255 || b == 63 || (b >= 48 && b <= 57) || b == 184 || (b >= 192 && b <= 223)||b==61||b==45||b==46||b==33)
            {   
                    Hkey = key.ToString();
                    hook = true;
                    return;               
            }
            
        }
        public String BuildSSML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<speak xml:lang=''ru-RU'' version=''1.0''>");
            sb.Append("<voice name=''ELAN TTS Russian (Nicolai 16Khz)''><say-as interpret-as=''letters''>" + Hkey + "</say-as></voice></speak>");
            return sb.ToString().Replace("''", '"'.ToString());
        }
        public String BuildSSML(string key)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<speak xml:lang=''ru-RU'' version=''1.0''>");
            sb.Append("<voice name=''ELAN TTS Russian (Nicolai 16Khz)''><say-as interpret-as=''letters''>" + key + "</say-as></voice></speak>");
            return sb.ToString().Replace("''", '"'.ToString());
        }
    }
}
