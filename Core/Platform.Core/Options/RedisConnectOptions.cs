using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Core.Options
{
   public class RedisConnectOptions
    {
        /// <summary>
        /// redis 服务器
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// redis 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 使用redis的数据库编号
        /// </summary>
       public int DataBaseNo { get; set; }
    }
}
