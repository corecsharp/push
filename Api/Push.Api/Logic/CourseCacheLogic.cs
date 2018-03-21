using Platform.Core.Helper;
using Push.Api.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Push.Api.Logic
{
    /// <summary>
    /// 进程缓存逻辑
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    public class CourseCacheLogic<TKey, TValue> where TValue : CourseBaseDto, new()
    {
        public static ConcurrentDictionary<TKey, TValue> dic = new ConcurrentDictionary<TKey, TValue>();
        /// <summary>
        /// 获取字典里的值
        /// </summary>
        /// <param name="tKey">键值</param>
        /// <param name="db">数据源</param>
        /// <returns></returns>
        public static TValue Get(TKey tKey, Func<TValue> db)
        {
            TValue dto;
            dic.TryGetValue(tKey, out dto);
            if (dto == null)
            {
                //如果字典里没有 
                TValue dbVal = db();
                if (dbVal != null)
                {
                    dbVal.UpdateAt = DateTimeHelper.GetNow();
                    dto = dbVal;
                    dic.AddOrUpdate(tKey, dto, (u, v) => { return dto; });
                }
            }
            else if (dto.UpdateAt.AddMinutes(5) < DateTimeHelper.GetNow())
            {
                //如果字典的LoadTime超时
                TValue dbVal = db();
                if (dbVal != null)
                {
                    dbVal.UpdateAt = DateTimeHelper.GetNow();
                }
                dto = dbVal;
                dic.AddOrUpdate(tKey, dto, (u, v) => { return dto; });
            }
            return dto;
        }
    }

    public class CourseCacheListLogic<TKey, TValue>
    {
        public static ConcurrentDictionary<TKey, CourseBaseDto<List<TValue>>> dic = new ConcurrentDictionary<TKey, CourseBaseDto<List<TValue>>>();
        /// <summary>
        /// 获取字典里的值
        /// </summary>
        /// <param name="tKey">键值</param>
        /// <param name="db">数据源</param>
        /// <returns></returns>
        public static List<TValue> Get(TKey tKey, Func<List<TValue>> db)
        {
            CourseBaseDto<List<TValue>> dto;
            dic.TryGetValue(tKey, out dto);
            if (dto == null)
            {
                //如果字典里没有 
                List<TValue> dbVal = db();
                if (dbVal != null)
                {
                    dto = new CourseBaseDto<List<TValue>>();
                    dto.Content = dbVal;
                    dto.UpdateAt = DateTimeHelper.GetNow();
                    dic.AddOrUpdate(tKey, dto, (u, v) => { return dto; });
                }
            }
            else if (dto.UpdateAt.AddMinutes(5) < DateTimeHelper.GetNow())
            {
                //如果字典的LoadTime超时
                List<TValue> dbVal = db();
                if (dbVal != null)
                {
                    dto.Content = dbVal;
                    dto.UpdateAt = DateTimeHelper.GetNow();
                }
                else
                {
                    dto.Content = null;
                }
                dic.AddOrUpdate(tKey, dto, (u, v) => { return dto; });
            }
            return dto.Content;
        }
    }

}
