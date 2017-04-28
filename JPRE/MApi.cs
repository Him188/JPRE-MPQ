using System.Runtime.InteropServices;

namespace JPRE.xx
{
    public  class MApi
    {
        public MApi()
        {
        }
        
        //Api_GetNick  //获取对象昵称
        //参数1 对象QQ, 文本型
        //返回对象昵称,文本型
        /// <summary>
        /// 获取对象昵称
        /// </summary>
        /// <param name="qq">参数1 对象QQ, 文本型</param>
        /// <returns>返回对象昵称,文本型</returns>
        [DllImport("Message.dll")]
        public static extern string Api_GetNick(string qq);

        //Api_Reply //发送信息
        //参数1 响应的QQ, 文本型 
        //参数2 类型, 文本型，1 好友 2群 3讨论组 4临时会话
        //参数3 对象, 文本型, QQ或群号
        //参数4 内容, 文本型, 具体发送的信息内容

//        Api_SendMsg 返回类型:整数型 说明:向对象、目标发送信息_注:好友图片发送暂不支持(2015年5月23日
//.参数 响应QQ, 文本型
//.参数 信息类型, 整数型, , 1好友 2群 3讨论组 4群临时会话 5讨论组临时会话
//.参数 参考子类型, 整数型, , 无特殊说明情况下留空或填零
//.参数 收信群_讨论组, 文本型, , 发送群信息、讨论组信息、群临时会话信息、讨论组临时会话信息时填写
//.参数 收信对象, 文本型, , 最终接收这条信息的对象QQ
//.参数 内容, 文本型, , 信息内容
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="qq">参数1 响应的QQ, 文本型 </param>
        /// <param name="msgType">参数2 类型, 文本型，1 好友 2群 3讨论组 4临时会话</param>
        /// <param name="msgTo">参数3 对象, 文本型, QQ或群号</param>
        /// <param name="msgContent">参数4 内容, 文本型, 具体发送的信息内容</param>
  //      [DllImport("Message.dll")]
   //     public static extern int Api_Reply(string qq, int msgType, string msgTo, string msgContent);
        public static int Api_Reply(string qq, int msgType, string msgTo, string msgContent)
        {
            if (msgType == 1)
            {
                return Api_SendMsg(qq, msgType, 0, "", msgTo, msgContent);
            }
            else
            {
                return Api_SendMsg(qq, msgType, 0, msgTo, "", msgContent);
            }

        }

        //Api_Send //向服务器发送封包
        //参数1 响应的QQ, 文本型
        //参数2 封包本体, 文本型
        /// <summary>
        /// 向服务器发送封包
        /// </summary>
        /// <param name="qq">参数1 响应的QQ, 文本型</param>
        /// <param name="pack">参数2 封包本体, 文本型</param>
        [DllImport("Message.dll")]
        //public static extern void Api_Send(string qq,string pack);
        public static extern int Api_Send( string pack);

        //Api_OutPut  //向主程序输出框输出一条信息
        //参数1 信息内容, 文本型
        /// <summary>
        /// 向主程序输出框输出一条信息
        /// </summary>
        /// <param name="logStr">参数1 信息内容, 文本型</param>
        [DllImport("Message.dll")]
        public static extern int Api_OutPut(string logStr);

         //Api_Tea加密  //根据秘钥通过QQTEA加密一段数据
         //参数1 加密内容, 文本型
         //参数2 Key, 文本型
         //返回加密内容,文本型
        /// <summary>
        /// Api_Tea加密  //根据秘钥通过QQTEA加密一段数据
        /// </summary>
        /// <param name="unencryptedContent">参数1 加密内容, 文本型</param>
        /// <param name="key">参数2 Key, 文本型</param>
        /// <returns>返回加密内容,文本型</returns>
        [DllImport("Message.dll")]
         public static extern string Api_Tea加密(string unencryptedContent, string key);

         //Api_Tea解密//根据秘钥通过QQTEA解密一段数据
         //参数1 解密内容, 文本型
         //参数2 Key, 文本型
         //返回解密内容,文本型
         /// <summary>
        /// Api_Tea解密//根据秘钥通过QQTEA解密一段数据
         /// </summary>
        /// <param name="encryptedContent">参数1 解密内容, 文本型</param>
        /// <param name="key">参数2 Key, 文本型</param>
        /// <returns>返回解密内容,文本型</returns>
         [DllImport("Message.dll")]
         public static extern string Api_Tea解密(string encryptedContent, string key);


//Api_UploadPic 返回类型:文本型 说明:返回值:成功返回图片GUID用于发送该图片.失败返回空.  图片尺寸应小于4MB
//.参数 响应的QQ, 文本型, , 机器人QQ
//.参数 参_上传类型, 整数型, , 1好友2群 注:好友图和群图的GUID并不相同并不通用 需要非别上传。群、讨论组用类型2 临时会话、好友信息需要类型1
//.参数 参_参考对象, 文本型, , 上传该图片所属的群号或QQ
//.参数 参_图片数据, 字节集, 参考, 图片字节集数据或字节集数据指针() 
        /// <summary>
         /// 根据文件路径或url上传一张群图片以用于发送
        /// </summary>
         /// <param name="qq">参数1 响应的QQ, 文本型</param>
         /// <param name="picPathOrURL">参数2 路径, 文本型，图片路径或URL 可任一选填路径或图片数据</param>
         /// <param name="picBytes">参数3 图片数据, 字节集, 图片字节集 , 调用请提交字节集，可任一选填路径或图片数据</param>
         /// <returns>返回 图片guid,文本型</returns>
        [DllImport("Message.dll")]
         public static extern string Api_UploadPic(string qq,int uploadtype, string uploaderQQ, byte[] picBytes);

//Api_GuidGetPicLink 返回类型:文本型 说明:根据图片GUID取得图片下载连接 失败返回空
//.参数 图片GUID, 文本型, , {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}.jpg这样的GUID
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GuidGetPicLink(string picGUID);


         //Api_Ban//拉入QQ黑名单
         //参数1 响应QQ, 文本型
         //参数2 被拉黑对象, 文本型
         //无返回值
         [DllImport("Message.dll")]
         public static extern void Api_Ban(string qq, string qqForBan);


         //Api_DBan//移除QQ黑名单
         //参数1 响应QQ, 文本型
         //参数2 被拉黑对象, 文本型
         //无返回值
         [DllImport("Message.dll")]
         public static extern void Api_DBan(string qq, string qqForDBan);


//Api_JoinGroup 返回类型:无 说明:主动加群.为了避免广告、群发行为。出现验证码时将会直接失败不处理
//.参数 响应的QQ, 文本型
//.参数 群号, 文本型
//.参数 附加理由, 文本型
         [DllImport("Message.dll")]
         public static extern void Api_JoinGroup(string qq, string groupid, string joinReason);


        //Api_QuitGroup//退群
        //参数1 响应的QQ, 文本型
        //参数2 群号, 文本型
        //无返回值
         [DllImport("Message.dll")]
         public static extern void Api_QuitGroup(string qq, string groupid);


         //Api_Kick//退群
         //参数1 响应的QQ, 文本型
         //参数2 群号, 文本型

         //返回 是否成功,逻辑型
         [DllImport("Message.dll")]
         public static extern bool Api_Kick(string qq, string groupid,string qqid);


         //Api_DelFriend//删好友
         //参数1 响应的QQ, 文本型
         //参数2 QQ, 文本型

         //无返回值
         [DllImport("Message.dll")]
         public static extern bool Api_DelFriend(string qq, string friendQQ);

         //Api_QuitDid//推出讨论组
         //参数1 响应的QQ, 文本型
         //参数2 讨论组ID, 文本型

         //无返回值
         [DllImport("Message.dll")]
         public static extern void Api_QuitDid(string qq, string did);


        //Api_SetNameCard//改群名片
        //参数1 响应QQ, 文本型
        //参数2 群号, 文本型
        //参数3 QQ, 文本型
        //参数4 名片, 文本型
        //返回结果,文本型
         [DllImport("Message.dll")]
         public static extern void Api_SetNameCard(string qq, string groupid,string qqForSetNameCard,string strNameCard);


//        Api_QuitDG 返回类型:无 说明:退出讨论组
//.参数 响应的QQ, 文本型
//.参数 讨论组ID, 文本型
         [DllImport("Message.dll", CharSet = CharSet.Ansi)]
         public static extern string Api_QuitDG(string qq, string dgroupid);

         //Api_GetNameCard//取群名片
         //参数1 响应QQ, 文本型
         //参数2 群号, 文本型
         //参数3 QQ, 文本型
         //返回结果,文本型
         [DllImport("Message.dll")]
         public static extern string Api_GetNameCard(string qq, string groupid, string qqForSetNameCard);

         //Api_GetNotice 返回类型:文本型 //取群公告
         //.参数 响应QQ, 文本型
         //.参数 群号, 文本型

         [DllImport("Message.dll", CharSet = CharSet.Ansi)]
         public static extern string Api_GetNotice(string qq, string gid);
       
          
         //Api_SetNotice//发群公告
         //参数1 响应QQ, 文本型
         //参数2 群号, 文本型
         //参数3 标题, 文本型
         //参数4 公告, 文本型
         //返回结果,文本型
         [DllImport("Message.dll")]
         public static extern void Api_SetNotice(string qq, string groupid,string title, string noticeContent);

//        Api_IsShutup 返回类型:逻辑型 说明:根据群号+QQ判断指定群员是否被禁言  获取失败的情况下亦会返回假
//.参数 响应的QQ, 文本型
//.参数 群号, 文本型, , 欲判断对象所在群.
//.参数 QQ, 文本型, , 欲判断对象

         [DllImport("Message.dll", CharSet = CharSet.Ansi)]
         public static extern bool Api_IsShutup(string qq, string groupid, string qqForShutup);

    

         //Api_Shutup//禁言
         //参数1 响应QQ, 文本型
         //参数2 群号, 文本型
         //参数3 QQ, 文本型
         //参数4 秒数, 整数型
         //返回类型 逻辑型
         [DllImport("Message.dll")]
         public static extern bool Api_Shutup(string qq, string groupid,string qqForShutup,int second);

         //Api_GetQQLevel //取QQ等级\会员等级信息
         //参数1 QQ, 文本型
         //返回类型:文本型 
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
         public static extern string Api_GetQQLevel(string qq);

        //Api_Cache_NameCard  // 将群名片加入高速缓存中
        //.参数 群号, 文本型
        //.参数 QQ, 文本型
        //.参数 名片, 文本型
        //无返回值
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern void Api_Cache_NameCard(string groupid,string qq,string nameCard);

        //API_GetCookies // 取指定QQ的页面登录cookies
        //.参数 被取COOKIESのQQ, 文本型
        //返回类型:文本型 
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetCookies(string qq);

        //Api_GetQQlist // 取框架中的QQ列表 返回以换行符分隔开的文本型列表
        //返回类型:文本型 
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetQQlist();

        //Api_GetOnlineQQlist // 取框架中在线的QQ列表 返回以换行符分隔开的文本型列表
        //返回类型:文本型 
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetOnlineQQlist();

        //文本型Api_GetRunPath 
        //取mypcqq目录
        //返回类型:文本型 
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetRunPath();

        //取得会话秘钥 
        //.参数 QQ, 文本型
        // 返回类型:文本型
       [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetSessionkey(string qq);

        //取得客户秘钥
        //.参数 QQ, 文本型
        // 返回类型:文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetClientkey(string qq);

        //取得112字节客户秘钥
        //.参数 QQ, 文本型
       // 返回类型:文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetLongClientkey(string qq);

        //取得信息前缀
        // 返回类型:文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetPrefix();

        //计算安全中心页面操作用Ldw参数
        //.参数 QQ, 文本型
        // 返回类型:文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetLdw(string qq);

        //计算页面操作用32字节Bkn参数
        //.参数 QQ, 文本型
        // 返回类型:文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetBkn32(string qq);

       //计算常规页面登录Gtk或Bkn参数
        //.参数 QQ, 文本型
        // 返回类型:文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetGtk_Bkn(string qq);


        //通过群号转换得到群ID
        //参数 群号 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GNGetGid(string gnum);

        //根据群ID转换得到群号
        //参数 群号 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GidGetGN(string gid);


        //Api_IsEnable返回类型:逻辑型
        //取得本插件的启用状态
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern bool Api_IsEnable();

        //Api_Login 返回类型:逻辑型
        //重登陆指定QQ 
        //.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern bool Api_Login(string qq);

        //Api_Logout 无返回值//请求登出某Q
        //.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern void Api_Logout(string qq);

        //Api_GetVersion 返回类型:整数型
        //取框架当前版本号(发布时间戳)
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetVersion();

        //Api_GetVersionName 返回类型:文本型
        //取当前框架版本名
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetVersionName();

        //Api_GetTimeStamp 返回类型:整数型
        //取得框架内部时间戳(该时间戳于腾讯服务器时间同步)
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetTimeStamp();


        //Api_GetLog 返回类型:文本型
        //取得输出框当前的记录
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetLog();

        //Api_IfBlock 返回类型:逻辑型
        //帐号是否处于被屏蔽状态
        //.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern bool Api_IfBlock(string qq);



        //Api_GetRadomOnlineQQ 返回类型:文本型
        //随机取得一个框架内在线的QQ
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetRadomOnlineQQ();

        //Api_GetAdminList 返回类型:文本型
        //取群管列表(包括群主)
        //.参数 响应的QQ, 文本型
        //.参数 群号, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetAdminList(string qq,string gid);

//        Api_SendMsg 返回类型:整数型 说明:向对象、目标发送信息 支持好友 群 讨论组 群临时会话 讨论组临时会话
//.参数 响应的QQ, 文本型
//.参数 信息类型, 整数型, , 1好友 2群 3讨论组 4群临时会话 5讨论组临时会话
//.参数 参考子类型, 整数型, , 无特殊说明情况下留空或填零
//.参数 收信群_讨论组, 文本型, , 发送群信息、讨论组信息、群临时会话信息、讨论组临时会话信息时填写
//.参数 收信对象, 文本型, , 最终接收这条信息的对象QQ
//.参数 内容, 文本型, , 信息内容
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_SendMsg(string qq, int msgType, int msgSubType, string reciveGid, string reviveQQ, string msg);
        

        //Api_AddTaotao 返回类型:文本型
        //发说说
        //.参数 QQ, 文本型
        //.参数 内容, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_AddTaotao(string qq, string content);

        //Api_GetSign 返回类型:文本型
        //取个签
        //.参数 响应的QQ, 文本型
        //.参数 对象, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetSign(string qq, string fromQQ);

//Api_SetSign 返回类型:文本型//设置个签
//.参数 QQ, 文本型
//.参数 内容, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_SetSign(string qq, string content);

//Api_GetGroupListA 返回类型:文本型//取群列表_详细的
//.参数 响应的QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetGroupListA(string qq);


//Api_GetGroupListB 返回类型:文本型//取群列表_简约的
//.参数 响应的QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetGroupListB(string qq);

//Api_GetGroupMemberA 返回类型:文本型//取群成员信息_详细的
//.参数 响应的QQ, 文本型
//.参数 群号, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetGroupMemberA(string qq,string gid);

//Api_GetGroupMemberB 返回类型:文本型//取群成员信息_简约的
//.参数 响应的QQ, 文本型
//.参数 群号, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetGroupMemberB(string qq, string gid);

//Api_GetFriendList 返回类型:文本型//取好友列表
//.参数 响应的QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetFriendList(string qq);

//Api_GetQQAge 返回类型:整数型//取Q龄 未公开或失败返回-1
//.参数 响应的QQ, 文本型
//.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetQQAge(string qq, string qqForm);

//Api_GetAge 返回类型:整数型//取年龄 未公开或失败返回-1
//.参数 响应的QQ, 文本型
//.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetAge(string qq, string qqForm);

//Api_GetEmail 返回类型:整数型//取邮箱 未公开或失败返回-1
//.参数 响应的QQ, 文本型
//.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetEmail(string qq, string qqForm);

//Api_GetGender 返回类型:整数型//取性别 2 男 1 女 未公开或失败返回-1
//.参数 响应的QQ, 文本型
//.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetGender(string qq, string qqForm);

//Api_SendTyping 返回类型:整数型//发送“正在输入”状态
//.参数 响应的QQ, 文本型
//.参数 QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_SendTyping(string qq, string qqForm);


//        Api_SendShake 返回类型:整数型//向好友发送抖动
//.参数 响应的QQ, 文本型
//.参数 QQ, 文本型

        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_SendShake(string qq, string qqForm);


                //返回类型:整数型 说明:呵呵呵呵呵呵呵呵呵呵呵呵呵
//.参数 响应的QQ, 文本型
//.参数 QQ_群号, 文本型, , 呵呵
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_CrackIOSQQ (string qq, string qqForm);


        //Api_AddQQ 返回类型:逻辑型 说明:往帐号列表添加一个Q.当该Q已存在时则覆盖密码
//.参数 QQ, 文本型
//.参数 密码, 文本型
//.参数 自动登录, 逻辑型, , 运行框架时是否自动登录该Q.若添加后需要登录该Q则需要通过Api_Login操作

         [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern bool Api_AddQQ (string qq, string password,bool isAutoLogin);

        //Api_SetOLStatus 返回类型:逻辑型 说明:设置在线状态+附加信息
//.参数 响应的QQ, 文本型
//.参数 在线状态, 整数型, , 1~6 分别对应我在线上, Q我吧, 离开, 忙碌, 请勿打扰, 隐身
//.参数 状态附加信息, 文本型, , 最大255字节
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern bool Api_SetOLStatus(string qq, int OLStatus, string OLStatusInfo);


//Api_GetMC 返回类型:文本型 说明:取得机器码
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetMC();

//Api_GroupInvitation 返回类型:文本型 说明:邀请对象加入群 失败返回错误理由
//.参数 响应的QQ, 文本型
//.参数 好友QQ, 文本型, , 多个好友用换行分割
//.参数 群号, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GroupInvitation(string qq,string friendQQ,string gid);

//Api_CreateDG 返回类型:文本型 说明:创建一个讨论组 成功返回讨论组ID 失败返回空 注:每24小时只能创建100个讨论组 悠着点用
//.参数 响应的QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_CreateDG(string qq);

//Api_KickDG 返回类型:文本型 说明:将对象移除讨论组.成功返回空 失败返回理由
//.参数 响应的QQ, 文本型
//.参数 讨论组ID, 文本型
//.参数 成员, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_KickDG(string qq, string dgid, string memberQQ);

//Api_DGInvitation 返回类型:文本型 说明:邀请对象加入讨论组 成功返回空 失败返回理由
//.参数 响应的QQ, 文本型
//.参数 讨论组ID, 文本型
//.参数 成员组, 文本型, , 多个成员用换行符分割
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_DGInvitation(string qq, string dgid, string memberQQ);

//Api_GetDGList 返回类型:文本型 说明:成功返回以换行符分割的讨论组号列表.最大为100个讨论组  失败返回空
//.参数 响应的QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GetDGList(string qq);



    
 

//Api_SendMusic 返回类型:逻辑型 说明:向对象发送一条音乐信息（所谓的点歌）次数不限
//.参数 响应的QQ, 文本型
//.参数 收信对象类型, 整数型, , 同Api_SendMsg  1好友 2群 3讨论组 4群临时会话 5讨论组临时会话
//.参数 收信对象所属群_讨论组, 文本型, , 发群内、临时会话必填 好友可不填
//.参数 收信对象QQ, 文本型, , 临时会话、好友必填 发至群内可不填
//.参数 音乐简介, 文本型, , 留空默认‘QQ音乐 的分享’

//.参数 音乐播放页面连接, 文本型, , 任意直连或短链接均可 留空为空 无法点开
//.参数 音乐封面连接, 文本型, , 任意直连或短链接均可 可空 例:http://url.cn/cDiJT4
//.参数 音乐文件直连连接, 文本型, , 任意直连或短链接均可 不可空 例:http://url.cn/djwXjr

//.参数 曲名, 文本型, , 可空
//.参数 歌手名, 文本型, , 可空
//.参数 音乐来源名, 文本型, , 可空 为空默认QQ音乐
//.参数 音乐来源图标连接, 文本型, , 可空 为空默认QQ音乐 http://qzonestyle.gtimg.cn/ac/qzone/applogo/64/308/100497308_64.gif

           [DllImport("Message.dll", CharSet = CharSet.Ansi)]
          public static extern bool Api_SendMusic(string qq, int msgType, string reciveGid, string reviveQQ,
               string musicMsg,string musicPageLink,string musicCover,string musicFileLink,
               string musicName,string musicSingerName,string musicSource,string musicSourceIcon
               );

//Api_SendObjectMsg 返回类型:逻辑型 说明:
//.参数 响应的QQ, 文本型
//.参数 收信对象类型, 整数型, , 同Api_SendMsg  1好友 2群 3讨论组 4群临时会话 5讨论组临时会话
//.参数 收信对象所属群_讨论组, 文本型, , 发群内、临时会话必填 好友可不填
//.参数 收信对象QQ, 文本型, , 临时会话、好友必填 发至群内可不填
//.参数 ObjectMsg, 文本型
//.参数 结构子类型, 整数型, , 00 基本 02 点歌 其他不明
         [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_SendObjectMsg(string qq, int msgType, string reciveGid, string reviveQQ,
            string objectMsg, string objectMsgSubType);


         //Api_IsFriend 返回类型:逻辑型 说明:判断对象是否为好友（双向）
         //.参数 响应的QQ, 文本型
         //.参数 对象QQ, 文本型
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]

        public static extern bool Api_IsFriend(string qq, string friendQQ);


//        Api_AddFriend 返回类型:逻辑型 说明:主动加好友 成功返回真 失败返回假 当对象设置需要正确回答问题或不允许任何人添加时无条件失败
//.参数 响应的QQ, 文本型, , 机器人QQ
//.参数 对象QQ, 文本型, , 加谁
//.参数 附加理由, 文本型, , 加好友提交的理由

        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern bool Api_AddFriend(string qq, string friendQQ,string message);


//Api_SelfDisable 返回类型:无 说明:无参 用于插件自身请求禁用插件自身
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern void Api_SelfDisable();

//Api_GetClientType 返回类型:整数型 说明:取协议客户端类型常量 失败返回0
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetClientType();

//Api_GetClientVer 返回类型:整数型 说明:取协议客户端版本号常量  失败返回0
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetClientVer();

//Api_GetPubNo 返回类型:整数型 说明:取协议客户端公开版本号常量  失败返回0
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetPubNo();

//Api_GetMainVer 返回类型:整数型 说明:取协议客户端主版本号常量  失败返回0
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetMainVer();

//Api_GetTXSSOVer 返回类型:整数型 说明:取协议客户端通信模块(TXSSO)版本号常量  失败返回0
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern int Api_GetTXSSOVer();

//Api_UploadVoice 返回类型:文本型 说明:上传音频文件 成功返回guid用于发送
//.参数 响应的QQ, 文本型
//.参数 amr音频数据, 整数型, , 音频字节集数据
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_UploadVoice(string qq,byte[] amrBytes);

//Api_GuidGetVoiceLink 返回类型:文本型 说明:通过音频、语音guid取得下载连接
//.参数 响应的QQ, 文本型
//.参数 GUID, 文本型, , 格式:{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxx}.amr
        [DllImport("Message.dll", CharSet = CharSet.Ansi)]
        public static extern string Api_GuidGetVoiceLink(string qq,string amrGUID);

//Api_AddLogHandler 返回类型:逻辑型说明: 添加一个日志处理函数。每条新日志信息输出都会投递给该函数处理、重复添加将覆盖旧的、之前的接口
//.参数 参_处理函数指针, 子程序指针, , 回调子程序、函数指针(内存地址)。函数仅一个参数。参数1为 结构体LOGSTRUCT指针
        //[DllImport("Message.dll", CharSet = CharSet.Ansi)]
        //public static extern bool Api_AddLogHandler();
//Api_RemoveLogHandler 返回类型:无返回 说明:移除由Api_AddLogHandler添加、设置的日志处理函数
        //[DllImport("Message.dll", CharSet = CharSet.Ansi)]
        //public static extern void Api_RemoveLogHandler();


    }
}
