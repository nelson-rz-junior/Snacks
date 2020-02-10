
var setMeioMask = function (jSelector) {
    jSelector.setMask({
        mask: "99,999.999.999.999.999.9",
        type: "reverse",
        defaultValue: "000",
        autoTab: false
    });

    jSelector.on("drop", function (e) {
        return false;
    });
};

$(document).ready(function () {
    setMeioMask($("#snackPrice"));
});