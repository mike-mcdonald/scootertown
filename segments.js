const segments = require('./street_segments.json');
const turf = require('@turf/turf');
const fs = require('fs');

let collection = turf.featureCollection(segments.features);
let features = [];
const radius = 100; // feet
const mileConversionConstant = 0.000189394;

collection.features.forEach((feature) => {
  const length = turf.length(feature.geometry, { units: 'meters' });
  const midpoint = turf.along(feature.geometry, length / 2, { units: 'meters' });
  const buffer = turf.circle(midpoint.geometry.coordinates, radius * mileConversionConstant, { steps: 8, units: 'miles' });
  features.push(
    turf.feature(
      buffer.geometry,
      {
        name: feature.properties.FULL_NAME,
        leftadd1: feature.properties.LEFTADD1,
        leftadd2: feature.properties.LEFTADD2,
        rgtadd1: feature.properties.RGTADD1,
        rgtadd2: feature.properties.RGTADD2,
        objectId: feature.properties.OBJECTID,
      }
    )
  );
});

collection = turf.featureCollection(features);

fs.writeFileSync('street_segment_midpoints.json', JSON.stringify(collection));
