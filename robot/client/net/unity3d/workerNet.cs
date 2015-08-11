using System;
using System.Collections.Generic;

namespace net.unity3d
{

    //public class QueueNode
    //{
    //    public ushort msg;
    //    public NetEventArgs args;
    //}


    public class workerNet
    {
        public workerNet()
        {

        }
		
        /**
		public static workerNet getInstance()
        {
            if (_self == null)
                _self = new workerNet();

            return _self;
        }
		**/

		// 单独出啦，管理所有包。在逻辑层取包，调用回调。
        private Queue<NodeQueue> _recv_queue = new Queue<NodeQueue>();
		private static workerNet _self;
		
		/// <summary>
        /// 回调事件加入链表
        /// </summary>
        public NodeQueue tick()
        {
            lock (_recv_queue)
            {
                if (_recv_queue.Count != 0)
                {
                    NodeQueue node = _recv_queue.Dequeue();
                    return node;
                }
            }
            return null;
        }

        public void AddQueue(NodeQueue node)
        {
			lock (_recv_queue)
            {
            	_recv_queue.Enqueue(node);
            }
        }

        public Queue<NodeQueue> getQueue()
        {
            return _recv_queue;
        }

    }

}
