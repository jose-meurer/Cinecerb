# 07 — Assets, fontes e proveniência

## Origem e referência preservada

Origem: `C:\Users\Jose Meurer\Desktop\Projetos\prototype`.

Cópia: [referencia/prototype](referencia/prototype/index.html), dentro deste projeto. O snapshot preserva o HTML/CSS/JS e os quatro arquivos locais sem reescrever seu conteúdo. O [manifesto](referencia/MANIFESTO.md) registra tamanho e SHA-256 para comprovar equivalência.

O arquivo principal tem 75.202 bytes. As linhas citadas pela documentação são as do HTML original capturado. Caso o protótipo original mude depois, isso não muda automaticamente esta referência.

## Assets locais

| Arquivo | Dimensões | Bytes | Uso na referência |
| --- | --- | --- | --- |
| `assets/logo_icon.png` | 765 × 1024 px | 991.285 | Marca na barra e no login, com query `?v=7` |
| `assets/logo.png` | 765 × 1024 px | 991.285 | Arquivo disponível, sem uso no HTML |
| `assets/login_backdrop.jpg` | 1376 × 768 px | 952.241 | Fundo do login |
| `assets/app.ico` | 256 × 256 px reportados pelo leitor de imagem | 207.430 | Favicon; um ICO pode conter múltiplas resoluções internas |

Os dois PNGs são idênticos em conteúdo, confirmado por SHA-256. O sufixo `?v=7` serve de versão de URL; não é parte do nome físico do arquivo.

## Marca

O asset inspecionado mostra um escudo de tons azul-escuros e ciano, Cérbero com três cabeças, uma claquete e um símbolo de play luminoso. Preservar o arquivo e sua proporção vertical. Nas caixas de 28 px e 92 px o CSS usa `object-fit: contain`, sem recorte ou deformação.

O nome `CINECERB` é renderizado em texto ao lado do ícone: Inter na barra e Outfit no login. Não substituir pelo PNG `logo.png` supondo que ele contenha um lockup horizontal.

## Cenário do login

O JPG inspecionado mostra personagem à esquerda e cidade futurista ao centro/direita sob céu espacial. É o principal asset ambiental da identidade. O CSS aplica `cover`, centralização, expansão de bordas, escala, vinheta e parallax. A imagem sozinha não reproduz o resultado: manter o tratamento em camadas do documento 02.

## Imagens remotas

O hero, as miniaturas, os pôsteres, o episódio, o player, o Multiview e o PiP usam URLs de `images.unsplash.com`. O inventário exato, incluindo parâmetros e linhas de uso, está no documento 08.

Essas fotos são conteúdo demonstrativo. Os títulos dos filmes e canais não comprovam que as imagens sejam seus materiais oficiais. No produto, imagens podem vir do catálogo real, mantendo dimensões, recorte, gradientes e composição dos componentes.

**Limite:** os bytes das imagens remotas não foram baixados nem arquivados. Preservar URLs no HTML não garante disponibilidade futura ou reprodução offline. A referência só é autossuficiente para os assets locais. O código depende da rede para reproduzir o catálogo com as fotos originais.

## Fontes externas

Importação original:

```text
https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800&family=Outfit:wght@500;700;800;900&display=swap
```

Há preconnect para `fonts.googleapis.com` e `fonts.gstatic.com`. Não há arquivos WOFF/WOFF2 no protótipo. Os fallbacks são os descritos no documento 01; o carregamento das fontes externas não foi verificado nesta entrega.

**Recomendado para distribuição estável:** avaliar empacotamento das fontes permitidas e definir famílias de controles explicitamente. Isso exige decisão de implementação e nova comparação de renderização; não houve substituição de fontes nesta documentação.

## Ícones e símbolos

Os ícones principais estão embutidos como SVG, em geral `viewBox="0 0 24 24"`, com `fill: currentColor` ou fill específico do componente. Os paths permanecem no HTML arquivado; reutilizá-los permite conservar a forma sem adotar outra biblioteca por aproximação.

Também existem glifos Unicode e emojis: setas de carrossel, minimizar/maximizar/fechar, coração, download, áudio, legendas, gravação e PiP. Sua aparência depende do sistema e da fonte de emoji. Não há família de ícones externa importada.

**Recomendado:** se for necessário estabilizar emojis em SVG, registrar a mudança e comparar tamanho, peso e alinhamento. Não afirmar equivalência visual automática.

## O que foi preservado e o que não foi

| Item | Situação |
| --- | --- |
| HTML/CSS/JS original | Preservado byte a byte |
| Quatro assets locais | Preservados byte a byte |
| Tokens, CSS, inline styles e handlers | Extraídos em Markdown |
| URLs de fontes e imagens externas | Registradas |
| Binários de fontes e imagens externas | Não arquivados |
| Capturas de tela / propriedades computadas | Não produzidas |
| Backend, banco e engine de vídeo | Não existem na referência |

## Consultar a referência no futuro

Abrir `referencia/prototype/index.html` em um ambiente de navegador autorizado ou usar o servidor local adotado pelo desenvolvimento. Manter o diretório `assets` ao lado do HTML. Consultar os IDs e seletores no anexo técnico quando houver dúvida de medida ou estilo.

A cópia é uma referência histórica, não código de produção. Não aplicar correções diretamente nela; desenvolver a implementação real em sua estrutura própria e registrar as diferenças necessárias.
