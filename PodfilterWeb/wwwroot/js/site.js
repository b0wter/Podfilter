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

 function newFilterSelectionChanged(){
    console.log("selection changed");
    var selectBox = document.getElementById("newFilterSelector");
    var filterType = selectBox.value;

    hideFilterParameters();

    if(filterType != ""){
        var parameterSetName = filterType + "FilterParameters";
        console.log("hiding all elements except " + parameterSetName);
        document.getElementById(parameterSetName).style.display = 'block';
    }
 }

 function hideFilterParameters(){
     document.getElementById("titleFilterParameters").style.display = 'none';
     document.getElementById("descriptionFilterParameters").style.display = 'none';
     document.getElementById("durationFilterParameters").style.display = 'none';
     document.getElementById("pubDateFilterParameters").style.display = 'none';
 }