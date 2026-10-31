# 01 — Fundamentos visuais

## Direção visual

**Extraído:** tema único escuro; superfícies em preto e cinza com leve componente azul; texto branco; hierarquia secundária em cinzas; destaque ciano e azul elétrico; bordas brancas de baixa opacidade; brilho localizado; cantos arredondados; imagens com gradientes de proteção ao texto.

Preservar a densidade de aplicativo desktop: textos operacionais entre 11 e 14,5 px, controles compactos e ações primárias visualmente mais fortes. O login usa mais transparência e profundidade que os cards de catálogo.

## Tokens originais

Fonte: `index.html`, linhas 17–38. O bloco abaixo é literal e deve ser usado como ponto inicial da implementação visual.

```css
:root {
  --bg-base: #0a0a0d;
  --bg-surface: #121216;
  --bg-card: #18181f;
  --bg-card-hover: #22222b;
  --accent-red: #00d2ff;
  --accent-red-hover: #38bdf8;
  --accent-glow: rgba(0, 210, 255, 0.45);
  --accent-blue-deep: #0072ff;
  --text-main: #ffffff;
  --text-muted: #9e9ea7;
  --text-dim: #64646e;
  --border-subtle: rgba(255, 255, 255, 0.08);
  --acrylic-bg: rgba(20, 20, 26, 0.78);
  --glass-border: rgba(255, 255, 255, 0.14);
  --shadow-elevation: 0 12px 32px rgba(0, 0, 0, 0.6);
  --shadow-hover: 0 20px 40px rgba(0, 0, 0, 0.85);
  --radius-sm: 6px;
  --radius-md: 10px;
  --radius-lg: 16px;
  --transition-fluid: all 0.28s cubic-bezier(0.16, 1, 0.3, 1);
}
```

O nome `--accent-red` é legado: seu valor é **ciano**, não vermelho. Não deduzir cores pelo nome. Uma implementação nova pode chamá-lo `accent-primary`, mantendo o valor; essa renomeação semântica não constitui mudança visual. `--bg-card-hover`, `--accent-red-hover`, `--accent-glow`, `--accent-blue-deep`, `--acrylic-bg` e `--shadow-elevation` estão declarados sem referências `var()` no restante do CSS: não presumir sua aplicação automática.

## Cores aplicadas fora dos tokens

| Papel / contexto | Valor extraído |
| --- | --- |
| Gradiente primário | `linear-gradient(135deg, #00d2ff 0%, #0072ff 100%)` |
| Gradiente primário em hover | `linear-gradient(135deg, #38bdf8 0%, #0088ff 100%)` |
| Estado conectado / áudio ativo / buffer | `#10b981` |
| Relevância | `#46d369` |
| Ao vivo / gravação | `#ef4444` |
| Classificação etária | Fundo `#eab308`, texto `#000` |
| Nota de pôster | `#fbbf24` |
| Sinopse do hero | `#d1d1d8` |
| Sinopse do modal | `#ccc` |
| Ícone de campo em repouso | `#7d9cb8` |
| Placeholder do login | `rgba(255,255,255,0.38)` |
| Fechar janela em hover | `#c42b1c` |
| Desconectar em hover | Texto `#ff5555`, fundo vermelho com alfa `0.15` |
| Marca dos canais demonstrativos | Premiere verde; ESPN vermelho; HBO `#a855f7`; Discovery `#3b82f6` |

Há vermelho literal em foco da busca, hover de contas e canais, fundo do item ativo e DVR. Esses valores fazem parte do baseline e estão detalhados no documento 05. Não substituir todos automaticamente por ciano e não ampliar o uso desses resíduos para novos componentes.

## Tipografia

Fontes solicitadas por Google Fonts: **Inter** pesos 300, 400, 500, 600, 700 e 800; **Outfit** pesos 500, 700, 800 e 900. Corpo: `'Inter', sans-serif`. Campos de login: `'Segoe UI Variable Text', 'Segoe UI', sans-serif`.

**Limite da referência:** o reset não define `font: inherit` para `button` e `input`. Vários controles têm tamanho e peso, mas não família explícita; sua fonte final pode vir do navegador. Não declarar que todos usam Inter como fato medido. Para estabilizar isso no produto, definir famílias explicitamente após conferir a referência renderizada e registrar a decisão.

| Uso | Família declarada / herdada | Tamanho | Peso | Detalhes |
| --- | --- | --- | --- | --- |
| Marca na barra | Inter herdada | 13,5 px | 700 | Tracking 0,5 px |
| Marca no login | Outfit | 32 px | 800 | Tracking −0,5 px |
| Subtítulo do login | Inter | 13 px | Padrão | Cinza secundário |
| Aba de login | Sem família explícita | 13 px | 600 | Ícone 15 px |
| Campo de login | Segoe UI Variable Text / Segoe UI | 13,5 px | Padrão | Altura 46 px |
| CTA primário | Sem família explícita | 14,5 px | 800 | Tracking 0,3 px; sombra de texto |
| Item de menu | Inter | 13,5 px | 500 / 600 ativo | Ícone 19 px |
| Título do hero | Outfit | 52 px | 900 | Uppercase; tracking −1 px; line-height 1 |
| Sinopse do hero | Inter | 14 px | Padrão | Line-height 1,6; até 3 linhas |
| Título de seção | Outfit | 20 px | 700 | Tracking −0,3 px |
| Título de continuar assistindo | Inter | 13,5 px | 600 | Linha única e reticências |
| Metadados / badges | Inter | 12 px | 600 | Gap 10 px |
| Título de pôster | Inter | 13 px | 700 | Line-height 1,3 |
| Título do modal | Inter, não Outfit | 32 px | 800 | Margem inferior 8 px |
| Título do player | Inter | 18 px | 700 | Diagnósticos 12 px abaixo |
| Título de episódios | Inter | 16 px | 700 | Margem superior 10 px |
| Microtexto | Inter ou controle nativo | 9,5–12,5 px | Conforme componente | Não uniformizar sem consultar a tela |

Line-height global não foi definido. Exceto valores explícitos, o valor é `normal`, dependente da fonte e do navegador.

## Geometria e espaçamento

`box-sizing: border-box`, `margin: 0` e `padding: 0` são globais. Há suavização `-webkit-font-smoothing: antialiased`. O protótipo também aplica `user-select: none` globalmente; ver os limites no documento 05.

Não há escala de espaçamento formal. Valores recorrentes: **4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 28, 30, 36, 38, 44, 56 e 60 px**. Não arredondar todos a múltiplos de 8: isso altera a referência.

| Medida estrutural | Valor declarado |
| --- | --- |
| Barra da janela | 42 px de altura, sem encolhimento |
| Sidebar | `width: 220px`; pode encolher por Flexbox |
| Card de login | `width: 480px`; altura automática |
| Campo e CTA de login | 46 px de altura |
| Hero | 520 px de altura |
| Recuo lateral de hero / catálogo | 44 px |
| Card de continuidade | 280 × 160 px |
| Card de canal | 240 × 130 px |
| Pôster | 170 × 255 px, proporção 2:3 |
| Modal | 860 px de largura; altura máxima 90vh |
| Imagem do modal | 360 px de altura |
| PiP | 320 × 180 px, proporção 16:9 |

O card de continuidade é aproximadamente 1,75:1; não o converter automaticamente para 16:9. As medidas são do contêiner com borda incluída.

## Materiais, sombras e raios

- Raio **6 px**: campos, botões, itens de navegação e contas salvas.
- Raio **10 px**: continuidade, canais, pôsteres e PiP.
- Raio **16 px**: painel de login e modal.
- Exceções extraídas: badges / miniabas / controles da janela 4 px; avatar salvo 8 px; pills 20 px; controles circulares 50%.
- Bordas: em geral 1 px; PiP e célula Multiview têm 2 px; marcador ativo do menu tem 3,5 px.
- Vidro: login `blur(36px) saturate(190%)`; barra `blur(20px)`; abas/campos e overlay do modal `blur(14px)`; cards salvos, secundários e controles flutuantes `blur(10px)`.
- Sombras de hover devem reforçar elevação. O ciano deve brilhar nas ações, foco e marca; não transformar toda superfície em um painel luminoso.

Os gradientes completos e as quatro camadas de sombra do login estão preservados no documento 08. Reutilizar as expressões exatas, incluindo alfas.

## Camadas

| Contexto | z-index extraído | Observação |
| --- | --- | --- |
| Hero content | 5 | Local ao conteúdo |
| Seções do catálogo | 10 | Margem negativa sobre o hero |
| Setas de carrossel | 15 | Locais à fileira |
| Sidebar | 20 | Estrutura principal |
| Login | 400 | Abaixo da barra de janela |
| Barra da janela / PiP | 500 | Mesmo valor; ordem do DOM também importa |
| Modal de detalhes | 600 | Cobre toda a janela, incluindo barra |
| Player | 700 | Acima do modal e da barra |
| Fundo / vinheta / card do login | 1 / 2 / 10 | Dentro do contexto do login |
| Multiview / controles do player | 5 / 10 | Dentro do player |

Esses números não formam uma escala global simples: transforms, backdrop-filter e posicionamento criam contextos de empilhamento. Preservar a relação visual, não apenas copiar números isolados.

## Movimento

Transição principal: **280 ms**, curva `cubic-bezier(0.16, 1, 0.3, 1)`, aplicada como `all`. Login entra com `cardPop` em 400 ms e sai em 400 ms `ease`. Modal entra em 300 ms. Spinner gira em 800 ms linear; indicador ao vivo pulsa em 1,5 s.

O login mantém o cartão parado e move apenas o cenário. Os hovers dos cards usam elevação e escala específicas; os valores estão nas respectivas fichas. Não adicionar animações automáticas do hero, partículas, zoom contínuo ou autoocultação de controles: nada disso está implementado.

**Lacunas técnicas:** `--transition-fast` e `--transition-bounce` não foram declaradas. Algumas regras usam `all var(--transition-fluid)` apesar de o token já conter `all`, produzindo uma declaração inválida. Ver documento 05 antes de portar o movimento.
