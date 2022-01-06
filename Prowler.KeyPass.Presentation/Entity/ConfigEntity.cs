using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Prowler.KeyPass.Presentation.Entity
{
    [Serializable]
    internal class ConfigEntity
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool Maximized { get; set; }        

        public void Save(Window window)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prowler Key Pass");

            Top = window.Top;
            Left = window.Left;
            Width = window.Width;
            Height = window.Height;           
            Maximized = window.WindowState == WindowState.Maximized;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string jsonString = JsonSerializer.Serialize(this);

            File.WriteAllText(Path.Combine(folder,"prowlerSettings.pst"), jsonString);
        }

        public static ConfigEntity? Load(Window window)
        {
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prowler Key Pass", "prowlerSettings.pst");
            
            if (File.Exists(file))
            {
                var jsonString = File.ReadAllText(file);
                var config = JsonSerializer.Deserialize(jsonString, typeof(ConfigEntity)) as ConfigEntity;

                if(config != null)
                {
                    window.Top = config.Top;
                    window.Left = config.Left;
                    window.Width = config.Width;
                    window.Height = config.Height;

                    if (config.Maximized)
                    {
                        window.WindowState = WindowState.Maximized;
                    }                   
                }
            }

            return null;           
        }
    }
}
