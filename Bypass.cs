using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BypassAddUser
{
    public class Bypass
    {
        //创建用户
        [DllImport("Netapi32.dll")]
        extern static int NetUserAdd([MarshalAs(UnmanagedType.LPWStr)] string servername, int level, ref USER_INFO_1 buf, int parm_err);

        //把用户添加到本地组
        [DllImport("Netapi32.dll")]
        extern static int NetLocalGroupAddMembers([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string groupname, int level, ref LOCALGROUP_MEMBERS_INFO_3 buf, int totalentries);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //链接：https://docs.microsoft.com/en-us/windows/win32/api/lmaccess/ns-lmaccess-localgroup_members_info_3
        public struct LOCALGROUP_MEMBERS_INFO_3
        {
            public string domainandname;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //链接：https://docs.microsoft.com/en-us/windows/win32/api/lmaccess/ns-lmaccess-user_info_1
        public struct USER_INFO_1
        {
            public string usri1_name;
            public string usri1_password;
            public int usri1_password_age;
            public int usri1_priv;
            public string usri1_home_dir;
            public string comment;
            public int usri1_flags;
            public string usri1_script_path;
        }

        /// <summary>
        /// 添加一个用户，添加失败后返回非0
        /// </summary>
        /// <param name="serverName"></param>机器名，如果是本机，设置为null 
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        public void AddUser(string serverName, string userName, string passWord)
        {
            USER_INFO_1 NewUser = new USER_INFO_1(); 

            NewUser.usri1_name = userName; 
            NewUser.usri1_password = passWord; 
            NewUser.usri1_priv = 1; // 账户类型

            if (NetUserAdd(serverName, 1, ref NewUser, 0) != 0) //添加失败后返回非0
            {
                Console.WriteLine("Error Adding User");
            }
            else
            {
                Console.WriteLine("Success Adding User!!!");
            }
        }

        /// <summary>
        /// 把用户添加到本地组。添加失败后返回非0
        /// </summary>
        /// <param name="serverName"></param>机器名，如果是本机，设置为null 
        /// <param name="groupName"></param>
        /// <param name="userName"></param>
        public void GroupAddMembers(string serverName, string groupName, string userName)
        {
            LOCALGROUP_MEMBERS_INFO_3 NewMember = new LOCALGROUP_MEMBERS_INFO_3();
            NewMember.domainandname = userName;
            if (NetLocalGroupAddMembers(serverName, groupName, 3, ref NewMember, 1) != 0) //添加失败后返回非0
            {
                Console.WriteLine("Error Adding Group Member");
            }
            else
            {
                Console.WriteLine("Success Adding Group Member!!!");
            }
        }
    }
    

}
