
document.addEventListener("load", DocumentLoaded());

//The ID's of the currently selected layouts in the selector.
var headerId, bodyId, footerId;
var currentElementSelected;


//Hooks up all the click events for the selectors for a different layout.
function DocumentLoaded() {
    var headerClassLookup = $(".selector-headers");
    var headerSelector = headerClassLookup[0];
    var option = headerSelector.options[headerSelector.selectedIndex];
    if (option) {
        headerId = option.value;
        headerClassLookup.click(function (event) {
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
        bodyClassLookup.click(function (event) {
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
        footerClassLookup.click(function (event) {
            LayoutSelectorChanged({
                Type: "Footer",
                ChangeID: function () {
                    footerId = footerSelector.options[footerSelector.selectedIndex].value;
                }
            });
        });
        footerClassLookup.trigger('click');
    }

    $('#fontColor').on('click', ChangeFontColor);
    $('#backgroundColor').on('click', ChangeBackgroundColor);
    $('#marginLeft').on('click', ChangeLeftMargin);
    $('#marginRight').on('click', ChangeRightMargin);
    $('#marginTop').on('click', ChangeTopMargin);
    $('#marginBottom').on('click', ChangeBottomMargin);
    $('#fontSize').on('click', ChangeFontSize);
};


//Performs ajax request for the new layouts. Updates to the live preview area occur on received response.
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

    //Update the page creation options for a layout.
}

//Responds to json received from server when new layouts are requested.
function ReceiveNewLayout(json) {
    switch (json.Type) {
        case "Header":
            ChangeContent(json.Content, json.CSS, '#headerPreview');
            break;
        case "Body":
            ChangeContent(json.Content, json.CSS, '#bodyPreview');
            break;
        case "Footer":
            ChangeContent(json.Content, json.CSS, '#footerPreview');
            break;
        default:
            break;
    }
}


//These Change/Header|Body|Footer/ methods update the live preview area when a click occurs on the selection control.
function ChangeContent(content, css, idSelectorForContentPlacement) {
    var previewArea = $(idSelectorForContentPlacement)[0];

    if ($(idSelectorForContentPlacement + 'Style').length != 0) { $(idSelectorForContentPlacement + 'Style').remove(); }

    var cssStyle = document.createElement('style');
    cssStyle.setAttribute('id', idSelectorForContentPlacement + 'Style');
    cssStyle.setAttribute('type', 'text/css');

    for (var i = 0; i < css.length; i++) {
        cssStyle.innerText += ' ' + idSelectorForContentPlacement + ' ' + css[i];
    }

    $('head').append(cssStyle);

    var newHeader = document.createElement("div");
    newHeader.innerHTML = content;
    previewArea.innerHTML = '';
    while (newHeader.hasChildNodes()) {
        previewArea.appendChild(newHeader.childNodes[0]);
    }

    WireContentForEditableText(previewArea);
}
//Sends the page to the server to be saved to the database.
function SavePage() {

    //Creates header, body, and footer tags to send to the server.
    var header = document.createElement('header');
    header.innerHTML = $("#headerPreview")[0].innerHTML;
    var body = document.createElement('body');
    body.innerHTML = $("#bodyPreview")[0].innerHTML;
    var footer = document.createElement('footer');
    footer.innerHTML = $("#footerPreview")[0].innerHTML;

    //Finding the style tags of the live preview areas contents.
    var headerCssRules = $("#headerPreviewStyle");
    var bodyCssRules = $("#bodyPreviewStyle");
    var footerCssRules = $("#footerPreviewStyle");

    //An array of the style tags applying to the live preview areas contents
    var cssRulesOfPreviewAreas = [];

    //If these exist on the page they will be added to the array that is used to populate the css field of the page.
    if (headerCssRules.length > 0) { cssRulesOfPreviewAreas.push(headerCssRules[0]); }
    if (bodyCssRules.length > 0) { cssRulesOfPreviewAreas.push(bodyCssRules[0]); }
    if (footerCssRules.length > 0) { cssRulesOfPreviewAreas.push(footerCssRules[0]); }


    var cssRules = ConvertAllRulesToOneString(cssRulesOfPreviewAreas);

    //This objects fields needs to match JSONForSavingWebPage object in the models folder of server.
    var page = {
        Header: header.outerHTML,
        Body: body.outerHTML,
        Footer: footer.outerHTML,
        CSS: cssRules
        //Need ImageURLS
        //Need RouteOfPage
    }

    //Sending page to server as JSON
    $.post("SavePage", page);//Need to react to bad post (i.e. Route is already taken)
}

//Converts all the css in several style tags into one blob of css text.
function ConvertAllRulesToOneString(arrayOfStyles) {
    var cssRulesTogetherAsString = "";
    while (arrayOfStyles.length > 0) {
        var cssRulesAsArray = SeparateCSSRulesIntoArray(arrayOfStyles[0].innerText);
        for (var i = 0; i < cssRulesAsArray.length; i++) {
            cssRulesTogetherAsString += cssRulesAsArray[0].trim() + ' ';
        }
        arrayOfStyles.shift();
    }
    return cssRulesTogetherAsString;
}

//Separates out each css rule of a blob of css into an array.
function SeparateCSSRulesIntoArray(cssText) {
    var matchCssRulesRegex = /[\)\(\]\[:\w," =\-\*^#\.\@>\n\+]+\{\s*[^}{]+\s*\}/;
    return cssText.match(matchCssRulesRegex);
}


//Finds nodes with 0 children and makes their text editable
function WireContentForEditableText(content) {
    if (content.children.length != 0) {
        for (var i = 0; i < content.children.length; i++) {
            WireContentForEditableText(content.children[i]);
        }
    } else {
        $(content).click(function (event) {
            if (content.children.length == 0) {
                var inputTag = document.createElement('input');
                inputTag.type = 'text';
                inputTag.value = content.innerText;
                content.innerText = '';
                $(inputTag).blur(function (event) {
                    var inputTagChild = content.childNodes[0];
                    content.removeChild(inputTagChild);
                    content.innerText = inputTagChild.value;
                    $(content).off();
                });
                content.appendChild(inputTag);
                currentElementSelected = content;
            }
        });
    }
}


//Need function that makes the page creation option for color and sizing etc. appear with appropriate options(TBD)
function UpdateCreatorOptions() {
    document.getElementById('marginLeft').value = currentElementSelected.style.marginLeft != null ? currentElementSelected.style.marginLeft : 0;
    document.getElementById('marginRight').value = currentElementSelected.style.marginRight != null ? currentElementSelected.style.marginRight : 0;
    document.getElementById('marginTop').value = currentElementSelected.style.marginTop != null ? currentElementSelected.style.marginTop : 0;
    document.getElementById('marginBottom').value = currentElementSelected.style.marginBottom != null ? currentElementSelected.style.marginBottom : 0;
    document.getElementById('backgroundColor').value = currentElementSelected.style.backgroundColor != null ? currentElementSelected.style.backgroundColor : 'black';
    document.getElementById('fontColor').value = currentElementSelected.style.color != null ? currentElementSelected.style.color : 'black';
}

function ChangeLeftMargin(margin) {
    currentElementSelected.style.marginLeft = document.getElementById('marginLeft').value;
}

function ChangeRightMargin(margin) {
    currentElementSelected.style.marginRight = document.getElementById('marginRight').value;
}

function ChangeTopMargin(margin) {
    currentElementSelected.style.marginTop = document.getElementById('marginTop').value;
}

function ChangeBottomMargin(margin) {
    currentElementSelected.style.marginBottom = document.getElementById('marginBottom').value;
}

function ChangeFontSize(fontSize) {

}

function ChangeHeight() {
    currentElementSelected.style.height = document.getElementById('height').value;
}

function ChangeWidth() {
    currentElementSelected.style.width = document.getElementById('width').value;
}

function ChangeBackgroundColor() {
    currentElementSelected.style.backgroundColor = document.getElementById('backgroundColor').value;
}

function ChangeFontColor() {
    currentElementSelected.style.color = document.getElementById('fontColor').value;
}
