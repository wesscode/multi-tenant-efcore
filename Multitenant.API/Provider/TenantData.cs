namespace Multitenant.API.Provider
{
    //classe criada para ser atribuido o tenant que estiver fazendo a requisição.
    public class TenantData
    {
        //Estratégia 01
        //public string TenantId { get; set; }

        //Estratégia 02
        public string TenantId { get; set; } = "dbo"; //quando aplicatioContext inciar sem essa anotação o schema iria iniciar com erro. Pois a tenantId só é preenchida na execução.
    }
}
