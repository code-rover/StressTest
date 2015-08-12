using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using utils;
//using System.Xml;

namespace net
{
    /// <summary>
    /// 工厂接口.
    /// </summary>
    public interface FactoryNet
    {
        T createObject<T>(); 
        string Name { get; }
        string IID { get; }
        string Discription { get; }
    }
    
    /// <summary>
    /// 错误标识
    /// </summary>
    public static class NET_MGR_ERR
    {
        public static readonly uint NM_ERR = 10000;
        public static readonly uint NM_ERR_FAC_EXISTED = NM_ERR + 1;
        public static readonly uint NM_ERR_FAC_UNEXISTED = NM_ERR + 2;
        public static readonly uint NM_ERR_PFAC_EXISTED = NM_ERR + 3;
    }

    /// <summary>
    /// 服务器连接信息结构体
    /// </summary>
    public struct NoteServer
    {
        public string _serverIp;
        public short _serverProt;
        public int _timeOut;
		public int _serverId;
    }

    public class ManagerNoteServer
    {
        public ManagerNoteServer()
        {
            _serverNotes = new List<NoteServer>();
        }
		
		private List<NoteServer> _serverNotes;
		
		public NoteServer this[int num]
        {
            get{return _serverNotes[num];}
        }
		
		public void addNoteServer(NoteServer pNote)
        {
            NoteServer sNote = new NoteServer();
            sNote= pNote;
            _serverNotes.Add(sNote);
        }
		
//		public void addNoteServer(XmlNode sN)
//        {
//            NoteServer sNote = new NoteServer();
//            sNote._serverIp = sN.SelectSingleNode("server_listen_ip").InnerText;
//            sNote._serverProt = short.Parse(sN.SelectSingleNode("server_listen_port").InnerText);
//            sNote._timeOut = int.Parse(sN.SelectSingleNode("server_connect_timeout").InnerText);
//			sNote._serverId = int.Parse(sN.SelectSingleNode("server_id").InnerText);
//            _serverNotes.Add(sNote);
//        }
		
        public NoteServer getNoteServer(int index)
        {
			foreach(NoteServer server in _serverNotes)
			{			  	
				if(index == server._serverId)
				{
					return server;
				}
			}
            return default(NoteServer);
        }

		
		public int Count
        {
            get { return _serverNotes.Count; }
        }   
    }

    /// <summary>
    /// 读取服务器xml信息类
    /// </summary>
//    public class ReaderXml
//    {
//        public XmlDocument getXmlDoc(string xml_str)
//        {
//            XmlDocument xmlDoc = new XmlDocument();
//			if(UtilLog.isBulidLog)UtilLog.Log("the xml str is" + xml_str);
//			//从字符串中读Xml
//            xmlDoc.LoadXml(xml_str);
//			//从文件中读Xml
//			//xmlDoc.Load(xml_str);
//            return xmlDoc;
//        }
//
//        public XmlNodeList getXmlNodeList(string xml_str)
//        {
//            XmlNodeList servers = getXmlDoc(xml_str).SelectNodes("servers/server");
//            return servers;
//        }
//    }

    /// <summary>
    /// 客户端网络管理类.
    /// </summary>
    public sealed class ManagerNet
    {
        private ManagerNet()
        {
			curServerId = 0;
            _factorys = new Dictionary<string, FactoryNet>();
            _serverNotesManager = new ManagerNoteServer();
			_serverAccountNotesManager = new ManagerNoteServer();
        }
		
		private int curServerId;
        private ManagerNoteServer _serverNotesManager;
		private ManagerNoteServer _serverAccountNotesManager;
        private static ManagerNet _self = null;
        private Dictionary<string, FactoryNet> _factorys;
        private List<string> _libs = null;

        private void OnDestroy(ArgsEvent args)
        {
            HandleNetEvent handle = EventDestroy;
            if (handle != null)
            {
                handle(this, args);
            }
        }
        
		private void OnInit(ArgsEvent args)
        {
            HandleNetEvent handle = EventInit;
            if (handle != null)
            {
                handle(this, args);
            }
        }
		
		public event HandleNetEvent EventInit;
        public event HandleNetEvent EventDestroy;

        public int getCurServerId()
		{
			return curServerId;
		}
		public void addNote(NoteServer sNote)
		{
			_serverNotesManager.addNoteServer(sNote);
		}

        
		public int getNoteServersCnt()
		{
			return _serverNotesManager.Count;
		}
		
		public void addCurServerId()
		{
			curServerId++;
		}

        /// <summary>
        /// 移除个对象工厂.
        /// </summary>
        /// <param name="iid">类型id</param>
        public void removeFactory(string iid)
        {
            if (!_factorys.ContainsKey(iid))
            {
                throw new NetException("the factory '" + iid + "' does not exist.",
                    NET_MGR_ERR.NM_ERR_FAC_UNEXISTED);
            }

            _factorys.Remove(iid);
        }

        /// <summary>
        /// 添加个工厂实例
        /// </summary>
        /// <param name="fac">工厂</param>
        public void addFactory(FactoryNet fac)
        {
            if (_factorys.ContainsKey(fac.IID))
            {
                throw new NetException("the factory '" + fac.IID + "'has exist.",
                    NET_MGR_ERR.NM_ERR_FAC_EXISTED);
            }

            _factorys.Add(fac.IID, fac);
            
        }

		/// <summary>
        /// 创建对象实例.
        /// </summary>
        /// <typeparam name="T">要创建的对象类型.</typeparam>
        /// <param name="iid">要创建的类型ID</param>
        /// <returns>返回创建的对象</returns>
        public T createNetObject<T>(string iid)
        {
            if (_factorys.ContainsKey(iid))
            {
                return _factorys[iid].createObject<T>();
            }

            return default(T);
        }

        
		/// <summary>
        /// 得到服务器参数.
        /// </summary>
        /// <param name="index">服务器编号</param>
        /// <returns>返回创建的对象</returns>
        public NoteServer getNoteServer(int index)
        {
			return _serverNotesManager.getNoteServer(index);
        }
		
		public NoteServer getAccountNoteServer()
		{
			if (0 >= _serverAccountNotesManager.Count)
			{
				return default(NoteServer);
			}
			else
			{
				return _serverAccountNotesManager[0];
			}
		}

        /// <summary>
        /// 销毁已加载的库.
        /// </summary>
        public void destroy()
        {
            OnDestroy( new ArgsEvent(null) );

            foreach (string lib in _libs)
            {
                Assembly assem = Assembly.LoadFile(lib);
                MethodInfo mtd = assem.GetType("Global").GetMethod("LibUnLoad",
                    new Type[] { typeof(ManagerNet) });
                mtd.Invoke(null, new System.Object[] { this });
            }
            _self = null;
            _factorys.Clear();

        }
		
		public void init(List<NoteServer> ListNote)
		{
			int index = 0;
			try
            {
				curServerId++;
                //addFactory(new net.unity3d.UConnNetFactory(UNITY3D.IID_CONN + index.ToString()));
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
			
			foreach (NoteServer server in ListNote)
            {
				 _serverNotesManager.addNoteServer(server);
                index++;
            }
			OnInit(new ArgsEvent(null));
		}

		public void initAccountNote(List<NoteServer> ListNote)
		{
			foreach (NoteServer server in ListNote)
            {
				 _serverAccountNotesManager.addNoteServer(server);
            }
		}
		
//		public void initAccountNote(string xml_str)
//		{
//			if (xml_str != null && xml_str != "")
//            {
//
//                ReaderXml XRer = new ReaderXml();
//                XmlNodeList servers = XRer.getXmlNodeList(xml_str);
//                int index = 0;
//
//				foreach (XmlNode server in servers)
//                {
//					 _serverAccountNotesManager.addNoteServer(server);
//                    index++;
//                }
//
//            }	
//		}

		public static ManagerNet getInstance()
        {
            if (_self == null)
                _self = new ManagerNet();

            return _self;
        }
    }
}
