# FI.WebAtividadeEntrevista

## Visão geral
Aplicação ASP.NET MVC (.NET Framework 4.8) com projeto Web e projeto de lógica de negócio/dados.

- Solução: `FI.WebAtividadeEntrevista.sln`
- Web: `FI.WebAtividadeEntrevista`
- Lib: `FI.AtividadeEntrevista`

## Requisitos
- Visual Studio 2022
- .NET Framework 4.8
- SQL Server LocalDB (MSSQLLocalDB)
- IIS Express (VS)

## Como rodar
1. Restaurar pacotes NuGet.
2. Confirmar connectionString do LocalDB.
3. Buildar a solução.
4. Rodar via IIS Express no Visual Studio.

## Pacotes e restauração
- Via Visual Studio: Restore NuGet Packages na solução.
- Via CLI: `nuget restore FI.WebAtividadeEntrevista.sln`

## Conexão com banco (LocalDB)
- Configurada em `FI.WebAtividadeEntrevista/Web.config` (name: `BancoDeDados`) apontando para `App_Data\BancoDeDados.mdf`.

## Branches e commits
- Branch principal: `main` (protegida)
- Branches: `feature/*`, `fix/*`, `chore/*`
- Commits: Conventional Commits

## Problemas comuns
- Falta do Roslyn (csc.exe) em runtime: atualizar/reinstalar `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` e rebuild.