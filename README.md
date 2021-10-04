
Repositório para o projeto base em .Net Core. Este é o template principal para todos os projetos.

# Projeto Core

## Ao fazer o clone do projeto:

1. no "Package Manage Console", digitar:

```Update-Database -Context ApplicationDbContext```

- OBS1: não esquecendo de setar o "default project" como **Core.API**


2. no "Package Manage Console", digitar 

```Update-Database -Context MyDbContext```

- OBS2: não esquecendo de setar o "default project" como **Core.Data**


## Regras para a Padronização de Controllers

### Exemplo em Operações CRUD
Verbo | Controller | Action | Definição 
------------ | ------------- | ------------- | -------------
GET | /clients | index() | Exibe DataTable
GET | /clients/{id} | edit(guid id) | Form para edição
DELETE | /clients/{id} | delete(guid id) | Para excluir um registo
POST | /clients | create(_view_model) | Para insert ou update
POST | /clients/enable | enable(_view_model) | Para operações custom
POST | /clients/disable | disable(_view_model) | Para operações custom
 
Controllers **sempre** no plural.
Actions **sempre** no singular.

**Atenção:** Commits fora deste padrão serão rejeitados.

Para gerar o scritp da base em sql

no "Package Manage Console", digitar:

Command:

Script-Migration -Context MyDbContext -o .\sql\sql.sql

Script-Migration -Context ApplicationDbContext -o .\sql\sql_identity.sql
