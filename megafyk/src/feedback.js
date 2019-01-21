'use strict'
const puppeteer = require('puppeteer');
const fs = require('fs');
const readline = require('readline-sync');

const email = readline.question('what is your email:');
const password = readline.question('what is your password: ', {hideEchoBack: true});
// const email = 'dungphse05411@fpt.edu.vn';
// const password = 'YenAnh@99';
(async () => {
    console.log('Open browser.');
    const browser = await puppeteer.launch({
        headless: false
    });

    const page = await browser.newPage();
    const navigationPromise = page.waitForNavigation()

    await page.goto('http://fap.fpt.edu.vn');
    await page.select('#ctl00_mainContent_ddlCampus', '3');
    await page.click('#ctl00_mainContent_btnLoginToGoogle');

    await navigationPromise;
    await page.waitForSelector('input[type="email"]');
    await page.type('input[type="email"]', email);
    await page.click('#identifierNext')

    await page.waitForSelector('input[type="password"]', {
        visible: true
    });
    await page.type('input[type="password"]', password);

    await page.waitForSelector('#passwordNext', {
        visible: true
    })
    await page.click('#passwordNext')

    await navigationPromise;
    // finish login
    // click y kien giang day
    let selector = '#ctl00_mainContent_divFed > table > tbody > tr > td > ul > li > a';
    await page.waitForSelector(selector);
    await page.click(selector);
    await page.waitForSelector('#aspnetForm > table > tbody > tr:nth-child(1) > td');
    let numFeedback = await page.evaluate(() => {
        return $('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody:nth-child(2) tr').length;
    });

    for (let i = 1; i <= numFeedback; i++) {
        let selector = "#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody:nth-child(2) > tr:nth-child(" + i + ") > td:nth-child(6) a";
        await page.waitForSelector(selector);
        let numLink = await page.evaluate(selector => {
            return $(selector).length;
        }, selector);
        if (numLink == 1) {
            await page.click(selector);
            for (let j = 0; j <= 4; j++) {
                let id = "#ctl00_mainContent_reload_ctl0" + j + "_chkList_0";
                await page.waitForSelector(id);
                await page.click(id);
            }
            await page.waitForSelector('#ctl00_mainContent_btSendFeedback');
            await page.click('#ctl00_mainContent_btSendFeedback');
            await page.waitForSelector('#ctl00_mainContent_lblMege');
            await page.click('#ctl00_lblNavigation > a:nth-child(2)');
        }
        // console.log(numLink);
    }
    // console.log('Close browser.')
    // await browser.close();
})();
