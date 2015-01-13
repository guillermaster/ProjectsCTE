//script.js

var infos = [];
var loading = false;
var displayCount = 0;

function Info(infoArray) {
    var infoVars = ['id', 'latitude', 'longitude', 'location_name', 'text'];

    for (var i = 0; i < infoVars.length; i++) {
        this[infoVars[i]] = infoArray[i];
    }
}


Info.prototype.lastStatus = function () {
    var myDiv = document.createElement('div');
    myDiv.innerHTML = this.location_name + "-" + this.text;
    myDiv.id = "lastStatus";

    return myDiv;
}

Info.prototype.infoWindow = function () {
    var myInfoWindow = document.createElement('div');
    myInfoWindow.id = "infoWindow";
    myInfoWindow.appendChild(this.lastStatus());

    return myInfoWindow;
}

function loadData() {
    if (!loading) {
        new Ajax.Request('querymapdata.aspx', {
            method: 'post',
            onCreate: function () { loading = true; },
            onSuccess: function (transport) { addInfos(transport.responseText); },
            onComplete: function () { loading = false; }
        });
    }
}

function showInfos() {
    var currentInfo = infos.pop();
    if (currentInfo && currentInfo.show) {
        currentInfo.show();
        displayCount++;
    }
    if (infos.length == 0)
        loadData();

    setTimeout("showInfos();", 500);
}

function addInfos(responseText) {
    var newInfos = eval(responseText);
    if (newInfos.length == 0)
        alert("No Infos Available!");
    else {
        for (i = 0; i < newInfos.length; i++) {
            infos.push(new Info(newInfos[i]));
        }
    }
    if (displayCount == 0)
        showInfos();
}

var map;
var userMarker;
var marker;
var markers = [];

var map_icon = addOptionsToIcon(new GIcon(), { iconSize: new GSize(48, 48), iconAnchor: new GPoint(18, 43), infoWindowAnchor: new GPoint(45, 20), image: '/images/icon.png', shadow: '/images/birdshadow.png' });

// add options to a GMaps icon
function addOptionsToIcon(icon, options) {
    for (var k in options) {
        icon[k] = options[k];
    }
    return icon;
}

function loadMap() {
    var w = 800; // Width of map container
    map = new GMap2(document.getElementById("map"), { mapTypes: [G_PHYSICAL_MAP] });
    map.setCenter(new GLatLng(39, 76), w > 700 ? 5 : 4);
    map.addControl(new GSmallMapControl());
}

Info.prototype.show = function () {
    // clear marker
    //map.closeInfoWindow();
    if (markers[this.id])
        map.removeOverlay(markers[this.id]);

    // show marker with infoWindow
    //markers[this.id] = new GMarker(new GLatLng(this.latitude,this.longitude), { icon : map_icon });
    markers[this.id] = new GMarker(new GLatLng(this.latitude, this.longitude));
    map.addOverlay(markers[this.id]);
    map.openInfoWindow(markers[this.id].getLatLng(), this.infoWindow(), { noCloseOnClick: true, pixelOffset: new GSize(15, -22) });
}

addEvent(window, 'load', init);

function init() {
    if (GBrowserIsCompatible()) {
        loadMap();
        loadData();
    } else {
        alert("Sorry, your browser isn't compatible with Google Maps!")
    }
}

function addEvent(obj, evType, fn) {
    if (obj.addEventListener) {
        obj.addEventListener(evType, fn, false);
        return true;
    } else if (obj.attachEvent) {
        var r = obj.attachEvent("on" + evType, fn);
        return r;
    } else {
        return false;
    }
}