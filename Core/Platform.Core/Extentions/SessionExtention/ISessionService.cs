using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core.Extentions
{
    public interface ISessionService
    {

        /// <summary>
        /// 获取用户资料
        /// </summary>
        /// <param name="token">用户token</param>
        /// <returns></returns>
        T GetSession<T>(string token);

        /// <summary>
        /// 移除Token信息
        /// </summary>
        /// <param name="token">用户token</param>
        void RemoveToken(string token);

        /// <summary>
        /// 添加用户Session
        /// </summary>
        /// <param name="token">用户token</param>
        /// <param name="userSession">用户Session</param>
        void SetSession<T>(string token, T session, TimeSpan expireTime = default(TimeSpan));
    }
}
