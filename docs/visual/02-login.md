# 02 — Login e conexão

Fonte: CSS linhas 194–611; HTML linhas 1528–1675; funções `switchLoginMode`, `togglePassVisibility`, `clearLoginForm`, `quickLogin` e `simulateLogin`.

## Composição

```text
Barra da janela — 42 px
└─ Área de login: restante da janela
   ├─ Cenário sci-fi, ampliado, com parallax
   ├─ Vinheta radial + gradiente vertical
   └─ Cartão acrílico centralizado — largura declarada 480 px
      ├─ Marca: ícone + Cinecerb
      ├─ Subtítulo
      ├─ Abas Xtream / M3U
      ├─ Formulário ativo
      ├─ Texto de conexão automática + toggle
      ├─ Entrar e Sincronizar Catálogo
      └─ Contas salvas: três colunas
```

## Área de fundo

`.login-screen`: `position: absolute; inset: 42px 0 0 0`, Flexbox centralizado nos dois eixos, padding 20 px, overflow oculto, z-index 400 e perspective 1200 px. O catálogo já existe atrás dessa camada desde o carregamento; não há uma segunda montagem da aplicação após o login.

`.login-backdrop-layer`: `inset: -60px`, imagem local `login_backdrop.jpg`, centralizada com `cover`, escala inicial 1,06. A imagem mostra um personagem à esquerda e uma cidade futurista sob céu espacial; preservar o enquadramento central e o escurecimento, sem substituir por um fundo genérico. A vinheta fica acima da imagem e não intercepta o ponteiro.

```css
/* Vinheta literal */
background:
  radial-gradient(circle at center,
    rgba(10, 10, 14, 0.48) 0%,
    rgba(8, 8, 12, 0.85) 60%,
    rgba(4, 4, 6, 0.98) 100%),
  linear-gradient(to bottom,
    rgba(4, 4, 6, 0.75) 0%, transparent 25%,
    transparent 75%, rgba(4, 4, 6, 0.95) 100%);
```

Parallax extraído: normalizar o ponteiro em relação ao retângulo do login entre −0,5 e 0,5; aplicar `scale(1.08) translate(-x*36px, -y*36px)`, deslocamento aproximado de ±18 px por eixo. Ao sair, retornar a `scale(1.06) translate(0px,0px)`. Transição do fundo: 120 ms, curva `(0.1,0.4,0.2,1)`. O card não acompanha o ponteiro.

## Cartão acrílico

| Propriedade | Regra extraída |
| --- | --- |
| Largura | 480 px declarados; sem `max-width` explícito |
| Altura | Automática, acompanha o formulário ativo |
| Padding | 38 px topo; 36 px laterais; 32 px base |
| Espaço entre filhos diretos | 20 px |
| Fundo | `rgba(16,16,22,0.5)` |
| Vidro | `blur(36px) saturate(190%)`, incluindo prefixo WebKit |
| Borda | 1 px `rgba(255,255,255,0.18)` |
| Raio | 16 px |
| Organização | Flex vertical; posição relativa; z-index local 10 |

```css
box-shadow:
  0 30px 80px rgba(0, 0, 0, 0.604),
  0 0 35px rgba(9, 148, 229, 0.219),
  inset 0 1px 1px rgba(255, 255, 255, 0.32),
  inset 0 -1px 1px rgba(0, 0, 0, 0.5);
```

**Derivado:** com largura efetiva de 480 px e borda de 1 px, a área interna horizontal é 406 px. Não fixar a altura do painel: o modo M3U tem um campo a menos. A altura final também depende das fontes.

Animação `cardPop`: de opacidade 0, `translateY(20px) scale(0.96)` até opacidade 1, `translateY(0) scale(1)`, em 400 ms com curva principal.

## Cabeçalho e marca

Header vertical centralizado, gap 8 px. Marca em linha, gap 12 px. Ícone dentro de caixa 92 × 92 px, sem encolher; imagem com `object-fit: contain`. O PNG é vertical (765 × 1024), portanto a caixa quadrada não implica esticar o desenho.

Brilho do ícone: `drop-shadow(0 0 24px rgba(0,210,255,0.75))` mais sombra preta `0 8px 16px` alfa 0,6. No hover: escala 1,07, brilho 35 px alfa 0,95 e sombra `0 12px 24px` alfa 0,8. A duração está indefinida no código por tokens ausentes.

Texto `Cinecerb`: Outfit 32 px / 800 / tracking −0,5 px / branco. Subtítulo: `Conecte sua assinatura IPTV para carregar o catálogo`, 13 px e `--text-muted`.

## Abas de modo

Contêiner `.tab-switcher`: Flexbox, padding 4 px, gap 4 px, fundo preto alfa 0,45, blur 14 px, borda branca alfa 0,10, raio 6 px e sombra interna `0 1px 3px` preta alfa 0,4.

Cada `.tab-btn` ocupa uma fração igual (`flex: 1`), padding 8 × 12 px, raio 4 px, gap ícone/texto 6 px; texto 13 px / 600. Ícones inline 15 × 15 px. Inativa transparente e cinza; ativa branca com fundo branco alfa 0,14 e sombras `0 2px 8px` preta alfa 0,4 + inset branco alfa 0,25.

Rótulos: `Xtream Codes API` e `Lista M3U / EPG`. Xtream é ativo inicialmente. Troca altera `.active` e alterna o `display` dos formulários entre `flex` e `none`, sem animação de altura ou slide.

## Formulários

`.login-form`: coluna com gap 13 px. Cada grupo posiciona o ícone absolutamente à esquerda e o campo ocupa 100% da largura.

| Xtream, nesta ordem | Identificador |
| --- | --- |
| Nome do Servidor (ex: Servidor Sala) | `iptvName` |
| URL do Servidor (ex: http://meu-iptv.net:8080) | `iptvUrl` |
| Usuário | `iptvUser` |
| Senha, com botão de visibilidade | `iptvPass` |

| M3U, nesta ordem | Observação |
| --- | --- |
| Nome da Lista (ex: Esportes M3U) | Sem ID individual |
| URL da Lista M3U (http://...) | Sem ID individual |
| URL do EPG XMLTV (opcional) | Sem ID individual |

Os campos vêm preenchidos com exemplos. Esses valores são conteúdo demonstrativo; não devem se tornar contas ou credenciais padrão do produto. A senha no HTML é uma sequência literal de caracteres `•`, não uma credencial recuperada.

### Campo reutilizável

| Propriedade | Valor extraído |
| --- | --- |
| Altura / largura | 46 px / 100% |
| Padding horizontal | 46 px em ambos os lados |
| Fonte | Segoe UI Variable Text / Segoe UI / sans-serif; 13,5 px |
| Fundo | `rgba(10,14,22,0.7)` com blur 14 px |
| Borda / raio | 1 px `rgba(0,210,255,0.16)` / 6 px |
| Sombra | Inset `0 2px 6px rgba(0,0,0,0.6)` |
| Placeholder | Branco alfa 0,38 |
| Ícone | 18 × 18 px, left 15 px, `#7d9cb8`, sombra preta |

Hover do campo: fundo `rgba(10,14,22,0.82)` e borda ciano alfa 0,38. Foco: fundo `rgba(7,10,16,0.95)`, borda `#00d2ff`, sombra inset preta alfa 0,7, anel externo de 2 px ciano alfa 0,35 e brilho de 20 px alfa 0,25. Ícone no `focus-within`: ciano com brilho de 8 px alfa 0,85.

O botão de mostrar senha fica a 10 px da direita, mede 32 × 32 px, raio 6 px, fundo transparente e ícone 18 px. Hover: ciano, fundo ciano alfa 0,12 e brilho 8 px alfa 0,7. Clique alterna `password` / `text` e o desenho do olho. Não há mensagem auxiliar, rótulo externo visível, validação, estado de erro ou sucesso desenhado.

## Lembrar conexão

Linha Flexbox com `space-between`, texto de 12,5 px cinza e margem vertical 4 px. Rótulo literal: `Salvar credenciais e conectar automaticamente`.

Toggle 38 × 20 px; trilho branco alfa 0,2 e raio 20 px. Pino branco 14 × 14 px com recuo esquerdo/inferior 3 px. Ligado: trilho ciano e pino deslocado 18 px. Começa marcado. O HTML muda o controle, mas o script não persiste nem utiliza a preferência.

## CTA de conexão

`.btn-login`: largura preenchida pelo Flexbox do card, altura 46 px, raio 6 px, sem borda, gradiente primário, branco, fonte 14,5 px / 800, tracking 0,3 px, sombra de texto preta e gap 10 px. Margem superior 4 px; sombra `0 4px 22px rgba(0,210,255,0.45)`.

| Estado | Aparência / comportamento |
| --- | --- |
| Repouso | `Entrar e Sincronizar Catálogo` |
| Hover | Gradiente de hover; `translateY(-2px)`; sombra `0 6px 28px` ciano alfa 0,65 |
| Pressionado | `translateY(0)` |
| Carregando | `disabled = true`; texto `Autenticando e Baixando Catálogo...`; spinner visível |
| Final da simulação | Após 1200 ms restaura texto, botão e spinner; esconde login e exibe busca |

Spinner 20 × 20 px, borda 2 px branca alfa 0,3 e topo branco, circular, rotação completa em 800 ms linear infinita. Não há estilo `:disabled` específico nem tratamento de falha. Os 1200 ms são apenas simulação e não um requisito para o backend futuro.

Saída da tela: `.login-screen.hidden` usa opacidade 0, `pointer-events: none` e escala 1,05 em 400 ms. A camada permanece no DOM. A referência contém falha por `accountBadge` ausente; ver documento 05.

## Contas salvas

Seção com borda superior sutil, padding-top 16 px e gap 10 px. Título `Contas Salvas (Acesso em 1 Clique)`: 11,5 px / 600, uppercase, tracking 0,8 px e cor dim.

Grade de três colunas iguais, gap 10 px. Conteúdo: `Servidor Sala / Xtream API`, `Lista Esportes / M3U8 + EPG`, `+ Adicionar / Nova Conta`.

Card: fundo preto alfa 0,42, blur 10 px, borda branca alfa 0,1, raio 6 px, padding 10 px vertical / 8 px horizontal, alinhamento central, gap 6 px e sombra inset superior branca alfa 0,12.

Hover: fundo branco alfa 0,1, borda **vermelha** `rgba(229,9,20,0.5)`, deslocamento vertical −3 px, sombra preta `0 8px 20px` alfa 0,6 e inset branco alfa 0,25.

Avatar 32 × 32 px, raio 8 px, texto 12 px / 800. Xtream: gradiente 135° `#1e3c72 → #2a5298`. M3U: `#11998e → #38ef7d`. Adicionar: fundo branco alfa 0,1, borda tracejada branca alfa 0,3, sinal `+` de 16 px. Nome 11,5 px / 600, linha única, ellipsis, max-width 95%; tipo 9,5 px dim.

Clique nas contas chama `quickLogin`, atualmente interrompido por elemento ausente. Adicionar limpa somente os quatro campos Xtream e foca `iptvName`; não cria conta nem ajusta o modo M3U. Não há tela adicional de cadastro desenhada.
