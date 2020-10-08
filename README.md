# BypassAddUser
调用Windows Api，绕过杀软添加用户，怎么去使用，就仁者见仁智者见智了

`.NET4.0+`

已测Win2008,Win2012,Win2016,Win10都没问题



**注意密码复杂性！！！**

**注意密码复杂性！！！**

**注意密码复杂性！！！**

### 2020.10.8更新：新增更改用户密码、删除用户功能

```
Usage: BypassAddUser.exe -u/-U username -p/-P password -g/-G groups    添加用户
       BypassAddUser.exe -c/-C UserName NewPassword    更改用户密码
       BypassAddUser.exe -d/-D UserName    删除用户
Example: BypassAddUser.exe -u test -p testpass -g administrators
         BypassAddUser.exe -c test NewtestPass
         BypassAddUser.exe -d test
```
## 更改指定用户密码

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-08_19-23-29.jpg)

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-08_19-24-14.jpg)

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-08_19-26-27.jpg)

## 测试环境：360+火绒

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-04_11-56-10.jpg)

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-04_11-42-15.jpg)

使用CS来内存执行

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-04_11-47-57.jpg)

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-04_11-49-09.jpg)

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-04_11-50-53.jpg)

![images](https://github.com/TryA9ain/BypassAddUser/blob/master/Pictures/Snipaste_2020-10-04_11-52-55.jpg)

## 参考链接
https://blog.csdn.net/weixin_34307464/article/details/94449261

https://docs.microsoft.com/en-us/windows/win32/api/lmaccess/

## 问题建议

欢迎大佬们提出问题或建议
