using Dapper;
using El_Master.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class DapperQueryRepository<T> : IQueryRepository<T>
    {
        private readonly IDbConnection _connection;

        public DapperQueryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string query, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(query, parameters);
        }

        public async Task<T?> GetAsync(string query, object? parameters = null)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<IEnumerable<TMain>> GetAllWithIncludeAsync<TMain, TRelated>(string mainTable,
                                                                                    string relatedTable,
                                                                                    string mainTableFK,
                                                                                    string relatedTablePK,
                                                                                    string joinCondition,
                                                                                    string whereClause = "",
                                                                                    object? parameters = null)
        {
            var sql = $@"SELECT m.*, r.*
                     FROM {mainTable} m
                     INNER JOIN {relatedTable} r ON {joinCondition}
                     {whereClause}";
            return await _connection.QueryAsync<TMain, TRelated, TMain>(
                sql,
                (main, related) =>
                {
                    var prop = typeof(TMain).GetProperty(typeof(TRelated).Name);
                    if (prop != null)
                        prop.SetValue(main, related);
                    return main;
                },
                parameters,
                splitOn: relatedTablePK
            );
        }


        public async Task<TMain?> GetWithIncludeSingleAsync<TMain, TRelated>(string mainTable,
                                                                                string relatedTable,
                                                                                string mainTableFK,
                                                                                string relatedTablePK,
                                                                                string joinCondition,
                                                                                string whereClause = "",
                                                                                object? parameters = null)
        {
            var sql = $@"
        SELECT m.*, r.*
        FROM {mainTable} m
        INNER JOIN {relatedTable} r ON {joinCondition}
        {whereClause}";

            var result = await _connection.QueryAsync<TMain, TRelated, TMain>(
                sql,
                (main, related) =>
                {
                    var prop = typeof(TMain).GetProperty(typeof(TRelated).Name);
                    if (prop != null)
                        prop.SetValue(main, related);
                    return main;
                },
                parameters,
                splitOn: relatedTablePK
            );

            return result.FirstOrDefault();
        }

    }
}
