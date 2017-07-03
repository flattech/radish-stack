function showGoogleMeeraMap() {
    var currentmap = $("#mapgps");
    var latinput = $("#LocationLat");
    var longinput = $("#LocationLong");
    var zoominput = $("#LocationZoom");
    var addresstxt = $("#txtaddress");
    function showAddress(marker) {
        var latlong = marker.getPosition();
       
        latinput.val(latlong.lat());
        longinput.val(latlong.lng());
        currentmap.gmap3({
            getaddress: {
                latLng: marker.getPosition(),
                callback: function (results) {

                    var map = currentmap.gmap3("get"),
                        infowindow = currentmap.gmap3({ get: "infowindow" }),
                        content = results && results[1] ? results
                            && results[1].formatted_address : "no address";
                    zoominput.val(map.getZoom());
                    if (infowindow) {
                        infowindow.open(map, marker);
                        infowindow.setContent(content);
                    } else {
                        currentmap.gmap3({
                            infowindow: {
                                anchor: marker,
                                options: { content: content }
                            }
                        });
                    }
                }
            }
        });
    }
    function initMap(latLng) {
        addresstxt.prop('disabled', false);
        // Create marker 
        //var marker = new google.maps.Marker({
        //    map: map,
        //    position: new google.maps.LatLng(53, -2.5),
        //    title: 'Some location'
        //});

        //// Add circle overlay and bind to marker
        //var circle = new google.maps.Circle({
        //    map: map,
        //    radius: 16093,    // 10 miles in metres
        //    fillColor: '#AA0000'
        //});
        //circle.bindTo('center', marker, 'position');

        currentmap.gmap3({
            marker: {
                latLng: latLng,
                options: {
                    draggable: true
                },
                events: {
                    dragend: function (marker) {
                        showAddress(marker);
                    }, click: function (marker, event, context) {
                        showAddress(marker);
                    },
                }
            },
            map: {
                options: {
                    zoom: parseFloat((!latinput.val() || latinput.val() == 0) ? 3 : 13),
                    center: latLng
                }
            }
        });
    }
    var defualtMarker = [!latinput.val() || latinput.val() == 0 ? 34.17145093834947 : latinput.val(), !longinput.val() || longinput.val() == 0 ? 35.87841796875 : longinput.val()];
    initMap(defualtMarker);
    addresstxt.autocomplete({
        source: function () {
            currentmap.gmap3({
                getaddress: {
                    address: $(this).val(),
                    callback: function (results) {
                        if (!results) return;
                        addresstxt.autocomplete("display", results, false);
                    }
                }
            });
        },
        cb: {
            cast: function (item) {
                return item.formatted_address;
            },
            select: function (item) {
                currentmap.gmap3({
                    clear: {
                        name: ["marker"],
                        last: true
                    }
                });
                initMap(item.geometry.location);
            }
        }
    })
    .focus();
    return false;
}

