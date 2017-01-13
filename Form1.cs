using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech;
using System.Globalization;
using System.IO;
using SpeechLib;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Key4
{
    public partial class Form1 : Form
    {
        Phraser replics = new Phraser();
        SpeechVoiceSpeakFlags Async = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        SpeechVoiceSpeakFlags Sync = SpeechVoiceSpeakFlags.SVSFDefault;
        SpeechVoiceSpeakFlags Cancel = SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak;
        SpVoice speech = new SpVoice();
        Teacher teacher = new Teacher();
        KeyHook keyhook = new KeyHook();
        Keyboard Saha = new Keyboard();
        Readtxt reader = new Readtxt();
        SpVoice speaker = new SpVoice();
        SpVoice speaker2 = new SpVoice();
        Voiceseeker voicefinder = new Voiceseeker();
        string hello, bye;



        string History = null;
        int n = 0;




        public Form1()
        {

            InitializeComponent();
            this.Width = 260;

            SetVoiceForm SVF = new SetVoiceForm();
            SVF.ShowDialog();

            try
            {
                speech.Rate = 0;
                speech.Volume = 100;
                speech.Voice = SVF.speech.Voice;


                replics.loadtext(Application.StartupPath + "\\KeysText\\Replics.txt");

                hello = replics.list[0];
                bye = replics.list[1];


            }
            catch (Exception) { }


        }

      
        private void Key3_Load(object sender, EventArgs e)
        {
            speech.Speak(hello, Sync);
        }
        



        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Hooking keys and speaking
            keyhook.Keyhook(e.KeyChar);
            if (keyhook.hook == true)
            {
                label1.Text = keyhook.Hkey;
                speaker.Speak("", Cancel);
                speaker2.Speak("", Cancel);
                speaker.Speak(keyhook.BuildSSML());

                History += keyhook.Hkey+Environment.NewLine;
                if (teacher.loaded == true)
                    if (label1.Text == label2.Text)
                    {

                        n = 0;
                        //System.Threading.Thread.Sleep(1 * 1000);
                        speaker2.Speak(reader.success);
                        System.Threading.Thread.Sleep(1 * 1000);
                        teacher.nextletter();
                        label2.Text = teacher.CurrentLetter;
                        speaker.Speak(keyhook.BuildSSML(teacher.CurrentLetter));
                        History += "Teacher " + teacher.CurrentLetter + Environment.NewLine;
                    }
                    else 
                    { 
                        speaker.Speak(reader.fail);
                        System.Threading.Thread.Sleep(1 * 1000);
                        n++;
                        if (n == 3)
                        {
                            teacher.nextletter();
                            speaker.Speak("Следующая буква");
                            label2.Text = teacher.CurrentLetter;
                            History += "Teacher " + teacher.CurrentLetter + Environment.NewLine;
                            n = 0;
                            speaker.Speak(keyhook.BuildSSML(teacher.CurrentLetter));
                           
                        }
                    }
             }
            
            keyhook.hook = false;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Discreasing the rate of speaker
            if (e.KeyCode.Equals(Keys.Left))
            {
                try
                {
                    speaker.Rate--;
                    speaker2.Rate--;
                    History += "<-" + Environment.NewLine;
                }
                catch (ArgumentOutOfRangeException) { }
            }

            //Increasing the rate of speaker
            if (e.KeyCode.Equals(Keys.Right))
            {
                try
                {
                    speaker.Rate++;
                    speaker2.Rate++;
                    History += "->" + Environment.NewLine;
                }
                catch (ArgumentOutOfRangeException) { }
            }

            //Mode changing
            if (e.KeyCode.Equals(Keys.Tab))
            {
                if (teacher.loaded == true)
                {
                    teacher.ChangeMode();
                    //label5.Text = teacher.CurrentMode;
                    speaker2.Speak("", Cancel);
                    speaker2.Speak(teacher.CurrentMode);
                    History += "TAB"+ Environment.NewLine;
                }
            }
            //teacher on\off
            if (e.KeyCode.Equals(Keys.F5))
            {
                if (teacher.loaded == true)
                {
                    teacher.loaded = false;
                    label2.Hide();
                    label4.Hide();
                    //label5.Hide();
                    Form1.ActiveForm.Width = 260;
                    speaker2.Speak("", Cancel);
                    speaker2.Speak(reader.teacheroff);
                    History += "F5"+reader.teacheroff + Environment.NewLine;
                }
                else
                {
                    teacher.loaded = true;
                    label2.Show();
                    label4.Show();
                    //label5.Show();
                    Form1.ActiveForm.Width = 458;
                    label2.Text = teacher.CurrentLetter.ToString();
                    //label5.Text = teacher.CurrentMode;
                    speaker2.Speak("", Cancel);
                    speaker2.Speak(reader.teacheron);
                    speaker.Speak(keyhook.BuildSSML(teacher.CurrentLetter));
                    History += "F5"+reader.teacheron + Environment.NewLine;
                    History += "Teacher "+teacher.CurrentLetter + Environment.NewLine;
                } 
            }

            //Help
            if (e.KeyCode.Equals(Keys.F1))
            {
                speaker2.Speak("", Cancel);
                speaker2.Speak(reader.Help);   
                History += "F1 -- help" + Environment.NewLine;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region Write history
            FileStream Historyfile = new FileStream("KeysHistory\\History.txt", FileMode.OpenOrCreate);
            StreamWriter HystoryWhriter = new StreamWriter(Historyfile);
            HystoryWhriter.Write(History);
            HystoryWhriter.Close();
            Historyfile.Close();
            #endregion Write history
            speaker2.Speak("", Cancel);
            speaker2.Speak(reader.close);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Saha.SetJakutian();
            speaker.Voice = speech.Voice;
            speaker2.Voice = speech.Voice;
            speaker2.Rate = 1;
            speaker2.Speak(reader.Greetings);
        }

    }
}
