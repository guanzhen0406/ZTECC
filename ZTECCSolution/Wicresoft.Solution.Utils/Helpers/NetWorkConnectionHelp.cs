using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;


namespace Wicresoft.Solution.Utils
{
    public class NetWorkConnectionHelp
    {
        #region imports
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static bool DuplicateToken(IntPtr existingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr duplicateTokenHandle);
        #endregion
        #region logon consts
        // logon types 
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_LOGON_NETWORK = 3;
        private const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
        // logon providers 
        private const int LOGON32_PROVIDER_DEFAULT = 0;
        private const int LOGON32_PROVIDER_WINNT50 = 3;
        private const int LOGON32_PROVIDER_WINNT40 = 2;
        private const int LOGON32_PROVIDER_WINNT35 = 1;
        #endregion

        public static void ExcuteNetWorkDirectory( string domain, string username, string password,Action s)
        {
            IntPtr token = IntPtr.Zero;

            bool isSuccess = LogonUser(username, domain, password,
            LOGON32_LOGON_NEW_CREDENTIALS,
            LOGON32_PROVIDER_DEFAULT, ref token);
            using (WindowsImpersonationContext person = new WindowsIdentity(token).Impersonate())
            {
                s();
            }
        }
    }
}
