using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IQueryRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string query, object? parameters = null);
        Task<T?> GetAsync(string query, object? parameters = null);
        Task<IEnumerable<M>> QueryAsync<M>(string sql, object? parameters = null);
        Task<IEnumerable<TMain>> GetAllWithIncludeAsync<TMain, TRelated>(string mainTable,
                                                                                    string relatedTable,
                                                                                    string mainTableFK,
                                                                                    string relatedTablePK,
                                                                                    string joinCondition,
                                                                                    string whereClause = "",
                                                                                    object? parameters = null);
        Task<TMain?> GetWithIncludeSingleAsync<TMain, TRelated>(string mainTable,
                                                                                string relatedTable,
                                                                                string mainTableFK,
                                                                                string relatedTablePK,
                                                                                string joinCondition,
                                                                                string whereClause = "",
                                                                                object? parameters = null);

    }
}
