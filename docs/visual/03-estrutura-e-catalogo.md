# 03 — Estrutura da aplicação e catálogo

Fonte: CSS linhas 59–189 e 616–1187; HTML linhas 1500–1523 e 1680–1997. Localizar os blocos pelos IDs no anexo de fonte.

## Estrutura geral

```text
body: 100vh, coluna, sem rolagem externa
├─ win-titlebar: 42 px
└─ app-container: ocupa a altura restante, linha, overflow hidden
   ├─ sidebar: largura declarada 220 px
   │  ├─ sete itens de navegação
   │  └─ perfil e troca de conta no rodapé
   └─ viewport: flex 1; rolagem vertical própria
      ├─ hero: 520 px
      └─ catálogo: margem superior −30 px
         ├─ Continuar Assistindo
         ├─ Canais em Destaque (TV Ao Vivo)
         └─ Filmes em Alta (Top 10)
```

A barra e a navegação ficam fora da região rolável do catálogo. Não tornar o `body` inteiro rolável na reprodução desktop sem registrar uma alteração intencional.

## Barra da janela

`.win-titlebar`: altura 42 px, padding horizontal 16 px, fundo `rgba(12,12,16,0.95)`, blur 20 px, borda inferior sutil, Flexbox com alinhamento central e `space-between`; z-index 500 e `flex-shrink: 0`.

Marca à esquerda: ícone 28 × 28 px, `contain`, brilho ciano de 8 px alfa 0,85; gap 10 px para `Cinecerb`; texto 13,5 px / 700 / tracking 0,5 px.

Busca declarada com largura 420 px; começa oculta. Não há centragem absoluta: seu posicionamento decorre dos filhos da barra e de `space-between`. Campo 30 px de altura, raio 6 px, fundo branco alfa 0,06, borda sutil, padding esquerdo 34 px / direito 12 px, texto branco de 12,5 px. Ícone 15 × 15 px na esquerda a 10 px, centralizado verticalmente e cor dim.

Placeholder: `Buscar filmes, séries, canais ao vivo (ex: Premiere, HBO, Gladiador)...`. O texto pode ser cortado pela largura; não aumentar a busca apenas para caber integralmente. Foco: fundo branco alfa 0,1, borda ciano e anel de 2 px **vermelho** alfa 0,25. A busca não filtra dados no script.

Controles da janela: três caixas 28 × 28 px, raio 4 px, gap 6 px e margem esquerda do grupo 10 px; símbolos `─`, `□`, `✕`. Cor muted; hover branco sobre fundo branco alfa 0,1. Fechar usa `#c42b1c` no hover. Não há ações reais de janela ligadas a eles.

O CSS também prevê `.win-controls`, `.account-badge` e `.status-dot`, mas não há elementos correspondentes no HTML. O badge teria fonte 12 px, padding 4 × 12 px, raio 20 px, gap 8 px e ponto verde 7 px. Isso é **estilo órfão**, não parte confirmada da composição visível; não adicionar um badge novo automaticamente.

O HTML contém um fechamento `</div>` excedente antes de `</header>`. Preservar a intenção da barra e corrigir a estrutura no produto, sem usar a recuperação do navegador como regra de layout.

## Navegação lateral

Sidebar: largura 220 px, fundo `--bg-surface`, borda direita sutil, padding 16 px vertical / 10 px horizontal, Flexbox vertical com `space-between`, z-index 20. Não há `flex-shrink: 0`; a largura não é garantida em janelas estreitas.

Ordem e rótulos: **Início → TV Ao Vivo → Filmes → Séries → Minha Lista → Downloads → Configurações**. Início começa ativo. O protótipo só muda o estado ativo, sem trocar o conteúdo central.

| Item | Regra |
| --- | --- |
| Lista | Coluna, gap 4 px, sem marcadores |
| Caixa do item | Padding 10 × 14 px, raio 6 px, alinhamento central, gap 14 px |
| Ícone | SVG 19 × 19 px, `fill: currentColor` |
| Texto em repouso | 13,5 px / 500, muted |
| Hover | Branco, fundo branco alfa 0,05 |
| Ativo | Branco / 600, fundo vermelho `rgba(229,9,20,0.12)` |
| Marcador ativo | Borda visual esquerda de 3,5 px; altura 70%; top 15%; ciano; raio `0 4px 4px 0`; brilho ciano 10 px |

Rodapé: borda superior sutil, padding-top 12 px, coluna com gap 8 px. Perfil tem padding 8 px, gap 10 px e raio 6 px; hover branco alfa 0,05. Avatar 32 × 32 px, raio 6 px, gradiente azul `#1e3c72 → #2a5298`, iniciais 13 px / 700. Nome 12,5 px / 600; plano 11 px dim. Nome, iniciais e plano são dados demonstrativos.

`Trocar de Conta`: padding 7 × 10 px, raio 6 px, borda sutil, fundo branco alfa 0,05, texto 11,5 px / 600 muted, gap 6 px e ícone 13 px. Hover: fundo vermelho alfa 0,15, texto `#ff5555`, borda vermelha alfa 0,3. Perfil e botão chamam `returnToLogin`; os limites dessa função estão no documento 05.

## Viewport e rolagem

`flex: 1`, `position: relative`, `overflow-y: auto`, `overflow-x: hidden` e `scroll-behavior: smooth`. Scrollbar WebKit de 8 px, thumb branco alfa 0,15 com raio 4 px. Não existe customização global equivalente para todas as plataformas.

## Hero do catálogo

Altura 520 px; `position: relative`; Flexbox alinhado à base; padding `0 44px 56px`; overflow oculto. Imagem remota centralizada em `center 25%`, `cover`, sem repetição, coberta por dois gradientes.

```css
background:
  linear-gradient(to top,
    var(--bg-base) 0%, rgba(10,10,13,0.8) 35%,
    rgba(10,10,13,0.3) 70%, transparent 100%),
  linear-gradient(to right,
    var(--bg-base) 0%, rgba(10,10,13,0.85) 40%, transparent 80%),
  url(/* ver inventário de assets */) center 25% / cover no-repeat;
```

Conteúdo em coluna, gap 14 px, max-width 620 px e z-index 5. Ordem: metadados → título → sinopse → ações. O conteúdo inicial é `GLADIADOR II`, mas a foto do protótipo é ilustrativa; não confundir com pôster oficial.

Título: Outfit 52 px / 900, uppercase, tracking −1 px, line-height 1; preenchimento de texto com gradiente vertical branco até 40%, cinza `#b0b0b8` a 100%; sombra preta `0 8px 30px` alfa 0,8.

Sinopse: 14 px, line-height 1,6, `#d1d1d8`, no máximo 3 linhas com `-webkit-line-clamp`; sombra preta `0 2px 10px` alfa 0,6.

Metadados em linha, gap 10 px, 12 px / 600; sem `flex-wrap`. Exemplo: `98% Relevante`, `4K ULTRA HD`, `DOLBY 5.1`, `16`, `2h 28m • Ação / Ficção`. Relevância verde `#46d369`; qualidade em fundo branco alfa 0,12, borda branca alfa 0,2, padding 2 × 8 px, raio 4 px e tracking 0,5 px; idade amarela com texto preto, padding 2 × 6 px, raio 4 px e peso 800; duração/gênero muted.

### Ações reutilizáveis

Linha `.hero-actions`: gap 14 px, margin-top 6 px, alinhamento central, sem quebra explícita.

| Propriedade | Primário `.btn-play` | Secundário `.btn-info` |
| --- | --- | --- |
| Padding | 12 × 28 px | 12 × 24 px |
| Fundo | Gradiente ciano/azul | `rgba(100,100,110,0.5)` + blur 10 px |
| Texto | Branco, 14,5 px / 800, tracking 0,3 px | Branco, 14 px / 600 |
| Raio / borda | 6 px / sem borda | 6 px / 1 px glass-border |
| Gap interno | 10 px | 8 px |
| Sombra | `0 6px 24px` ciano alfa 0,45; texto preto | Sem sombra própria |
| Hover | Gradiente de hover, escala 1,04, sombra `0 8px 30px` ciano alfa 0,65 | Fundo `rgba(130,130,140,0.6)`, escala 1,03 |

Ícone do primário 18 × 18 px. Botões do hero: `Assistir Agora` e `Mais Informações`. Alturas não são fixadas nesses dois componentes: derivam de padding, fonte, borda e line-height. Não atribuir automaticamente os 46 px do CTA de login.

## Seções e carrosséis

`.catalog-sections`: padding `0 44px 60px`, margem superior **−30 px**, posição relativa, z-index 10, coluna com gap **38 px**. A primeira seção sobe sobre a parte inferior do hero.

Cabeçalho de seção: Flexbox `space-between`, alinhamento central e margin-bottom 14 px. Título Outfit 20 px / 700 / tracking −0,3 px. A seção de canais inclui ação `Abrir Multiview (4 Telas) ›`, em ciano, 12,5 px / 600.

Fileira: Flexbox horizontal, gap 16 px, overflow-x automático, padding 12 px vertical / 4 px horizontal, scroll suave e scrollbar invisível. Cards não encolhem. As setas chamam scroll de **300 px** por clique, positivo ou negativo; não existe snap nem paginação por quantidade de cards.

Setas: 40 × 80 px, absolutas, verticalmente centralizadas, fundo `rgba(15,15,18,0.85)`, blur 10 px, borda sutil, raio 4 px e z-index 15. Anterior fica `left: -22px`; próxima `right: -22px`. Começam com opacidade 0 e aparecem no hover do contêiner, mesmo quando não há conteúdo adicional. No hover da própria seta, fundo e borda ciano. Não há lógica para desabilitar nas extremidades.

## Card de continuidade

`.card-continue`: width e min-width 280 px, height 160 px, flex-shrink 0, raio 10 px, fundo card, borda sutil, overflow oculto e posição relativa. Imagem em 100% × 100%, `object-fit: cover`, display block.

Overlay permanente: gradiente de baixo para cima preto alfa 0,9 a 0%, preto alfa 0,2 a 60%, transparente a 100%; coluna alinhada ao fundo, padding 12 px. Título 13,5 px / 600 com linha única e reticências; subtítulo 11 px muted. Progresso 100% de largura, altura 3,5 px, trilho branco alfa 0,2, raio 2 px, margem superior 8 px; preenchimento ciano com brilho 8 px.

Hover: `translateY(-6px) scale(1.03)`, borda branca alfa 0,3 e `--shadow-hover`. Clique abre o player diretamente.

Quatro exemplos: The Last of Us 65%; Duna: Parte 2 80%; Breaking Bad 25%; Interestelar 48%. Esses percentuais servem para comparação do baseline e não são regras para os dados reais.

## Card de canal

`.card-channel`: width/min-width 240 px, height 130 px, flex-shrink 0, padding 14 px, raio 10 px, borda sutil, gradiente 135° `#181820 → #121216`. Coluna com `space-between`: cabeçalho com nome do canal e selo ao vivo; base com programa e metadados.

Nome do canal no cabeçalho: 14 px / 800 e cor própria do exemplo. Programa 13 px / 600 com reticências em uma linha. Linha secundária 11 px dim.

Selo `AO VIVO`: texto/borda `#ef4444`, fundo vermelho alfa 0,15, fonte 10,5 px / 800, padding 2 × 7 px, raio 4 px, gap 5 px. Ponto de 6 × 6 px, vermelho e circular; pulsa em 1,5 s entre opacidade 1 / escala 1 e opacidade 0,3 / escala 1,4.

Hover do card: `translateY(-5px) scale(1.03)`, borda vermelha `rgba(229,9,20,0.5)` e sombra vermelha `0 10px 25px` alfa 0,2. Clique abre o player. Quatro cards demonstrativos: Premiere 1, ESPN Extra, HBO 4K e Discovery.

## Card de pôster

`.card-poster`: width/min-width 170 px, height 255 px, flex-shrink 0, fundo card, raio 10 px, borda sutil, posição relativa e overflow oculto. Imagem 100% × 100%, `cover`, display block.

Overlay inicialmente invisível; no hover, opacidade 1. Gradiente para cima: preto alfa 0,92 a 0%, alfa 0,3 a 50% e transparente a 100%; padding 14 px; coluna alinhada à base. Nota amarela 11 px / 700 e margem inferior 4 px. Título branco 13 px / 700 / line-height 1,3.

Hover do card: `translateY(-8px) scale(1.06)`, borda branca alfa 0,35, `--shadow-hover` e z-index 10. Clique abre detalhes. Existem cinco exemplos: Oppenheimer, The Batman, Avatar 2, John Wick 4 e Blade Runner 2049. Apesar do título `Top 10`, o HTML tem apenas cinco pôsteres e não desenha numeração de ranking.
