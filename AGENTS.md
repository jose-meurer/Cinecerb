# Orientações para desenvolver o Cinecerb

- O nome oficial do projeto é `Cinecerb`. Use essa grafia em nomes técnicos, arquivos e textos da interface. A referência histórica arquivada e sua extração literal preservam a grafia original.

## Colaboração e ritmo de desenvolvimento

- O usuário determina o ritmo, a ordem das etapas e o escopo do desenvolvimento. Trabalhe na etapa solicitada e não antecipe funcionalidades, instalações, estrutura de código ou mudanças de arquitetura sem que façam parte do pedido.
- Desenvolva em conjunto com o usuário, em incrementos pequenos e revisáveis. Explique decisões técnicas relevantes, alterações realizadas, verificações executadas e eventuais limitações.
- Quando uma regra estiver ambígua, incompleta ou gerar dúvida, converse com o usuário antes de implementar a parte dependente dessa definição.
- Nunca decida uma regra de negócio sozinho. Não transforme hipóteses, dados do protótipo ou comportamentos simulados em requisitos. Isso inclui validações, valores padrão, limites, permissões, persistência e fluxos de negócio.
- Apresente alternativas e suas consequências quando necessário, deixando a decisão de negócio com o usuário. Ausência de resposta não significa aprovação.
- Não crie documentação, arquivos de documentação, projetos de teste ou arquivos de teste sem solicitação explícita do usuário. Preserve os documentos e testes existentes, quando houver, e não amplie esse escopo por iniciativa própria.
- Mantenha as orientações de colaboração atualizadas neste `AGENTS.md`. A criação ou ampliação de outros documentos depende de solicitação explícita do usuário.
- Ao executar o aplicativo para inspeção visual ou capturas de tela, use sempre a segunda tela. Não use a tela principal para essa verificação.
- A tela de inspeção é a que fica à esquerda da principal. Preserve o foco do usuário durante as verificações: priorize compilação, análise estática e observação sem ativação. Não mova, ative ou interaja com janelas enquanto o usuário estiver trabalhando, sem combinar isso com ele.

## Direção técnica e plataformas

- A direção técnica definida é .NET MAUI com Blazor Hybrid: C#, componentes Razor (`.razor`), HTML e CSS, com JavaScript quando necessário para integração com APIs da WebView.
- Organize o código por funcionalidades e casos de uso, seguindo Vertical Slice e a estrutura discutida com o usuário. Mantenha interface, estado e comportamento específicos próximos de sua funcionalidade.
- Os namespaces devem corresponder ao caminho das pastas relativo ao projeto, com o prefixo `Cinecerb.Client`. Mantenha as referências C#, Razor e XAML alinhadas. Não distribua partes de uma mesma classe entre pastas que exigiriam namespaces diferentes; separe a integração de plataforma por contratos do framework ou contratos adequados à necessidade real.
- O primeiro alvo de desenvolvimento e distribuição é Windows. Android será disponibilizado no futuro, a partir da mesma base de código.
- Tudo que for específico de qualquer plataforma deve ficar dentro de `Platforms/`, na subpasta correspondente: por exemplo, `Platforms/Windows/` e, futuramente, `Platforms/Android/`. Isso inclui código, recursos, configurações, integrações nativas, customizações visuais e comportamentos exclusivos do sistema. Use a grafia padrão do MAUI: `Platforms`.
- Preserve o código compartilhado independente de APIs e dependências específicas de sistema operacional. Isole o acesso às capacidades nativas por contratos adequados, com implementações na subpasta da respectiva plataforma.
- Evite espalhar condicionais de plataforma, dependências nativas ou decisões exclusivas de um sistema pelos componentes Razor, funcionalidades e serviços compartilhados.
- Prepare os limites necessários para a futura implementação Android, sem implementar suporte Android nem criar abstrações especulativas antes da etapa definida pelo usuário.

## Valores globais e reutilizáveis

- Defina nome, identificador, versão exibida e número de build nos metadados de `src/Cinecerb.Client/Cinecerb.Client.csproj`. Consuma essas informações em C# e Razor por `AppInfo.Current`, sem duplicar seus valores em constantes.
- A versão exibida acordada é `0.1` (`ApplicationDisplayVersion`), com número técnico de build `1` (`ApplicationVersion`). Não confunda o estágio alfa/beta ou a versão exibida com o contador inteiro de build.
- Centralize todas as cores configuráveis em `src/Cinecerb.Client/Shared/DesignSystem/AppColors.xaml`, inclusive cores utilizadas somente por um componente ou plataforma e as cores de fundo/tinta de ícones e splash. Não deixe literais de cor em componentes, telas, classes de plataforma, CSS ou propriedades de build.
- `AppColors.xaml` é um ResourceDictionary MAUI carregado por `App.xaml`. `AppTheme.cs` acessa seus recursos tipados como `Color`, e `ThemeStyles.razor` os disponibiliza como variáveis CSS. Para CSS, converta com `ToRgbaHex(includeAlpha: true)`, preservando a posição correta do canal alfa.
- Consuma `AppTheme` em C#/Razor e `var(--color-...)` no CSS. Antes de adicionar um literal global, procure e reutilize a definição correspondente; não mantenha uma paleta manual paralela no CSS. Use um alias para a classe `AppTheme` quando necessário para evitar conflito com o enum do MAUI.
- Centralize pelo significado e alcance do valor, não apenas por sua igualdade numérica ou textual. Valores de responsabilidades diferentes não devem ser acoplados por coincidirem.
- Valores exclusivos de uma funcionalidade ou componente permanecem próximos de seu uso, exceto cores, que sempre pertencem à paleta central. O código e o comportamento específicos de plataforma continuam em `Platforms/<Plataforma>/`, consumindo a paleta central quando necessário.
- O `.csproj` lê as cores de ícones e splash do mesmo dicionário por `XmlPeek`, tarefa padrão do MSBuild. Os SVGs monocromáticos fornecem a geometria, e a tinta é aplicada pelo build. Não duplique essas cores nos SVGs ou no `.csproj`. Preserve as cores intrínsecas das imagens de marca PNG/ICO e o snapshot histórico.
- Use recursos MAUI, `AppInfo` e a ponte Razor/CSS. Não reintroduza geradores próprios de C#/CSS ou arquivos `.props`/`.targets` para essa centralização sem uma nova decisão explícita do usuário.
- Se houver dúvida sobre o significado, o valor ou o alcance de uma definição, converse com o usuário antes de estabelecê-la ou alterá-la. Centralizar um valor não autoriza decidir uma regra de negócio.

## Armazenamento local e cache

- Use SQLite para armazenamento local e cache da aplicação.
- A biblioteca de acesso ao SQLite e eventual ORM ainda não foram definidos. Não considere a escolha de SQLite como aprovação automática de um pacote ou ORM.
- Mantenha as particularidades de armazenamento de cada plataforma dentro de `Platforms/`, conforme a regra geral de isolamento. O acesso compartilhado ao SQLite deve permanecer independente dessas particularidades.
- Defina com o usuário quais dados serão persistidos ou armazenados em cache e suas regras de validade, expiração, invalidação, atualização e remoção. Não invente essas regras de negócio.

## Abertura do aplicativo

- A composição aprovada é a logo original com 200 px de largura e proporção preservada, com o nome do aplicativo abaixo em Outfit, peso 800, tamanho 60 px, `line-height: 0.75`, `letter-spacing: -0.5px` e `gap: 0` entre os elementos. O conjunto fica centralizado na área de conteúdo.
- A animação atual é “Linha em movimento”: fundo escuro com o brilho radial azulado do antigo Mar de luz, logo e nome estáticos e uma linha ciano/azul abaixo, animada apenas com CSS. Não use ondas animadas, Vanta.js ou Three.js nesta etapa; não há interações com mouse, toque ou giroscópio.
- Durante a inicialização, a página e a WebView devem usar a mesma cor de fundo base da splash, consumindo `Background` da paleta central, inclusive antes da renderização do Blazor, para evitar a piscada branca.
- Exiba apenas o fundo enquanto a splash se prepara. Revele a logo, o nome e a linha juntos após a decodificação da logo, o carregamento da fonte e a preparação do layout, sem espera fixa. Inicie a animação da linha somente nessa revelação.
- Na splash, a região superior mostra apenas os botões nativos da janela sobre o fundo. A titlebar personalizada fica oculta para que o conteúdo se estenda até o topo, sem a marca no canto.
- No Windows, oculte a titlebar no evento `HandlerChanged`, após a criação de seu controle nativo. Definir `IsVisible = false` antes de o handler existir não remove a margem superior na versão atual do MAUI.
- A faixa superior da splash deve preservar o arraste, o duplo clique e o menu de sistema nativos do Windows, respeitando a área dos botões. Configure regiões nativas de legenda; não simule esses comportamentos em Razor ou JavaScript.
- A janela MAUI inicia com `Width="1280"` e `Height="720"`, definidos em `Composition/MainWindow.xaml`. Não calcule automaticamente a proporção nem adapte o tamanho inicial ao monitor. O redimensionamento posterior pelo usuário permanece livre.
- Em uma etapa futura, o JSON de configuração do usuário guardará a última resolução utilizada, que será restaurada nas aberturas seguintes. 1280 × 720 continuará sendo o padrão da primeira abertura. Não implemente essa persistência nesta etapa.
- Nesta etapa, a abertura permanece visível como tela inicial para avaliação. A saída será definida com o usuário quando existir a próxima tela. Não introduza segundos fixos ou carregamento simulado; o comportamento final deve acompanhar o carregamento necessário.

## Qualidade e referências técnicas

- Siga boas práticas de desenvolvimento, Clean Code e SOLID, com nomes claros, responsabilidades coesas, dependências explícitas e baixo acoplamento.
- Aplique esses princípios de forma proporcional à necessidade real. Evite camadas, interfaces, padrões ou dependências que apenas aumentem a complexidade sem benefício concreto.
- Use as recomendações oficiais do Microsoft Learn como referência principal para C#, .NET, MAUI e Blazor: https://learn.microsoft.com/.
- Nas explicações para estudo, priorize links para a documentação do Microsoft Learn. Diferencie o que está documentado do que foi confirmado somente na implementação do framework.
- Consulte a documentação oficial aplicável à versão adotada antes de decisões técnicas que dependam do comportamento do framework. Não presuma APIs, suporte de plataforma ou configurações quando houver dúvida.
- Quando uma recomendação técnica entrar em conflito com uma regra do projeto ou exigir uma decisão de negócio, explique o conflito e converse com o usuário antes de seguir na parte afetada.
- Faça verificações proporcionais à alteração, como restauração de dependências e compilação. Crie testes somente quando o usuário solicitar explicitamente. Informe o que foi efetivamente validado e não declare testes ou compatibilidade de plataforma que não foram verificados.

## Referência visual

Antes de criar ou alterar interface, leia `docs/visual/README.md`, os fundamentos e a especificação da tela envolvida. Use `docs/visual/referencia/prototype/index.html` como referência arquivada e `docs/visual/08-fonte-extraida.md` para consultar declarações exatas.

- Implemente a composição existente; não redesenhe telas por preferência estética.
- Preserve tema escuro, destaque ciano/azul, materiais translúcidos, tipografia, medidas, proporções e estados documentados.
- Reutilize os componentes documentados nas telas novas. Não introduza outra linguagem visual por meio dos padrões de uma biblioteca.
- Diferencie regras extraídas, derivações, recomendações e lacunas. Consulte `docs/visual/05-comportamentos-e-lacunas.md` antes de reproduzir código do protótipo.
- Preserve os valores vermelhos legados na reprodução fiel até existir uma decisão explícita de normalização. O token chamado `--accent-red` contém ciano.
- Corrija falhas funcionais sem tratá-las como requisitos visuais. Não copie autenticação simulada, `alert()` ou dados demonstrativos como funcionalidades prontas.
- Não invente telas, breakpoints ou estados aprovados que não existem na referência. Extensões devem manter a linguagem existente e ser documentadas como novas decisões.
- Verifique a interface usando `docs/visual/06-validacao-e-evolucao.md`. Só declare comparação visual ou teste de navegador quando realmente realizados.
- Mantenha o snapshot de referência intacto. Registre mudanças intencionais de design na documentação e no histórico de decisões.

As instruções explícitas do usuário têm precedência. Mantenha este arquivo alinhado às decisões do usuário; não crie ou amplie outra documentação sem solicitação explícita.
