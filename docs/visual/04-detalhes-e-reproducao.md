# 04 — Detalhes, player, Multiview e PiP

Fonte: CSS linhas 1190–1493; HTML de `modalDetails` na linha 2000, `playerOverlay` na linha 2055 e `pipWindow` na linha 2141; JavaScript nas funções de detalhes, reprodução, Multiview e PiP, no bloco iniciado na linha 2148.

## Modal de detalhes

```text
Overlay preto translúcido + blur, janela inteira
└─ Painel central, largura 860 px, altura máxima 90vh
   ├─ Fechar circular, canto superior direito
   ├─ Hero de 360 px
   │  ├─ Imagem e gradiente inferior
   │  └─ Título e metadados na base
   └─ Corpo com padding 30 px e gap 20 px
      ├─ Iniciar Reprodução | Favoritos | Baixar Offline
      ├─ Sinopse
      ├─ Episódios & Temporadas
      └─ Lista demonstrativa de episódios
```

### Overlay e painel

Overlay fixo, `inset: 0`, fundo `rgba(0,0,0,0.85)`, blur 14 px, z-index 600. Fechado usa `display: none`; aberto `display: flex` com centragem nos dois eixos. Ele cobre também a barra da janela.

Painel `.modal-container`: largura declarada 860 px, max-height 90vh, fundo `--bg-surface`, borda sutil, raio 16 px, posição relativa, overflow-y automático; sombra `0 25px 60px rgba(0,0,0,0.9)`. Não há `max-width` responsivo. Entrada `zoomUp` em 300 ms com curva principal: opacidade 0 / escala 0,95 → opacidade 1 / escala 1. Fechamento troca diretamente o display, sem animação de saída.

Fechar: círculo 36 × 36 px, top/right 18 px, fundo preto alfa 0,6, blur 10 px, borda glass-border, branco e z-index local 20. Hover: fundo ciano e rotação de 90°. Clique e Escape fecham; não há fechamento ao clicar no backdrop.

### Hero e corpo

Hero: 360 px de altura, padding 30 px, alinhado à base, imagem centralizada com `cover`; gradiente para cima de `--bg-surface` a 0% até transparente a 60%. `openDetails()` troca `backgroundImage`, mantendo as demais propriedades do CSS.

Título `modalTitle`: 32 px / 800, margem inferior 8 px, fonte Inter herdada. A linha de badges reutiliza o catálogo: nota `★ N (TMDb)` em verde, `4K HDR`, `ÁUDIO 5.1`, classificação `14` e ano `2024`. Só título, sinopse, nota e URL são substituídos por `openDetails`; qualidade, classificação e ano permanecem fixos no protótipo.

Corpo `.modal-details`: padding 30 px, coluna, gap 20 px. A linha de ações reutiliza `.hero-actions`, `.btn-play` e `.btn-info`, incluindo a margem superior de 6 px e gap 14 px. Ordem literal:

1. `Iniciar Reprodução`, primário com ícone de play.
2. `💖 Adicionar aos Favoritos`, secundário.
3. `⬇ Baixar Offline`, secundário.

A linha não tem quebra explícita, e seus textos/paddings podem pressionar a largura disponível. Não afirmar que cabe em todas as janelas sem medir. Os dois secundários só executam `alert()`; não há toast, confirmação interna nem progresso de download desenhado.

Sinopse: `#ccc`, line-height 1,6, sem tamanho explícito próprio; em condições padrão deriva do tamanho do corpo do navegador. Título `Episódios & Temporadas`: 16 px / 700, margin-top 10 px. Não existe seletor visual de temporada, apesar desse rótulo.

### Item de episódio

Lista vertical, gap 12 px e margin-top 10 px. Item em linha, alinhamento central, gap 16 px, padding 12 px, raio 6 px, fundo branco alfa 0,03. Hover: fundo branco alfa 0,08 e `translateX(4px)`.

| Elemento | Regra extraída |
| --- | --- |
| Número | Largura declarada 24 px, fonte 18 px / 800, dim |
| Miniatura | 110 × 65 px, raio 6 px, `object-fit: cover` |
| Bloco de texto | `flex: 1` |
| Título do episódio | 13,5 px / 600 |
| Descrição | 12 px, muted |
| Duração | 12 px / 600, dim |

Há um único episódio demonstrativo, com título de The Last of Us e duração `81 min`, mostrado para qualquer título. O item chama `openPlayer('Episódio 1')` sem fechar o modal; o player cobre o modal e, ao voltar, ele reaparece. Esse fluxo é diferente do CTA principal, que fecha o modal antes de abrir o player. O conteúdo fixo não define um requisito para filmes reais.

## Player

### Contêiner e imagem

`.player-overlay`: fixo, `inset: 0`, fundo preto, z-index 700, inicialmente oculto; aberto como Flexbox vertical com `justify-content: space-between`. Ocupa toda a janela, inclusive a área da titlebar.

`player-video-bg`: imagem estática absoluta, 100% × 100%, `object-fit: cover`, opacidade 0,85. O título do player muda conforme a seleção, mas essa imagem não muda junto. Não há elemento `<video>`, LibVLC ou reprodução de stream implementada no HTML.

### Cabeçalho sobreposto

Padding 24 px vertical / 30 px horizontal; posição relativa, z-index local 10; gradiente para baixo preto alfa 0,85 a 0% → transparente a 100%; Flexbox horizontal com `space-between` e alinhamento central.

Grupo esquerdo: gap 16 px entre voltar e texto. Voltar: círculo 40 × 40 px, fundo branco alfa 0,15, ícone 20 px branco; hover ciano. Título 18 px / 700. Diagnósticos em linha abaixo, margem superior 2 px, gap 12 px, fonte 12 px muted:

- `LibVLC Engine • 1080p 60fps`
- `Bitrate: 8.4 Mbps`
- `● Buffer Estável`, verde `#10b981`

São textos demonstrativos e não uma escolha já tomada de tecnologia, codec ou bitrate.

Grupo direito: gap 12 px, pills `⊞ Multiview (4 Telas)` e `🗗 Picture-in-Picture`.

### Pill reutilizável

Fundo branco alfa 0,12; borda sutil; padding 6 × 14 px; raio 20 px; fonte 12 px / 600 branca; Flexbox centralizado com gap 6 px. Hover ciano no fundo e na borda. Aplicação do token de transição principal.

### Controles inferiores

Padding 30 px; coluna com gap 14 px; posição relativa, z-index 10. Gradiente para cima preto alfa 0,95 a 0%, preto alfa 0,6 a 60% e transparente a 100%. Os controles permanecem visíveis: não há autoocultação ou reação ao movimento do mouse.

Timeline: largura 100%, altura 6 px, trilho branco alfa 0,25, raio 3 px, cursor pointer. Fill inicialmente 42%, fundo ciano e raio 3 px. Pino branco 14 × 14 px, circular, `right: -6px`, `top: -4px` em relação ao fill.

Clique calcula `(clientX - left) / width`, limita o resultado a 0–1 e ajusta apenas a largura visual. Não existe arraste contínuo, tempo real de reprodução nem atualização do relógio.

Linha de ações: Flexbox `space-between`, alinhamento central. À esquerda gap 18 px entre play/pause e relógio. Ícone 28 × 28 px, branco; começa mostrando pause, com `isPlaying = true`. Clique alterna somente o ícone e o booleano. Tempo fixo `48:12 / 2:14:00`, em 13 px muted — não é sincronizado aos 42% da barra.

À direita, gap 14 px: pills `💬 Áudio: PT-BR 5.1`, `🔤 Legendas: PT-BR` e `● Gravar DVR`. DVR usa fundo vermelho alfa 0,2, borda ciano e texto vermelho `#ef4444`. Os três executam alertas; não existe menu de faixas, seleção de legenda ou gravação real. Não há controle de volume, mute ou fullscreen desenhado.

## Multiview

Grade absoluta dentro do player, `inset: 0`, z-index local 5. Duas colunas e duas linhas `1fr`, gap 4 px, padding 4 px e fundo preto. Inicialmente `display: none`; ativo `grid`. Fica acima da imagem única, abaixo de cabeçalho e controles.

Célula: posição relativa, borda 2 px transparente, raio 6 px, overflow oculto, cursor pointer. Imagem em 100% × 100%, `cover`. Célula com áudio ativo: borda verde `#10b981`, sombra `0 0 15px rgba(16,185,129,0.4)`. A primeira começa ativa.

Badge por célula: absoluto top/left 10 px; fundo preto alfa 0,7; padding 4 × 8 px; raio 4 px; fonte branca 11 px / 700. Exemplos: `PREMIERE 1`, `ESPN EXTRA`, `SPORTV 4K`, `DISCOVERY`. A célula selecionada acrescenta ` (Áudio Ativo 🔊)`.

| Estado do botão Multiview | Regra extraída |
| --- | --- |
| Inativo | Pill padrão; `⊞ Multiview (4 Telas)` |
| Ativo | Fundo/borda verde `#10b981`; `✓ Multiview Ativo (4 Telas)` |

O hover ciano da pill continua definido no CSS, mas o fundo e a borda verdes inline do estado ativo têm precedência. Selecionar uma célula remove `.active-audio` das demais e atualiza os badges; não existe roteamento de áudio real.

Atalhos de abertura: ação no cabeçalho da seção de canais abre player com título `Multiview Esportes (4 Canais Ao Vivo)` e liga a grade; pill no player alterna a grade mantendo o título existente. Fechar player desliga Multiview. A seleção da célula não é reinicializada por essa função.

## Picture-in-Picture

Janela flutuante **dentro da página**, não uma janela nativa sobre outros aplicativos. `.pip-float-window`: fixa a 24 px da direita e da base; 320 × 180 px; fundo preto; borda 2 px ciano; raio 10 px; sombra `0 15px 40px rgba(0,0,0,0.8)`; z-index 500; overflow oculto; começa `display: none`.

Imagem 100% × 100%, `cover`, estática e própria do PiP; não acompanha o conteúdo selecionado do player.

| Controle | Geometria / estilo |
| --- | --- |
| Fechar | Top/right 8 px; 24 × 24 px; círculo; fundo preto alfa 0,7; texto branco 12 px |
| Restaurar | Bottom/right 8 px; padding 4 × 8 px; raio 4 px; fundo preto alfa 0,7; texto branco 10 px; rótulo `Maximizar ⛶` |

Não há hover especial, arraste, resize ou persistência de posição desenhados. Ativar PiP fecha player e desliga Multiview antes de mostrar a janela. Fechar PiP a oculta; restaurar abre player com título fixo `Transmissão Recuperada do PiP`.

O modal de detalhes tem z-index maior e pode cobrir o PiP. Escape fecha modal e player, mas não PiP. Os efeitos de camadas e as lacunas de estado estão no documento 05.
