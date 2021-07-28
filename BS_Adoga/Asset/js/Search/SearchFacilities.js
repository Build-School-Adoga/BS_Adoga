import cardComponent from './SearchFacilitiesComponent.js'

//import { forEach } from "../../StartbootstrapAdminPages/vendor/fontawesome-free/js/v4-shims";

var allHotel = '';
var list;

//axios去get資料先
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
    allHotel = response.data;
    var btnPrev = document.getElementById("Prev");
    var btnNext = document.getElementById("Next");
    
    //debugger;
    var startDate = DateFormat(filternav.startDate);
    var endDate = DateFormat(filternav.endDate);

    //var pageBegin = 1;
    //var pageEnd = pageBegin + 1;

    list = new Vue({
        el: "#new_card",
        data: {
            //pagebegin: 0,
            //nextpage: 1,
            hotelList: allHotel,
            filternav: {
                city: filternav.Value,
                start: startDate,
                end: endDate,
                adult: filternav.adult,
                kid: filternav.kids,
                room: filternav.room,
            }
        },
        components: {
            'hotel-card': cardComponent
        },
        methods: {
            prevPage: function () {
                if (pageBegin > 1) {
                    pageBegin -= 1;
                }
                else { alert("First page"); }
            },
            nextPage: function () {
                if (page >= 5) {
                    alert("Final Page");
                }
                else {
                    nextPage += 1;
                }
            }
        }
    })

    Vue.component('paginated-list', {
        data() {
            return {
                pageNumber: 0
            }
        },
        props: {
            listData: {
                type: Array,
                required: true
            },
            size: {
                type: Number,
                required: false,
                default: 10
            }
        },
        methods: {
            nextPage() {
                this.pageNumber++;
            },
            prevPage() {
                this.pageNumber--;
            }
        },
        computed: {
            pageCount() {
                let l = this.listData.length,
                    s = this.size;
                return Math.ceil(l / s);
            },
            paginatedData() {
                const start = this.pageNumber * this.size,
                    end = start + this.size;
                return this.listData.slice(start, end);
            }
        },
        template: `<div>
              <hotel-card v-for="hotel in hotelList" :hcard="hotel" :fnav="filternav"></hotel-card>
              <button 
                  :disabled="pageNumber === 0" 
                  @click="prevPage">
                  Previous
              </button>
              <button 
                  :disabled="pageNumber >= pageCount -1" 
                  @click="nextPage">
                  Next
              </button>
             </div>
  `
    });
    function DateFormat(date) {
        var date = new Date(date);
        return date.getFullYear() + '-' + (date.getMonth() + 1).toString().padStart(2, '0') + '-' + date.getDate().toString().padStart(2, '0');
    }

}).catch((error) => console.log(error))

//var a =axios.get('https://localhost:44352/api/Search/GetHotelByCity', {
//    //一開始用params裡面的資料去跑Api抓資料；成功抓完資料就會跑response
//    params: {
//        CityName: filternav.Value,
//        startDate: filternav.startDate,
//        endDate: filternav.endDate,
//        adult: filternav.adult,
//        kid: filternav.kids,
//        room: filternav.room,
//    }
//}).then(function (response) {
//    return [response.data];
//}).catch((error) => console.log(error))

/*console.log(a);*/
//try分頁
//Vue.components('paginated-list', {
//    data() {
//        return {
//            pageNumber: 0
//        }
//    },
//    props: {
//        listData: {
//            type: Array,
//            required: true
//        },
//        size: {
//            type: Number,
//            required: false,
//            default: 2
//        }
//    },
//    methods: {
//        nextPage() {
//            this.pageNumber++;
//        },
//        prevPage() {
//            this.pageNumber--;
//        }
//    },
//    computed: {
//        pageCount() {
//            let l = this.listData.length,
//                s = this.size;
//            return Math.ceil(l / s);
//        },
//        paginatedData() {
//            const start = this.pageNumber * this.size,
//                end = start + this.size;
//            return this.listData
//                .slice(start, end);
//        }
//    },
//    template:
//        `<div class="d-flex">   
//             <button 
//                 :disabled="pageNumber === 0" 
//                 @click="prevPage">
//                 上一頁
//             </button>
//             <button 
//                 :disabled="pageNumber >= pageCount -1" 
//                 @click="nextPage">
//                 下一頁
//             </button>
//        </div>`
//});

//function getCurrentBindData(){
//    return list.$data.hotelList;
//}

//function DataBind(rec) {
//    //把最新的資料更新
//    list.$data.hotelList = rec;
//}

//function DataPrev() {
//    var tmp = getCurrentBindData();
//    var i = allHotel.indexOf(tmp);

//    if (i > 0) {
//        DataBind(allHotel[i - 1]);
//    }
//}

//function NextPage() {
//    var tmp = GetCurrentBindData(); //抓當前的資料出來
//    var i = allHotel.indexOf(tmp); //判斷排在第幾

//    //防呆
//    if (i < allHotel.length - 1) {
//        DataBind(allHotel[i + 1]);
//    }
//}

//function turnback() {
//    alert("turnback");
//}
//$(document).ready(function () {
//    //Binding();
//    //$('#orderPrice').click(OrderPrice);
//    //$('#orderStar').click(OrderStar);
//    //$('#next').click(NextPage);

    
//});

