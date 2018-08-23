const moment = require('moment');
const axios = require('axios');

let totalTrips = [];

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function analyzeTrips(trips) {
    trips = trips.reduce((accu, curr) => {
        const today = moment().local().startOf('day');
        const endDay = moment.unix(curr.end_time).local();

        if (curr.trip_duration >= 60) {
            accu.push(curr);
        }
        return accu;
    }, []);
    console.log(`Total trips: ${trips.length}`);

    let miles = trips.reduce((accu, curr) => {
        const tripLength = curr.trip_distance * 0.00062137; // meters to miles
        return accu += tripLength;
    }, 0);

    console.log(`Total miles: ${miles} miles`);
    console.log(`Average trip length: ${miles / trips.length} miles`);

    let milesbreakdown = trips.reduce((accu, curr) => {
        const key = `${Math.round(curr.trip_distance * 0.00062137).toString()} miles`;
        if(accu[key]) {
            accu[key] += 1
        }
        else {
            accu[key] = 1;
        }
        return accu;
    }, {});
    console.log(`Miles breakdown: ${JSON.stringify(milesbreakdown, null, 2)}`);

    let max = trips.reduce((accu, curr) => {
        if (curr.trip_distance * 0.00062137 > accu) {
            return curr.trip_distance * 0.00062137;
        }
        return accu;
    }, 0);
    console.log(`Max distance travelled: ${max} miles`);
    max = trips.reduce((accu, curr) => {
        if (curr.trip_duration > accu) {
            return curr.trip_duration;
        }
        return accu;
    }, 0);
    console.log(`Max time travelled: ${max / 60} minutes`);

    let short_trips = trips.reduce((accu, curr) => {
        if (curr.trip_duration <= 60) {
            accu += 1;
        }
        return accu;
    }, 0);
    console.log(`Minute or less trips: ${short_trips}`);

    short_trips = trips.reduce((accu, curr) => {
        if (curr.trip_duration <= 30) {
            accu += 1;
        }
        return accu;
    }, 0);
    console.log(`Thirty seconds or less trips: ${short_trips}`);

    let scooters = trips.reduce((accu, curr) => {
        if (accu[curr.device_id]) {
            accu[curr.device_id] += 1;
        }
        else {
            accu[curr.device_id] = 1;
        }
        return accu;
    }, {});
    console.log(`Total scooters used: ${Object.keys(scooters).length}`);
}

async function getBirdInfo() {
    console.log('--BIRD--');
    let trips = [];
    let offset = 0;
    const limit = 500;
    while (true) {
        let res = await axios.get('https://gbfs.bird.co/portland/trips', {
            params: {
                offset,
                limit,
            },
            headers: {
                'Authorization': 'Bird eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJBVVRIIiwidXNlcl9pZCI6ImI3YmFlZTZjLTZhOGMtNDI1NC1hZjFhLTU3NDVjYzExMmFjZiIsImRldmljZV9pZCI6ImViYjMyZjJlLTg3OTktNGQ5My04OWQ4LTFjYThhZDgyYzg1MiIsImV4cCI6MTU2NDYyNTYwNn0.K_1gnC8KvwmB1cUuJWH0QJB0LuX3DEATk_-AmkckkSA',
                'APP-Version': '3.0.0',
            },
        }).catch((err) => {
            console.log(err.message);
        });
        trips = trips.concat(res.data.trips);
        if (res.data.trips.length === 0) {
            break;
        }
        offset += res.data.trips.length;
    }
    totalTrips = totalTrips.concat(trips);
    //analyzeTrips(trips);
}

async function getLimeInfo() {
    console.log('--LIME--');
    let trips = [];
    let page = 1;
    while (true) {
        let res = await axios.get('https://lime.bike/api/partners/v1/pbot/trips', {
            params: {
                page
            },
        }).catch((err) => {
            console.log(err.message);
            res.data.data = [];
        });
        trips = trips.concat(res.data.data);
        if (res.data.data.length === 0) {
            break;
        }
        page += 1;
    }
    totalTrips = totalTrips.concat(trips);
    //analyzeTrips(trips);
}

async function getSkipInfo() {
    console.log('--SKIP--');
    let res = await axios.get('https://us-central1-waybots-production.cloudfunctions.net/pdx/trips.json', {
        headers: {},
    }).catch((err) => {
        console.log(err.message);
    });
    totalTrips = totalTrips.concat(res.data);
    //analyzeTrips(res.data);
}

getBirdInfo().then(() => {
    getLimeInfo().then(() => {
        getSkipInfo().then(() => {
            console.log('--OVERALL--');
            analyzeTrips(totalTrips);
        })
        .catch((err) => {
            console.log(err);
        });;
    }).catch((err) => {
        console.log(err);
    });;
})
.catch((err) => {
    console.log(err);
});
