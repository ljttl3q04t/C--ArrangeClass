'use strict'
const fs = require('fs');

const readDataFromFile = (filename) => {
    let data = fs.readFileSync(filename, "utf-8");
    return data;
}

let course = readDataFromFile('SubjectCode.txt').split("\n");
let courseID = [];
let courseName = [];
for (var i = 0; i < course.length; i++) if (course[i].length >= 1) {
    (i % 2 == 0) ? courseID.push(course[i]) : courseName.push(course[i]);
}
let group = readDataFromFile('Group.txt').split("\n");
group.pop();
//get current schedule
let curCourse = readDataFromFile('CurrentCourse.txt').split("\n");
let curSchedule = readDataFromFile('CurrentSchedule.txt').split("\n");
curCourse.pop(); curSchedule.pop();
let yourSchedule = [];
for (var i = 0; i < curCourse.length; i++) {
    let s = curCourse[i];
    let code = s.substring(s.indexOf('(') + 1, s.indexOf(')'));
    let lop = s.substring(s.lastIndexOf('(') + 1, s.indexOf(','));
    s = curSchedule[i];
    let day = s.substring(0, s.indexOf(" "));
    let slot = s.substring(s.indexOf('|') + 1);
    if (day == 'Tuesday' || day == 'Thursday') {
        if (day == 'Tuesday' && slot == 4) slot = 'E4';
        else if (day == 'Tuesday' && slot == 6) slot = 'E5';
        else if (day == 'Tuesday' && slot == 1) slot = 'M4';
        else if (day == 'Tuesday' && slot == 3) slot = 'M5';
    } else {
        if (slot <= 3) slot = 'M' + slot;
        else slot = 'E' + (slot - 3);
    }
    yourSchedule.push([code, lop, slot]);
}
console.log(yourSchedule);
let file = fs.createWriteStream('YourSchedule.txt');
yourSchedule.forEach(x => { file.write(x.join('|') + '\n') });
file.end();
// get record
// subjectID, class, schedule, number
let courseType = ['CF', 'JPN', 'LAB', 'Math', 'SoftSkill', 'SE'];
let record = [];
courseType.forEach(x => {
    let subject = readDataFromFile(x + 'Subject.txt').split('\n');
    subject.pop();
    let subRecord = readDataFromFile(x + 'Record.txt').split('\n');
    subRecord.pop();
    let k = 0;
    for (var i = 0; i < subRecord.length; i++) {
        if (subRecord[i] == "==========") {
            k++;
            continue;
        }
        let s = subRecord[i];
        let lop = s.substring(0, s.indexOf(' '));
        let lich = s.substring(s.indexOf('(') + 1, s.indexOf('|') - 1);
        let siso = s.substring(s.indexOf('|') + 2, s.indexOf('-'));
        if (courseID.includes(subject[k]))
            record.push([subject[k], lop, lich, siso]);
    }
});
file = fs.createWriteStream('Record.txt');
record.forEach(x => { file.write(x.join('|') + '\n') });
file.end();
// console.log(record);