'use strict'

const puppeteer = require('puppeteer');

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
    await page.type('input[type="email"]', 'dungphse05411@fpt.edu.vn');
    await page.click('#identifierNext')

    await page.waitForSelector('input[type="password"]', {
        visible: true
    });
    await page.type('input[type="password"]', 'Dungph@98');

    await page.waitForSelector('#passwordNext', {
        visible: true
    })
    await page.click('#passwordNext')

    await navigationPromise;
    // get list of class
    let selector = '#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(1) > td:nth-child(2) > ul > li:nth-child(5) > a';
    await page.waitForSelector(selector);
    await page.click(selector);
    const group = await page.evaluate(() => {
        let titleLinks = document.querySelectorAll('#ctl00_mainContent_divGroup a');
        titleLinks = [...titleLinks];
        let articles = titleLinks.map(link => ({
            url: link.getAttribute('href')
        }));
        let group = [];
        articles.forEach(x => {
            group.push(x.url.substr(x.url.lastIndexOf('=') + 1));
        });
        return group;
    });
    console.log(group);
    // await page.goBack();
    // await navigationPromise;
    // //select days
    // await Promise.race([
    //     page.waitForNavigation({waitUntil:'networkidle0'}),
    //     page.select('#ctl00_mainContent_drpSelectWeek', '38')
    // ]);

    // //select data from table;
    // let dataFromTableSelector = await page.evaluate(() => document.querySelector('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody').innerHTML);
    // console.log(dataFromTableSelector);



    console.log('Close browser.')
    await browser.close();
})();
