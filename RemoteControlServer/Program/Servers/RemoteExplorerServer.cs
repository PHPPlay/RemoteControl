using System;
using System.Net.Sockets;
using System.Threading;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlServer.Program.Servers.RequestProcessors;

namespace iWay.RemoteControlServer.Program.Servers
{
    public class RemoteExplorerServer : RCServer
    {
        public RemoteExplorerServer(Socket socket)
            : base(socket)
        {
        }

        private Thread mProcessThread;

        public override void BeginService()
        {
            mProcessThread = new Thread(new ThreadStart(ProcessRequest));
            mProcessThread.IsBackground = true;
            mProcessThread.Start();
        }

        private void ProcessRequest()
        {
            try
            {
                while (true)
                {
                    int protocalType = mSocketTalker.ReceiveInt();
                    BasicProcessor requestProcessor = null;
                    switch (protocalType)
                    {
                        case ProtocolTypes.TYPE_LIST_CONTENTS:
                            requestProcessor = new ListContentsProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_LIST_ALL_FILES:
                            requestProcessor = new ListAllFilesProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_LIST_ALL_DIRECTORIES:
                            requestProcessor = new ListAllDirectoriesProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_DELETE_CONTENTS:
                            requestProcessor = new DeleteContentsProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_RENAME_CONTENT:
                            requestProcessor = new RenameContentProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_CREATE_DIRECTORY:
                            requestProcessor = new CreateDirectoryProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_START_PROCESS:
                            requestProcessor = new StartProcessProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_GET_CONTENT_INFO:
                            requestProcessor = new GetContentInfoProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_MOVE_CONTENTS:
                            requestProcessor = new MoveContentsProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_COPY_CONTENTS:
                            requestProcessor = new CopyContentsProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_GET_CONTENT:
                            requestProcessor = new GetContentProcessor(mSocketTalker);
                            break;
                        case ProtocolTypes.TYPE_PUT_CONTENT:
                            requestProcessor = new PutContentProcessor(mSocketTalker);
                            break;
                        default:
                            throw new Exception("Invalid requested protocol type.");
                    }
                    requestProcessor.ProcessRequest();

                    if (requestProcessor.CanProcessNextRequest)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                Close();
            }
        }
    }
}
