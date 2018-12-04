(() => {
    window.onload = () => {
        bootstrap();
    };
})();

function bootstrap() {
    var getDependencies = () => {
        var input = $("#node").val();
        $.ajax({
            url: window.location.pathname + "api/Dependency/" + input
        }).then(function (data) {
            data.forEach(function (item) {
                $('#nodeDependencies').append(item);
            });
        });
    };

    $("#getNodeButton").on("click",
        function () {
            $('#nodeDependencies').html('');
            getDependencies();
        });
}