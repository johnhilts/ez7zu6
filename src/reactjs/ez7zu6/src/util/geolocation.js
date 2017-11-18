export const getCoordinatesFromPosition = (position) => {
    return { latitude: position.coords.latitude, longitude: position.coords.longitude };
}

export const getAddressFromComponents = (addressComponents) => {
    return addressComponents.reduce((previous, current) => {
        switch (current.types[0]) {
            case "street_number":
                previous.streetNumber = current.long_name;
                break;
            case "route":
                previous.streetName = current.long_name;
                break;
            case "locality":
                previous.city = current.long_name;
                break;
            case "administrative_area_level_1":
                previous.state = current.short_name;
                break;
            case "country":
                previous.country = current.short_name;
                break;
            case "postal_code":
                previous.zip = current.long_name;
                break;
        }
        return previous;
    }, {})
}