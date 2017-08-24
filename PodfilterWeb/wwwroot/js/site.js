// Open/close overlay for new filters.
//
function openNewFilterOverlay() {
    document.getElementById("newFilterOverlay").style.width = "100%";
}
function closeNewFilterOverlay() {
    document.getElementById("newFilterOverlay").style.width = "0%";
}

// Retrieves the url given by the user and encodes it.
//
function encodeInputUrl() {
    var url = document.getElementById("urlInputField").textContent;
    return encodeURI(url);
}

function currentFilters_selectionChanged() {
    console.log("Changing selected filter.");

    $('.list-group .list-group-item').click(function(e) {
        e.preventDefault();
        $that = $(this);

        $that.parent().find('.list-group-item').removeClass('active');
        $that.addClass('active');
    });
}

$(".list-group list-group-item").click(function(e) {
    $(".list-group list-group-item").removeClass("active");
    $(e.target).addClass("active");
 });