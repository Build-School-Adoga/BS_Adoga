
var hoverEquimServ = new Vue({
    el: '#hover-equim-serv',
    data: {
        hover: false,
    },
})

$().ready(function () {

    //一開始載入頁面時要帶入Room的全部資料
    axios.get('https://localhost:44352/api/HotelDetail/GetAllRoom', {
        params: {
            hotelName: filternav.Value,
            startDate: filternav.startDate,
            endDate: filternav.endDate,
            orderRoom: filternav.room,
            adult: filternav.adult,
            child: filternav.kids,
        }
    }).then(function (response) {
            appendRoomType(response.data);
        }).catch((error) => console.log(error))


    Vue.component('room-type', {
        props: { room: ['room'] },
        template: '#roomTypeTemplate',
        data: function () {
            return {
                bookingRoom: this.room.RoomID
            }
        }
    })

    var filterRoom = new Vue({
        el: '#filter-room',
        data: {
            FreeBreakfast: false,
            NoSmoking: false,
            FamilyRoom: false
        },
        watch: {
            FreeBreakfast() {
                console.log(`brkf:${this.FreeBreakfast}`)
                this.filter();
            },
            NoSmoking() {
                console.log(`nosmoking:${this.NoSmoking}`)
                this.filter();
            },
            FamilyRoom() {
                console.log(`family:${this.FamilyRoom}`)
                this.filter();
            }
        },
        methods: {
            filter() {
                axios.get('https://localhost:44352/api/HotelDetail/GetSpecificRoom', {
                    params: {
                        hotelName: filternav.Value,
                        startDate: filternav.startDate,
                        endDate: filternav.endDate,
                        orderRoom: filternav.room,
                        adult: filternav.adult,
                        child: filternav.kids,
                        freeBreakfast: this.FreeBreakfast,
                        noSmoking: this.NoSmoking,
                        family: this.FamilyRoom,
                    }
                }).then(function (response) {
                    appendRoomType(response.data)
                }).catch((error) => console.log(error))
            },
            clearFilter() {
                this.FreeBreakfast = false;
                this.NoSmoking = false;
                this.FamilyRoom = false;
                $.post('https://localhost:44352/api/HotelDetail/GetAllRoom', appendRoomType)
            }
        }
    })

    var roomGroup = new Vue({
        el: '#room-group',
        data: {
            group: []
        }
    })

    function appendRoomType(response) {
        //這邊response進來好像就轉成js了，可以直接用，所以不需要用JSON.parse轉格式喔
        roomGroup.group = [];
        var data = response;
        (data).forEach((item, index) => {
            // a = item.HotelID
            // roomGroup.group = Object.assign({ }, roomGroup.group,)
            // roomGroup.group.push(

            //給RoomBed[]塞物件 方法1
            var roomBed = [];
            var bedTypeStr = '';
            (item.RoomBed).forEach((bed, index) => {
                roomBed.push({
                    Name: bed.Name,
                    Amount: bed.Amount
                })
                if (index != item.RoomBed.length - 1)
                    bedTypeStr += bed.Name + ","
                else
                    bedTypeStr += bed.Name
            })
            roomGroup.$set(roomGroup.group, index,
                {
                    HotelID: item.HotelID,
                    RoomID: item.RoomID,
                    RoomName: item.RoomName,
                    WiFi: item.WiFi,
                    Breakfast: item.Breakfast,
                    NoSmoking: item.NoSmoking,
                    BathroomName: item.BathroomName,
                    RoomBed: roomBed,
                    Adult: item.Adult,
                    Child: item.Child,
                    RoomOrder: item.RoomOrder,
                    RoomPrice: Math.ceil(item.RoomPrice),
                    RoomDiscount: Math.round((1 - item.RoomDiscount) * 10),
                    RoomNowPrice: Math.ceil(item.RoomNowPrice),
                    RoomLeft: item.RoomLeft,
                    Booking: function () {
                        axios.get('https://localhost:44352/HotelDetail/SetCheckOutData', {
                            params: {
                                hotelId: item.HotelID,
                                roomId: item.RoomID,
                                roomName: item.RoomName,
                                noSmoking: item.NoSmoking,
                                breakfast: item.Breakfast,
                                bedType: bedTypeStr,
                                roomOrder: item.RoomOrder,
                                roomPrice: item.RoomPrice,
                                roomDiscount: item.RoomDiscount,
                                roomNowPrice: item.RoomNowPrice
                            }
                        }).then(function (response) {
                            console.log(response);
                        }).catch((error) => console.log(error));
                    }
                }
            );

            //給RoomBed[]塞物件 方法2
            // for (var i = 0; i < item.RoomBed.length; {
            //     roomGroup.group[index].RoomBed.push({
            //         Name: item.RoomBed[i].Name,
            //         Amount: item.RoomBed[i].Amount
            //     })
            // }
        })
    }
})