using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Multitenant.API.Data.ModelFactory
{
    //SEMPRE QUE CRIAR UMA ISTANCIA DO APPCONTEXT E FOR EXECUTAR UMA CONSULTA, OBTEREMOS O SCHEMA DO TENANT DATA E PASSAREMOS PARA OS ARQUIVOS DE MODEL CACHE DO ENTITY
    public class StrategySchemaModelCacheKey : IModelCacheKeyFactory
    {
        //Metodo que altera o contexto e nesse contexto alteramos o schema que o contexto deve usar nas consultas.
        public object Create(DbContext context, bool designTime)
        {
            var model = new
            {
                Type = context.GetType(),
                Schema = (context as ApplicationContext)?._tenant.TenantId
            };

            return model;
        }
    }
}
