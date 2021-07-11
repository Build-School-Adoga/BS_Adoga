
var hoverEquimServ = new Vue({
    el: '#hover-equim-serv',
    data: {
        hover: false,
    },
})

$().ready(function () {
    Vue.component('room-type', {
        props: ['room'],
        template: '#roomTypeTemplate'
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
                        freeBreakfast: this.FreeBreakfast,
                        noSmoking: this.NoSmoking,
                        family: this.FamilyRoom,
                    }
                }).then(function (response) {
                    console.log(response.data);
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
        },
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
            (item.RoomBed).forEach((bed) => {
                roomBed.push({
                    Name: bed.Name,
                    Amount: bed.Amount
                })
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
                    RoomLeft: item.RoomLeft
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