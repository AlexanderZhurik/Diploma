var script = document.createElement('script');
script.src = 'https://code.jquery.com/jquery-3.6.0.min.js';
document.getElementsByTagName('head')[0].appendChild(script);
// подгрузка категорий для формы добавления
$(document).ready(function () {
    $.getJSON("/Categories")
        .done(function (response) {
            $.each(response, function (key, item) {

                $('<input>', { type: "radio", name: "categoryCheck", id: item.name, class: "form-radio" }).appendTo($('#radio-container'));
                $('<label>', { for: item.name, text: item.name }).appendTo($('#radio-container'));
                $('<br />').appendTo($('#radio-container'));
            });
        })
})
// получение списка всех товаров
C
// отправка формы добавления
$("#form-submit").click(function () {
    var outName = $("#form-name").val();
    var outPrice = $("#form-price").val();
    var outDescription = $("#form-description").val();
    var outCategory = $('input[name=categoryCheck]:checked').attr('id');
    $.ajax
        ({
            type: "POST",
            url: "/AddItem?name=" + outName + "&price=" + outPrice + "&description=" + outDescription + "&category=" + outCategory,
            dataType: 'text'
        })
    $("form")[0].reset();
})
//удаление товара
$("#delete-submit").click(function () {
    $(".delete-result").remove();
    var deleteVal = $("#delete-search").val();
    $.getJSON("/Search?name=" + deleteVal)
        .done(function (response) {
            if (response != undefined) {
                $.ajax({
                    type: "DELETE",
                    url: "/DeleteItem?name=" + deleteVal,
                    dataType: 'text'
                })
                $("<p>", { text: deleteVal + " успешно удалён.", class: "delete-result" }).appendTo($("#delete-container"));
            }
            else $("<p>", { text: "Не удалось найти товар с именем " + deleteVal + ".", class: "delete-result" }).appendTo($("#delete-container"));
        })
})