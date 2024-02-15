using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AxWMPLib;
using System.Reflection;

namespace DIP.SoftPhoneAPI
{
    public class MediaPlayer
    {
        AxWindowsMediaPlayer wmp;

        public MediaPlayer()
        {
            wmp = new AxWindowsMediaPlayer();
            wmp.settings.autoStart = false;
            wmp.settings.playCount = 1000;
            wmp.URL = this.CreateMusicFile();
        }

        private string CreateMusicFile()
        {
            var url = Directory.GetCurrentDirectory() + "\\ringphone.mp3";

            try
            {
                MemoryStream stream = new MemoryStream(Properties.Resources.ring);
                using (Stream output = new FileStream(url, FileMode.Create))
                {
                    byte[] buffer = new byte[32 * 1024];
                    int read;

                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, read);
                    }
                }
            }
            catch { }

            return url;
        }

        public void Play()
        {
            wmp.Ctlcontrols.play();
        }

        public void Stop()
        {
            wmp.Ctlcontrols.stop();
        }
    }
}
