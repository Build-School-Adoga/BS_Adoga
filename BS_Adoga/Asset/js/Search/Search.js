import $bus from './SearchDataComponent.js';

Vue.component('filterstar', {
    data() {
        return {
            select:[]
        }
    },
    props: {
        starList: {
            type: Array,
            required: true
        }
    },
    methods: {
        AddStar: function (num) {
            if (checkData(this.select, num)) {
                this.select.push(num);
            }
        },
    },
    //mounted() {
    //    debugger;
    //    console.log(this.select);
    //    $bus.$emit("onStar", this.select);
    //},
    watch: {
        select: function(){
            console.log(this.select);
            debugger;
            $bus.$emit('onStar', this.select);
        }
    },
    template: `<ul id="dropdown-star">
                    <li>星级</li>
                    <li class="dropdown-item" v-for="item in starList">
                        <input class="form-check-input" type="checkbox" :id="item.name" @click="AddStar(item.num)">
                        <label class="form-check-label" :for="item.name">
                            <p v-if="item.num==0">尚未標示</p>
                            <i v-else v-for="s in item.num" class="fas fa-star"></i>
                        </label>
                    </li>
                </ul>`
})

////var starList;
new Vue({
    //el: '#filter-equipment',
    el: '#filter',
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
        star: [
            {
                num: 5,
                name: "five"
            },
            {
                num: 4,
                name: "four"
            },
            {
                num: 3,
                name: "three"
            },
            {
                num: 2,
                name: "two"
            },
            {
                num: 1,
                name: "one"
            },
            {
                num: 0,
                name: "zero"
            }],
        //select: []
    },
    method: {    }
})

function checkData(arraylist, num) {
    console.log(arraylist);
    debugger;
    if (arraylist.length == 0) {
        //初始資料量為0，直接用for會出bug
        return true;
    }
    else {
        //如果判斷發現有重複的就直接移除
        for (var i = 0; i < arraylist.length; i++) {
            if (arraylist[i] == num) {
                arraylist.splice(i, 1);
                //debugger;
                return false;
            }
        }
        //debugger;
        return true;
    }
    
}

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

var btnStar = document.getElementById("filter-star");
var ulStar = document.getElementById("dropdown-star");

btnStar.addEventListener('click', function () {
    if (ulStar.style.display == "none") {
        ulStar.style.display = "block";
    }
    else {
        ulStar.style.display = "none";
    }
})
//debugger;

