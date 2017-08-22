// Open/close overlay for new filters.
//
function openNewFilterOverlay() {
    document.getElementById("newFilterOverlay").style.width = "100%";
}
function openNewFilterOverlay() {
    document.getElementById("newFilterOverlay").style.width = "0%";
}

// Retrieves the url given by the user and encodes it.
//
function encodeInputUrl() {
    var url = document.getElementById("urlInputField").textContent;
    return encodeURI(url);
}