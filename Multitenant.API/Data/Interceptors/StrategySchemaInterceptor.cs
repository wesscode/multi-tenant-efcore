using Microsoft.EntityFrameworkCore.Diagnostics;
using Multitenant.API.Provider;
using System.Data.Common;

namespace Multitenant.API.Data.Interceptors
{
    //INTERCEPTARÁ AS CONSULTAS EXECUTADAS E FARÁ O REPLACE DO SCHEMA, ANTES DE CONSULTAR NO BANCO DE DADOS.
    public class StrategySchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantData _tenantData;

        public StrategySchemaInterceptor(TenantData tenantData)
        {
            _tenantData = tenantData;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            ReplaceSchema(command);
            return base.ReaderExecuting(command, eventData, result);
        }

        private void ReplaceSchema(DbCommand command)
        {
            //FROM PRODUCTS -> FROM [tenan-1].PRODUCTS
            command.CommandText = command.CommandText
                .Replace("FROM ", $" FROM [{_tenantData.TenantId}].")
                .Replace("JOIN ", $" JOIN [{_tenantData.TenantId}].");
        }
    }
}
