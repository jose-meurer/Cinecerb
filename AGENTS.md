# Orientações para desenvolver o CineCerb

## Colaboração e ritmo de desenvolvimento

- O usuário determina o ritmo, a ordem das etapas e o escopo do desenvolvimento. Trabalhe na etapa solicitada e não antecipe funcionalidades, instalações, estrutura de código ou mudanças de arquitetura sem que façam parte do pedido.
- Desenvolva em conjunto com o usuário, em incrementos pequenos e revisáveis. Explique decisões técnicas relevantes, alterações realizadas, verificações executadas e eventuais limitações.
- Quando uma regra estiver ambígua, incompleta ou gerar dúvida, converse com o usuário antes de implementar a parte dependente dessa definição.
- Nunca decida uma regra de negócio sozinho. Não transforme hipóteses, dados do protótipo ou comportamentos simulados em requisitos. Isso inclui validações, valores padrão, limites, permissões, persistência e fluxos de negócio.
- Apresente alternativas e suas consequências quando necessário, deixando a decisão de negócio com o usuário. Ausência de resposta não significa aprovação.
- Não crie documentação, arquivos de documentação, projetos de teste ou arquivos de teste sem solicitação explícita do usuário. Preserve os documentos e testes existentes, quando houver, e não amplie esse escopo por iniciativa própria.
- Mantenha as orientações de colaboração atualizadas neste `AGENTS.md`. A criação ou ampliação de outros documentos depende de solicitação explícita do usuário.

## Direção técnica e plataformas

- A direção técnica definida é .NET MAUI com Blazor Hybrid: C#, componentes Razor (`.razor`), HTML e CSS, com JavaScript quando necessário para integração com APIs da WebView.
- Organize o código por funcionalidades e casos de uso, seguindo Vertical Slice e a estrutura discutida com o usuário. Mantenha interface, estado e comportamento específicos próximos de sua funcionalidade.
- O primeiro alvo de desenvolvimento e distribuição é Windows. Android será disponibilizado no futuro, a partir da mesma base de código.
- Tudo que for específico de qualquer plataforma deve ficar dentro de `Platforms/`, na subpasta correspondente: por exemplo, `Platforms/Windows/` e, futuramente, `Platforms/Android/`. Isso inclui código, recursos, configurações, integrações nativas, customizações visuais e comportamentos exclusivos do sistema. Use a grafia padrão do MAUI: `Platforms`.
- Preserve o código compartilhado independente de APIs e dependências específicas de sistema operacional. Isole o acesso às capacidades nativas por contratos adequados, com implementações na subpasta da respectiva plataforma.
- Evite espalhar condicionais de plataforma, dependências nativas ou decisões exclusivas de um sistema pelos componentes Razor, funcionalidades e serviços compartilhados.
- Prepare os limites necessários para a futura implementação Android, sem implementar suporte Android nem criar abstrações especulativas antes da etapa definida pelo usuário.

## Armazenamento local e cache

- Use SQLite para armazenamento local e cache da aplicação.
- A biblioteca de acesso ao SQLite e eventual ORM ainda não foram definidos. Não considere a escolha de SQLite como aprovação automática de um pacote ou ORM.
- Mantenha as particularidades de armazenamento de cada plataforma dentro de `Platforms/`, conforme a regra geral de isolamento. O acesso compartilhado ao SQLite deve permanecer independente dessas particularidades.
- Defina com o usuário quais dados serão persistidos ou armazenados em cache e suas regras de validade, expiração, invalidação, atualização e remoção. Não invente essas regras de negócio.

## Qualidade e referências técnicas

- Siga boas práticas de desenvolvimento, Clean Code e SOLID, com nomes claros, responsabilidades coesas, dependências explícitas e baixo acoplamento.
- Aplique esses princípios de forma proporcional à necessidade real. Evite camadas, interfaces, padrões ou dependências que apenas aumentem a complexidade sem benefício concreto.
- Use as recomendações oficiais do Microsoft Learn como referência principal para C#, .NET, MAUI e Blazor: https://learn.microsoft.com/.
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
