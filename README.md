# FI.WebAtividadeEntrevista

Aplicação ASP.NET MVC (.NET Framework 4.8) com projeto Web e projeto de lógica de negócio/dados. Implementa cadastro e gestão de Clientes e seus Beneficiários, com validações (incluindo CPF), interface com jTable e persistência via LocalDB.

- Solução: `FI.WebAtividadeEntrevista.sln`
- Web: [FI.WebAtividadeEntrevista/FI.WebAtividadeEntrevista.csproj](FI.WebAtividadeEntrevista/FI.WebAtividadeEntrevista.csproj)
- Lib: [FI.AtividadeEntrevista/FI.AtividadeEntrevista.csproj](FI.AtividadeEntrevista/FI.AtividadeEntrevista.csproj)

## Como Rodar

1. Restaurar pacotes NuGet
   - VS: Restore NuGet Packages na solução.
   - CLI: `nuget restore FI.WebAtividadeEntrevista.sln`.
2. Confirmar connectionString
   - Em [FI.WebAtividadeEntrevista/Web.config](FI.WebAtividadeEntrevista/Web.config), nome `BancoDeDados`, apontando para `App_Data\BancoDeDados.mdf`.
3. Buildar a solução
   - VS: Build na solução.
   - CLI (MSBuild): `msbuild FI.WebAtividadeEntrevista.sln /p:Configuration=Debug`.
4. Executar
   - VS: IIS Express (HTTPS padrão configurado em [`FI.WebAtividadeEntrevista.csproj`](FI.WebAtividadeEntrevista/FI.WebAtividadeEntrevista.csproj), porta SSL 44333).
   - Navegar para a raiz (Home) e acessar o menu Clientes.

## Arquitetura e Camadas

- Apresentação (ASP.NET MVC)
  - Controllers:
    - [`FI.WebAtividadeEntrevista.Controllers.HomeController`](FI.WebAtividadeEntrevista/Controllers/HomeController.cs)
    - [`FI.WebAtividadeEntrevista.Controllers.ClienteController`](FI.WebAtividadeEntrevista/Controllers/ClienteController.cs)
    - [`FI.WebAtividadeEntrevista.Controllers.BeneficiarioController`](FI.WebAtividadeEntrevista/Controllers/BeneficiarioController.cs)
  - Views:
    - [Views/Cliente/Incluir.cshtml](FI.WebAtividadeEntrevista/Views/Cliente/Incluir.cshtml)
    - [Views/Cliente/Alterar.cshtml](FI.WebAtividadeEntrevista/Views/Cliente/Alterar.cshtml)
    - Layout: [Views/Shared/_Layout.cshtml](FI.WebAtividadeEntrevista/Views/Shared/_Layout.cshtml)
  - Bundles e assets:
    - [`FI.WebAtividadeEntrevista.App_Start.BundleConfig`](FI.WebAtividadeEntrevista/App_Start/BundleConfig.cs)
    - jTable: [Scripts/jtable/jquery.jtable.min.js](FI.WebAtividadeEntrevista/Scripts/jtable/jquery.jtable.min.js), tema (metro/darkgray): [Scripts/jtable/themes/metro/darkgray/jtable.css](FI.WebAtividadeEntrevista/Scripts/jtable/themes/metro/darkgray/jtable.css)
  - Scripts da aplicação:
    - Clientes: [Scripts/Clientes/FI.Clientes.js](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.Clientes.js), [Scripts/Clientes/FI.AltClientes.js](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.AltClientes.js), [Scripts/Clientes/FI.ListClientes.js](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.ListClientes.js), máscaras: [Scripts/Clientes/FI.Mascaras.js](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.Mascaras.js)
    - Beneficiários: [Scripts/Beneficiario/FI.Beneficiarios.js](FI.WebAtividadeEntrevista/Scripts/Beneficiario/FI.Beneficiarios.js)

- Serviços (aplicação)
  - [`FI.WebAtividadeEntrevista.Service.ClienteService`](FI.WebAtividadeEntrevista/Service/ClienteService.cs)
  - [`FI.WebAtividadeEntrevista.Service.BeneficiarioService`](FI.WebAtividadeEntrevista/Service/BeneficiarioService.cs)
  - Interfaces: [Service/Interface](FI.WebAtividadeEntrevista/Service/Interface)

- Domínio/Negócio (BLL/DAL/DML)
  - BLL:
    - [`FI.AtividadeEntrevista.BLL.BoCliente`](FI.AtividadeEntrevista/BLL/BoCliente.cs)
    - [`FI.AtividadeEntrevista.BLL.BoBeneficiario`](FI.AtividadeEntrevista/BLL/BoBeneficiario.cs)
    - Validações: [`FI.AtividadeEntrevista.BLL.Validators.CpfValidator`](FI.AtividadeEntrevista/BLL/Validators/CpfValidator.cs)
  - DAL:
    - [`FI.AtividadeEntrevista.DAL.Clientes.DaoCliente`](FI.AtividadeEntrevista/DAL/Clientes/DaoCliente.cs)
    - [`FI.AtividadeEntrevista.DAL.Beneficiarios.DaoBeneficiario`](FI.AtividadeEntrevista/DAL/Beneficiarios/DaoBeneficiario.cs)
    - Infra de acesso: [`FI.AtividadeEntrevista.DAL.AcessoDados`](FI.AtividadeEntrevista/DAL/Padrao/FI.AcessoDados.cs)
    - Procedures (SQL):
      - Clientes: [FI_SP_IncCliente.sql](FI.AtividadeEntrevista/DAL/Clientes/Procedures/FI_SP_IncCliente.sql), [FI_SP_AltCliente.sql](FI.AtividadeEntrevista/DAL/Clientes/Procedures/FI_SP_AltCliente.sql), [FI_SP_DelCliente.sql](FI.AtividadeEntrevista/DAL/Clientes/Procedures/FI_SP_DelCliente.sql), [FI_SP_PesqCliente.sql](FI.AtividadeEntrevista/DAL/Clientes/Procedures/FI_SP_PesqCliente.sql), [FI_SP_ConsCliente.sql](FI.AtividadeEntrevista/DAL/Clientes/Procedures/FI_SP_ConsCliente.sql)
      - Beneficiários: [FI_SP_IncBeneficiario.sql](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_IncBeneficiario.sql), [FI_SP_AltBeneficiario.sql](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_AltBeneficiario.sql), [FI_SP_DelBeneficiario.sql](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_DelBeneficiario.sql), [FI_SP_PesqBeneficiario.sql](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_PesqBeneficiario.sql), [FI_SP_ConsBeneficiario.sql](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_ConsBeneficiario.sql), verificação de CPF: [FI_SP_ExisteBeneficiarioPorCpf.sql](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_ExisteBeneficiarioPorCpf.sql)
  - DML (entidades):
    - [`FI.AtividadeEntrevista.DML.Cliente`](FI.AtividadeEntrevista/DML/Cliente.cs)
    - [`FI.AtividadeEntrevista.DML.Beneficiario`](FI.AtividadeEntrevista/DML/Beneficiario.cs)

## Funcionalidades Entregues

- Clientes
  - Inclusão, alteração, exclusão e listagem com paginação/ordenação (jTable).
  - Validações:
    - CPF obrigatório e válido no servidor via [`CpfValidator`](FI.AtividadeEntrevista/BLL/Validators/CpfValidator.cs).
    - Regras de negócio encapsuladas em [`BoCliente`](FI.AtividadeEntrevista/BLL/BoCliente.cs).
  - UX:
    - Máscaras (CPF, CEP, etc.) em [`FI.Mascaras.js`](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.Mascaras.js).
    - Diálogo modal de feedback (utilizado em [`FI.Clientes.js`](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.Clientes.js) e [`FI.AltClientes.js`](FI.WebAtividadeEntrevista/Scripts/Clientes/FI.AltClientes.js)).

- Beneficiários (vinculados ao Cliente)
  - Inclusão, alteração, exclusão e listagem no formulário do Cliente (parcial renderizada no Alterar/Incluir).
  - Validações:
    - CPF obrigatório, válido e único por Cliente (suporte em [`FI_SP_ExisteBeneficiarioPorCpf.sql`](FI.AtividadeEntrevista/DAL/Beneficiarios/Procedures/FI_SP_ExisteBeneficiarioPorCpf.sql)).
    - Regras de negócio em [`BoBeneficiario`](FI.AtividadeEntrevista/BLL/BoBeneficiario.cs).

- UI/Front-end
  - jQuery 3.4.1, Bootstrap, jQuery Validate (com unobtrusive), jTable (localização pt-BR e tema Metro Dark Gray).
  - Bundling/Minification configurados em [`BundleConfig`](FI.WebAtividadeEntrevista/App_Start/BundleConfig.cs) com bundles:
    - `~/bundles/clientes`, `~/bundles/listClientes`, `~/bundles/altClientes`, `~/bundles/beneficiarios`, `~/bundles/jtable`, além de jQuery/jQueryUI/Bootstrap.

- Persistência
  - LocalDB (MSSQLLocalDB) usando arquivo MDF em [App_Data/BancoDeDados.mdf](FI.WebAtividadeEntrevista/App_Data/BancoDeDados.mdf).
  - Acesso a dados com ADO.NET + Procedures via [`AcessoDados`](FI.AtividadeEntrevista/DAL/Padrao/FI.AcessoDados.cs).

## Estrutura de Pastas (resumo)

- [FI.WebAtividadeEntrevista](FI.WebAtividadeEntrevista)
  - App_Start, Controllers, Models, Service, Scripts, Views, Content, App_Data
- [FI.AtividadeEntrevista](FI.AtividadeEntrevista)
  - BLL, DAL, DML, Properties

## Requisitos

- Visual Studio 2022
- .NET Framework 4.8
- SQL Server LocalDB (MSSQLLocalDB)
- IIS Express (fornecido pelo VS)

## Principais Telas/Fluxos

- Clientes
  - Listagem com jTable (paginação/ordenção).
  - Inclusão: [Views/Cliente/Incluir.cshtml](FI.WebAtividadeEntrevista/Views/Cliente/Incluir.cshtml) com scripts do bundle `~/bundles/clientes`.
  - Alteração: [Views/Cliente/Alterar.cshtml](FI.WebAtividadeEntrevista/Views/Cliente/Alterar.cshtml) com bundles `~/bundles/altClientes` e `~/bundles/beneficiarios`.
- Beneficiários
  - Gerenciados dentro do formulário do Cliente (parcial e grid, scripts em [Scripts/Beneficiario/FI.Beneficiarios.js](FI.WebAtividadeEntrevista/Scripts/Beneficiario/FI.Beneficiarios.js)).

## Padrões e Boas Práticas

- Separação de camadas (Controller → Service → BLL → DAL → DB).
- Validações de domínio no BLL (ex.: CPF).
- Scripts modulares por contexto (Clientes, Beneficiários, Máscaras).
- Internacionalização do jTable (pt-BR) e tema consistente via bundle CSS.

## Dependências Principais (NuGet)

- ASP.NET MVC 5.2.9, WebPages/Razor 3.2.9
- jQuery 3.4.1, jQuery.Validation/Unobtrusive
- Bootstrap
- jTable
- Newtonsoft.Json
- WebGrease (bundling/minification)
- Microsoft.CodeDom.Providers.DotNetCompilerPlatform (Roslyn para compilação)

## Configuração Visual

- CSS base: [Content/Site.css](FI.WebAtividadeEntrevista/Content/Site.css)
- Tema jTable: [Scripts/jtable/themes/metro/darkgray/jtable.css](FI.WebAtividadeEntrevista/Scripts/jtable/themes/metro/darkgray/jtable.css) (incluído em `~/Content/jtable` no [`BundleConfig`](FI.WebAtividadeEntrevista/App_Start/BundleConfig.cs)).

## Problemas Comuns

- Roslyn (csc.exe) ausente em runtime:
  - Atualizar/reinstalar `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` e rebuild.
- LocalDB:
  - Garantir instalação do SQL Server Express LocalDB (MSSQLLocalDB) e permissões de acesso ao arquivo MDF.

## Próximos Passos (Sugeridos)

- Adicionar testes de unidade para BLL (ex.: `CpfValidator`, regras de negócio de Cliente/Beneficiário).
- Endpoints para APIs JSON dedicadas (otimizando jTable e integração).
- Melhorar tratamento de erros e mensagens (padronização no front e back).
- Atualizar jQuery e Bootstrap para versões suportadas atuais (analisando compatibilidade com jTable).
