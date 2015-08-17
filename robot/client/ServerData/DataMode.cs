
using System.Collections;
using System.Collections.Generic;
using robot.client;

/// 服务器发送来的数据模型缓存
public class DataMode
{
    /// 我的主角
    public static InfoPlayer myPlayer;

    /// 主角缓存(idServer, value)
    public static Dictionary<uint, InfoPlayer> _serverPlayer = new Dictionary<uint, InfoPlayer>();

	//邮件数据缓存
	public Dictionary<ulong, EmailInfo> _emailInfo = new Dictionary<ulong, EmailInfo>();

	//邮件ID列表
	public List<ulong> emailList = new List<ulong>();

	///邮件列表
	//public InfoEmailList _infoEmailList = new InfoEmailList();

    public EmailInfo getEmailInfo( ulong serverEmailId )
    {
        if( !_emailInfo.ContainsKey( serverEmailId ) )
            return null;
        return _emailInfo[ serverEmailId ];
    }

    /// 碎片信息
    public InfoHeroChip infoHeroChip = new InfoHeroChip();

    /// 获得 主角
    public static InfoPlayer getPlayer( uint idServer )
    {
        if( idServer == 0 )
            return null;
        if( !_serverPlayer.ContainsKey( idServer ) )
            return null;
        return _serverPlayer[ idServer ];
    }
}


