var person_info = document.getElementById("person-info");
var choosing_box = document.getElementById("choosing-box");
var count_person = document.getElementById("count-person");
var travel = document.querySelectorAll('.travel');
var kid_num = document.getElementById("kids-num");

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
    if (item.classList.contains("single") || item.classList.contains("couple")) {
        close_filter();
}
    else {
        open_filter();
        if(item.classList.contains("bussiness")){
    kid_num.style.visibility = "hidden";
}
    }
}))

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