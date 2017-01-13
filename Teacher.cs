using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Key4
{
    class Teacher
    {
        public bool loaded = false;
        public int lenght = 5, n=0;
        public string history=null;
        public string CurrentLetter;
        public string CurrentMode;
        public string letters = "абвгдеёжзиклмнопрстуфкцчшщъыьэюя-.=0123456789";
        public string numbers = "0123456789";
        public string other = "-.=";
        public string current;

        
        public Teacher()
        {
            Random random = new Random();
            current = letters;
            CurrentMode = "Только буквы";
            CurrentLetter = current[random.Next(0, current.Length - 1)].ToString();
            
        }
        public void ChangeMode()
        {

            
        }
        public void nextletter()
        {
            
                 Random random = new Random();
                 CurrentLetter = current[random.Next(0, current.Length - 1)].ToString();
             
           
        }

    }

}
