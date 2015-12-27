using System.Net;
using System.Net.Mail;

namespace iWay.RemoteControlBase.Utilities
{
    public class MailSender
    {
        private string mServerAddr;
        private int mServerPort;
        private string mLoginAccount;
        private string mLoginPassword;
        private string mDisplayName;
        private string[] mReceiverAddrs;
        private string mSubject;
        private string mBody;
        private string[] mAttachFiles;

        public void SetServerInfo(string serverAddr, int serverPort = 25)
        {
            mServerAddr = serverAddr;
            mServerPort = serverPort;
        }

        public void SetLoginInfo(string account, string password, string displayName = null)
        {
            mLoginAccount = account;
            mLoginPassword = password;
            mDisplayName = displayName;
        }

        public void SetReceivers(params string[] receivers)
        {
            mReceiverAddrs = receivers;
        }

        public void SetContent(string subject, string body, params string[] attachFiles)
        {
            mSubject = subject;
            mBody = body;
            mAttachFiles = attachFiles;
        }

        public void Send()
        {
            SmtpClient smtpClient = new SmtpClient(mServerAddr, mServerPort);
            smtpClient.Credentials = new NetworkCredential(mLoginAccount, mLoginPassword);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mLoginAccount, mDisplayName);
            mailMessage.Sender = new MailAddress(mLoginAccount, mDisplayName);
            foreach (string receiver in mReceiverAddrs)
                mailMessage.To.Add(receiver);
            mailMessage.Subject = mSubject;
            mailMessage.Body = mBody;
            if (mAttachFiles != null && mAttachFiles.Length > 0)
                foreach (string path in mAttachFiles)
                    mailMessage.Attachments.Add(new Attachment(path));
            smtpClient.Send(mailMessage);
        }
    }
}
