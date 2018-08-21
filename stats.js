let trips = require('./trips.json');
const moment = require('moment');
const turf = require('@turf/turf');

console.log(`Raw trips: ${trips.length}`);

const neighborhoods = turf.featureCollection(require('./neighborhoods.json').features);

trips = trips.reduce((accu, curr) => {
  const date = moment.unix(curr.start_time).local();
  const firstDay = moment('2018-07-25');
  const lastDay = moment('2018-08-19')
  const miles = curr.trip_distance * 0.000621371;
  if (curr.trip_duration >= 60
    && date.isSameOrAfter(firstDay.startOf("day"))
    && date.isSameOrBefore(lastDay.endOf("day"))
    && miles > 0
    && miles < 20
  ) {
    const key = `${curr.device_id}-${curr.start_time}`;
    accu[key] = curr;
    // let intersections = turf.flattenReduce(neighborhoods, (previous, current) => {
    //   if (turf.booleanContains(current, accu[key].start_point)) {
    //     previous.start = current.properties.MAPLABEL;
    //   }
    //   if (turf.booleanContains(current, accu[key].end_point)) {
    //     previous.end = current.properties.MAPLABEL;
    //   }
    //   return previous;
    // }, {
    //     start: {},
    //     end: {}
    //   });

    // Object.keys(intersections).forEach((value) => {
    //   accu[key][`neighborhood_${value}`] = intersections[value];
    // });
  }
  return accu;
}, {});

console.log(`Filtered trips: ${Object.keys(trips).length}`);

const keys = Object.keys(trips);

const unordered = keys.reduce((accu, curr) => {
  curr = trips[curr];
  const date = moment.unix(curr.start_time).local();
  const key = `${date.format("YYYY")}-${date.format("MM")}-${date.format("DD")}`;
  if (accu[key]) {
    accu[key] += 1;
  }
  else {
    accu[key] = 1;
  }
  return accu;
}, {});

const ordered = {};
Object.keys(unordered).sort().forEach(function (key) {
  ordered[key] = unordered[key];
});

const daybreakdown = ordered;


let milebreakdown = keys.reduce((accu, curr) => {
  curr = trips[curr];
  const miles = curr.trip_distance * 0.000621371;
  if (miles < .25) {
    accu['.25'] += 1;
  }
  else if (miles <= .5) {
    accu['.5'] += 1;
  }
  else if (miles <= 1) {
    accu['1'] += 1;
  }
  else if (miles <= 2) {
    accu['2'] += 1;
  }
  else if (miles <= 3) {
    accu['3'] += 1;
  }
  else if (miles <= 10) {
    accu['10'] += 1;
  }
  else if (miles > 10) {
    accu['more'] += 1;
  }
  return accu;
}, {
    '.25': 0,
    '.5': 0,
    '1': 0,
    '2': 0,
    '3': 0,
    '10': 0,
    'more': 0,
  });

let companybreakdown = keys.reduce((accu, curr) => {
  curr = trips[curr];
  accu[curr.company_name] = accu[curr.company_name] + 1 || 1;
  return accu;
}, {});


console.log(JSON.stringify(daybreakdown, null, 2));
console.log(JSON.stringify(milebreakdown, null, 2));
console.log(JSON.stringify(companybreakdown, null, 2));
