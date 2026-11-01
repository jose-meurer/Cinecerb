# 06 — Critérios de fidelidade e evolução

Esta lista serve para desenvolver e revisar o produto real. As caixas abaixo são **critérios futuros**, não testes declarados como executados nesta entrega.

## Preparar a comparação

Usar o HTML arquivado como baseline, com os mesmos assets e dados demonstrativos. Registrar navegador/webview, versão, tamanho da área de conteúdo em pixels CSS, escala do sistema, zoom, fontes realmente carregadas e disponibilidade de imagens remotas. Comparações sem essas condições podem confundir diferenças de fonte ou enquadramento com mudanças no layout.

**Matriz recomendada, ainda não validada:** 1440 × 900 como primeira janela de comparação; 1920 × 1080, 1366 × 768 e 1024 × 768 como verificações adicionais de espaço. Esses tamanhos não são breakpoints nem mínimos oficiais. Depois de medir o baseline, registrar limites aceitos e eventuais adaptações.

Esperar o carregamento das fontes e imagens e a conclusão das animações antes de capturar repouso. Para screenshots estáveis, manter o ponteiro fora dos cards e do cenário; controlar ou desativar movimento apenas no ambiente de comparação, usando a mesma condição nas duas versões.

Não existe snapshot visual de telas nesta entrega. As primeiras capturas deverão ser anexadas com suas condições, sem alegar que já fizeram parte da aprovação inicial.

## Fundamentos

- [ ] Fundo base `#0a0a0d`, superfícies `#121216` e cards `#18181f`.
- [ ] Primários com gradiente 135° ciano → azul, brilho e pesos corretos.
- [ ] Fontes de marca / hero / seções em Outfit e corpo em Inter onde declaradas.
- [ ] Campos de login mantêm a referência Segoe UI.
- [ ] Raios 6 / 10 / 16 px e exceções preservados.
- [ ] Opacidades, blur e gradientes não foram substituídos por cores opacas genéricas.
- [ ] Não houve normalização silenciosa dos vermelhos legados.
- [ ] Ícones locais e proporção da marca preservados; nenhum esticamento do PNG vertical.

## Login

- [ ] Barra de janela com 42 px e login abaixo dela.
- [ ] Fundo sci-fi correto, `cover`, vinheta radial e gradiente vertical.
- [ ] Painel central com width 480 px no espaço suficiente, padding 38/36/32 e gap 20 px.
- [ ] Logo em caixa de 92 px, texto Outfit 32 px e subtítulo de 13 px.
- [ ] Abas iguais, Xtream inicial e M3U com um campo a menos.
- [ ] Campos de 46 px, gap 13 px, ícones de 18 px e foco ciano com brilho.
- [ ] Toggle 38 × 20 px e pino de 14 px, inicialmente ligado no baseline.
- [ ] CTA com altura 46 px, hover −2 px e loading com spinner de 20 px.
- [ ] Contas em três colunas, gap 10 px; avatares e textos nas medidas documentadas.
- [ ] Fundo reage ao ponteiro; card fica estável; saída do login usa escala 1,05 e fade.

## Estrutura e catálogo

- [ ] Sidebar tem largura de referência 220 px, padding 16/10 e sete itens na ordem original.
- [ ] Marcador ativo ciano de 3,5 px e 70% da altura do item.
- [ ] Perfil e troca de conta no rodapé; viewport rola sem arrastar a barra e sidebar.
- [ ] Busca tem largura de referência 420 px e campo com 30 px de altura.
- [ ] Hero tem 520 px, recuo lateral 44 px e inferior 56 px.
- [ ] Hero content max-width 620 px; título 52 px / 900; sinopse até três linhas.
- [ ] Catálogo sobe 30 px sobre o hero e separa seções por 38 px.
- [ ] Fileiras têm gap 16 px e padding 12/4; setas 40 × 80 px a −22 px das laterais.
- [ ] Cards: continuidade 280 × 160; canal 240 × 130; pôster 170 × 255.
- [ ] Continuidade mostra progresso permanente; pôster revela texto só no hover do baseline.
- [ ] Hovers respeitam deslocamentos e escalas próprios, sem uniformização entre os cards.

## Detalhes e reprodução

- [ ] Modal cobre a janela, painel 860 px, max-height 90vh, raio 16 px e blur externo 14 px.
- [ ] Hero do modal 360 px; conteúdo com padding 30 px e gap 20 px.
- [ ] Fechar circular de 36 px, recuo 18 px e rotação de 90° no hover.
- [ ] Episódio preserva ordem número / imagem / texto / duração e miniatura 110 × 65 px.
- [ ] Player cobre inclusive a titlebar e mantém controles acima da imagem ou grade.
- [ ] Header do player usa padding 24/30; base usa 30 px.
- [ ] Timeline tem 6 px e pino branco 14 px; fill do baseline começa em 42%.
- [ ] Pills têm padding 6/14 e raio 20 px; modo Multiview ativo fica verde.
- [ ] Multiview mantém grade 2 × 2, gap/padding 4 px e seleção de áudio com borda verde de 2 px.
- [ ] PiP tem 320 × 180 px, recuos 24 px e borda ciano de 2 px.

## Roteiro de estados para capturar

1. Login Xtream em repouso e campo focado.
2. Senha visível; modo M3U; toggle desligado; conta salva em hover.
3. CTA em loading, sem depender da conclusão quebrada de `accountBadge` no baseline.
4. Catálogo no topo; item lateral ativo; busca focada.
5. Continuidade e setas em hover; seção de canais; pôster com overlay revelado.
6. Modal aberto pelo hero e por um pôster; item de episódio em hover.
7. Player normal e pausado; timeline após clique.
8. Multiview ativo, selecionando duas células para comparar borda e badge.
9. PiP aberto, restaurado e fechado; Escape nos overlays.

As ações com referências DOM quebradas devem ser registradas como limitações do baseline. Na implementação real, verificar os fluxos corrigidos e descrever a correção funcional separadamente da comparação estética.

## Como avaliar diferenças

Conferir primeiro a geometria: largura/altura, espaçamentos, alinhamentos, proporção de imagens e distribuição do conteúdo. Depois verificar fonte, peso, truncamento, cores, gradientes, blur e sombras. Por último comparar estados e movimento.

Não foi estabelecida uma tolerância numérica de comparação de pixels. Antialiasing, fontes, imagens remotas e backdrop-filter variam entre ambientes. Uma ferramenta de diff pode ajudar, mas a aprovação deve explicar diferenças relevantes em vez de usar um percentual arbitrário.

## Implementar telas ainda não desenhadas

**Recomendado:** manter a mesma barra, sidebar, viewport, recuo de 44 px, títulos Outfit de 20 px, ações ciano/azul, bordas sutis e cards existentes quando a função permitir. Esse conjunto forma uma base para TV Ao Vivo, Filmes, Séries, Minha Lista, Downloads e Configurações.

Isso não equivale a um layout aprovado dessas páginas. Antes de fechar sua implementação, documentar estrutura, componentes novos, estados necessários e diferenças em relação ao catálogo. Se uma biblioteca for usada, ajustar seu tema e medidas; os defaults da biblioteca não substituem a referência.

## Fluxo para mudanças intencionais

1. Identificar a regra atual e a tela afetada.
2. Explicar a necessidade funcional ou a mudança de preferência.
3. Registrar a decisão no histórico do documento 05.
4. Atualizar as fichas relevantes, sem modificar o snapshot original.
5. Comparar antes/depois nas mesmas condições e anexar as evidências disponíveis.

O objetivo é permitir evolução consciente mantendo a identidade do Cinecerb. A stack e a organização do código podem mudar; os valores e a composição visual só mudam mediante uma decisão explícita.
