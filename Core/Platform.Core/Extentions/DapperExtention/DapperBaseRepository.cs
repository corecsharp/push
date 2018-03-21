using Dapper;
using Microsoft.Extensions.Logging;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Platform.Core.Extentions.DapperExtention
{
    /// <summary>
    /// 单表查询的抽象 Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DapperBaseRepository<T> : DapperRepository<T> where T : class
    {
        public DapperBaseRepository(DapperContext dapperContext, ILoggerFactory loggerFactory = null) : base(dapperContext, loggerFactory)
        {
        }

        /// <summary>
        /// 根据 <see cref="QueryFilter"/> 和 <see cref="object[]"/> 查询并更新字段
        /// </summary>
        /// <param name="filter">搜索条件</param>
        /// <param name="ids">主键 In 语句的包含数组</param>
        /// <param name="fieldsToUpdate">需要更新的字段</param>
        /// <returns>影响数</returns>
        protected async Task<int> UpdateByIdInclauseAndCondition(QueryFilter filter, object[] ids, IEnumerable<KeyValuePair<string, object>> fieldsToUpdate)
        {
            var sqlgen = Context.Runtime.SqlGenerator;
            var parameters = new DynamicParameters();
            var metdata = Context.Runtime.GetMetadata(EntityType);
            string sets = base.Context.Runtime.SqlGenerator.GenerateSetSegments<T>(fieldsToUpdate, parameters);
            List<string> where = new List<string>();
            if (filter != null)
            {
                var whilefilter = sqlgen.GenerateFilter<T>(filter, parameters);
                where.Add(whilefilter);
            }
            var fieldName = metdata.Fields.FirstOrDefault(r => r.IsKey).Field;
            var inclause = sqlgen.GenerateInClause<T>(fieldName.Name, ids.AsEnumerable(), parameters);
            where.Add(inclause);
            var wheresql = where.ToArrayString(" AND ");
            var sql = $"UPDATE {base.Context.Runtime.DelimitIdentifier(typeof(T),metdata.TableName)} SET {sets} WHERE {wheresql}";
            var connection = base.GetWritingConnection();
            return await connection.ExecuteAsync(sql, parameters);
        }

        /// <summary>
        /// 根据 <see cref="QueryFilter"/> 和 <see cref="object[]"/> 查询
        /// </summary>
        /// <param name="filter">搜索条件</param>
        /// <param name="ids">主键 In 语句的包含数组</param>
        /// <returns>DBModel 集合</returns>
        protected async Task<IEnumerable<T>> QueryByIdInclauseAndCondition(QueryFilter filter, object[] ids)
        {
            var sqlgen = Context.Runtime.SqlGenerator;
            var parameters = new DynamicParameters();
            var metdata = Context.Runtime.GetMetadata(EntityType);

            List<String> where = new List<String>();
            if (filter != null)
            {
                var whilefilter = sqlgen.GenerateFilter<T>(filter, parameters);
                where.Add(whilefilter);
            }
            var fieldName = metdata.Fields.FirstOrDefault(r => r.IsKey).Field;
            var inclause = sqlgen.GenerateInClause<T>(fieldName.Name, ids.AsEnumerable(), parameters);
            where.Add(inclause);
            var wheresql = where.ToArrayString(" AND ");

            var sql = $"SELECT * FROM {this.Context.Runtime.DelimitIdentifier(typeof(T),metdata.TableName)} WHERE {wheresql}";
            return await this.GetReadingConnection().QueryAsync<T>(sql, parameters);
        }

        /// <summary>
        /// 根据 <see cref="QueryFilter"/> 和 <see cref="object[]"/> 查询，一般用于捞取简单的数据
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="filter"></param>
        /// <param name="ids"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<TResult>> QueryByIdInclauseAndCondition<TResult>(QueryFilter filter, object[] ids, Func<IEnumerable<T>, IEnumerable<TResult>> func)
        {
            var temp = await this.QueryByIdInclauseAndCondition(filter, ids);
            return func(temp);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="fieldsInClause">in条件集合</param>
        /// <param name="likeFilds">like条件集合</param>
        /// <param name="options">排序</param>
        /// <param name="pageIndex">Sherlock页码（从0开始）</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        protected async Task<Tuple<int, IEnumerable<T>>> QueryPagedByInClauseAndOtherFilter(QueryFilter filter, Dictionary<string, List<object>> fieldsInClause, Dictionary<string, string> likeFilds, SortOptions options,
            int pageIndex, int pageSize)
        {
            var sqlgen = Context.Runtime.SqlGenerator;
            var metdata = Context.Runtime.GetMetadata(EntityType);
            string tableIdentifier = this.Context.Runtime.DelimitIdentifier(typeof(T),metdata.TableName);

            var parameters = new DynamicParameters();
            //构造 SelectSql
            var selectSql = Context.Runtime.GetCrudSegments(EntityType).SelectSql;
            //构造 WhereSql
            var whereSql = sqlgen.GenAndWhereSql<T>(filter, fieldsInClause, likeFilds, parameters);
            //构造 OrderSql
            var order = Context.Runtime.SqlGenerator.GenerateOrderBy<T>(options);
            //构造 分页SQL
            var sql = Context.Runtime.GetDatabaseProvider().BuildPaginationTSql(pageIndex, pageSize, selectSql, order, whereSql);
            //构造 计数SQL
            var countSql = $" SELECT COUNT(1) FROM {tableIdentifier} ";
            if (!string.IsNullOrWhiteSpace(whereSql))
            {
                countSql = $" {countSql} where {whereSql} ";
            }

            var connection = this.GetReadingConnection();
            //此处允许脏读,所以走两次
            var data = await connection.QueryAsync<T>(sql, parameters);
            var count = await connection.ExecuteScalarAsync<int>(countSql, parameters);
            return new Tuple<int, IEnumerable<T>>(count, data);
        }
    }
}
