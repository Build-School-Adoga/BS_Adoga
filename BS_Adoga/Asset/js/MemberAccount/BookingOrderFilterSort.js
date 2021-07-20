﻿
import BookingCard from './BookingComponent.js'

//一開始載入頁面時要帶入order的資料，未入住的。
axios.get('https://localhost:44352/Account/GetMemberBookingList', {
        params: {
            filterOption: "ComingSoon",
            sortOption: "CheckInDate"
        }
    }).then(function (response) {
        console.log(response.data)
        console.log('success')
        appendBookingList(response.data);
    }).catch((error) => console.log(error))

var filterBookingOrder = new Vue({
    el: "#filter-sort-wrap",
    data: {
        filterOption: 'ComingSoon',
        sortOption: 'CheckInDate'
    },
    watch: {
        filterOption() {
            console.log(`filter:${this.filterOption}`)
            this.filter_sort();
        },
        sortOption() {
            console.log(`sort:${this.sortOption}`)
            this.filter_sort();
        },
    },
    methods: {
        filter_sort() {
            axios.get('https://localhost:44352/Account/GetMemberBookingList', {
                params: {
                    filterOption: this.filterOption,
                    sortOption: this.sortOption
                }
            }).then((response) => {
                console.log(response.data)
                appendBookingList(response.data)
            }).catch((error) => console.log(error))
        },
    }
})
var BookingList = new Vue({
    el: '#BookingList',
    data: {
        group: []
    },
    components: {
        'booking-card': BookingCard
    }
})

function appendBookingList(response) {

    BookingList.group = []; //先清空資料

    var data = response;     //好像response進來就直接轉成js了，所以不需要用JSON.parse轉格式喔

    (data).forEach((item, index) => {

        //產生床型的字串
        var bedTypeStr = '';
        (item.RoomBed).forEach((bed, index) => {
            if (index != item.RoomBed.length - 1)
                bedTypeStr += bed.Name + "x" + bed.Amount + " , "
            else
                bedTypeStr += bed.Name
        })
        console.log(item.OrderID)

        //開始給BookingList（Vue物件）的group塞資料&設定裡面的屬性
        BookingList.$set(BookingList.group, index,
            {
                CustomerID: item.CustomerID,
                OrderID: item.OrderID,
                HotelID: item.HotelID,
                HotelName: item.HotelName,
                HotelEngName: item.HotelEngName,
                BedStr: bedTypeStr,
                RoomPriceTotal: Math.ceil(item.RoomPriceTotal).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','),

                OrderDate: item.OrderDate,
                CheckInDate: item.CheckInDate,
                CheckOutDate: item.CheckOutDate,

                FewDaysAgo: item.FewDaysAgo,

                OrderDateStr: item.OrderDateStr,

                CheckInDay: item.CheckInDay,
                CheckInWeek: item.CheckInWeek,
                CheckInMonth: item.CheckInMonth,

                CheckOutDay: item.CheckOutDay,
                CheckOutWeek: item.CheckOutWeek,
                CheckOutMonth: item.CheckOutMonth,

                CheckCheckOut: item.CheckCheckOut,
                City: item.City,
                Breakfast: item.Breakfast,
                PayStatus: item.PayStatus,
                In24Hours: item.In24Hours,

                ContinuePay: function () {
                    axios.post('https://localhost:44352/CheckOut/RePayOrder', {
                        params: {
                            orderID: item.OrderID
                        }
                    }).then(response => console.log(response)
                    ).catch(error => console.log(error))
                },
                GoToDetail: function () {
                    window.location.href = 'https://localhost:44352/BookingDetail/' + item.OrderID;
                }
            }
        );
    })
}