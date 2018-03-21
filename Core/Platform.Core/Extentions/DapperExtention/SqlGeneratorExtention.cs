using Dapper;
using System;
using System.Collections.Generic;

namespace Sherlock.Framework.Data
{
    public static class SqlGeneratorExtention
    {
        /// <summary>
        /// 生成以 AND 连接的 不带 WHERE 关键字的 WHERE 语句
        /// </summary>
        /// <param name="sqlgen"></param>
        /// <param name="filter">查询条件</param>
        /// <param name="fieldsInClause">in条件集合</param>
        /// <param name="likeFilds">like条件集合</param>
        /// <returns></returns>
        public static string GenAndWhereSql<T>(this DapperSqlGenerator sqlgen, QueryFilter filter, Dictionary<string, List<object>> fieldsInClause, Dictionary<string, string> likeFilds, DynamicParameters parameters)
        {
            List<string> where = new List<string>();

            if (filter != null)
            {
                var whilefilter = sqlgen.GenerateFilter<T>(filter, parameters);
                if (!string.IsNullOrWhiteSpace(whilefilter))
                    where.Add(whilefilter);
            }
            if (!fieldsInClause.IsNullOrEmpty())
            {
                foreach (var item in fieldsInClause)
                {
                    var inclause = sqlgen.GenerateInClause<T>(item.Key, item.Value, parameters);
                    if (!string.IsNullOrWhiteSpace(inclause))
                        where.Add(inclause);
                }
            }
            if (!likeFilds.IsNullOrEmpty())
            {
                foreach (var item in likeFilds)
                {
                    var likeclause = sqlgen.GenLikeClause(LikeRegion.Both, item.Key, item.Value, parameters);
                    if (!string.IsNullOrWhiteSpace(likeclause))
                        where.Add(likeclause);
                }
            }
            return where.ToArrayString(" AND ");
        }

        /// <summary>
        /// 生成 like 语句从句
        /// </summary>
        /// <param name="sqlgen"></param>
        /// <param name="region"></param>
        /// <param name="fieldName"></param>
        /// <param name="likevalue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GenLikeClause(this DapperSqlGenerator sqlgen, LikeRegion region, string fieldName, string likevalue, DynamicParameters parameters)
        {
            string value = likevalue;
            var underLineFieldName = DapperContextExtentions.ConvertNameFromPascalToUnderline(null, fieldName);
            switch (region)
            {
                case LikeRegion.Both:
                    value = $"%{likevalue}%";
                    break;
                case LikeRegion.Left:
                    value = $"%{likevalue}";
                    break;
                case LikeRegion.Right:
                    value = $"{likevalue}%";
                    break;
                default:
                    throw new SherlockException($"{nameof(SqlGeneratorExtention)}-->{nameof(GenLikeClause)}:不支持的region");
            }
            parameters.Add(fieldName, value);
            return $" {underLineFieldName} like @{fieldName} ";
        }
    }
}
