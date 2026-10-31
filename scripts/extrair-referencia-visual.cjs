// Extrai a fonte documental sem executar o HTML ou acessar URLs externas.
// Uso: node scripts/extrair-referencia-visual.cjs [pasta-do-prototipo]
// Se o snapshot já existir, recusa substituir arquivos por conteúdos diferentes.
const fs = require('node:fs');
const path = require('node:path');
const crypto = require('node:crypto');

const root = path.resolve(__dirname, '..');
const visual = path.join(root, 'docs', 'visual');
const reference = path.join(visual, 'referencia');
const snapshot = path.join(reference, 'prototype');
const origin = process.argv[2] ? path.resolve(process.argv[2]) : snapshot;
const sha = bytes => crypto.createHash('sha256').update(bytes).digest('hex');
const files = ['index.html', 'assets/app.ico', 'assets/login_backdrop.jpg', 'assets/logo.png', 'assets/logo_icon.png'];
const items = files.map(name => {
  const bytes = fs.readFileSync(path.join(origin, name));
  const destination = path.join(snapshot, name);
  if (fs.existsSync(destination) && sha(fs.readFileSync(destination)) !== sha(bytes)) {
    throw new Error(`Snapshot existente diverge de ${name}; não será sobrescrito.`);
  }
  return { name, bytes, destination, hash: sha(bytes) };
});
for (const item of items) {
  fs.mkdirSync(path.dirname(item.destination), { recursive: true });
  if (!fs.existsSync(item.destination)) fs.writeFileSync(item.destination, item.bytes);
  if (sha(fs.readFileSync(item.destination)) !== item.hash) throw new Error(`Integridade inválida: ${item.name}`);
}

const source = items[0].bytes.toString('utf8');
const lines = source.split(/\r?\n/);
const lineAt = offset => source.slice(0, offset).split('\n').length;
const escapeCell = text => text.replace(/\r?\n/g, ' ').replace(/\|/g, '&#124;').replace(/`/g, '&#96;');
const code = (language, content) => '\n```' + language + '\n' + content.trim() + '\n```\n';
const styleMatch = /<style>([\s\S]*?)<\/style>/.exec(source);
const scriptMatch = /<script>([\s\S]*?)<\/script>/.exec(source);
if (!styleMatch || !scriptMatch) throw new Error('Estrutura de fonte inesperada.');
const css = styleMatch[1];
const js = scriptMatch[1];
const declaredVars = new Set([...css.matchAll(/(--[\w-]+)\s*:/g)].map(match => match[1]));
const usedVars = new Set([...source.matchAll(/var\((--[\w-]+)/g)].map(match => match[1]));
const missingVars = [...usedVars].filter(value => !declaredVars.has(value));
const unusedVars = [...declaredVars].filter(value => !usedVars.has(value));
const ids = new Map();
for (const match of source.matchAll(/\bid="([^"]+)"/g)) {
  const entries = ids.get(match[1]) || [];
  entries.push(lineAt(match.index));
  ids.set(match[1], entries);
}
const usedIds = new Set([...js.matchAll(/getElementById\('([^']+)'\)/g)].map(match => match[1]));
const missingIds = [...usedIds].filter(id => !ids.has(id));
const urls = new Map();
for (const match of source.matchAll(/https?:\/\/[^\s'"<>`)]+/g)) {
  const occurrences = urls.get(match[0]) || new Set();
  occurrences.add(lineAt(match.index));
  urls.set(match[0], occurrences);
}
const inline = [];
const handlers = [];
for (const match of source.matchAll(/<[a-z][^>]*>/gi)) {
  const tag = match[0];
  const name = /^<([\w-]+)/.exec(tag)[1];
  const id = /\bid="([^"]+)"/.exec(tag)?.[1];
  const classes = /\bclass="([^"]+)"/.exec(tag)?.[1];
  const label = name + (id ? '#' + id : '') + (classes ? '.' + classes.trim().split(/\s+/).join('.') : '');
  const style = /\bstyle="([^"]*)"/.exec(tag);
  if (style) inline.push({ line: lineAt(match.index), label, value: style[1] });
  for (const handler of tag.matchAll(/\b(on\w+)="([^"]*)"/g)) {
    handlers.push({ line: lineAt(match.index), label, event: handler[1], value: handler[2] });
  }
}

let output = '# 08 — Fonte extraída do protótipo\n\n';
output += 'Extração estática do baseline de 31/10/2026. O conteúdo CSS e JavaScript abaixo foi extraído automaticamente, sem executar scripts, renderizar páginas ou consultar a rede. Espaços externos dos blocos foram removidos; declarações internas permanecem preservadas.\n\n';
output += 'Este anexo guarda os valores literais, inclusive falhas e estilos órfãos; use os documentos 01–06 para interpretar sua aplicação. A cópia HTML e seus assets estão em [referencia/prototype](referencia/prototype/index.html).\n\n';
output += `SHA-256 do HTML: \`${items[0].hash}\`.\n\n`;
output += '## Mapa técnico\n\n';
output += '| Dado | Resultado |\n| --- | --- |\n';
output += `| Linhas lógicas no HTML | ${lines.length} (incluindo eventual linha final vazia) |\n`;
output += `| IDs declarados | ${ids.size} |\n| Estilos inline | ${inline.length} |\n| Handlers inline | ${handlers.length} |\n`;
output += `| Media queries | ${(css.match(/@media\b/g) || []).length} |\n| Container queries | ${(css.match(/@container\b/g) || []).length} |\n`;
output += `| Variáveis usadas e não declaradas | ${missingVars.map(value => '`' + value + '`').join(', ')} |\n`;
output += `| Variáveis declaradas sem uso via var() | ${unusedVars.map(value => '`' + value + '`').join(', ')} |\n`;
output += `| IDs estáticos usados por getElementById e ausentes | ${missingIds.map(value => '`' + value + '`').join(', ')} |\n\n`;
output += '## Índice de IDs\n\n| ID | Linha do HTML |\n| --- | --- |\n';
for (const [id, occurrences] of ids) output += `| \`${id}\` | ${occurrences.join(', ')} |\n`;
output += '\n## URLs externas literais\n\nOs endereços são inventário da fonte, sem verificação de disponibilidade. Parâmetros foram preservados. Não há cópia local dos binários remotos.\n\n| URL | Linhas de ocorrência |\n| --- | --- |\n';
for (const [url, occurrences] of urls) output += `| \`${escapeCell(url)}\` | ${[...occurrences].join(', ')} |\n`;
output += '\n## CSS integral\n\n';
output += `Bloco original iniciado na linha ${lineAt(styleMatch.index)} do HTML. Inclui tokens, reset, componentes, pseudoestados e keyframes.\n`;
output += code('css', css);
output += '\n## Estilos inline do HTML\n\nCada linha da tabela aponta o início da tag no HTML original. Declarações inline têm precedência sobre regras normais de seletor quando concorrem pela mesma propriedade.\n\n| Linha | Elemento | Declarações |\n| --- | --- | --- |\n';
for (const item of inline) output += `| ${item.line} | \`${escapeCell(item.label)}\` | \`${escapeCell(item.value)}\` |\n`;
output += '\n## Handlers inline do HTML\n\n| Linha | Elemento | Evento | Expressão original |\n| --- | --- | --- | --- |\n';
for (const item of handlers) output += `| ${item.line} | \`${escapeCell(item.label)}\` | \`${item.event}\` | \`${escapeCell(item.value)}\` |\n`;
output += '\n## JavaScript integral\n\n';
output += `Bloco original iniciado na linha ${lineAt(scriptMatch.index)} do HTML. Os listeners registrados por addEventListener e as mudanças de estilo em runtime estão incluídos aqui.\n`;
output += code('javascript', js);
fs.writeFileSync(path.join(visual, '08-fonte-extraida.md'), output, 'utf8');

let manifest = '# Manifesto da referência visual\n\n';
manifest += 'Baseline: 31/10/2026. Origem histórica: `C:\\Users\\Jose Meurer\\Desktop\\Projetos\\prototype`.\n\n';
manifest += 'Arquivos preservados sem alteração de bytes. Os hashes abaixo identificam esta versão, e não validam funcionalidades ou renderização.\n\n';
manifest += '| Arquivo | Bytes | SHA-256 |\n| --- | --- | --- |\n';
for (const item of items) manifest += `| [${item.name}](prototype/${item.name}) | ${item.bytes.length} | \`${item.hash}\` |\n`;
manifest += '\nPara verificar ou regenerar apenas a extração técnica a partir do snapshot, executar na raiz do projeto:\n';
manifest += code('powershell', 'node scripts/extrair-referencia-visual.cjs');
manifest += '\nO script não modifica a origem e recusa substituir um snapshot existente por bytes diferentes. O catálogo de arquivos está fixado aos cinco arquivos deste baseline. Documentação interpretativa deve ser atualizada separadamente.\n';
fs.writeFileSync(path.join(reference, 'MANIFESTO.md'), manifest, 'utf8');
console.log(JSON.stringify({ arquivosPreservados: items.length, estilosInline: inline.length, handlersInline: handlers.length, urls: urls.size, idsAusentes: missingIds, variaveisAusentes: missingVars, integridade: 'SHA-256 conferido para todos os arquivos' }, null, 2));
