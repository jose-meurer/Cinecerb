export async function waitUntilReady(splash) {
    const logo = splash.querySelector(".splash-screen__logo");
    const title = splash.querySelector(".splash-screen__name");
    const titleFont = getComputedStyle(title).font;

    await Promise.all([
        logo.decode(),
        document.fonts.load(titleFont, title.textContent)
    ]);
    await document.fonts.ready;
    
    await new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(resolve)));
}
