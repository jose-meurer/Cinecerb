# 05 — Comportamentos, responsividade e lacunas

Este documento separa o que o protótipo faz do que o produto real ainda precisa definir. As falhas abaixo foram identificadas por análise estática; não são resultados de testes de navegador.

## Estado inicial

- Login visível, aba Xtream ativa, M3U oculta.
- Toggle de salvar credenciais marcado e campos com exemplos.
- Busca da titlebar oculta.
- Catálogo já presente por trás do login e Início marcado como ativo.
- Modal, player, Multiview e PiP ocultos.
- Primeiro canal do Multiview marcado com áudio ativo, mesmo antes da grade ficar visível.
- Estado de reprodução simulado começa em `true`; timeline em 42% e relógio fixo.

## Contrato de interação extraído

| Origem / ação | Resultado no código | Limite |
| --- | --- | --- |
| Aba Xtream / M3U | Troca active e display do formulário | Não limpa os dados do outro modo |
| Olho da senha | Alterna tipo do input e SVG | Só existe no modo Xtream |
| Toggle de credenciais | Muda checkbox e aparência | Não persiste nem altera o login |
| CTA conectar | Loading; aguarda 1200 ms; remove visualmente login e mostra busca | Simulação; erro ao acessar accountBadge ao final |
| Conta salva | Tenta atualizar accountLabel e chamar login | Falha antes do login por ID inexistente |
| Adicionar conta | Limpa campos Xtream e foca nome | Não troca aba nem modifica M3U |
| Perfil / Trocar de Conta | Volta login e oculta busca | Falha em accountBadge antes de fechar modal e player |
| Item lateral | Move classe active | Não navega para outra página |
| Busca | Permite digitar | Sem filtro, resultado ou atalho |
| Setas de carrossel | Rolam ±300 px suavemente | Sem snap, disable de extremos ou ocultação por overflow |
| Continuar assistindo / canal / Assistir Agora | Abre player com título | Imagem e dados de reprodução fixos |
| Mais Informações / pôster | Preenche e abre modal | Ano/qualidade/episódio continuam fixos |
| Fechar detalhes / Escape | Oculta detalhes | Sem clique no backdrop ou animação de saída |
| Iniciar Reprodução no modal | Fecha detalhes e abre player com título atual | Não seleciona mídia real |
| Episódio | Abre player acima dos detalhes | Modal permanece montado e visível atrás |
| Favoritar / baixar / áudio / legenda / DVR | Alertas nativos | Sem layout próprio ou alteração real |
| Voltar do player | Fecha player e desliga Multiview | Não redefine isPlaying, progresso nem áudio selecionado |
| Play/pause | Alterna SVG e booleano | Não controla vídeo |
| Timeline | Ajusta largura do fill pelo clique | Não arrasta nem atualiza relógio |
| Multiview | Alterna grade e estilo/texto da pill | Sem streams reais |
| Célula Multiview | Troca borda e indicação de áudio ativo | Sem áudio real |
| Ativar PiP | Fecha player e mostra PiP | Imagem fixa; fecha Multiview |
| Restaurar PiP | Fecha PiP e abre player | Troca título para texto fixo |
| Fechar PiP | Oculta PiP | Escape não o fecha |

A implementação real deve manter os pontos de entrada e a linguagem visual, substituindo as simulações por estados vinculados a dados reais. O tempo artificial de 1200 ms, nomes de filmes e mensagens de `alert` não são regras do produto.

## Inventário de estados por componente

| Componente | Estados desenhados no CSS/HTML | Ausentes ou incompletos |
| --- | --- | --- |
| Aba | Inativa / ativa | Foco de teclado explícito, disabled |
| Campo login | Normal / placeholder / hover / foco | Erro, validação, disabled, mensagem auxiliar |
| CTA login | Normal / hover / active / loading com disabled lógico | Estilo disabled específico, falha e retry |
| Toggle | Desligado / ligado | Focus-visible e persistência |
| Card salvo | Normal / hover | Foco, remover, editar, conta inválida |
| Busca | Normal / foco | Loading, resultados, vazio, erro, limpar |
| Navegação | Normal / hover / ativo | Páginas de destino e foco explícito |
| Cards de catálogo | Normal / hover; progresso ou live quando aplicável | Imagem quebrada, loading, indisponível, teclado |
| Modal | Fechado / aberto / entrada | Gestão de foco e estados de dados |
| Player | Aberto / fechado; play/pause visual | Buffer real, erro, volume, fullscreen, autoocultação |
| Multiview | Oculto / ativo / áudio selecionado | Adicionar canal, stream indisponível, grade diferente |
| PiP | Oculto / visível | Arraste, resize, comportamento nativo |

Não atribuir a esses estados ausentes uma aparência aprovada. Se necessários na implementação, desenhá-los a partir dos componentes existentes e registrar que são extensões.

## Responsividade: o que existe

**Extraído:** há meta viewport, Flexbox, algumas larguras em porcentagem, imagens com `cover`/`contain`, rolagem horizontal nos carrosséis e limite de altura do modal em `vh`.

**Não há nenhuma media query ou container query.** Não existe breakpoint de celular/tablet, sidebar recolhível, navegação inferior ou menu hambúrguer. A referência é voltada a desktop Windows. Não inventar suporte mobile aprovado.

| Restrição | Consequência possível, ainda não medida em navegador |
| --- | --- |
| Busca 420 px + marca + controles | Pode pressionar a barra em larguras pequenas |
| Sidebar 220 px sem flex-shrink 0 | Pode encolher em vez de permanecer fixa |
| Card de login 480 px, área com padding 20 px | Pode encolher horizontalmente; textos/controles impõem mínimos |
| Login sem rolagem e body com overflow hidden | Card pode ser cortado em janelas baixas |
| Modal 860 px sem max-width | Pode ultrapassar janelas estreitas |
| Metadados e ações sem flex-wrap | Podem ultrapassar ou comprimir a composição |
| Hero fixo de 520 px e conteúdo max-width 620 px | Ocupa grande parte de janelas baixas |
| Player com grupos em linha | Ações e diagnósticos podem se sobrepor ou cortar |
| PiP fixo 320 × 180 px | Pode ocupar grande parte de uma janela pequena |

**Recomendado para implementação futura:** preservar as medidas em janelas desktop com espaço suficiente; medir limites reais antes de escolher uma largura mínima ou breakpoints. Se for preciso suportar janelas menores, usar as mesmas cores, componentes e hierarquia, documentando as adaptações. `max-width`, quebra de ações e rolagem segura são soluções candidatas, não regras já extraídas.

## Inconsistências de cor

O destaque principal já é ciano, mas ficaram valores vermelhos literais em contextos de interação:

| Seletor / local | Valor original |
| --- | --- |
| `.win-search-input:focus` | Anel `rgba(229,9,20,0.25)` |
| `.saved-account-card:hover` | Borda `rgba(229,9,20,0.5)` |
| `.nav-item.active` | Fundo `rgba(229,9,20,0.12)` |
| `.card-channel:hover` | Borda `rgba(229,9,20,0.5)` e sombra alfa 0,2 |
| `.btn-disconnect:hover` | Fundo vermelho alfa 0,15; borda alfa 0,3; texto `#ff5555` |
| Pill DVR inline | Fundo vermelho alfa 0,2 e texto `#ef4444`, borda ciano |

Os vermelhos de **ao vivo, gravação e fechar janela** têm papel semântico próprio; não são automaticamente o mesmo problema dos resíduos de destaque. A origem exata desses resíduos não pode ser confirmada apenas pelo código.

**Regra documental:** preservar os valores do baseline na reprodução fiel. Uma normalização para ciano exige uma decisão de design registrada, incluindo quais seletores mudaram. Nenhuma normalização foi aplicada nesta entrega.

## Falhas estruturais e funcionais

### IDs usados e ausentes

`accountLabel` e `accountBadge` são referenciados pelo script, mas não existem no HTML:

- `quickLogin()` tenta acessar `accountLabel.textContent` e falha antes de chamar `simulateLogin()`.
- `simulateLogin()` executa a maior parte da transição, inclusive ocultar login e mostrar busca, antes de falhar em `accountBadge.style`.
- `returnToLogin()` mostra login e oculta busca, mas falha em `accountBadge.style` antes de `closePlayer()` e `closeDetails()`.

Assim, não descrever login rápido ou troca de conta como fluxos já validados. Corrigir as referências e decidir se o badge deve existir é parte da implementação futura; o CSS órfão não basta para afirmar sua presença no layout.

### Transições inválidas ou indefinidas

- `--transition-fast` e `--transition-bounce` são usados e não declarados: logo do login, ícones e botão do olho não têm duração válida por essas regras.
- `.input-field` declara `transition: all var(--transition-fluid)`. Como o token já é `all 0.28s ...`, a expansão duplica `all` e a declaração é inválida.

**Recomendado:** ao portar, usar `transition: var(--transition-fluid)` onde se quer a transição principal, ou separar tokens de propriedade/duração/curva. Para os tokens ausentes, reutilizar 280 ms da curva principal como escolha provisória claramente registrada, sem alegar duração extraída. Não introduzir uma curva de bounce arbitrária.

### HTML e estado de reprodução

- Há fechamento `</div>` excedente no cabeçalho.
- Nenhuma função de autenticação consome os campos ou chama servidor.
- Não há localStorage, gravação de credenciais, banco de dados ou persistência de favoritos.
- `isPlaying` e a largura da timeline não são reinicializados ao abrir outro título.
- O modal mantém conteúdo de episódio demonstrativo para todos os filmes.
- A nota no pôster diz IMDb; a mesma nota aparece como TMDb ao abrir detalhes, sem conversão.
- PiP usa título e imagem fixos; não conserva o item selecionado.
- Escape fecha detalhes e player na mesma chamada, mesmo que ambos estejam abertos; não fecha PiP.

Esses comportamentos não devem ser congelados como regras de negócio. O objetivo de fidelidade é o desenho e a interação visual comprovada, com correções funcionais documentadas.

## Acessibilidade e operação por teclado

**Extraído:** muitos elementos clicáveis são `div`, `li` ou `span`, sem semântica de botão; campos dependem de placeholder; não há tratamento explícito de foco do modal, trap de teclado ou restauração de foco; há `outline: none` em campos e foco customizado só em alguns controles. `user-select: none` é global. Não existe regra `prefers-reduced-motion`.

**Recomendado para o produto:** usar botões e links sem alterar sua aparência, rótulos acessíveis, estados ARIA de abas/toggle, foco visível coerente com o anel ciano e gestão correta do modal. Permitir seleção nos inputs e em textos quando útil. Oferecer redução de movimento para parallax, escalas e pulse. Essas melhorias não exigem um redesign, mas sua implementação ainda precisa ser verificada.

Não há auditoria de contraste nem declaração de conformidade de acessibilidade nesta documentação. Especial atenção futura aos microtextos dim e aos controles sobre imagens.

## Decisões ainda abertas

| ID | Tema | Situação |
| --- | --- | --- |
| P-01 | Normalizar resíduos vermelhos de interação? | Pendente; baseline preservado |
| P-02 | Badge de conta na barra deve existir? | Pendente; só CSS/script órfãos |
| P-03 | Família explícita dos controles nativos | Pendente de comparação renderizada |
| P-04 | Tamanho mínimo de janela e adaptações | Não definido |
| P-05 | Estados de erro, vazio, loading de catálogo e imagem indisponível | Não desenhados |
| P-06 | Páginas laterais além de Início | Não desenhadas |
| P-07 | Comportamento real de PiP / Multiview / modal de filmes e séries | Parcialmente simulado |
| P-08 | Distribuição offline de fontes e imagens remotas | Não implementada |

## Histórico de decisões

| Data | Registro |
| --- | --- |
| 31/10/2026 | Baseline documentado por análise estática; HTML e assets arquivados sem modificação. Não houve redesign, normalização de cor, definição de breakpoint ou escolha de stack. |

Novas decisões devem registrar contexto, telas afetadas, diferença visual e evidência de comparação. Não substituir o histórico do baseline.
