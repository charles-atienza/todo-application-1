using System.Runtime.InteropServices;

namespace Exam.Web.Helpers;

/// <summary>
///     https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/host-and-deploy/aspnet-core-module/samples_snapshot/2.x/CurrentDirectoryHelpers.cs
/// </summary>
public class CurrentDirectoryHelpers
{
    internal const string AspNetCoreModuleDll = "aspnetcorev2_inprocess.dll";

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport(AspNetCoreModuleDll)]
    private static extern int http_get_application_properties(ref IISConfigurationData iiConfigData);

    public static void SetCurrentDirectory()
    {
        try
        {
            // Check if physical path was provided by ANCM
            var sitePhysicalPath = Environment.GetEnvironmentVariable("ASPNETCORE_IIS_PHYSICAL_PATH");
            if (string.IsNullOrEmpty(sitePhysicalPath))
            {
                // Skip if not running ANCM InProcess
                if (GetModuleHandle(AspNetCoreModuleDll) == IntPtr.Zero)
                {
                    return;
                }

                var configurationData = default(IISConfigurationData);
                if (http_get_application_properties(ref configurationData) != 0)
                {
                    return;
                }

                sitePhysicalPath = configurationData.pwzFullApplicationPath;
            }

            Environment.CurrentDirectory = sitePhysicalPath;
        }
        catch
        {
            // ignore
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct IISConfigurationData
    {
        public readonly IntPtr pNativeApplication;

        [MarshalAs(UnmanagedType.BStr)]
        public readonly string pwzFullApplicationPath;

        [MarshalAs(UnmanagedType.BStr)]
        public readonly string pwzVirtualApplicationPath;

        public readonly bool fWindowsAuthEnabled;
        public readonly bool fBasicAuthEnabled;
        public readonly bool fAnonymousAuthEnable;
    }
}