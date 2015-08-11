using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace utils
{
    public  class Logger
    {
        public static void Debug(string msg)
        {
            //if(UtilLog.isBulidLog)UtilLog.Log(msg);
        }

        public static void Error(string msg)
        {
            //if(UtilLog.isBulidLog)UtilLog.Log(msg);
        }

		public  enum SSYIDS
		{
			/// <summary>
			/// load 
			/// </summary>
			LOADMNGR = 1,
			/// <summary>
			/// avata system
			/// </summary>
			AVATAR,
			/// <summary>
			/// bind audio
			/// </summary>
			AUDIOBIND,
			/// <summary>
			/// effect bind
			/// </summary>
			EFFBIND,
			/// <summary>
			/// change sence logic
			/// </summary>
			MANAGESENCE,
			/// <summary>
			/// guid system
			/// </summary>
			GUID, 
			/// <summary>
			/// recv data from server
			/// </summary>
			DATAMODERECV,
			/// <summary>
			/// solve camera move bug
			/// </summary>
			CAMERA,
			/// <summary>
			/// iphone npc can't not click
			/// </summary>
			IPHONE_NPC,
			/// <summary>
			/// The IPHON e_ CAMER.
			/// </summary>
			IPHONE_CAMERA,
			/// <summary>
			/// animation system 
			/// </summary>
			ANIMATION_SYSTEM,
			/// <summary>
			/// dialog system
			/// </summary>
			DIALOG,
			/// <summary>
			/// fb ui
			/// </summary>
			FB_UI,
			/// <summary>
			/// 各种弹出框
			/// </summary>
			POPUP,
			/// <summary>
			/// version 
			/// </summary>
			VERSION,
			/// <summary>
			/// 优化
			/// </summary>
			OPT,


		}

		public static SSYIDS[] sValidLogs = {};
		public static List<SSYIDS> list_ids = null;

        /**
		/// <summary>
		/// ssy开发调使用
		/// </summary>
		/// <param name="msg">Message.</param>
		public static void traceSSY(SSYIDS id, string msg)
		{
			bool is_log = false;
			if(list_ids == null)
			{
				list_ids = new List<SSYIDS> ();
				// changed by ssy
				// ios not support good addrage
				//list_ids.AddRange (sValidLogs);
				GUtil.addRange(list_ids, sValidLogs);
				// changed end
			}


			if(Application.platform == RuntimePlatform.WindowsEditor)
			{
				if (Network.player.ipAddress == "192.168.1.12" || Network.player.ipAddress == "192.168.1.108")			 
				{
					if (list_ids.Contains (id)) 
					{
						is_log = true;

					}
				}
			}
			else if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				if(list_ids.Contains(id))
				{
					is_log = true;
				}
			}


			if(is_log)
			{
				UtilLog.LogWarning("$$$ ssy trace sys = " + id.ToString() + " content = " + msg + " time = " + Time.realtimeSinceStartup);
			}

		}

		public static void stopSSY()
		{
			if (Network.player.ipAddress == "192.168.1.53") 
			{
				UtilLog.LogError("$ ssy stop time = " + Time.realtimeSinceStartup);		
			}
		}
         **/
    }
}
