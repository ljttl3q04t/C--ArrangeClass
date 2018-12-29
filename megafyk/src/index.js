'use strict'
const puppeteer = require('puppeteer');
const fs = require('fs');
const readline = require('readline-sync');

const email = readline.question('what is your email:');
const password = readline.question('what is your password:: ', {hideEchoBack: true});
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
    // get list of class
    let selector = '#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(1) > td:nth-child(2) > ul > li:nth-child(5) > a';
    await page.waitForSelector(selector);
    await page.click(selector);
    await page.waitForSelector('#ctl00_mainContent_divGroup');
    const group = await page.evaluate(() => {
        let titleLinks = document.querySelectorAll('#ctl00_mainContent_divGroup a');
        let group = [];
        titleLinks.forEach(x => { group.push(x.innerHTML) });
        return group;
    });
    var file = fs.createWriteStream('Group.txt');
    group.forEach(x => { file.write(x + '\n') });
    file.end();
    console.log("get group success");
    // get SUBJECTCODE
    await navigationPromise;
    await page.goBack();
    await page.waitForSelector('#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(2) > td:nth-child(2) > ul > li:nth-child(4) > a');
    await page.click('#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(2) > td:nth-child(2) > ul > li:nth-child(4) > a');
    await page.waitForSelector('#aspnetForm');
    const subjectCode = await page.evaluate(() => {
        let tdCode = document.querySelectorAll('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > div > table:nth-child(3) > tbody td:nth-child(2)');
        let tdName = document.querySelectorAll('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > div > table:nth-child(3) > tbody td:nth-child(3)');
        tdCode = [...tdCode];
        tdName = [...tdName];
        let res = [];
        for (i = 0; i < tdCode.length; i++) {
            let tg = [tdCode[i].innerHTML, tdName[i].innerHTML];
            res.push(tg);
        }
        return res;
    });
    file = fs.createWriteStream('SubjectCode.txt');
    subjectCode.forEach(x => { file.write(x.join('\n') + '\n') });
    file.end();
    console.log('get subjectCode success');

    //get record : subject|class|schedule|number
    // example: CSD201|IA1302|E1|16
    await navigationPromise;
    await page.goBack();
    // lich hoc
    await page.waitForSelector('#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(1) > td:nth-child(2) > ul > li:nth-child(1) > a');
    await page.click('#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(1) > td:nth-child(2) > ul > li:nth-child(1) > a');
    //  Computing Fundamental
    await page.waitForSelector('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(2) > td > a');
    await page.click('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(2) > td > a');
    await page.waitForSelector('#id');
    let alu = await page.evaluate(() => {
        let sec = $('#id > tbody > tr');
        let adau = document.querySelectorAll('#id > tbody td:nth-child(1)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerHTML);
        });
        return res;
    });
    file = fs.createWriteStream('CFSubject.txt');
    alu.forEach(x => { file.write(x + '\n') });
    file.end();
    let dautay = await page.evaluate(() => {
        let adau = document.querySelectorAll('#id > tbody td:nth-child(5)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('CFRecord.txt');
    dautay.forEach(x => { file.write(x + "\n==========\n") });
    file.end();
    console.log('get computing fundamental success');
    // get japanese record
    await page.click('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(10) > td > a');
    await page.waitForSelector('#id');
    let aluJapan = await page.evaluate(() => {
        let sec = $('#id > tbody > tr');
        let adau = document.querySelectorAll('#id > tbody td:nth-child(1)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerHTML);
        });
        return res;
    });
    file = fs.createWriteStream('JPNSubject.txt');
    aluJapan.forEach(x => { file.write(x + '\n') });
    file.end();
    let dautayJapan = await page.evaluate(() => {
        let adau = document.querySelectorAll('#id > tbody td:nth-child(5)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('JPNRecord.txt');
    dautayJapan.forEach(x => { file.write(x + "\n==========\n") });
    file.end();
    console.log("get japanese success");
    // get LAB
    await page.click('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(11) > td > a');
    await page.waitForSelector('#id');
    let aluLAB = await page.evaluate(() => {
        let sec = $('#id > tbody > tr');
        let adau = document.querySelectorAll('#id > tbody td:nth-child(1)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerHTML);
        });
        return res;
    });
    file = fs.createWriteStream('LABSubject.txt');
    aluLAB.forEach(x => { file.write(x + '\n') });
    file.end();
    let dautayLAB = await page.evaluate(() => {
        let adau = document.querySelectorAll('#id > tbody td:nth-child(5)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('LABRecord.txt');
    dautayLAB.forEach(x => { file.write(x + "\n==========\n") });
    file.end();
    console.log("get LAB success");
    //get mathematics
    await page.click('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(14) > td > a');
    await page.waitForSelector('#id');
    let aluMath = await page.evaluate(() => {
        let sec = $('#id > tbody > tr');
        let adau = document.querySelectorAll('#id > tbody td:nth-child(1)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerHTML);
        });
        return res;
    });
    file = fs.createWriteStream('MathSubject.txt');
    aluMath.forEach(x => { file.write(x + '\n') });
    file.end();
    let dautayMath = await page.evaluate(() => {
        let adau = document.querySelectorAll('#id > tbody td:nth-child(5)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('MathRecord.txt');
    dautayMath.forEach(x => { file.write(x + "\n==========\n") });
    file.end();
    console.log("get mathematics success");
    // get softskill
    await page.click('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(18) > td > a');
    await page.waitForSelector('#id');
    let aluSoftSkill = await page.evaluate(() => {
        let sec = $('#id > tbody > tr');
        let adau = document.querySelectorAll('#id > tbody td:nth-child(1)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerHTML);
        });
        return res;
    });
    file = fs.createWriteStream('SoftSkillSubject.txt');
    aluSoftSkill.forEach(x => { file.write(x + '\n') });
    file.end();
    let dautaySoftSkill = await page.evaluate(() => {
        let adau = document.querySelectorAll('#id > tbody td:nth-child(5)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('SoftSkillRecord.txt');
    dautaySoftSkill.forEach(x => { file.write(x + "\n==========\n") });
    file.end();
    console.log("get softSkill success");
    //get software Engineering
    await page.click('#ctl00_mainContent_divDepartment > table > tbody > tr:nth-child(19) > td > a');
    await page.waitForSelector('#id');
    let aluSE = await page.evaluate(() => {
        let sec = $('#id > tbody > tr');
        let adau = document.querySelectorAll('#id > tbody td:nth-child(1)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerHTML);
        });
        return res;
    });
    file = fs.createWriteStream('SESubject.txt');
    aluSE.forEach(x => { file.write(x + '\n') });
    file.end();
    let dautaySE = await page.evaluate(() => {
        let adau = document.querySelectorAll('#id > tbody td:nth-child(5)');
        let res = [];
        adau.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('SERecord.txt');
    dautaySE.forEach(x => { file.write(x + "\n==========\n") });
    file.end();
    console.log("get softSkill success");
    //get current timetable
    await page.click('#ctl00_lblNavigation > a');
    await page.waitForSelector('#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(2) > td:nth-child(2) > ul > li:nth-child(1) > a');
    await page.click('#ctl00_mainContent_divMain > div:nth-child(2) > div > table > tbody > tr > td > table > tbody > tr:nth-child(2) > td:nth-child(2) > ul > li:nth-child(1) > a');
    await page.waitForSelector('#ctl00_mainContent_divCourse');
    let course = await page.evaluate(() => {
        let alu = document.querySelectorAll('#ctl00_mainContent_divCourse td');
        let res = [];
        alu.forEach(x => {
            res.push(x.innerText);
        });
        return res;
    });
    file = fs.createWriteStream('CurrentCourse.txt');
    course.forEach(x => { file.write(x + "\n") });
    file.end();
    // console.log(course);
    let currentSchedule = await page.evaluate(() => {
        let d = document.querySelector('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody > tr > td:nth-child(2) > table > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(2)').innerHTML;
        let s = document.querySelector('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody > tr > td:nth-child(2) > table > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(3)').innerHTML;
        let currentSchedule = [];
        currentSchedule.push([d, s]);
        return currentSchedule;
    });
    let num = await page.evaluate(() => {
        return $('#ctl00_mainContent_divCourse > table > tbody tr').length;
    });
    for (var i = 2; i <= num; i++) {
        let selector = "#ctl00_mainContent_divCourse > table > tbody > tr:nth-child(" + i + ") > td > a";
        await page.click(selector);
        await page.waitForSelector('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody > tr > td:nth-child(2) > table > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(2)');
        let alu = await page.evaluate(() => {
            let d = document.querySelector('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody > tr > td:nth-child(2) > table > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(2)').innerHTML;
            let s = document.querySelector('#aspnetForm > table > tbody > tr:nth-child(1) > td > div > table > tbody > tr > td:nth-child(2) > table > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(3)').innerHTML;
            return [d, s];
        });
        currentSchedule.push(alu);
    }
    file = fs.createWriteStream('CurrentSchedule.txt');
    currentSchedule.forEach(x => { file.write(x.join('|') + '\n') });
    file.end();
    // console.log(currentSchedule);

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
