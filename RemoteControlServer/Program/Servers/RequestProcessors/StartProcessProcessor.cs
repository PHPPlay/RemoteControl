using System;
using System.Diagnostics;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class StartProcessProcessor : BasicProcessor
    {
        public StartProcessProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            StartProcessReq req = mSocketTalker.ReceiveObject<StartProcessReq>();
            StartProcessRes res = new StartProcessRes();
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = req.FileName;
                startInfo.Arguments = req.Arguments;
                startInfo.CreateNoWindow = req.CreateNoWindow;
                startInfo.UseShellExecute = req.UseShellExecute;
                Process.Start(startInfo);

                mSocketTalker.SendInt(ProtocolTypes.TYPE_START_PROCESS);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_START_PROCESS);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
