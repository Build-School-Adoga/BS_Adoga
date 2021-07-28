////import cardComponent from './SearchFacilitiesComponent.js'
////import dataComponent from './SearchDataComponent.js'


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
    HotelList(response.data);
}).catch((error) => console.log(error))

function HotelList(response) {
    //var DataList = [];
    var DataList = response;
    console.log(DataList);
    debugger;
    Vue.component('hotel-card', {
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
                default: 2
            }
        },
        filters: {
            PriceFormat: function (price) {
                if (!price) return '0'
                // 获取整数部分
                const intPart = Math.trunc(price)
                // 整数部分处理，增加,
                const intPartFormat = intPart.toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,')

                return intPartFormat
            }
        },
        methods: {
            nextPage() {
                this.pageNumber++;
            },
            prevPage() {
                this.pageNumber--;
            },
            emitEvent: function (name) {
                window.location.href = '/HotelDetail/' + name + '-(' + this.fnav.start + ')-(' + this.fnav.end + ')-' + this.fnav.room + '-' + this.fnav.adult + '-' + this.fnav.kid;
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
                return this.listData
                    .slice(start, end);
            }
        },
        template: `<div>
                <div v-for="hotel in paginatedData">
                    <div class="card">
                        <div class="img">
                            <img src="//pix6.agoda.net/hotelImages/1158270/-1/e11b2751b3be0ee28417b1b61904cf22.jpg?s=450x450" alt="">
                            <div class="small-img">
                                <img src="https://picsum.photos/id/685/50/33" alt="">
                                <img src="https://picsum.photos/id/685/50/33" alt="">
                                <img src="https://picsum.photos/id/685/50/33" alt="">
                                <img src="https://picsum.photos/id/685/50/33" alt="">
                                <div class="see-more">
                                    <p>查看更多</p>
                                </div>
                            </div>
                        </div>
                        <div class="detail">
                            <a href="#">{{ hotel.HotelName }}</a>
                            <div class="small-rank">
                                <div class="star">
                                    <i class="fas fa-star" v-for="item in hotel.Star"></i>
                                </div>
                                <div class="address">
                                    <i class="fas fa-map-marker-alt"></i>
                                    <p>{{hotel.HotelAddress}}</p>
                                </div>
                            </div>
                            <div class="include-list">
                                <p class="Breakfast" v-if="hotel.I_RoomVM.Breakfast">早餐</p>
                                <p class="NoSomking" v-if="hotel.I_RoomVM.NoSmoking">禁煙</p>
                                <p class="wifi" v-if="hotel.I_RoomVM.WiFi">WiFi</p>
                            </div>
                        </div>
                        <div class="rank">
                            <div class="offer" v-if="hotel.I_RoomDetailVM.RoomDiscount!=0">
                                <p>只剩<span>{{(hotel.I_RoomDetailVM.RoomCount - hotel.I_RoomDetailVM.RoomOrder)}}</span>間房可享<span>{{hotel.I_RoomDetailVM.RoomDiscount * 100}}</span>％優惠</p>
                            </div>
                            <div class="price">
                                <p>每晚含稅價</p>
                                <del v-if="hotel.I_RoomDetailVM.RoomCount!=0">{{hotel.I_RoomVM.RoomPrice | PriceFormat}}</del>
                                <ins>NT$<span>{{parseInt(hotel.I_RoomVM.RoomPrice*(1-hotel.I_RoomDetailVM.RoomDiscount)) | PriceFormat}}</span></ins>
                            </div>
                            <a @click="emitEvent(hotel.HotelName)" class="moreInfo btn">查看空房情況</a>
                        </div>
                    </div>
                </div>
                <div class="d-flex">
                    <button class="btn"
                        :disabled="pageNumber === 0" 
                        @click="prevPage">
                        上一頁
                    </button>
                    <button class="btn"
                        :disabled="pageNumber >= pageCount -1" 
                        @click="nextPage">
                        下一頁
                    </button>
                </div>
            </div>`
    })

    new Vue({
        el: "#new_card",
        data: {
            card: DataList
        }
    })
    debugger;
}

function DateFormat(date) {
    var date = new Date(date);
    return date.getFullYear() + '-' + (date.getMonth() + 1).toString().padStart(2, '0') + '-' + date.getDate().toString().padStart(2, '0');
}