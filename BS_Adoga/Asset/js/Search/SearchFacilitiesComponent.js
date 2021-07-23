export default {
    props: {
        card: ['card'],
    },
    template:
    //    `<div class="serv-group d-flex my-4">
    //    <i :class="cat.categoryIcon" class=" fz-big-icon color-dark-gray border-e-light-gray me-4"></i>
    //    <div class="serv-row">
    //        <h4 class="fz-16 fw-bold color-dark-gray">{{cat.categoryName}}</h4>
    //        <div class="row color-dark-gray">
    //            <p v-for="f in cat.facilities" v-if="f.haveFacility" class="col-4"><i :class="f.icon" class=" fz-14 me-2"></i>{{f.facility_Ch}}</p>
    //        </div>
    //    </div>
    //</div>`
//<div class="form-list col-4" v-for="(value, name) in HotelEquipName">
//                <input class="form-check-input" type="checkbox"  :id={{name}}>
//                <label class="form-check-label" :for={{name}}>
//                    {{value}}
//                </label>
//            </div>
        `
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
                <a href="#" v-model="card.HotelName"></a>
                <div class="small-rank">
                    <div class="star">
                        @for (int i = 1; i <= card.Star; i++)
                        {
                            <i class="fas fa-star"></i>
                        }
                    </div>
                    <div class="address">
                        <i class="fas fa-map-marker-alt"></i>
                        <p>{{card.HotelAddress}}</p>
                    </div>
                </div>
                <div class="include-list">
                    @if (card.I_RoomVM.Breakfast)
                    {
                        <p class="Breakfast">早餐</p>
                    }
                    @if (card.I_RoomVM.NoSmoking)
                    {
                        <p class="NoSomking">禁煙</p>
                    }
                    @if (card.I_RoomVM.WiFi)
                    {
                        <p class="wifi">WiFi</p>
                    }
                </div>
                <div class="tag-list">
                    <i class="fas fa-money-bill-alt"></i>
                    <p>價格比同區域其他住宿低</p>
                </div>
                <div class="advantage">
                    <i class="far fa-credit-card"></i>
                    <p>免信用卡訂房</p>
                </div>
            </div>
                @if (card.I_RoomDetailVM.RoomDiscount == 0)
                {
                    <div class="price">
                        <p>每晚含稅價</p>
            ****            <ins>NT$<span>@newPrice.ToString("N0")</span></ins>
                    </div>
                }
                else
                {
                    <div class="offer">
            ***            <p>只剩<span>@leftroom</span>間房可享<span>@roomdiscount</span>％優惠</p>
                    </div>
                    <div class="price">
                        <p>每晚含稅價</p>
                        <del>@roomprice.ToString("N0")</del>
                        <ins>NT$<span>@newPrice.ToString("N0")</span></ins>
                    </div>
                }

                @* ActionLink沒辦法進去HotelDetail的Action *@
                @Html.ActionLink("查看空房情況", "Detail", "HotelDetail", new { hotelId = Model.HotelID, startDate = "2021/06/20", endDate = "2021/06/22", orderRoom = 2, adult = 6 }, new { @class = "btn btn-primary", })
            </div>
        </div>

        `
}