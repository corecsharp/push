using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sherlock.Framework;
using Sherlock.Framework.Data;
using Push.Service.MessageCenterService.DBModel;
using Push.Service.MessageCenterService.DomainModel;

namespace Push.Service.MessageCenterService.Repository
{
    /// <summary>
    /// 消息推送访问仓储接口
    /// </summary>
    public interface IPushMessageRepository: IRepository<PushMessage>, IDependency
    {
        /// <summary>
        /// 批量设置消息阅读状态为已读。
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Operator"></param>
        /// <returns></returns>
        Task<int> BatchSetReadAsync(IEnumerable<long> ids,long userId,int appId);

    }
}
