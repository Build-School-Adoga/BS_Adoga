
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