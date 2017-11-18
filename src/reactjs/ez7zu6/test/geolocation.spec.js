import { expect } from 'chai';
import * as geolocation from '../src/util/geolocation';

describe('geolocation', () => {
    context('have coordinates', () => {
        it('gets latitude and longitude from position', () => {
            let position = { coords: { latitude: 1, longitude: 2 } };
            let expected = { latitude: 1, longitude: 2 };
            let actual = geolocation.getCoordinatesFromPosition(position);
            expect(actual).to.eql(expected);
        })

        it('gets an address from address components', () => {
            let addressInfo = { streetNumber: '123', streetName: 'Main St', city: 'Somewhere', state: 'CA', country: 'US', zip: '91234' };
            let addressComponents = [
                { types: ['street_number'], long_name: addressInfo.streetNumber },
                { types: ['route'], long_name: addressInfo.streetName },
                { types: ['locality'], long_name: addressInfo.city },
                { types: ['administrative_area_level_1'], short_name: addressInfo.state },
                { types: ['country'], short_name: addressInfo.country },
                { types: ['postal_code'], long_name: addressInfo.zip },
            ]
            let expected = addressInfo;
            let actual = geolocation.getAddressFromComponents(addressComponents);
            expect(actual).to.eql(expected);
        })
    })
})