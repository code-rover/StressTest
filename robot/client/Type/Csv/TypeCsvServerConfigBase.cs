
using System.Collections;
using System.Collections.Generic;

public class TypeCsvServerConfigBase : SuperType {
	//编号
	public int id;
	//ip
	public string[] server_listen_ip;
	//端口
	public int server_listen_port;
	//延时
	public int server_connect_timeout;
	//id
	public int server_id;
	
	public string getIp()
	{
		string ip = "";
		for(int i = 0; i < server_listen_ip.Length; i++)
		{
			ip += server_listen_ip[i];
			if(i!= server_listen_ip.Length-1)
			{
				ip+=".";	
			}
		}
		return ip;
	}
}
