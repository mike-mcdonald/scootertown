const segments = require('./bike_paths.json');
const turf = require('@turf/turf');
const fs = require('fs');

let collection = turf.featureCollection(segments.features);
let features = [];
const radius = 100; // feet
const mileConversionConstant = 0.000189394; // feet to miles

const statusMap = {
  'ACTIVE': 'active',
  'PLANNED': 'planned',
  'RECOMM': 'recommended'
}

collection.features.forEach((feature) => {
  // only keep those bike paths that are either existing or possible in the future
  if (['ACTIVE', 'PLANNED', 'RECOMM'].includes(feature.properties.Status)) {
    const length = turf.length(feature.geometry, { units: 'meters' });
    const midpoint = turf.along(feature.geometry, length / 2, { units: 'meters' });
    const buffer = turf.circle(midpoint.geometry.coordinates, radius * mileConversionConstant, { steps: 8, units: 'miles' });
    features.push(
      turf.feature(
        buffer.geometry,
        {
          name: feature.properties.SegmentName,
          status: statusMap[feature.properties.Status],
          type: feature.properties.Facility,
          objectId: feature.properties.OBJECTID,
          tranPlanID: feature.properties.TranPlanID,
        }
      )
    );
  }
});

collection = turf.featureCollection(features);

fs.writeFileSync('bike_path_midpoints.json', JSON.stringify(collection));
