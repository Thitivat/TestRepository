function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    url = url.toLowerCase(); // This is just to avoid case sensitiveness  
    name = name.replace(/[\[\]]/g, "\\$&").toLowerCase();// This is just to avoid case sensitiveness for query parameter name
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
function showMessageError(msg,redirect) {
    closeAllModal();
    $('#failModal').modal('show');
    $('#txtError').text(msg);

    if (redirect != "" && redirect != null) {
        $("#btnCloseFailModal").attr("href", redirect);
    } else {
        $("#btnCloseFailModal").attr("href", "#");
        $("#btnCloseFailModal").attr("data-dismiss", "modal");
    }
}
function hideMessageError() {
    $('#failModal').modal('hide');
}
function showLoading(header) {
    debugger;
    if (header != null) {
        $("#titleLoading").text(header);
    } else {
        $("#titleLoading").text('Loading...');
    }
    $('#loadingModal').modal('show');
}
function closeAllModal() {
    $('.modal').modal('hide');
}