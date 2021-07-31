
var filterEquip = new Vue({
    el: '#filter-equipment',
    data: {
        FacilityList: [
            {
                hotelFacilities: [
                    {
                        facility: "swim",
                        facilityName: "游泳池",
                        haveFacility: false
                    },
                    {
                        facility: "airport",
                        facilityName: "機場接送",
                        haveFacility: false
                    },
                    {
                        facility: "familyFriendly",
                        facilityName: "親子友善住宿",
                        haveFacility: false
                    },
                    {
                        facility: "restaurant",
                        facilityName: "餐廳",
                        haveFacility: false
                    },
                    {
                        facility: "club",
                        facilityName: "夜店",
                        haveFacility: false
                    },
                    {
                        facility: "golf",
                        facilityName: "附設高爾夫球場",
                        haveFacility: false
                    },
                    {
                        facility: "gym",
                        facilityName: "健身房",
                        haveFacility: false
                    },
                    {
                        facility: "nSmoke",
                        facilityName: "禁菸區",
                        haveFacility: false
                    },
                    {
                        facility: "Smoke",
                        facilityName: "吸菸區",
                        haveFacility: false
                    },
                    {
                        facility: "FFG",
                        facilityName: "無障礙友善設施",
                        haveFacility: false
                    },
                    {
                        facility: "carPark",
                        facilityName: "停車場",
                        haveFacility: false
                    },
                    {
                        facility: "frontDesk",
                        facilityName: "24小時櫃台服務",
                        haveFacility: false
                    },
                    {
                        facility: "Spa",
                        facilityName: "Spa桑拿",
                        haveFacility: false
                    },
                    {
                        facility: "business",
                        facilityName: "商務設備",
                        haveFacility: false
                    }
                ]
            },
            {
                roomFacilities: [
                    {
                        facility: "internet",
                        facilityName: "網路",
                        haveFacility: false
                    },
                    {
                        facility: "petAllow",
                        facilityName: "可帶寵物",
                        haveFacility: false
                    }
                ]
            }
        ],
        StarList: [5, 4, 3, 2, 1, 0],
        //priceRange: [ ]
    },
    method: {
        
    }
})


// JS
var openFilter = document.getElementById("openFilter");
var filter = document.getElementById("filter-equipment");
// var closeFilter = document.getElementsByClassName("closeFilter");

openFilter.addEventListener('click', function () {
    if (filter.style.visibility == "visible") {
        filter.style.visibility = "hidden";
    }
    else {
        filter.style.visibility = "visible";
        //document.getElementsByTagName("body").preventDefault();
    }
})

function closeFilter() {
    if (filter.style.visibility == "visible") {
        filter.style.visibility = "hidden";
    }
}

function dropright() {
    var droprightBox = document.getElementsByClassName("dropdown-menu");
    // debugger;
    // droprightBox.style.visibility = "visible";

    //droprightBox.addEventListener('click', function () {

    //})
        //下拉框查询组件点击查询栏时不关闭下拉框
        //需要在tag裡加入data-stopPropagation="true"才能執行成功
        event.stopPropagation();

}

//var dropdownItem = document.querySelectorAll('.dropdown-item');
//dropdownItem.forEach(item => item.getElementsByClassName("fas fa-minus").addEventListener('click', function () {
//    debugger;
//    alert("Minus");
//}))
//dropdownItem.forEach(item => item.addEventListener('click', function (e) {
//    // e.stopPropagation();

//}))


