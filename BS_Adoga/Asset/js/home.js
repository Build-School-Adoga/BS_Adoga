var person_info = document.getElementById("person-info");
var choosing_box = document.getElementById("choosing-box");
var count_person = document.getElementById("count-person");
var travel = document.querySelectorAll('.travel');
var kid_num = document.getElementById("kids-num");

var chooseInfo = document.getElementsByClassName('choose-info');
var showPerson = document.getElementById('final-person');
var showRoom = document.getElementById('final-room');

//debugger;
person_info.addEventListener('click', function () {
    if (choosing_box.style.visibility == "visible") {
        choosing_box.style.visibility = "hidden";
        close_filter();
    }
    else {
        choosing_box.style.visibility = "visible";
    }
})
travel.forEach(item => item.addEventListener('click', function () {
    //加class前先把有onUse的子物件先清掉
    [].forEach.call(travel, function (e) {
        e.classList.remove("onUse");
    })
    on_Use(item);
    debugger;
    if (item.classList.contains("single") || item.classList.contains("couple")) {
        if (item.classList.contains("single")) {
            showPerson.innerHTML = "1位大人";
            showRoom.innerHTML = "1間客房";
        }
        else {
            showPerson.innerHTML = "2位大人";
            showRoom.innerHTML = "1間客房";
        }
        close_filter();
    }
    else {
        open_filter();
        if (item.classList.contains("bussiness")) {
            kid_num.style.visibility = "hidden";
        }
    }
}))

var btnDone = document.getElementById('done-counting');
btnDone.addEventListener('click', function () {
    var active = document.getElementById('choosing-box').getElementsByClassName('travel onUse');
    var r = document.getElementById('room-num').getElementsByTagName('span');
    var a = document.getElementById('adult-num').getElementsByTagName('span');
    var k = document.getElementById('kids-num').getElementsByTagName('span');


    showRoom.innerHTML = r[0].innerText + "间房间";
    debugger;
    showPerson.innerHTML = a[0].innerText + "位大人";
    if (parseInt(k[0].innerText) > 0) {
        showPerson.innerHTML += "," + k[0].innerText + "位兒童";
    }
    close_filter();



});

function on_Use(el) {
    el.classList.add("onUse");
}
function open_filter() {
    count_person.style.visibility = "visible";
    kid_num.style.visibility = "visible";

}
function close_filter() {
    choosing_box.style.visibility = "hidden";
    count_person.style.visibility = "hidden";
    kid_num.style.visibility = "hidden";

}

$(document).click(function (e) {
    e.stopPropagation();
    var container = $(".search-filter-nav");

    //check if the clicked area is dropDown or not
    if (container.has(e.target).length === 0) {
        close_filter();
    }
})


/*遮罩*/
$(function () {
    $('#inputGroupSelect02').click(function () {
        $('.bg').css({ 'display': 'block' });
        $('#inputGroupSelect02').css({ 'display': 'block' });
    });
    $('.bg').click(function () {
        $('.bg').css({ 'display': 'none' });
        $('#inputGroupSelect02').css({});
    });
});
$(function () {
    $('#demo').click(function () {
        $('.bg').css({ 'display': 'block' });
        $('#datetime').css({ 'display': 'block' });
    });
    $('.bg').click(function () {
        $('.bg').css({ 'display': 'none' });
        $('#datetime').css({});
    });
});
$(function () {
    $('#person-info').click(function () {
        $('.bg').css({ 'display': 'block' });
        $('.nav-item').css({ 'display': 'block' });
    });
    $('.bg').click(function () {
        $('.bg').css({ 'display': 'none' });
        $('.nav-item').css({});
    });
});


