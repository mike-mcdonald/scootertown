
const axios = require('axios');
const moment = require('moment');
const turf = require('@turf/turf');

const settings = require('./appsettings.json');

async function getBirdInfo(settings) {
  const limit = 500;
  let offset = 0;
  let trips = [];
  let res = {};

  const url = `${settings.BaseUrl}trips`;

  do {
    res = await axios.get(url, {
      params: {
        limit,
        offset,
      },
      headers: settings.Headers
    });

    trips = trips.concat(res.data.trips);

    offset += res.data.trips.length;
  } while (res.data.trips.length != 0);

  return trips;
}

async function getLimeInfo(settings) {
  let page = 1;
  let res = {};
  let trips = [];

  const url = `${settings.BaseUrl}trips`;

  do {
    res = await axios.get(url, {
      params: {
        page,
      },
      headers: settings.Headers
    });

    trips = trips.concat(res.data.data);

    page += 1;
  } while (res.data.data.length != 0);

  return trips;
}

async function getSkipInfo(settings) {
  let trips = [];

  const url = `${settings.BaseUrl}trips.json`;

  let res = await axios.get(url, {
    headers: settings.Headers
  });

  trips = trips.concat(res.data.reduce(
    (accu, curr) => {
      curr.start_point.coordinates = curr.start_point.coordinates.reverse();
      accu.push(curr);
      return accu;
    }, [])
  );

  return trips;
}

const bird = getBirdInfo(settings.CompanySettings.Bird);
const lime = getLimeInfo(settings.CompanySettings.Lime);
const skip = getSkipInfo(settings.CompanySettings.Skip);

let totalTrips = [];

var fs = require('fs');
var fileStream = fs.createWriteStream('trips.json');
let returns = 0;

function callback() {
  returns += 1;

  if (returns == 3) {
    fileStream.write(JSON.stringify(totalTrips));
    fileStream.close();
  }
}

bird.then((trips) => {
  totalTrips = totalTrips.concat(trips);
  console.log('Done reading Bird.');
  callback();
});
lime.then((trips) => {
  totalTrips = totalTrips.concat(trips);
  console.log('Done reading Lime.');
  callback();
});
skip.then((trips) => {
  totalTrips = totalTrips.concat(trips);
  console.log('Done reading Skip.');
  callback();
});
