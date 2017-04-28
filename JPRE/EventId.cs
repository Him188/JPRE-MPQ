using System;

namespace JPRE.xx
{
    public enum EventId
    {
        MessageGroup=2,
        MessageDiscussion=3,
        MessageGroupTemporary=4,
        MessageDiscussionTemporary=5,

        FriendAddResult=1000,
        FriendAddRequest=1001,
        FriendStatusChange=1002,
        FriendDelete=1003,
        FriendSignChange=1004,
        FriendTaotaoBeComment=1005,
        FriendTyping=1006,
        FriendFirstConvesation=1007,
        FriendShake=1008,

        GroupJoinRequest=2001,
        GroupInvitationRequest=2002,
        GroupInvitation=2003,
        GroupJoin=2005,

        GroupQuit=2006,
        GroupKick=2007,
        GroupDissolution=2008,
        GroupAdminChange=2009, //成为管理/取消管理均为此
        GroupCardChange=2011,
        [Obsolete] GroupNameChange=2012,
        GroupNotificationChange=2013,

        GroupMute=2014,
        GroupUnmute=2015,
        GroupWholeMute=2016,
        GroupWholeUnmute=2017,
        GroupAnonymousEnable=2018,
        GroupAnonymousDisable=2019,

        FrameStartup=10000,
        FrameReboot=10001,

        FrameQqAdd=11000,
        FrameQqLogin=11001,
        FrameQqOffline=11002,
        FrameQqForceOffline=11003,
        FrameQqCrash=11004,

        JpreMenuAction=12003,

        TenpayReceiveTransfer=80001,
    }
}