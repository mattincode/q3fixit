using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sickhouse.q3fixit.Utils
{
    public class Q3ConfigHandler
    {
        private readonly string _q3FolderPath;
        private readonly string _q3ConfigPath;

        public Q3ConfigHandler(string q3FolderPath)
        {
            _q3FolderPath = q3FolderPath;
            _q3ConfigPath = _q3FolderPath + @"//missionpack/q3config.cfg";
        }

        public void SetGraphicsCard(string displayAdapter)
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta r_lastValidRenderer", String.Format("{0}/PCIe/SSE2", displayAdapter));

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }

        public void SetMemoryAlloc()
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta com_zoneMegs", "24");
                SetValue(config, "seta com_hunkMegs", "512");

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }

        public void SetCustomResolution(Resolution screenResolution)
        {            
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta r_customheight", screenResolution.Vertical.ToString());
                SetValue(config, "seta r_customwidth", screenResolution.Horizontal.ToString());
               // SetValue(config, "seta r_customaspect", "1");
                SetValue(config, "seta r_mode", "-1");
               // SetValue(config, "seta r_fullscreen", "1");                

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }

        public void SetBrightness()
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta r_ignorehwgamma", "1");
                SetValue(config, "seta r_overBrightBits", "1");
                SetValue(config, "seta r_gamma", "2.000000");
     
                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }

        public void SetCustomDisplayRefresh(int refresh)
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta r_displayrefresh", refresh.ToString());

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }
        //http://www.mikemartin.co/gaming_guides/quake3_smoothness_guide
        public void SetMaxFps(int maxFps)
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta com_maxfps", maxFps.ToString());
                SetValue(config, "seta cl_maxpackets", maxFps.ToString());

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }

        // http://www.the-yarn.net/config/config.php
        public void SetNetworkToLan()
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta rate", "10000");

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }

        public void SetPlayerModel(string name, string modelname)
        {
            try
            {
                var config = File.ReadAllLines(_q3ConfigPath).ToList();
                // Modify
                SetValue(config, "seta name", name);
                SetValue(config, "seta team_headmodel", modelname);
                SetValue(config, "seta team_model", modelname);
                SetValue(config, "seta headmodel", modelname);
                SetValue(config, "seta model", modelname);

                // Save
                File.WriteAllLines(_q3ConfigPath, config.ToArray());
            }
            catch (IOException)
            {
                // File already opened, notify the user
                //DialogService.ShowDialog(UIResources.Resources.ImportAirportRequirement_Dialog_FileConflict, UIResources.Resources.ImportAirportRequirement_Dialog_FileOpenedMessage, false, b => { });
            }
        }


        private void SetValue(List<string> config, string key, string value)
        {
            var lineIndex = config.FindIndex(x => x.StartsWith(key));
            if (lineIndex != -1)
                config[lineIndex] = String.Format(@"{0} ""{1}""", key, value);  
            else
                config.Add(String.Format(@"{0} ""{1}""", key, value));
        }
    }

    public class Resolution
    {
        public double Vertical { get; set; }
        public double Horizontal { get; set; }
    }

}
