# Especificação visual do Cinecerb

Versão documental: 1.0 · Referência capturada em 31/10/2026 · Idioma da interface: português do Brasil.

Este conjunto descreve o protótipo existente para permitir sua implementação futura sem refazer o desenho das telas. Os valores são extraídos do HTML, CSS e JavaScript, incluindo estilos inline. Não representa um redesign.

## Por onde começar

| Documento | Conteúdo |
| --- | --- |
| [01 — Fundamentos](01-fundamentos.md) | Identidade, tokens, tipografia, medidas, camadas e movimento |
| [02 — Login](02-login.md) | Cenário, painel acrílico, abas, campos, toggle, CTA e contas salvas |
| [03 — Estrutura e catálogo](03-estrutura-e-catalogo.md) | Barra de janela, busca, navegação, hero, carrosséis e cards |
| [04 — Detalhes e reprodução](04-detalhes-e-reproducao.md) | Modal, episódios, player, Multiview e PiP |
| [05 — Comportamentos e lacunas](05-comportamentos-e-lacunas.md) | Transições, estados, limites responsivos, erros e decisões pendentes |
| [06 — Validação e evolução](06-validacao-e-evolucao.md) | Critérios verificáveis, roteiro visual e como criar telas novas |
| [07 — Assets e proveniência](07-assets-e-proveniencia.md) | Arquivos, dimensões, dependências e integridade da referência |
| [08 — Fonte extraída](08-fonte-extraida.md) | CSS integral, estilos inline, eventos e JavaScript com linhas de origem |
| [Manifesto da referência](referencia/MANIFESTO.md) | Hashes SHA-256 e inventário dos arquivos preservados |

## Como interpretar as regras

- **Extraído:** existe literalmente no código da referência. É o ponto de partida para a reprodução fiel.
- **Derivado:** calculado a partir do código; pode depender do tamanho da janela, fonte efetivamente carregada ou algoritmo de layout.
- **Recomendado:** orientação para implementar o produto real, sem afirmar que já exista no protótipo.
- **Não definido:** o protótipo não oferece uma decisão suficiente. Não assumir que uma tela ou estado tenha sido desenhado.

As tabelas de dimensões descrevem declarações CSS, não medições feitas em uma captura de tela. `width` e `height` usam `border-box`, salvo comportamento próprio dos elementos. Flexbox pode encolher elementos que não têm `flex-shrink: 0`.

Os links para linhas e os seletores se referem à [cópia arquivada de index.html](referencia/prototype/index.html). Consulte a fonte extraída para valores completos de sombras, gradientes e regras que sejam extensos demais para uma tabela.

## Identidade que deve permanecer reconhecível

Aplicação de streaming para desktop Windows, com fundos quase pretos, navegação compacta, conteúdo cinematográfico e ações principais em gradiente ciano/azul. O login tem cenário de ficção científica, vinheta forte e cartão acrílico. O catálogo combina hero largo com fileiras horizontais. A reprodução ocupa a janela inteira, com controles sobre a imagem.

O ícone da marca é o escudo com Cérbero, claquete e símbolo de reprodução ciano. O nome aparece como texto `Cinecerb`, não como substituição por uma nova marca.

## Mapa das superfícies existentes

| Superfície | Existe como layout? | Alcance real |
| --- | --- | --- |
| Login Xtream | Sim | Quatro campos e autenticação simulada |
| Login M3U / EPG | Sim | Três campos; mesmo CTA |
| Início / catálogo | Sim | Hero e três carrosséis |
| Detalhes | Sim | Modal compartilhado entre títulos, com episódio demonstrativo |
| Player | Sim | Imagem estática com controles simulados |
| Multiview | Sim | Grade 2 × 2 e seleção visual de áudio |
| PiP | Sim | Janela flutuante dentro da página |
| TV Ao Vivo, Filmes, Séries, Minha Lista, Downloads e Configurações | Só itens de navegação | Não há páginas específicas desenhadas |
| Resultados de busca, erros, vazios, configurações de áudio e legendas | Não | Exigem definição futura |

## Estado da verificação

Foi realizada análise estática integral do arquivo e inspeção das imagens locais de logo e fundo. Foram preservados o HTML e os quatro assets locais, com verificação de integridade por SHA-256.

Verificações documentais concluídas: 20 links locais válidos nos 12 arquivos Markdown; blocos de código fechados; tokens idênticos aos originais; CSS e JavaScript integrais do anexo iguais aos blocos da fonte, desconsiderando apenas whitespace externo. A extração inventariou 32 estilos inline e 50 handlers inline. A sintaxe do script de extração foi verificada com Node.js.

**Não foi realizada validação da renderização no navegador nem produzidas capturas das telas.** A tentativa de abrir o arquivo local no navegador foi bloqueada pela política de segurança da ferramenta. Assim, propriedades computadas, carregamento remoto de fontes/imagens, dimensões finais e comportamento visual em diferentes janelas continuam pendentes de conferência. Os problemas funcionais relatados foram identificados por leitura do código.

## Prioridade em caso de divergência

1. Decisões explícitas posteriores do usuário, registradas no histórico de design.
2. Regras desta documentação, observando a classificação de extração, recomendação e pendência.
3. Snapshot e fonte extraída para recuperar detalhes exatos do baseline.

Uma contradição não autoriza uma mudança estética silenciosa. Registre a diferença e a decisão tomada. Corrigir um erro de execução não implica redesenhar a tela.
