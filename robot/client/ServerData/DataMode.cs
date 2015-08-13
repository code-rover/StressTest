
using System.Collections;
using System.Collections.Generic;
using robot.client;

/// 服务器发送来的数据模型缓存
public class DataMode
{
	//邮件数据缓存
	public Dictionary<ulong, EmailInfo> _emailInfo = new Dictionary<ulong, EmailInfo>();

	//邮件ID列表
	public List<ulong> emailList = new List<ulong>();

	///邮件列表
	public InfoEmailList _infoEmailList = new InfoEmailList();

    public EmailInfo getEmailInfo( ulong serverEmailId )
    {
        if( !_emailInfo.ContainsKey( serverEmailId ) )
            return null;
        return _emailInfo[ serverEmailId ];
    }
}


