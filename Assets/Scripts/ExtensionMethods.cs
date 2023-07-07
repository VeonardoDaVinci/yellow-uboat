using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string ParseIntToTimeString(this int timeInt)
        {
            int minutes = (timeInt / 60);
            string seconds = (timeInt % 60).ToString();
            if (seconds.Length < 2)
            {
                seconds = "0" + seconds;
            }
            return minutes + ":" + seconds;
        } 
    }
}
