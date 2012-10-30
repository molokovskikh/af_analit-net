$(document).ready(function () {
	jQuery("#contactPhones").find("p").remove();
	jQuery.ajax({
		url: "../Content/GetContactPhones",
		cache: false,
		success: function (html) {
			jQuery("#contactPhones").append(html);
		}
	});
});