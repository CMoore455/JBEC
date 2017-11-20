
document.addEventListener("load", DocumentLoaded());
//var fileInput = document.createElement("input");  For later
//fileInput.setAttribute("type", "file"); For Later
var headerId, bodyId, footerId;

function DocumentLoaded() {
    var headerClassLookup = $(".selector-headers");
    var headerSelector = headerClassLookup[0];
    var option = headerSelector.options[headerSelector.selectedIndex];
    if (option) {
        headerId = option.value;
        headerSelector.addEventListener("click", function (event) {
            LayoutSelectorChanged({
                Type: "Header",
                ChangeID: function () {
                    var option = headerSelector.options[headerSelector.selectedIndex];
                    headerId = option.value;
                }
            });
        });
        headerClassLookup.trigger('click');
    }

    var bodyClassLookup = $(".selector-bodies");
    var bodySelector = bodyClassLookup[0];
    var option = bodySelector.options[bodySelector.selectedIndex];
    if (option) {
        bodyId = option.value;
        bodySelector.addEventListener("click", function (event) {
            LayoutSelectorChanged({
                Type: "Body",
                ChangeID: function (id) {

                    bodyId = bodySelector.options[bodySelector.selectedIndex].value;
                }
            });
        });
        bodyClassLookup.trigger('click');
    }

    var footerClassLookup = $(".selector-footers")
    var footerSelector = footerClassLookup[0];
    var option = footerSelector.options[footerSelector.selectedIndex];
    if (option) {
        footerId = option.value;
        footerSelector.addEventListener("change", function (event) {
            LayoutSelectorChanged({
                Type: "Footer",
                ChangeID: function () {
                    footerId = footerSelector.options[footerSelector.selectedIndex].value;
                }
            });
        });
        footerClassLookup.trigger('click');
    }
};

function LayoutSelectorChanged(IdChanger) {
    IdChanger.ChangeID();
    //Perform ajax request for new layout
    switch (IdChanger.Type) {
        case "Header":
            $.getJSON("GetNewLayout/" + headerId).done(ReceiveNewLayout);
            break;
        case "Body":
            $.getJSON("GetNewLayout/" + bodyId).done(ReceiveNewLayout);
            break;
        case "Footer":
            $.getJSON("GetNewLayout/" + footerId).done(ReceiveNewLayout);
            break;
        default:
            return;
    }
    //Apply new layouts to live preview
    //Update the page creation options for a layout.
}

function ReceiveNewLayout(json) {
    switch (json.Type) {
        case "Header":
            ChangeHeader(json.Content, json.CSS);
            break;
        case "Body":
            ChangeBody(json.Content, json.CSS);
            break;
        case "Footer":
            ChangeFooter(json.Content, json.CSS);
            break;
        default:
            break;
    }
}

function ChangeHeader(content, css) {
    var headerPreview = $("#headerPreview")[0];
    if ($('#headerPreviewStyle').length != 0) { $('#headerPreviewStyle').remove(); }
    var cssStyle = document.createElement('style');
    cssStyle.setAttribute('id', 'headerPreviewStyle');
    cssStyle.setAttribute('type', 'text/css');
    for (var i = 0; i < css.length; i++) {
        cssStyle.innerHTML += ' #headerPreview ' + css[i];
    }
    $('head').append(cssStyle);
    var newHeader = document.createElement("div");
    newHeader.innerHTML = content;
    headerPreview.innerHTML = '';
    while (newHeader.hasChildNodes()) {
        headerPreview.appendChild(newHeader.childNodes[0]);
    }
}
function ChangeBody(content, css) {
    var bodyPreview = $("#bodyPreview")[0];
    if ($('#bodyPreviewStyle').length != 0) { $('#bodyPreviewStyle').remove(); }
    var cssStyle = document.createElement('style');
    cssStyle.setAttribute('id', 'bodyPreviewStyle');
    cssStyle.setAttribute('type', 'text/css');
    for (var i = 0; i < css.length; i++) {
        cssStyle.innerHTML += ' #bodyPreview ' + css[i];
    }
    $('head').append(cssStyle);
    var newBody = document.createElement("div");
    newBody.innerHTML = content;
    bodyPreview.innerHTML = '';
    while (newBody.hasChildNodes()) {
        bodyPreview.appendChild(newBody.childNodes[0]);
    }
}
function ChangeFooter(content, css) {
    var footerPreview = $("#footerPreview")[0];
    if ($('#footerPreviewStyle').length != 0) { $('#footerPreviewStyle').remove(); }
    var cssStyle = document.createElement('style');
    cssStyle.setAttribute('id', 'footerPreviewStyle');
    cssStyle.setAttribute('type', 'text/css');
    for (var i = 0; i < css.length; i++) {
        cssStyle.innerHTML += ' #footerPreview ' + css[i];
    }
    $('head').append(cssStyle);
    var newFooter = document.createElement("div");
    newFooter.innerHTML = content;
    footerPreview.innerHTML = '';
    while (newFooter.hasChildNodes()) {
        footerPreview.appendChild(newFooter.childNodes[0]);
    }
}

function SavePage() {
    //Navigate DOM to page contents
    var page = {
        Header: $("#headerPreview")[0].innerHTML,
        Body: $("#bodyPreview")[0].innerHTML,
        Footer: $("#footerPreview")[0].innerHTML,
        //TODO Get CSS for the page
    }

    $.post("SavePage", page);
    //Send page up to server as json
}


//Need function that makes the page creation option for color and sizing etc. appear with appropriate options(TBD)