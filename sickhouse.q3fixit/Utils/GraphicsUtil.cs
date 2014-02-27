using System;
using System.Management;
using System.Runtime.InteropServices;

namespace sickhouse.q3fixit.Utils
{
    public class GraphicsUtil
    {
        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(
              string deviceName, int modeNum, ref DEVMODE devMode);
        const int ENUM_CURRENT_SETTINGS = -1;
        const int ENUM_REGISTRY_SETTINGS = -2;

        public enum ScreenOrientation
        {
            DMORIENT_PORTRAIT = 1,
            DMORIENT_LANDSCAPE = 2 
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {

            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;

        }


        public static string GetAdapterName()
        {
            string adapterName = "Not found";
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");

            string graphicsCard = string.Empty;
            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "Description")
                    {
                        adapterName = property.Value.ToString();
                    }
                }
            }
            return adapterName;

        }

        public static string GetSupportedMonitorResolutions()
        {
            string resolutions = "";
            DEVMODE vDevMode = new DEVMODE();
            int i = 0;
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                resolutions += String.Format("Width:{0} Height:{1} Color:{2} Frequency:{3}{4}",
                                        vDevMode.dmPelsWidth,
                                        vDevMode.dmPelsHeight,
                                        1 << vDevMode.dmBitsPerPel,
                vDevMode.dmDisplayFrequency, Environment.NewLine);
                //Console.WriteLine("Width:{0} Height:{1} Color:{2} Frequency:{3}",
                //                        vDevMode.dmPelsWidth,
                //                        vDevMode.dmPelsHeight,
                //                        1 << vDevMode.dmBitsPerPel, vDevMode.dmDisplayFrequency
                //                    );
                i++;
            }
            return resolutions;
        }

        public static string GetPrimaryScreenResolution()
        {
            return System.Windows.SystemParameters.PrimaryScreenWidth.ToString() + "x" +
                   System.Windows.SystemParameters.PrimaryScreenHeight.ToString();
        }

        public static Resolution GetPrimaryScreenResolutionObj()
        {
            return new Resolution() {Horizontal = System.Windows.SystemParameters.PrimaryScreenWidth, Vertical = System.Windows.SystemParameters.PrimaryScreenHeight};
        }

        public static string GetPrimaryAdapterDriver()
        {
            string RAM = "";
            string driverDate = "";
            string adapterName = "Not found";
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    Console.WriteLine(property.Name + " value: " + (property.Value == null ? "" : property.Value.ToString()) + Environment.NewLine);
                    if (property.Name == "DriverVersion")
                    {
                        adapterName = property.Value.ToString();
                    }
                    else if (property.Name == "AdapterRAMValue")
                    {
                        var val = (int)property.Value;
                        RAM = ((val/1024)/1000000).ToString() + "GB RAM";
                    }
                    else if (property.Name == "DriverDate")
                    {
                        driverDate = property.Value.ToString();
                    }
                }
            }
            return adapterName + " " + RAM + " Driver date:" + driverDate;

        }
    }
}
