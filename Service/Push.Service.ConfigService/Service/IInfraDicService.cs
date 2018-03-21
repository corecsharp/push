using Push.Service.ConfigService.DomainModel;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ConfigService.Service
{
    /// <summary>
    /// 字典
    /// </summary>
    public interface IInfraDicService : IDependency
    {
        /// <summary>
        /// 根据字典类型获取字典的列表
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        Task<List<InfraDicDomainModel>> GetDicListByTypeAsync(string dicType);

        /// <summary>
        /// 根据类型和键值获取字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <param name="dicValue"></param>
        /// <returns></returns>
        Task<InfraDicDomainModel> GetDicByTypeAndValueAsync(string dicType, string dicValue);

        /// <summary>
        /// 根据类型和键值获取字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        Task<InfraDicDomainModel> GetDicByTypeAndKeyAsync(string dicType, string dicKey);
    }
}
