const axios = require('axios');
const moment = require('moment');
const turf = require('@turf/turf');

const settings = require('./appsettings.json');

async function getBirdInfo(settings) {
  const limit = 500;
  let offset = 0;
  let availability = [];
  let res = {};

  const url = `${settings.BaseUrl}availability`;

  do {
    res = await axios.get(url, {
      params: {
        limit,
        offset,
      },
      headers: settings.Headers
    });

    availability = availability.concat(res.data.availability);

    offset += res.data.availability.length;
  } while (res.data.availability.length != 0);

  return availability;
}

async function getLimeInfo(settings) {
  let page = 0;
  let res = {};
  let availability = [];

  const url = `${settings.BaseUrl}availability`;

  do {
    res = await axios.get(url, {
      params: {
        page,
      },
      headers: settings.Headers
    });

    availability = availability.concat(res.data.data);

    page += 1;
  } while (res.data.data.length != 0);

  return availability;
}

async function getSkipInfo(settings) {
  let availability = [];

  const url = `${settings.BaseUrl}availability.json`;

  let res = await axios.get(url, {
    headers: settings.Headers
  });

  availability = availability.concat(res.data.reduce(
    (accu, curr) => {
      accu.push(curr);
      return accu;
    }, [])
  );

  return availability;
}

const bird = getBirdInfo(settings.CompanySettings.Bird);
const lime = getLimeInfo(settings.CompanySettings.Lime);
const skip = getSkipInfo(settings.CompanySettings.Skip);

let totalavailability = [];

var fs = require('fs');
var fileStream = fs.createWriteStream('availability.json');
let returns = 0;

function callback() {
  returns += 1;

  if (returns == 3) {
    fileStream.write(JSON.stringify(totalavailability));
    fileStream.close();
  }
}

bird.then((availability) => {
  totalavailability = totalavailability.concat(availability);
  console.log('Done reading Bird.');
  callback();
})
  .catch((err) => {
    console.error(JSON.stringify(err));
  });
lime.then((availability) => {
  totalavailability = totalavailability.concat(availability);
  console.log('Done reading Lime.');
  callback();
})
  .catch((err) => {
    console.error(JSON.stringify(err));
  });
skip.then((availability) => {
  totalavailability = totalavailability.concat(availability);
  console.log('Done reading Skip.');
  callback();
})
  .catch((err) => {
    console.error(JSON.stringify(err));
  });

