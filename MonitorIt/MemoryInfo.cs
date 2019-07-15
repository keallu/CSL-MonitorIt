using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MonitorIt
{
    public static class MemoryInfo
    {
        const int PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

        public static void Get(out ulong totalUsedPhysical, out ulong totalUsedVirtual, out ulong totalUsedPagedPool, out ulong totalUsedNonPagedPool)
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
                    Get(hObject, out totalUsedPhysical, out totalUsedVirtual, out totalUsedPagedPool, out totalUsedNonPagedPool);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MemoryInfo:Get -> Exception: " + e.Message);
            }
            finally
            {
                CloseHandle(hObject);
            }
        }

        public static void Get(int processId, out ulong totalUsedPhysical, out ulong totalUsedVirtual, out ulong totalUsedPagedPool, out ulong totalUsedNonPagedPool)
        {
            totalUsedPhysical = 0u;
            totalUsedVirtual = 0u;
            totalUsedPagedPool = 0u;
            totalUsedNonPagedPool = 0u;

            IntPtr hObject = IntPtr.Zero;

            try
            {
                hObject = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);

                if (hObject != IntPtr.Zero)
                {
                    Get(hObject, out totalUsedPhysical, out totalUsedVirtual, out totalUsedPagedPool, out totalUsedNonPagedPool);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Monitor It!] MemoryInfo:Get -> Exception: " + e.Message);
            }
            finally
            {
                CloseHandle(hObject);
            }
        }

        private static void Get(IntPtr hObject, out ulong totalUsedPhysical, out ulong totalUsedVirtual, out ulong totalUsedPagedPool, out ulong totalUsedNonPagedPool)
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
                Debug.Log("[Monitor It!] MemoryInfo:Get -> Exception: " + e.Message);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

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
