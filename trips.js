trips = require('./trips.json');
const moment = require('moment');

let ret = trips.data.reduce((accu, curr) => {
    const day = moment.unix(curr.start_time).local();
    const key = day.date();

    if(accu[key]) {
        accu[key] += 1;
    }
    else {
        accu[key] = 1;
    }
    return accu;
}, []);

console.log(ret);

ret = trips.data.reduce((accu, curr) => {
    const day = moment.unix(curr.start_time).local();
    const key = day.date();
    if([27, 28, 29].includes(key)) {
        accu += curr.trip_duration;
    }
    return accu;
}, 0);

console.log(ret);