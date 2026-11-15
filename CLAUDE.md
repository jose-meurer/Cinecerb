# Cinecerb

Cliente IPTV em .NET MAUI + Blazor Hybrid. Windows é o primeiro alvo; Android virá depois, pela mesma base.

## Escopo deste arquivo

- Contém apenas regras duradouras de arquitetura, padrões e comportamento que valem para todo o projeto.
- Não registre aqui detalhes de uma tela, componente ou funcionalidade (campos, medidas, tempos, mensagens, fluxos, arquivos específicos). Eles ficam próximos da implementação.
- Atualize este arquivo quando uma orientação global mudar. As instruções explícitas do usuário têm precedência.

## Comandos

- Compilar: `dotnet build src/Cinecerb.Client/Cinecerb.Client.csproj -f net10.0-windows10.0.19041.0` (apenas quando o usuário pedir)
- Executar: `dotnet run --project src/Cinecerb.Client -f net10.0-windows10.0.19041.0` (apenas quando combinado — ver "Inspeção visual")
- Migrations EF Core são criadas e executadas apenas pelo usuário (ver "Armazenamento"). Referência: `dotnet ef migrations add <Nome> --project src/Cinecerb.Client --context AppDbContext --output-dir Shared/Persistence/Migrations`

## Estrutura

- `Features/<Funcionalidade>/` — fatias verticais: interface, estado, serviços e persistência de cada funcionalidade.
- `Composition/` — composição da aplicação (janela principal, roteamento).
- `Shared/` — o que é reutilizado entre funcionalidades, incluindo o design system.
- `Platforms/<Plataforma>/` — tudo que é específico de sistema operacional.
- `docs/` — referências do projeto.

## Colaboração

- O usuário determina o ritmo, a ordem das etapas e o escopo. Trabalhe em incrementos pequenos e revisáveis, sem antecipar funcionalidades, instalações ou mudanças de arquitetura.
- Explique decisões técnicas relevantes, alterações, verificações realizadas e limitações.
- Não decida regras de negócio sozinho nem transforme hipóteses, dados demonstrativos ou comportamentos simulados em requisitos. Isso inclui validações, padrões, limites, permissões, persistência e fluxos.
- Sempre que algo não estiver claro ou faltar uma definição, pergunte ao usuário antes de implementar a parte dependente, apresentando alternativas quando houver. Não presuma intenção, escopo ou regra. Ausência de resposta não significa aprovação.
- Não crie nem amplie documentação, projetos de teste ou arquivos de teste sem pedido explícito. Preserve os existentes.

## Inspeção visual

- Preserve o foco do usuário. Inspeções visuais e capturas do aplicativo usam a segunda tela, à esquerda da principal.
- Não mova, ative ou interaja com janelas sem combinar antes. Priorize compilação, análise estática e observação sem ativação.

## Arquitetura

- O nome oficial é `Cinecerb`, inclusive no assembly e no executável de distribuição.
- Use .NET MAUI com Blazor Hybrid: C#, Razor, HTML e CSS, com JavaScript apenas quando necessário para integrar APIs da WebView.
- Organize por funcionalidades e casos de uso (Vertical Slice). Mantenha interface, estado e comportamento específicos próximos de sua funcionalidade.
- Namespaces refletem o caminho das pastas a partir de `Cinecerb.Client`. Mantenha C#, Razor e XAML alinhados e não distribua uma mesma classe entre pastas que exigiriam namespaces diferentes.
- Não implemente suporte ou abstrações especulativas para plataformas futuras antes da etapa definida pelo usuário.
- Todo código, recurso, configuração e comportamento específico de sistema operacional fica em `Platforms/<Plataforma>/`. O código compartilhado não usa APIs nativas nem condicionais de plataforma (`#if WINDOWS` e similares); a integração é isolada por contratos necessários à aplicação.

## Metadados e valores reutilizáveis

- Metadados da aplicação (nome, identificador, versão, build, assembly) ficam no `.csproj` e são consumidos por `AppInfo.Current`, sem duplicar em constantes.
- Todas as cores ficam no dicionário central do design system (`Shared/DesignSystem/AppColors.xaml`). Nenhum literal de cor ou paleta paralela em C#, Razor, CSS ou propriedades de build; consuma por `AppTheme` em C#/Razor e por `var(--color-...)` no CSS.
- O Bootstrap (somente CSS) tem suas variáveis globais mapeadas ao design system em `wwwroot/css/app.css`. Ao usar um componente ou utilitário do Bootstrap pela primeira vez, mapeie as variáveis `--bs-*` dele para `var(--color-...)`; sem equivalente no design system, pergunte antes de criar cor.
- Reutilize definições existentes pelo significado, sem acoplar responsabilidades só porque os valores coincidem.
- Valores exclusivos de uma funcionalidade ficam próximos do uso, exceto cores.
- Não introduza geradores próprios ou mecanismos paralelos de centralização sem decisão do usuário.

## Armazenamento

- SQLite com Entity Framework Core para armazenamento local e cache.
- Um único `AppDbContext` em `Shared/Persistence/`, com uma única pasta de migrations em `Shared/Persistence/Migrations/`. Cada funcionalidade mantém suas entidades e o mapeamento (`IEntityTypeConfiguration<T>`) em `Features/<Funcionalidade>/Persistence/`, registrado explicitamente no `AppDbContext` com `ApplyConfiguration`, sem `ApplyConfigurationsFromAssembly`.
- Projeto único: contexto, migrations e o `IDesignTimeDbContextFactory` (SQLite em memória, usado só pelo `dotnet ef`) ficam no projeto do app, em `Shared/Persistence/`. Não separar em projetos de dados ou de migrations sem decisão do usuário.
- Nunca crie migrations, nem escrevendo os arquivos à mão nem por ferramenta (`dotnet ef migrations add` ou equivalentes). Também não edite, aplique, reverta ou remova migrations (`migrations remove`, `database update` ou equivalentes). Quem cuida disso é sempre o usuário.
- Quando uma alteração de modelo exigir migration, informe o que mudou no modelo e aguarde o usuário criá-la.
- Comandos somente de leitura, como `dotnet ef migrations list` e `dotnet ef migrations has-pending-model-changes`, podem ser usados para verificação.
- No Windows, a raiz de dados e cache é `%LOCALAPPDATA%\Cinecerb`, sem subpastas de fabricante ou identificador de pacote. Os caminhos são definidos na camada de plataforma.
- O acesso compartilhado ao banco é independente das particularidades de armazenamento de cada plataforma.
- Combine com o usuário quais dados persistir ou guardar em cache e suas regras de validade, expiração, atualização, invalidação, proteção e remoção.

## Qualidade

- Clean Code e SOLID de forma proporcional: nomes claros, responsabilidades coesas, dependências explícitas e baixo acoplamento. Evite camadas, interfaces, padrões ou pacotes sem benefício concreto.
- Validações proporcionais às regras acordadas e aos contratos usados. Não acrescente restrições arbitrárias nem validações defensivas desnecessárias (checagens redundantes, casos que o contrato já impede).
- O projeto é pequeno: prefira a solução mais simples e direta. Sem excesso de abstração — nada de componentes, enums, interfaces ou arquivos extras quando uma classe simples (por exemplo, constantes) resolve.
- Mensagens ao usuário (erros, avisos e status) usam português do dia a dia, casual e acolhedor. O aplicativo fala com o usuário em primeira pessoa, quebrando a quarta parede, sem jargão técnico nem detalhes internos: diz o que aconteceu e o que fazer, em frases curtas. Detalhes técnicos vão para o log.
- Se uma recomendação técnica conflitar com uma regra do projeto ou exigir decisão de negócio, explique o conflito antes de seguir na parte afetada.
- Verificações proporcionais à alteração; informe o que foi efetivamente validado. Não declare inspeção visual, testes ou compatibilidade de plataforma sem realizá-los.

## Referências técnicas

- O Microsoft Learn é a referência principal para C#, .NET, MAUI, Blazor e EF Core.
- Sempre consulte o MCP do Microsoft Learn quando houver dúvida sobre boas práticas ou antes de decisões que dependem do framework, considerando a versão adotada (.NET 10). Não responda nem implemente essas questões apenas de memória.
- Priorize links oficiais nas explicações e diferencie comportamento documentado de comportamento confirmado apenas na implementação do framework.

## Interface

- Antes de criar ou alterar interface, consulte a referência visual em `docs/visual/` (ponto de entrada: `README.md`).
- Preserve a identidade visual aprovada. Não redesenhe por preferência estética nem introduza a linguagem padrão de outra biblioteca.
- Reutilize os componentes existentes do design system.
- Use as classes do Bootstrap ao máximo (layout, espaçamento, tipografia e componentes), aceitando a escala dele. CSS próprio só para o que o Bootstrap não oferece, como efeitos da identidade visual e medidas sem equivalente.
- Ícones vêm do Bootstrap Icons (classes `bi`, arquivos locais em `wwwroot/lib/bootstrap-icons`). Ícones decorativos levam `aria-hidden="true"`.
- Textos da interface não são selecionáveis; apenas os campos em que o usuário digita permitem seleção.
- Não invente telas, breakpoints ou estados aprovados; mudanças intencionais dependem de decisão do usuário.
- A cópia arquivada do protótipo em `docs/visual/referencia/` é histórica e permanece intacta.
