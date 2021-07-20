import component from './SearchFacilitiesComponent.js'

var allHotel = '';

//axios去get資料先
axios.get('https://localhost:44352/api/Search/GetHotelFromCity', {
    //一開始用params裡面的資料去跑Api抓資料；成功抓完資料就會跑response
    params: {
        CityName: filternav.,
        startDate: filternav.startDate,
        endDate: filternav.endDate,
        orderRoom: filternav.room,
        adult: filternav.adult,
        child: filternav.kids
    }
}).then(function (response) {
    console.log(response.data);
    allHotel = response.data;

}).catch((error) => console.log(error))

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