using System;
using System.Management;
using System.Runtime.InteropServices;

namespace sickhouse.q3fixit.Utils
{
    public static class CpuUtil
    {
               [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryPerformanceFrequency(out long lpFrequency);

        public static string GetInstalledMemory()
        {
            ulong installedMemory = 0;
            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
            if (GlobalMemoryStatusEx(memStatus))
            {
                installedMemory = memStatus.ullTotalPhys;
                installedMemory = installedMemory/1024;
                installedMemory = installedMemory/1000000;
            }
            return installedMemory.ToString() + " GB";
        }

        public static string GetCPUInfo()
        {
            return string.Format("{0} Mhz {1} {2} RAM", GetCpuClockSpeed(), GetCpuManufacturer(), GetInstalledMemory());

        }

        //Get the Caption of the Cpu (some rando-memum of the Cpu)
        public static string GetCpuCaption()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["Caption"].ToString();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        //Get the current operating voltage of the Cpu
        public static int GetCpuVoltage()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return Convert.ToInt32(queryObj["CurrentVoltage"]);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }

        //Dual core? Quad-core? "Many-core"?  http://en.wikipedia.org/wiki/Many-core_processing_unit
        public static int GetCpuCores()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return Convert.ToInt32(queryObj["NumberOfCores"]);
                }
            }
            catch (ManagementException e)
            {
                return -1;
            }
            return -1;
        }

        //Cpu serial
        public static string GetCpuId()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["ProcessorId"].ToString();
                }
            }
            catch (ManagementException e)
            {
                return null;
            }
            return null;
        }

        //Cpu socket designation, more info here: http://en.wikipedia.org/wiki/CPU_socket
        public static string GetCpuSocketDesignation()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["SocketDesignation"].ToString();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        // Intel vs AMD :)
        public static string GetCpuManufacturer()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["Manufacturer"].ToString();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        //think hyper-threading
        public static int GetCpuNumberOfLogicalProcessors()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return Convert.ToInt32(queryObj["NumberOfLogicalProcessors"]);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }

        public static int GetCpuClockSpeed()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return Convert.ToInt32(queryObj["CurrentClockSpeed"]);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }

        //Reads whether you are running a 32 or 64 Bit system
        public static int GetCpuDataWidth()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return Convert.ToInt32(queryObj["DataWidth"]);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }
    }
}
