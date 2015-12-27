using System;
using System.Drawing;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using System.Web.Script.Serialization;

namespace iWay.RemoteControlClient.RemoteExplorer
{
    public class OnResponseReceivedArgs : EventArgs
    {
        public int ResponseType
        {
            get;
            set;
        }

        public string ResponseJSON
        {
            get;
            set;
        }

        public T GetResponse<T>()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(ResponseJSON);
        }
    }
}
