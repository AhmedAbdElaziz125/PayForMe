// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".MainService").change(function(){
    let MainServiceId = $(".MainService option:selected").val();
    $.ajax({
        type:"GET",
    url: "/Orders/GetMiddleServices",
    data: {MainServiceId: MainServiceId },
    success:function(data){
        let middles ='',
    $.each(data, function(i,middle)){
        middles += '<option value="' + middle.value + '">' + middle.text + '</option>';
                    }
    $(".MiddleService").html(middles);
}