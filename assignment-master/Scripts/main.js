(() => {

})();

$(function () {
	$('#resolveBtn').click(function () {
		$("#resolveBtn").attr("disabled", "disabled");
		$.ajax({
			type: "POST",
			url: "http://localhost:59241/api/dependencyresolver/resolve",
			data: JSON.stringify({ "Input": $('#dependencyInput').val() }),
			contentType: "application/json; charset=utf-8",
		}).done(function (data) {
			$("#resolveBtn").removeAttr("disabled");
			$("#dependencyOutput").text(data)
		});
	});
});