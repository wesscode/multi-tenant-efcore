# Demo arquitetura Multitenant e estratégias

## Arquitetura Multitenant
- Multitenant(multi-inquilino) é um estilo de arquitetura de software que utiliza uma única aplicação
  para diversos clientes(inquilinos). Esta arquitetura é muito utilizada no conceito de cloud-computing(SaaS)
  onde aplica-se uma estratégia de isolamento dos dados para cada inquilino.
## Estratégias
- Identificador na tabela
  * Por meio de um campo identificado nas tabelas.
  * Com essa estratégia compartilhamos o mesmo banco de dados, tabelas, schemas com todos os inquilinos(clientes).
  * Os dados de todos os clientes ficam guardados em um unico lugar em uma unica tabela, sendo essas informações separadas apenas por esse identificador que define de quem é a informação.
  * Consulta com filtros para cada query por cliente.
- Schema
  * A menos indicada pois da bem mais trabalho para dar manutenção.
  * Utiliza o mesmo banco de dados para todos os inquilinos(clientes).
  * Informação de cada cliente é segregada por SCHEMA no mesmo banco de dados.
- Banco de dados
  * A melhor estratégia ao utilizar o estilo de arquitetura multitenant.
  * Permite Segregar as informação dos clientes por banco de dados isoladamente.
  * Permite aplicar uma politica de segurança maior criando restrições, ex: configurar para um usuário x, acessar somente determinado banco logo ficando mais próximo em conformidade com a lei do LGPD.
