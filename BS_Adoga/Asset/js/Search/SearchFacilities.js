import card from './SearchFacilitiesComponent.js'

var allHotel = '';


//axios去get資料先
debugger;
axios.get('https://localhost:44352/api/Search/GetHotelByCity', {
    //一開始用params裡面的資料去跑Api抓資料；成功抓完資料就會跑response
    params: {
        CityName: filternav.Value,
        startDate: filternav.startDate,
        endDate: filternav.endDate,
        adult: filternav.adult,
        kid: filternav.kids,
        room: filternav.room,
    }
}).then(function (response) {
    debugger;
    allHotel = response.data;
    console.log(allHotel);
    //console.log(response.data);
    var list = new Vue({
        el: "#new_card",
        data: {
            page: 1,
            hotelList: allHotel
        },
        components: {
            'hotel-card':card
        }
    });
    debugger;
}).catch((error) => console.log(error))



function getCurrentBindData(){
    return list.$data.hotelList;
}

function DataBind(rec) {
    //把最新的資料更新
    list.$data.hotelList = rec;
}

function DataPrev() {
    var tmp = getCurrentBindData();
    var i = allHotel.indexOf(tmp);

    if (i > 0) {
        DataBind(allHotel[i - 1]);
    }
}

function NextPage() {
    var tmp = GetCurrentBindData(); //抓當前的資料出來
    var i = allHotel.indexOf(tmp); //判斷排在第幾

    //防呆
    if (i < allHotel.length - 1) {
        DataBind(allHotel[i + 1]);
    }
}

$(document).ready(function () {
    //Binding();
    //$('#orderPrice').click(OrderPrice);
    //$('#orderStar').click(OrderStar);
    //$('#next').click(NextPage);

});


//var filterEquip = new Vue({
//    el: '#filter-equipment',
//    data: {
//        HotelEquipName: {
//            swim: '游泳池',
//            airport: '機場接送',
//            familyFriendly: '親子友善住宿',
//            restaurant: '餐廳',
//            club: '夜店',
//            golf: '附設高爾夫球場',
//            gym: '健身房',
//            nSmoke: '禁菸',
//            Smoke: '吸菸區',
//            FFG: '無障礙友善設施',
//            carPark: '停車場',
//            frontDesk: '24小時櫃台服務',
//            Spa: 'Spa桑拿',
//            business: '商務設備'
//        },
//        RoomEquipName: {
//            internet: '網路',
//            petAllow: '可帶寵物'
//        }
//    },
//    component: {
        
//    },
//    method: {

//    }
//})