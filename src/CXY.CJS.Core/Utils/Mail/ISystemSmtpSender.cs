using System.Threading.Tasks;

namespace CXY.CJS.Core.Utils.Mail
{
    public interface ISystemSmtpSender
    {
        /// <summary>
        /// 使用系统默认的发件人发送
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 使用配置节点的邮箱发送
        /// </summary>
        /// <param name="fromName">配置节点</param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        Task SendAsync(string fromName,string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 使用系统默认的发件人发送
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        void Send(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 使用配置节点的邮箱发送
        /// </summary>
        /// <param name="fromName">配置节点</param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        void Send(string fromName, string to, string subject, string body, bool isBodyHtml = true);
    }
}