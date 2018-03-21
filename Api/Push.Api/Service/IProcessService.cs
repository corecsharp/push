
using Sherlock.Framework;
using System;
using System.Collections.Generic;


namespace Push.Api.Service
{
    public interface IProcessService : IDependency
    {

        List<long> GetProcessIdList(out string retMsg);

        /// <summary>
        /// 从Process表取出待发送的BatchNO
        /// </summary>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        string GetProcessBatchNO(out string retMsg);

    }
}
