using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MonitorIt
{
    public static class SysInfo
    {
        public static void GetProcessorNumber(out uint processorNumber)
        {
            processorNumber = 1;

            try
            {
                processorNumber = GetCurrentProcessorNumber();
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] SysInfo:GetProcessorNumber -> Exception: " + e.Message);
            }
        }

        public static void GetProcessorTimes(out long kernelMicroseconds, out long userMicroseconds)
        {
            kernelMicroseconds = 0u;
            userMicroseconds = 0u;

            IntPtr hObject = IntPtr.Zero;

            try
            {
                hObject = GetCurrentProcess();

                if (hObject != IntPtr.Zero)
                {
                    GetProcessorTimes(hObject, out kernelMicroseconds, out userMicroseconds);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] SysInfo:GetProcessorTimes -> Exception: " + e.Message);
            }
            finally
            {
                CloseHandle(hObject);
            }
        }

        private static void GetProcessorTimes(IntPtr hObject, out long kernelMicroseconds, out long userMicroseconds)
        {
            System.Runtime.InteropServices.ComTypes.FILETIME creationTime;
            System.Runtime.InteropServices.ComTypes.FILETIME exitTime;
            System.Runtime.InteropServices.ComTypes.FILETIME kernelTime;
            System.Runtime.InteropServices.ComTypes.FILETIME userTime;
            kernelMicroseconds = 0u;
            userMicroseconds = 0u;

            try
            {
                GetProcessTimes(hObject, out creationTime, out exitTime, out kernelTime, out userTime);

                kernelMicroseconds = FileTimeToLong(kernelTime);
                userMicroseconds = FileTimeToLong(userTime);
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] SysInfo:GetProcessorTimes -> Exception: " + e.Message);
            }
        }

        public static void GetMemory(out ulong totalUsedPhysical, out ulong totalUsedVirtual, out ulong totalUsedPagedPool, out ulong totalUsedNonPagedPool)
        {
            totalUsedPhysical = 0u;
            totalUsedVirtual = 0u;
            totalUsedPagedPool = 0u;
            totalUsedNonPagedPool = 0u;

            IntPtr hObject = IntPtr.Zero;

            try
            {
                hObject = GetCurrentProcess();

                if (hObject != IntPtr.Zero)
                {
                    GetMemory(hObject, out totalUsedPhysical, out totalUsedVirtual, out totalUsedPagedPool, out totalUsedNonPagedPool);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] SysInfo:GetMemory -> Exception: " + e.Message);
            }
            finally
            {
                CloseHandle(hObject);
            }
        }

        private static void GetMemory(IntPtr hObject, out ulong totalUsedPhysical, out ulong totalUsedVirtual, out ulong totalUsedPagedPool, out ulong totalUsedNonPagedPool)
        {
            totalUsedPhysical = 0u;
            totalUsedVirtual = 0u;
            totalUsedPagedPool = 0u;
            totalUsedNonPagedPool = 0u;

            try
            {
                PROCESS_MEMORY_COUNTERS pmc;
                pmc.cb = (uint)Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS));

                if (GetProcessMemoryInfo(hObject, out pmc, pmc.cb))
                {
                    totalUsedPhysical = pmc.WorkingSetSize;
                    totalUsedVirtual = pmc.PagefileUsage;
                    totalUsedPagedPool = pmc.QuotaPagedPoolUsage;
                    totalUsedNonPagedPool = pmc.QuotaNonPagedPoolUsage;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] SysInfo:GetMemory -> Exception: " + e.Message);
            }
        }

        private static long FileTimeToLong(System.Runtime.InteropServices.ComTypes.FILETIME fileTime)
        {
            return (long)unchecked((((ulong)(uint)fileTime.dwHighDateTime) << 32) | (uint)fileTime.dwLowDateTime);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetCurrentProcessorNumber();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetProcessTimes(IntPtr hProcess, out System.Runtime.InteropServices.ComTypes.FILETIME lpCreationTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpExitTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpKernelTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpUserTime);

        [DllImport("psapi.dll", SetLastError = true)]
        private static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS counters, uint size);

        [StructLayout(LayoutKind.Sequential, Size = 72)]
        private struct PROCESS_MEMORY_COUNTERS
        {
            public uint cb;
            public uint PageFaultCount;
            public ulong PeakWorkingSetSize;
            public ulong WorkingSetSize;
            public ulong QuotaPeakPagedPoolUsage;
            public ulong QuotaPagedPoolUsage;
            public ulong QuotaPeakNonPagedPoolUsage;
            public ulong QuotaNonPagedPoolUsage;
            public ulong PagefileUsage;
            public ulong PeakPagefileUsage;
        }
    }
}
