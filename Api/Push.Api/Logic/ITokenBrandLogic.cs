using Push.Api.DTOs;
using Sherlock.Framework;
using System.Collections.Generic;

namespace Push.Api.Logic
{
    public interface ITokenBrandLogic : IDependency
    {
        /// <summary>
        /// 检查Token是否注册
        /// </summary>
        /// <param name="RZToken"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        bool CheckRZToken(string RzToken, out List<TokenBrandDto> list);

        /// <summary>
        /// 检查Token是否注册
        /// </summary>
        /// <param name="RZToken"></param>
        /// <returns></returns>
        List<TokenBrandDto> GetRzTokenBrandListByRZToken(string RzToken);


        /// <summary>
        /// 匹配品牌编号
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="brandId"></param>
        /// <returns></returns>
        bool MatchBrandId(string brand, out int brandId);

        /// <summary>
        /// 检查AppId是否存在
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        bool CheckAppId(int appId, out string retMsg);

        int MatchSystemType(int brandId);

        bool CheckRzTokenBrand(long id, out TokenBrandDto rzTokenBrandDto);

    }
}
