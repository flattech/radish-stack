
$(function () {
    var markers1 = [{ "data": "33.8848528571429,35.5022322857143", "latLng": [33.884852857142853, 35.502232285714285], "Time": "06:26" }, { "data": "33.884646,35.506193", "latLng": [33.884646, 35.506193], "Time": "06:32" }, { "data": "33.884591,35.506335", "latLng": [33.884591, 35.506335], "Time": "06:33" }, { "data": "33.88425,35.505051", "latLng": [33.88425, 35.505051], "Time": "06:34" }, { "data": "33.884853,35.504163", "latLng": [33.884853, 35.504163], "Time": "06:35" }, { "data": "33.888086,35.504546", "latLng": [33.888086, 35.504546], "Time": "06:36" }, { "data": "33.888028,35.508713", "latLng": [33.888028, 35.508713], "Time": "06:37" }, { "data": "33.882453,35.509698", "latLng": [33.882453, 35.509698], "Time": "06:42" }, { "data": "33.882476,35.50966", "latLng": [33.882476, 35.50966], "Time": "06:44" }, { "data": "33.881036,35.510331", "latLng": [33.881036, 35.510331], "Time": "06:45" }, { "data": "33.878685,35.51115", "latLng": [33.878685, 35.51115], "Time": "06:46" }, { "data": "33.882541,35.505655", "latLng": [33.882541, 35.505655], "Time": "06:52" }, { "data": "33.88257,35.502886", "latLng": [33.88257, 35.502886], "Time": "06:54" }, { "data": "33.882981,35.502526", "latLng": [33.882981, 35.502526], "Time": "06:55" }, { "data": "33.881881,35.501598", "latLng": [33.881881, 35.501598], "Time": "06:56" }, { "data": "33.8801643333333,35.501075", "latLng": [33.880164333333333, 35.501075000000007], "Time": "06:57" }, { "data": "33.84362,35.493721", "latLng": [33.84362, 35.493721], "Time": "07:02" }, { "data": "33.831993,35.500381", "latLng": [33.831993, 35.500381], "Time": "07:04" }, { "data": "33.823008,35.50224", "latLng": [33.823008, 35.50224], "Time": "07:05" }, { "data": "33.811385,35.493365", "latLng": [33.811385, 35.493365], "Time": "07:06" }, { "data": "33.797416,35.482441", "latLng": [33.797416, 35.482441], "Time": "07:08" }, { "data": "33.798735,35.49626", "latLng": [33.798735, 35.49626], "Time": "07:13" }, { "data": "33.798243,35.493701", "latLng": [33.798243, 35.493701], "Time": "07:14" }, { "data": "33.79687,35.493606", "latLng": [33.79687, 35.493606], "Time": "07:15" }, { "data": "33.796776,35.493521", "latLng": [33.796776, 35.493521], "Time": "07:16" }, { "data": "33.796015,35.494021", "latLng": [33.796015, 35.494021], "Time": "07:17" }];
    var markers2 = [{ "data": "33.79664735,35.49382995", "latLng": [33.796647350000015, 35.49382995], "Time": "06:26" }, { "data": "33.799075,35.490685", "latLng": [33.799075, 35.490685], "Time": "06:28" }, { "data": "33.799026,35.490035", "latLng": [33.799026, 35.490035], "Time": "06:29" }, { "data": "33.798865,35.489311", "latLng": [33.798865, 35.489311], "Time": "06:30" }, { "data": "33.800078,35.488518", "latLng": [33.800078, 35.488518], "Time": "06:31" }, { "data": "33.797533,35.484761", "latLng": [33.797533, 35.484761], "Time": "06:32" }, { "data": "33.797076,35.483968", "latLng": [33.797076, 35.483968], "Time": "06:33" }, { "data": "33.806955,35.490146", "latLng": [33.806955, 35.490146], "Time": "06:38" }, { "data": "33.80984,35.492385", "latLng": [33.80984, 35.492385], "Time": "06:39" }, { "data": "33.813931,35.495538", "latLng": [33.813931, 35.495538], "Time": "06:40" }, { "data": "33.819411,35.499683", "latLng": [33.819411, 35.499683], "Time": "06:41" }, { "data": "33.826113,35.504576", "latLng": [33.826113, 35.504576], "Time": "06:42" }, { "data": "33.878963,35.495293", "latLng": [33.878963, 35.495293], "Time": "06:48" }, { "data": "33.881355,35.49605", "latLng": [33.881355, 35.49605], "Time": "06:49" }, { "data": "33.882068,35.496571", "latLng": [33.882068, 35.496571], "Time": "06:50" }, { "data": "33.889536,35.500678", "latLng": [33.889536, 35.500678], "Time": "06:51" }, { "data": "33.889401,35.501535", "latLng": [33.889401, 35.501535], "Time": "06:52" }];
    var poly = [];
    function initialize() {
        var mapOptions = {
            zoom: 12,
            center: new google.maps.LatLng(markers1[0].latLng[0], markers1[0].latLng[1]),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        var map = new google.maps.Map(document.getElementById('map-canvas'),
            mapOptions);
        var marker1, marker2;
        function drawline(map, m, m2, color, first) {
            //setTimeout(function () {
           
            if (!first) {
                if (marker2)
                    marker2.setMap(null);
                
                marker2 = new google.maps.Marker({
                    position: new google.maps.LatLng(m2.latLng[0], m2.latLng[1]),
                    map: map, icon: 'http://i.stack.imgur.com/rU427.png',
                    title: ''
                });
            } else {
                if (marker1)
                    marker1.setMap(null);
                marker1 = new google.maps.Marker({
                    position: new google.maps.LatLng(m2.latLng[0], m2.latLng[1]),
                    map: map,
                    title: ''
                });
            }
            var flightPath = new google.maps.Polyline({
                path: [new google.maps.LatLng(m.latLng[0], m.latLng[1]), new google.maps.LatLng(m2.latLng[0], m2.latLng[1])],
                geodesic: true,
                strokeColor: color,
                strokeOpacity: 1,
                strokeWeight: 3
            });

            flightPath.setMap(map);
            poly.push(flightPath);
           //map.setCenter(new google.maps.LatLng(m2.latLng[0], m2.latLng[1]));
            // }, timeout);
        }

        function cleanPoly() {
            for (var i = 0; i < poly.length; i++) {
                poly[i].setMap(null);
            }
        }
        
        $("#range_4").ionRangeSlider({
            // type: "double",
            //   grid: true,
            //  grid_num: 1,
            // from: 6.10, to: 7.45,
            from:26,
            keyboard:true,keyboard_step:1,
            grid: true, prettify: function (num) {
                // var nn = (num + "").replace("6.", "").replace("7.", "");
                // if (nn % 5 != 0)
                //   return "";
                if ((num + "").length == 3) {
                    return  (num + "").replace(".", ":") + "0";
                }
                return (num + "").replace(".", ":");//.replace("6:", ":").replace("7:", ":");
            },
            values: [6.00, 6.01, 6.02, 6.03, 6.04, 6.05, 6.06, 6.07, 6.08, 6.09, 6.10, 6.11, 6.12, 6.13, 6.14, 6.15, 6.16, 6.17, 6.18, 6.19, 6.20, 6.21, 6.22, 6.23, 6.24, 6.25, 6.26, 6.27, 6.28, 6.29, 6.30, 6.31, 6.32, 6.33, 6.34, 6.35, 6.36, 6.37, 6.38, 6.39, 6.40, 6.41, 6.42, 6.43, 6.44, 6.45, 6.46, 6.47, 6.48, 6.49, 6.50, 6.51, 6.52, 6.53, 6.54, 6.55, 6.56, 6.57, 6.58, 6.59, 7.00, 7.01, 7.02, 7.03, 7.04, 7.05, 7.06, 7.07, 7.08, 7.09, 7.10, 7.11, 7.12, 7.13, 7.14, 7.15, 7.16, 7.17, 7.18, 7.19, 7.20, 7.21, 7.22, 7.23, 7.24, 7.25, 7.26, 7.27, 7.28, 7.29, 7.30, 7.31, 7.32, 7.33, 7.34, 7.35, 7.36, 7.37, 7.38, 7.39, 7.40, 7.41, 7.42, 7.43, 7.44, 7.45, 7.46, 7.47, 7.48, 7.49, 7.50, 7.51, 7.52, 7.53, 7.54, 7.55, 7.56, 7.57, 7.58, 7.59, 8.00]
        });
        function drawMarkers($this,mar,color,first) {
           
            var value = $this.prop("value");
            var newvalue = value.replace(".", ":");
            if (newvalue.length === 1)
                newvalue = "0" + newvalue + ":00";
            if (newvalue.length === 3)
                newvalue = "0" + newvalue + "0";
            if (newvalue.length === 4)
                newvalue = "0" + newvalue;
            //console.log(newvalue);
            var filtered = mar.filter(function (x) {
                if (Date.parse('01/01/2011 ' + newvalue) > Date.parse('01/01/2011 ' + x.Time)) {
                    return true;
                }
                return false;
            });
            //console.log(filtered.length);
            for (var i = 0; i < filtered.length; i++) {
                //timeout = i * 800;
                var m1 = filtered[i];
                var m2;
                if (i + 1 < filtered.length)
                    m2 = filtered[i + 1];
                else {
                    m2 = m1;
                }
                drawline(map, m1, m2, color, first);
            
            }
        }
        $("#range_4").on("change", function () {
            cleanPoly();
            drawMarkers($(this), markers1, '#FF0000',true);
            drawMarkers($(this), markers2, 'green');
        });
      
    }
    google.maps.event.addDomListener(window, 'load', initialize);
  

});