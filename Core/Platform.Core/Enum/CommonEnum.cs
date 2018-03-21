using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core
{
    public class CommonEnum
    {
        /// <summary>
        /// 数据状态
        /// </summary>
        public enum DataState
        {
            /// <summary>
            /// 标准状态
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 禁用状态
            /// </summary>
            Disable = 1,
            /// <summary>
            /// 删除状态
            /// </summary>
            Delete = 2

        }

        /// <summary>
        /// 启用禁用状态
        /// </summary>
        public enum EnableState
        {
            /// <summary>
            /// 禁用
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 启用
            /// </summary>
            Enable = 1

        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public enum AuditState
        {
            /// <summary>
            /// 待审核
            /// </summary>
            Waiting = 0,
            /// <summary>
            /// 审核通过
            /// </summary>
            Approve = 1,
            /// <summary>
            /// 被驳回
            /// </summary>
            Refuse = 2

        }

    }
}